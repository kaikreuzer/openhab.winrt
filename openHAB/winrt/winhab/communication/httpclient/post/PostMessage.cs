using JSON_Parser.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using openhab.winrt.winhab.communication.connectiontypes;
using openhab.winrt.winhab.ErrorManager;

namespace openhab.winrt.winhab.communication.httpclient.post
{
    class PostMessage
    {
        public String sh_url { get; set; }
        public String sh_path { get; set; }
        public Int32 sh_port { get; set; }
        public String sh_username { get; set; }
        public String sh_password { get; set; }
        private ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private PasswordVault passwordVault = new PasswordVault();
        public PostMessage()
        {

        }
        //public PostMessage(String sh_url, String sh_path, Int32 sh_port, String sh_username, String sh_password)
        //{

        //}

        //public PostMessage(String sh_url, String sh_path, Int32 sh_port)
        //{

        //}

        ////public async void sendInstruction(String link, String newState)
        ////{
        ////    link = link + "/state";
        ////    String method = newState;
        ////    UTF8Encoding encoder = new UTF8Encoding();
        ////    Byte[] bytes = encoder.GetBytes(method);
        ////    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        ////}
        private Windows.Web.Http.HttpClient httpClient;
        HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
        public async void sendInstruction(Item item, String newState)
        {
            if (localSettings.Values["connectionType"] != null)
            {
                if (localSettings.Values["connectionType"].ToString().Equals(ConnectionTypesEnum.HTTPSwithUsernameAndPassword.ToString()))
                {
                    filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.Untrusted);
                    filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.Expired);
                    filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.InvalidName);
                    filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.IncompleteChain);

                    filter.ServerCredential = new Windows.Security.Credentials.PasswordCredential("https", localSettings.Values["username"].ToString(), passwordVault.Retrieve("winHAB", localSettings.Values["username"].ToString()).Password);
                    httpClient = new Windows.Web.Http.HttpClient(filter);

                    String method = newState;
                    UTF8Encoding encoder = new UTF8Encoding();
                    Byte[] bytes = encoder.GetBytes(method);
                    UriBuilder uri = new UriBuilder(item.link);

                    try
                    {
                        HttpResponseMessage response = await httpClient.PostAsync(uri.Uri, new HttpStringContent(method));
                    }
                    catch (Exception ex)
                    {
                        //MessageDialog dialog = new MessageDialog(ex.Message + "\n" + ex.StackTrace, "Error while sending a request");
                        //dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
                        //dialog.ShowAsync();
                        ErrorLogger.printErrorToLog(ex.Message, ex.StackTrace);
                    }
                    finally
                    {

                    }
                }
                if (localSettings.Values["connectionType"].ToString().Equals(ConnectionTypesEnum.HTTPSwithoutUsernameAndPassword.ToString()))
                {
                    filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.Untrusted);
                    filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.Expired);
                    filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.InvalidName);
                    filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.IncompleteChain);


                    httpClient = new Windows.Web.Http.HttpClient(filter);

                    String method = newState;
                    UTF8Encoding encoder = new UTF8Encoding();
                    Byte[] bytes = encoder.GetBytes(method);
                    try
                    {
                        UriBuilder uri = new UriBuilder(item.link);

                        HttpResponseMessage response = await httpClient.PostAsync(uri.Uri, new HttpStringContent(method));
                    }
                    catch (Exception ex)
                    {
                        //MessageDialog dialog = new MessageDialog(ex.Message + "\n" + ex.StackTrace, "Error while sending a request");
                        //dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
                        //dialog.ShowAsync();
                        ErrorLogger.printErrorToLog(ex.Message, ex.StackTrace);
                    }
                    finally
                    {

                    }
                }
                if (localSettings.Values["connectionType"].ToString().Equals(ConnectionTypesEnum.HTTPwithUsernameAndPassword.ToString()))
                {
                    filter.ServerCredential = new PasswordCredential("winHAB", localSettings.Values["username"].ToString(), passwordVault.Retrieve("winHAB", localSettings.Values["username"].ToString()).Password);
                    httpClient = new HttpClient(filter);
                    String method = newState;
                    UTF8Encoding encoder = new UTF8Encoding();
                    Byte[] bytes = encoder.GetBytes(method);
                    try
                    {
                        UriBuilder uri = new UriBuilder(item.link);

                        HttpResponseMessage response = await httpClient.PostAsync(uri.Uri, new HttpStringContent(method));
                    }
                    catch (Exception ex)
                    {
                        //MessageDialog dialog = new MessageDialog(ex.Message + "\n" + ex.StackTrace, "Error while sending a request");
                        //dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
                        //dialog.ShowAsync();
                        ErrorLogger.printErrorToLog(ex.Message, ex.StackTrace);
                    }
                    finally
                    {

                    }
                }
                if (localSettings.Values["connectionType"].ToString().Equals(ConnectionTypesEnum.HTTPwithoutUsernameAndPassword.ToString()))
                {
                    httpClient = new Windows.Web.Http.HttpClient();
                    String method = newState;
                    UTF8Encoding encoder = new UTF8Encoding();
                    Byte[] bytes = encoder.GetBytes(method);
                    try
                    {
                        UriBuilder uri = new UriBuilder(item.link);

                        HttpResponseMessage response = await httpClient.PostAsync(uri.Uri, new HttpStringContent(method));
                    }
                    catch (Exception ex)
                    {
                        //MessageDialog dialog = new MessageDialog(ex.Message + "\n" + ex.StackTrace, "Error while sending a request");
                        //dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
                        //dialog.ShowAsync();
                        ErrorLogger.printErrorToLog(ex.Message, ex.StackTrace);
                    }
                    finally
                    {

                    }
                }
            }
        }
    }
}
