using JSON_Parser.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using openhab.winrt.winhab.ErrorManager;
using openhab.winrt.winhab.Parser;

namespace openhab.winrt.winhab.communication.httpclient.get
{
    class GetJson
    {
        #region Variablen
        private String sh_url { get; set; }
        private String sh_path { get; set; }
        private Int32 sh_port { get; set; }
        private String sh_username { get; set; }
        private String sh_passwort { get; set; }
        private UriBuilder sh_uriBuilder { get; set; }
        public delegate void jsonReady(JsonOpenHABDataContract result);
        public event jsonReady sh_downloadFinished;
        //public String json { get; set; }
        public Boolean secured { get; set; }
        private HttpClient httpClient;
        private HttpBaseProtocolFilter filter;
        private UriBuilder uriToOpenHAB { get; set; }
        private HttpResponseMessage httpResponseMessage { get; set; }
        private JsonSerializer parser = new JsonSerializer();
        //private Boolean _toastOnce = false;
        //ErrorVisualizer errorVisualizier = new ErrorVisualizer();
        #endregion
        #region Constructors
        /// <summary>
        /// Constructor with encryption username and password
        /// </summary>
        /// <param name="sh_url">URL to Server</param>
        /// <param name="sh_path">Path to JSON</param>
        /// <param name="sh_port">Serverport</param>
        /// <param name="sh_username">Username</param>
        /// <param name="sh_password">Passwort</param>
        public GetJson(String sh_url, String sh_path, Int32 sh_port, String sh_username, String sh_password, Boolean isHTTPSOn)
        {
            this.sh_url = sh_url;
            this.sh_path = sh_path;
            this.sh_port = sh_port;
            this.sh_username = sh_username;
            this.sh_passwort = sh_password;
            //this.sh_uriBuilder = new UriBuilder("http", this.sh_url, this.sh_port, "/rest/sitemaps/");
            this.secured = isHTTPSOn;
            if (secured)
            {
                try
                {
                    uriToOpenHAB = new UriBuilder(sh_url);
                }
                catch (ArgumentNullException ex)
                {
                    //Happens when the HostName could not parsed
                }
                catch (FormatException ex)
                {
                    //Is equal to UriFormatException
                }
                filter = new HttpBaseProtocolFilter();
                /**
                 * Ignore some Certifcateerrors
                 * */
                filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.Untrusted);
                filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.Expired);
                filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.InvalidName);
                filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.IncompleteChain);
                filter.ServerCredential = new PasswordCredential("winHAB", this.sh_username, this.sh_passwort);
                /**
                 * Establish HttpClient
                 * */
                httpClient = new HttpClient(filter);
                Windows.Web.Http.Headers.HttpNameValueHeaderValue cacheControl = new Windows.Web.Http.Headers.HttpNameValueHeaderValue("Cache-Control", "must-revalidate");
                Windows.Web.Http.Headers.HttpNameValueHeaderValue uft8Encoding = new Windows.Web.Http.Headers.HttpNameValueHeaderValue("charset", "utf-8");
                httpClient.DefaultRequestHeaders.CacheControl.Add(cacheControl);
                httpClient.DefaultRequestHeaders.CacheControl.Add(uft8Encoding);
            }
            else if (!secured)
            {

            }
        }
        /// <summary>
        /// Constructur with encryption but WITHOUT username and password
        /// </summary>
        /// <param name="sh_url"></param>
        /// <param name="sh_path"></param>
        /// <param name="sh_port"></param>
        /// <param name="isHTTPSOn"></param>
        public GetJson(String sh_url, String sh_path, Int32 sh_port, Boolean isHTTPSOn)
        {
            this.sh_url = sh_url;
            this.sh_path = sh_path;
            this.sh_port = sh_port;
            this.secured = isHTTPSOn;
            if (secured)
            {
                try
                {
                    uriToOpenHAB = new UriBuilder(sh_url);
                }
                catch (ArgumentNullException ex)
                {
                    //Happens when the HostName could not parsed
                }
                catch (FormatException ex)
                {
                    //Is equal to UriFormatException
                }
                filter = new HttpBaseProtocolFilter();
                /**
                 * Ignore some Certifcateerrors
                 * */
                filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.Untrusted);
                filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.Expired);
                filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.InvalidName);
                filter.IgnorableServerCertificateErrors.Add(Windows.Security.Cryptography.Certificates.ChainValidationResult.IncompleteChain);

                /**
                 * Establish HttpClient
                 * */
                httpClient = new HttpClient(filter);
                Windows.Web.Http.Headers.HttpNameValueHeaderValue cacheControl = new Windows.Web.Http.Headers.HttpNameValueHeaderValue("Cache-Control", "must-revalidate");
                Windows.Web.Http.Headers.HttpNameValueHeaderValue uft8Encoding = new Windows.Web.Http.Headers.HttpNameValueHeaderValue("charset", "utf-8");
                httpClient.DefaultRequestHeaders.CacheControl.Add(cacheControl);
                httpClient.DefaultRequestHeaders.CacheControl.Add(uft8Encoding);
            }
            else if (!secured)
            {

            }

        }
        /// <summary>
        /// Constructor with encryption but with username and password
        /// </summary>
        /// <param name="sh_url"></param>
        /// <param name="sh_path"></param>
        /// <param name="sh_port"></param>
        /// <param name="sh_username"></param>
        /// <param name="sh_password"></param>
        public GetJson(String sh_url, String sh_path, Int32 sh_port, String sh_username, String sh_password)
        {
            this.sh_username = sh_username;
            this.sh_passwort = sh_password;
            this.sh_url = sh_url;
            this.sh_path = sh_path;
            this.sh_port = sh_port;
            try
            {
                uriToOpenHAB = new UriBuilder(sh_url);
            }
            catch (ArgumentNullException ex)
            {
                //Happens when the HostName could not parsed
            }
            catch (FormatException ex)
            {
                //Is equal to UriFormatException
            }
            filter = new HttpBaseProtocolFilter();
            filter.ServerCredential = new PasswordCredential("winHAB", this.sh_username, this.sh_passwort);
            /**
             * Establish HttpClient
             * */
            httpClient = new HttpClient(filter);
            Windows.Web.Http.Headers.HttpNameValueHeaderValue cacheControl = new Windows.Web.Http.Headers.HttpNameValueHeaderValue("Cache-Control", "must-revalidate");
            Windows.Web.Http.Headers.HttpNameValueHeaderValue uft8Encoding = new Windows.Web.Http.Headers.HttpNameValueHeaderValue("charset", "utf-8");
            httpClient.DefaultRequestHeaders.CacheControl.Add(cacheControl);
            httpClient.DefaultRequestHeaders.CacheControl.Add(uft8Encoding);
        }
        /// <summary>
        /// Constructor without encryption, username and password
        /// </summary>
        /// <param name="sh_url"></param>
        /// <param name="sh_path"></param>
        /// <param name="sh_port"></param>
        public GetJson(String sh_url, String sh_path, Int32 sh_port)
        {
            uriToOpenHAB = new UriBuilder(sh_url);
            this.sh_url = sh_url;
            this.sh_path = sh_path;
            this.sh_port = sh_port;
            try
            {
                uriToOpenHAB = new UriBuilder(sh_url);
            }
            catch (ArgumentNullException ex)
            {
                //Happens when the HostName could not parsed
            }
            catch (FormatException ex)
            {
                //Is equal to UriFormatException
            }
            httpClient = new HttpClient();
            Windows.Web.Http.Headers.HttpNameValueHeaderValue cacheControl = new Windows.Web.Http.Headers.HttpNameValueHeaderValue("Cache-Control", "must-revalidate");
            Windows.Web.Http.Headers.HttpNameValueHeaderValue uft8Encoding = new Windows.Web.Http.Headers.HttpNameValueHeaderValue("charset", "utf-8");
            httpClient.DefaultRequestHeaders.CacheControl.Add(cacheControl);
            httpClient.DefaultRequestHeaders.CacheControl.Add(uft8Encoding);
        }
        #endregion
        #region Establish Connection happend here
        private String tmpjson = "";
        public async Task<String> connectAndLoad()
        {
            if (httpResponseMessage != null)
                tmpjson = httpResponseMessage.Content.ToString();
            //Windows.Web.Http.Headers.HttpNameValueHeaderValue cacheControl = new Windows.Web.Http.Headers.HttpNameValueHeaderValue("Cache-Control", "must-revalidate");

            //httpClient.DefaultRequestHeaders.CacheControl.Add(cacheControl);
            try
            {
                httpResponseMessage = await httpClient.GetAsync(uriToOpenHAB.Uri, HttpCompletionOption.ResponseContentRead);
                String json = await httpResponseMessage.Content.ReadAsStringAsync();
                //It is needed, to refractor the json before parsing it, because the .net DataContractJsonSeralizer has problems with the String
                JsonRefractor jsonRefractor = new JsonRefractor(json);
                if (httpResponseMessage.StatusCode == HttpStatusCode.Ok)
                {
                    json = jsonRefractor.refractor("\"widget\":{");
                    sh_downloadFinished(parser.parse(json));
                }
                else// if (json == "")
                {
                    int x = 0;
                    //errorVisualizier.showError("Fehler beim verbinden zum Server", "Fehler beim verbinden zum Server");
                    //System.Diagnostics.Debug.WriteLine("JSON ist leer");
                    //MainPage.taskReady = true;
                }
            }
            catch (COMException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                ErrorLogger.printErrorToLog(ex.Message, ex.StackTrace);
            }
            catch (ObjectDisposedException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                ErrorLogger.printErrorToLog(ex.Message, ex.StackTrace);
                //errorVisualizier.showError(ex.StackTrace, ex.Message);
                //MainPage._errorOccured = true;
            }
            catch (OutOfMemoryException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                ErrorLogger.printErrorToLog(ex.Message, ex.StackTrace);

                //errorVisualizier.showError(ex.StackTrace, ex.Message);
                //MainPage._errorOccured = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                ErrorLogger.printErrorToLog(ex.Message, ex.StackTrace);
                //showError(ex.StackTrace, ex.Message);
                //MainPage._errorOccured = true;
            }
            return "";


        }
        #endregion
    }
}