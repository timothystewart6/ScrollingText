using Glovebox.Graphics.Components;
using Glovebox.Graphics.Drivers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace ScrollingText
{
    /// <summary>
    /// Initial page
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var driver = new Ht16K33(new byte[] { 0x70 }, Ht16K33.Rotate.None, LedDriver.Display.On, 2, LedDriver.BlinkRate.Off, "I2C1");

            LED8x8Matrix matrix = new LED8x8Matrix(driver);

            matrix.SetBrightness(1);

            while (true)
            {
                matrix.FrameClear();
                matrix.ScrollStringInFromRight("Hello World 2016", 100);
            }
        }
    }
}
