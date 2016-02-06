using Glovebox.Graphics.Components;
using Glovebox.Graphics.Drivers;
using System;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Controls;

namespace ScrollingText
{
    /// <summary>
    /// Initial page
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private AppServiceConnection appServiceConnection;

        public MainPage()
        {
            InitializeComponent();
            InitAppSvc();
        }

        private void ScrollText(string message, int repeatCount)
        {
            // remove + from message (spaces)
            message = message.Replace("+", " ");

            //var driver = new Ht16K33(new byte[] { 0x70 }, Ht16K33.Rotate.None, LedDriver.Display.On, 2, LedDriver.BlinkRate.Off, "I2C1");
            var driver = new Ht16K33();

            LED8x8Matrix matrix = new LED8x8Matrix(driver);
            matrix.SetBrightness(1); // override default brightness

            // We'll repeat the message here
            for (int i = 0; i < repeatCount; i++)
            {
                matrix.ScrollStringInFromRight(message + "        ", 100);
                matrix.FrameClear();
            }
        }

        private async void InitAppSvc()
        {
            // Initialize the AppServiceConnection
            appServiceConnection = new AppServiceConnection();
            appServiceConnection.PackageFamilyName = "WebServer_jdtgg1kw6k9gj";
            appServiceConnection.AppServiceName = "App2AppComService";

            // Send a initialize request
            var res = await appServiceConnection.OpenAsync();
            if (res == AppServiceConnectionStatus.Success)
            {
                var message = new ValueSet();
                message.Add("Command", "Initialize");
                var response = await appServiceConnection.SendMessageAsync(message);
                if (response.Status != AppServiceResponseStatus.Success)
                {
                    throw new Exception("Failed to send message");
                }
                appServiceConnection.RequestReceived += OnMessageReceived;
            }
        }

        private void OnMessageReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            var message = args.Request.Message;

            string messageText = message["message"] as string;
            string messageRepeat = message["repeat"] as string;
            int repeat = 0;
            if (messageRepeat != null)
            {
                repeat = Int32.Parse(messageRepeat);
            }

            ScrollText(messageText, repeat);
        }
    }
}