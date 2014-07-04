using JSON_Parser.SettingsManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using openhab.winrt.winhab.communication;
using openhab.winrt.winhab.communication.connectiontypes;
using openhab.winrt.winhab.communication.imageCacheControlManager;
using JSON_Parser.Parser;
using Windows.UI.Popups;

// Die Elementvorlage "Einstellungs-Flyout" ist unter http://go.microsoft.com/fwlink/?LinkId=273769 dokumentiert.

namespace openhab.winrt.winhab.SettingsManager
{
    public sealed partial class WinHABSettingsFlyout
    {
        private Settings settings = new Settings();
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        PasswordVault passwordVault = new PasswordVault();
        public WinHABSettingsFlyout()
        {
            this.InitializeComponent();
            urlTextBox.IsEnabled = false;
            expertGrid.Visibility = Visibility.Collapsed;
            if (localSettings.Values["username"] != null)
            {
                userNameTextBox.Text = localSettings.Values["username"].ToString();
                //Do this only when a Password is needed
                if (Convert.ToInt32(localSettings.Values["connectionTypeChooser"].ToString()).Equals(0) && Convert.ToInt32(localSettings.Values["connectionTypeChooser"].ToString()).Equals(2))
                    try
                    {
                        //only on the first startup, if the Pasword isnt stored 
                        passwordTextBox.Password = passwordVault.Retrieve("winHAB", localSettings.Values["username"].ToString()).Password;
                    }
                    catch
                    {

                    }
            }
            if (localSettings.Values["password"] != null)
            {

            }

            if (localSettings.Values["url"] != null)
            {
                urlTextBox.Text = localSettings.Values["url"].ToString();
            }
            if (localSettings.Values["shortUrl"] != null)
            {
                urlTextBox1.Text = localSettings.Values["shortUrl"].ToString();
            }
            if(localSettings.Values["protocolChooser"] != null)
            {
                protocollChooser.SelectedIndex = Convert.ToInt32(localSettings.Values["protocolChooser"].ToString());
            }
            if (localSettings.Values["sitemap"] != null)
            {
                sitemapTextBox.Text = localSettings.Values["sitemap"].ToString();
            }
            if (localSettings.Values["port"] != null)
            {
                portTextBox.Text = localSettings.Values["port"].ToString();
            }
            if (localSettings.Values["refreshTime"] != null)
            {
                refreshTrackBar.Value = Convert.ToInt32(localSettings.Values["refreshTime"].ToString());
            }
            if (localSettings.Values["connectionTypeChooser"] != null)
            {
                connectionTypeChooser.SelectedIndex = Convert.ToInt32(localSettings.Values["connectionTypeChooser"].ToString());
            }


        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if (expertViewSwitch.IsOn)
            {
                expertGrid.Visibility = Visibility.Visible;
            }
            else if (!expertViewSwitch.IsOn)
            {
                expertGrid.Visibility = Visibility.Collapsed;
            }
        }


