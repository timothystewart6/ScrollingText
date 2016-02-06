using Glovebox.Graphics.Components;
using Glovebox.Graphics.Drivers;
using Windows.UI.Xaml.Controls;

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