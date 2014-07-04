using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;
using Windows.Storage;

namespace JSON_Parser.SettingsManager
{
    public class Settings
    {
        /**
         * Standard Usersettings stored secured
         * */
        public String username
        {
            get;
            set;
        }
        public String password
        {
            get;
            set;
        }
        /**
         * Standard Settings to access openhab.winrt.winhab
         * */
        public String connectionType
        {
            get;
            set;
        }
        public Int32 connectionTypeChooser
        {
            get;
            set;
        }
        public String sh_url
        {
            get;
            set;
        }
        public String short_url
        {
            get;
            set;
        }
        public int protocolChooser
        {
            get;
            set;
        }
        public String sh_path
        {
            get;
            set;
        }
        public Int32 sh_port
        {
            get;
            set;
        }
        public int refresh_time
        {
            get;
            set;
        }

        public String sitemap
        {
            get;
            set;
        }
        /**
         * Standard Settings for HTTPS
         * */
        public Boolean usesignedCertificate
        {
            get;
            set;
        }
        public Boolean trustunsignedCertificates
        {
            get;
            set;
        }
        public Boolean ignoreUntrusted
        {
            get;
            set;
        }
        public Boolean ignoreExpired
        {
            get;
            set;
        }
        public Boolean ignoreInvalidName
        {
            get;
            set;
        }
        public Boolean ignoreIncompleteChain
        {
            get;
            set;
        }
        /**
         * Load the SettingsContainer
         * */
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        /**
         * Load the securedSettingsContainer
         * */
        PasswordVault passwordVault = new PasswordVault();


        /// <summary>
        /// Saves the AppSettings as XML
        /// </summary>
        public void saveSettings()
        {
            /**
             * Adds the crentials to vault
             * */
            localSettings.Values["username"] = username;
            //localSettings.Values["password"] = password;
            try
            {
                passwordVault.Add(new PasswordCredential("winHAB", localSettings.Values["username"].ToString(), password));
            }
            catch (Exception ex)
            {
                //When the save Password operation failed
            }
            /**
             * Adds the connectionsettings to Appsettings
             * */
            localSettings.Values["connectionTypeChooser"] = connectionTypeChooser;
            localSettings.Values["connectiontype"] = connectionType;
            localSettings.Values["url"] = sh_url;
            localSettings.Values["port"] = sh_port;
            localSettings.Values["refreshTime"] = refresh_time;
            localSettings.Values["sitemap"] = sitemap;
            localSettings.Values["shortUrl"] = short_url;
            localSettings.Values["protocolChooser"] = protocolChooser;
            /*
             * Adds the HTTPS(SSL)-Settings to Appsettings 
             * */
            localSettings.Values["usesignedCertificate"] = "";
            localSettings.Values["trustunsignedCertificates"] = "";
            localSettings.Values["ignoreUntrusted"] = "";
            localSettings.Values["ignoreExpired"] = "";
            localSettings.Values["ignoreInvalidName"] = "";
            localSettings.Values["ignoreIncompleteChain"] = "";
        }
    }
}