        private void RichTextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        private async void realySave_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (connectionTypeChooser.SelectedIndex.Equals(-1))
            {
                MessageDialog dialog = new MessageDialog("Please choose an Connectiontype", "Connection Type is missing");
                dialog.ShowAsync();
            }
            //HTTPSwithUsernameAndPassword
            if (connectionTypeChooser.SelectedIndex.Equals(0) && !urlTextBox.Text.Equals(""))
            {
                settings.username = userNameTextBox.Text;
                settings.password = passwordTextBox.Password;
                settings.sh_url = urlTextBox.Text;
                settings.short_url = urlTextBox1.Text;
                settings.protocolChooser = protocollChooser.SelectedIndex;
                settings.sitemap = sitemapTextBox.Text;
                settings.connectionType = ConnectionTypesEnum.HTTPSwithUsernameAndPassword.ToString();
                settings.connectionTypeChooser = 0;
                if (!portTextBox.Text.Equals(""))
                {
                    settings.sh_port = Convert.ToInt32(portTextBox.Text);
                }
                if (refreshTrackBar.Value != 5)
                {
                    settings.refresh_time = Convert.ToInt32(refreshTrackBar.Value);
                }
                settings.saveSettings();
            }
            //HTTPSwithoutUsernameAndPassword
            if (connectionTypeChooser.SelectedIndex.Equals(1) && !urlTextBox.Text.Equals(""))
            {
                settings.sh_url = urlTextBox.Text;
                settings.short_url = urlTextBox1.Text;
                settings.protocolChooser = protocollChooser.SelectedIndex;
                settings.sitemap = sitemapTextBox.Text;
                settings.connectionType = ConnectionTypesEnum.HTTPSwithoutUsernameAndPassword.ToString();
                settings.connectionTypeChooser = 1;
                if (!portTextBox.Text.Equals(""))
                {
                    settings.sh_port = Convert.ToInt32(portTextBox.Text);
                }
                if (refreshTrackBar.Value != 5)
                {
                    settings.refresh_time = Convert.ToInt32(refreshTrackBar.Value);
                }
                settings.saveSettings();
            }
            //HTTPwithUsernameAndPassword
            if (connectionTypeChooser.SelectedIndex.Equals(2) && !urlTextBox.Text.Equals(""))
            {
                settings.username = userNameTextBox.Text;
                settings.password = passwordTextBox.Password;
                settings.sh_url = urlTextBox.Text;
                settings.short_url = urlTextBox1.Text;
                settings.protocolChooser = protocollChooser.SelectedIndex;
                settings.sitemap = sitemapTextBox.Text;
                settings.connectionType = ConnectionTypesEnum.HTTPwithUsernameAndPassword.ToString();
                settings.connectionTypeChooser = 2;
                if (!portTextBox.Text.Equals(""))
                {
                    settings.sh_port = Convert.ToInt32(portTextBox.Text);
                }
                if (refreshTrackBar.Value != 5)
                {
                    settings.refresh_time = Convert.ToInt32(refreshTrackBar.Value);
                }
                settings.saveSettings();
            }
            //HTTPwithoutUsernameAndPassword
            if (connectionTypeChooser.SelectedIndex.Equals(3) && !urlTextBox.Text.Equals(""))
            {
                settings.sh_url = urlTextBox.Text;
                settings.short_url = urlTextBox1.Text;
                settings.protocolChooser = protocollChooser.SelectedIndex;
                settings.sitemap = sitemapTextBox.Text;
                settings.connectionType = ConnectionTypesEnum.HTTPwithoutUsernameAndPassword.ToString();
                settings.connectionTypeChooser = 3;
                if (!portTextBox.Text.Equals(""))
                {
                    settings.sh_port = Convert.ToInt32(portTextBox.Text);
                }
                if (refreshTrackBar.Value != 5)//////////////////////////////////////////////////
                {
                    settings.refresh_time = Convert.ToInt32(refreshTrackBar.Value);
                }
                settings.saveSettings();
            }
            if ((connectionTypeChooser.SelectedIndex.Equals(0) || connectionTypeChooser.SelectedIndex.Equals(1) || connectionTypeChooser.SelectedIndex.Equals(2) || connectionTypeChooser.SelectedIndex.Equals(3)) && !urlTextBox.Text.Equals(""))
            {
                StorageFolder imageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("images", CreationCollisionOption.OpenIfExists);
                Application.Current.Exit();
            }
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SecuritySettingsFlyout securitySettings = new SecuritySettingsFlyout();
            securitySettings.Show();
        }

        private void generateURIButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ComboBoxItem x = protocollChooser.SelectedItem as ComboBoxItem;
            urlTextBox.Text = x.Content.ToString() + "://" + urlTextBox1.Text + ":" + portTextBox.Text.ToString() + "/rest/sitemaps/" + sitemapTextBox.Text + "?type=json";
        }

        private void demoCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            urlTextBox.Text = "http://demo.openhab.org:8080/rest/sitemaps/demo?type=json";
        }

        private void demoCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            urlTextBox.Text = "";
        }

        private async void refreshImageCacheBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StorageFolder imageFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("images");
            await imageFolder.DeleteAsync(StorageDeleteOption.PermanentDelete);
            MessageDialog messageDialog = new MessageDialog("You're going to delete all Images, this is an permanent Operation, wich can't be undone.", "Delete all Images?");
            messageDialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
            await messageDialog.ShowAsync();
        }

        private void connectionTypeChooser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (connectionTypeChooser.SelectedIndex.Equals(0) || connectionTypeChooser.SelectedIndex.Equals(2))
            {
                userNameTextBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
                passwordTextBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
                userNameTextView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                passwordTextView.Visibility = Windows.UI.Xaml.Visibility.Visible;
                savePasswordChbx.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else if (connectionTypeChooser.SelectedIndex.Equals(1) || connectionTypeChooser.SelectedIndex.Equals(3))
            {
                userNameTextBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                passwordTextBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                userNameTextView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                passwordTextView.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                savePasswordChbx.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void portTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (portTextBox.Text.Length > 5)
            {
                portTextBox.Text = portTextBox.Text.Remove(5);
            }
        }


        private void portTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if ((Int32)e.Key >= (Int32)Windows.System.VirtualKey.Number0 && (Int32)e.Key <= (Int32)Windows.System.VirtualKey.Number9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
