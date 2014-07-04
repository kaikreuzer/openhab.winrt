using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace openhab.winrt.winhab.ColorDialog
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ColorDialog : Page
    {
        public ColorDialog()
        {
            this.InitializeComponent();
            ImageBrush imageBrush = new ImageBrush();
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            Rectangle rectangle = new Rectangle();
            rectangle.Width = 100;
            rectangle.Height = 100;
            //colorDialogCanvas.Children.Add(rectangle);
            Image img = new Image();
            fileOpenPicker.FileTypeFilter.Add(".png");
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            open();


        }
        private async void open()
        {
            storageFile = await fileOpenPicker.PickSingleFileAsync();
        }
        private void colorDialogCanvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Pointer i = e.Pointer;
        }

        private void backButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private async void Image_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            //PointerPoint pointerPoint = e.GetCurrentPoint(null);
            //ImageSource image = colorWheel.Source;
            //BitmapSource bmpSource = null;
            //bmpSource.
            //BitmapImage bmpImage = (BitmapImage)image;// new BitmapImage(new Uri("../Assets/images/colorwheel.png"));
            //bmpImage.DecodePixelType = DecodePixelType.Logical;
            //WriteableBitmap x = new WriteableBitmap(1000, 1000);
        }

        private async void colorEllipse_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            ellipse.Visibility = Windows.UI.Xaml.Visibility.Visible;
            PointerPoint position = e.GetCurrentPoint(colorEllipse);
            double px = position.Position.X - (ellipse.Width / 2);
            double py = position.Position.Y - (ellipse.Width / 2);
            TranslateTransform translateTransform = new TranslateTransform();
            translateTransform.X = px;
            translateTransform.Y = py;
            ellipse.RenderTransform = translateTransform;
            //Point pointer = e.GetPosition(colorWheelEllipse);

            //WriteableBitmap writeableBitmap = new WriteableBitmap(400, 400);
            //writeableBitmap.SetSourceAsync(await storageFile.OpenAsync(FileAccessMode.Read));

            BitmapDecoder bitmapDecoder = await BitmapDecoder.CreateAsync(await storageFile.OpenAsync(FileAccessMode.Read));

            PixelDataProvider pp = await bitmapDecoder.GetPixelDataAsync();
            try
            {
                byte[] pixels = pp.DetachPixelData();
                double index = position.Position.Y * bitmapDecoder.OrientedPixelHeight + 4 * position.Position.X;
                byte r = pixels[(int)index];//;Convert.ToInt32(((position.Position.Y * bitmapDecoder.OrientedPixelHeight +4*position.Position.X)))];
                byte g = pixels[(int)index+1];//Convert.ToInt32(((position.Position.Y * bitmapDecoder.OrientedPixelHeight) + position.Position.X) * 4 + 1)];
                byte b = pixels[(int)index+2];//Convert.ToInt32(((position.Position.Y * bitmapDecoder.OrientedPixelHeight) + position.Position.X) * 4 + 2)];
                byte a = pixels[(int)index+3];//Convert.ToInt32(((position.Position.Y * bitmapDecoder.OrientedPixelHeight) + position.Position.X) * 4 + 3)];
                //byte a = pixels[Convert.ToInt32(((position.Position.Y * bitmapDecoder.OrientedPixelHeight) + position.Position.X) * 4)];
                //byte r = pixels[Convert.ToInt32(((position.Position.Y * bitmapDecoder.OrientedPixelHeight) + position.Position.X) * 4 + 1)];
                //byte g = pixels[Convert.ToInt32(((position.Position.Y * bitmapDecoder.OrientedPixelHeight) + position.Position.X) * 4 + 2)];
                //byte b = pixels[Convert.ToInt32(((position.Position.Y * bitmapDecoder.OrientedPixelHeight) + position.Position.X) * 4 + 3)];
                //Color color = Color.FromArgb(100, pixels[Convert.ToInt32((pointer.Y*32 + pointer.X)*4)], pixels[Convert.ToInt32((pointer.Y*32 + pointer.X)*4+1)], pixels[Convert.ToInt32((pointer.Y*32 + pointer.X)*4+2)]);

                Color color = Color.FromArgb(255, r, g, b);

                colorRectangle.Fill = new SolidColorBrush(color);
                //int k = 0;
            }
            catch (Exception ex)
            {
                //int k = 0;
            }
        }
        FileOpenPicker fileOpenPicker = new FileOpenPicker();
        StorageFile storageFile;// = await fileOpenPicker.PickSingleFileAsync();
        private async void colorWheelEllipse_Tapped(object sender, TappedRoutedEventArgs e)
        {

            Point pointer = e.GetPosition(colorWheelEllipse);

            //WriteableBitmap writeableBitmap = new WriteableBitmap(400, 400);
            //writeableBitmap.SetSourceAsync(await storageFile.OpenAsync(FileAccessMode.Read));

            BitmapDecoder bitmapDecoder = await BitmapDecoder.CreateAsync(await storageFile.OpenAsync(FileAccessMode.Read));

            PixelDataProvider pp = await bitmapDecoder.GetPixelDataAsync();
            try
            {
                byte[] pixels = pp.DetachPixelData();
                byte r = pixels[Convert.ToInt32(((pointer.Y * bitmapDecoder.OrientedPixelHeight) + pointer.X) * 4)];
                byte g = pixels[Convert.ToInt32(((pointer.Y * bitmapDecoder.OrientedPixelHeight) + pointer.X) * 4 + 1)];
                byte b = pixels[Convert.ToInt32(((pointer.Y * bitmapDecoder.OrientedPixelHeight) + pointer.X) * 4 + 2)];
                byte a = pixels[Convert.ToInt32(((pointer.Y * bitmapDecoder.OrientedPixelHeight) + pointer.X) * 4 + 3)];
                //Color color = Color.FromArgb(100, pixels[Convert.ToInt32((pointer.Y*32 + pointer.X)*4)], pixels[Convert.ToInt32((pointer.Y*32 + pointer.X)*4+1)], pixels[Convert.ToInt32((pointer.Y*32 + pointer.X)*4+2)]);

                Color color = Color.FromArgb(255, r, g, b);
                
                colorRectangle.Fill = new SolidColorBrush(color);
                //int k = 0;
            }
            catch (Exception ex)
            {
                //int k = 0;
            }


            //image1[(y * height + x) * 3 + 0] = image[(y * height - 1 - x) * 3 + 0];
            //image1[(y * height + x) * 3 + 1] = image[(y * height - 1 - x) * 3 + 1];
            //image1[(y * height + x) * 3 + 2] = image[(y * height - 1 - x) * 3 + 2]; 
            //Color color = Color.FromArgb(100, pixels[Convert.ToInt32((pointer.Y*32 + pointer.X)*4)], pixels[Convert.ToInt32((pointer.Y*32 + pointer.X)*4+1)], pixels[Convert.ToInt32((pointer.Y*32 + pointer.X)*4+2)]);
            //colorRectangle.Fill = new SolidColorBrush(color);
            //pp.DetachPixelData();

            //var width = bitmapDecoder.OrientedPixelWidth;
            //var height = bitmapDecoder.OrientedPixelHeight;
            //for (var i = 0; i < height; i++)
            //{
            //    for (var j = 0; j < width; j++)
            //    {
            //        pixels[(i * height + j) * 4 + 1] = 0; // Green channel (RGBA)
            //        pixels[(i * height + j) * 4 + 2] = 0; // Blue channel (RGBA)
            //        if(pointer.X.Equals(width) && pointer.Y == height)
            //        {
            //            ;
            //        }
            //    }
            //}

            ;
        }
    }
}
