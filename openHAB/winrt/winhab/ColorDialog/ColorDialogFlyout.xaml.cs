using JSON_Parser.Parser;
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
using openhab.winrt.winhab.communication.httpclient.post;

// Die Elementvorlage "Einstellungs-Flyout" ist unter http://go.microsoft.com/fwlink/?LinkId=273769 dokumentiert.

namespace openhab.winrt.winhab.ColorDialog
{
    public sealed partial class ColorDialogFlyout : SettingsFlyout
    {
        public double erg2, ergGes;
        public double factor;
        private bool check = false;
        private HsbConverter hsbConverter = new HsbConverter();
        public Item item { get; set; }
        public ColorDialogFlyout(Item item)
        {
            this.InitializeComponent();
            slider1.Value = slider1.Maximum; //S
            slider2.Value = slider2.Maximum; //V
            ergGes = 180;                    //H
            calculate();
            this.item = item;


            //item
            //slider1.Value = item;
            //slider2.Value = item;
            //ergGes = item;
        }


        private void slider1_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            calculate();
        }

        private void slider2_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            calculate();
        }

        private void path_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (!check)
            {
                double i = pathEllipse.ActualWidth * Math.PI;

                Windows.UI.Input.PointerPoint position = e.GetCurrentPoint(this);

                double px = position.Position.X - (grid.ActualWidth / 2) - (ellipse.ActualWidth / 2);
                double py = position.Position.Y - (grid.ActualHeight / 2) - (ellipse.ActualHeight / 2) - 112;




                TranslateTransform translateTransform = new TranslateTransform();
                translateTransform.X = px;
                translateTransform.Y = py;
                ellipse.RenderTransform = translateTransform;

                double m = Math.Abs(py) / Math.Abs(px);

                line1.X2 = Math.Abs(px);
                line1.Y2 = Math.Abs(py);
                factor = 0;

                if (px >= 0 && py >= 0)
                {
                    myScaleTransform.ScaleX = 1;
                    myScaleTransform.ScaleY = 1;
                    factor = 90;
                    m = Math.Abs(py) / Math.Abs(px);

                }
                else if (px < 0 && py >= 0)
                {
                    myScaleTransform.ScaleX = -1;
                    myScaleTransform.ScaleY = 1;
                    factor = 180;
                    m = Math.Abs(px) / Math.Abs(py);
                }
                else if (px >= 0 && py < 0)
                {
                    myScaleTransform.ScaleX = 1;
                    myScaleTransform.ScaleY = -1;
                    factor = 0;
                    m = Math.Abs(px) / Math.Abs(py);
                }
                else if (px < 0 && py < 0)
                {
                    myScaleTransform.ScaleX = -1;
                    myScaleTransform.ScaleY = -1;
                    factor = 270;
                    m = Math.Abs(py) / Math.Abs(px);
                }

                erg2 = (360 / (2 * Math.PI)) * Math.Atan(Math.Abs(0 - m / (1 + 0 * m)));

                // txt.Text = (erg2+factor).ToString();

                ergGes = erg2 + factor;

                calculate();
            }
        }

        public void calculate()
        {
            int r1, g1, b1;
            HSVConv hsv = new HSVConv();
            hsv.HsvToRgb(Math.Round((ergGes)), slider1.Value / 100, slider2.Value / 100, out r1, out g1, out b1);

            colorGrid.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, Convert.ToByte(r1), Convert.ToByte(g1), Convert.ToByte(b1)));
            // ellipse2.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, Convert.ToByte(r1), Convert.ToByte(g1), Convert.ToByte(b1)));

            //Label8.Content = Math.Round(slider1.Value * 100) + "%";
            //Label9.Content = Math.Round(slider2.Value * 100) + "%";

            //Farbenanpassen

            //value
            hsv.HsvToRgb(Math.Round((ergGes)), slider1.Value / 100, 0.5, out r1, out g1, out b1);
            valueColor.Color = Windows.UI.Color.FromArgb(255, Convert.ToByte(r1), Convert.ToByte(g1), Convert.ToByte(b1));
            valueColor2.Color = Windows.UI.Color.FromArgb(255, Convert.ToByte(r1), Convert.ToByte(g1), Convert.ToByte(b1));

            //saturation
            hsv.HsvToRgb(Math.Round((ergGes)), 0, slider2.Value / 100, out r1, out g1, out b1);
            valueSaturationLeft.Color = Windows.UI.Color.FromArgb(255, Convert.ToByte(r1), Convert.ToByte(g1), Convert.ToByte(b1));
            valueSaturationLeft2.Color = Windows.UI.Color.FromArgb(255, Convert.ToByte(r1), Convert.ToByte(g1), Convert.ToByte(b1));

            hsv.HsvToRgb(Math.Round((ergGes)), 1, slider2.Value / 100, out r1, out g1, out b1);
            valueSaturationRight.Color = Windows.UI.Color.FromArgb(255, Convert.ToByte(r1), Convert.ToByte(g1), Convert.ToByte(b1));
            valueSaturationRight2.Color = Windows.UI.Color.FromArgb(255, Convert.ToByte(r1), Convert.ToByte(g1), Convert.ToByte(b1));

            txt1.Text = slider1.Value.ToString() + "%";
            txt2.Text = slider2.Value.ToString() + "%";
            txtHueValue.Text = Math.Round((ergGes)).ToString() + "°";




        }

        private void ellipse2_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            check = false;
        }

        private void ellipse2_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            check = true;
        }
        PostMessage sendMessage = new PostMessage();
        private void saveColorBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int r1, g1, b1;
            HSVConv hsv = new HSVConv();
            hsv.HsvToRgb(Math.Round((ergGes)), slider1.Value / 100, slider2.Value / 100, out r1, out g1, out b1);
            //String value = Convert.ToString(r1) + Convert.ToString(g1) + Convert.ToString(b1);
            //Int32[] hsb = hsbConverter.convertToHsb(r1, g1, b1);
            //String hex = String.Format("{0:X}", hsb);
            String value = (Math.Round(ergGes)).ToString() + "," + Math.Round(slider1.Value).ToString() + "," + Math.Round(slider2.Value).ToString();
            sendMessage.sendInstruction(item, value);
        }
    }
}
