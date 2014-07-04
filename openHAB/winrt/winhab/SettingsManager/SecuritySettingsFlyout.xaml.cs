using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Einstellungs-Flyout" ist unter http://go.microsoft.com/fwlink/?LinkId=273769 dokumentiert.

namespace openhab.winrt.winhab.SettingsManager
{
    public sealed partial class SecuritySettingsFlyout : SettingsFlyout
    {
        public SecuritySettingsFlyout()
        {
            this.InitializeComponent();
            appPathTextView.TextWrapping = TextWrapping.Wrap;
            //appPathTextView.Blocks.Add("fhidsa");
            _puText();
        }
        private async void _puText()
        {
            StorageFolder storageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("ErrorLogs", CreationCollisionOption.OpenIfExists);
            txt1.Text = storageFolder.Path;
        }
    }
}
