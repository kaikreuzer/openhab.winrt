using JSON_Parser.Parser;
using openhab.winrt.winhab.ErrorManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace openhab.winrt.winhab.communication.imageCacheControlManager
{
    public class ImageCacheManager
    {
        #region Variablen
        private String sh_url { get; set; }
        private String sh_path { get; set; }
        private Int32 sh_port { get; set; }
        private String sh_username { get; set; }
        private String sh_passwort { get; set; }
        private UriBuilder sh_uriBuilder { get; set; }
        public delegate void progress(int result);
        public event progress reportProgress;
        //public String json { get; set; }
        public Boolean secured { get; set; }
        BackgroundDownloader backgroundDownloader { get; set; }
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
        public ImageCacheManager(String sh_url, String sh_path, Int32 sh_port, String sh_username, String sh_password, Boolean isHTTPSOn)
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
                backgroundDownloader = new BackgroundDownloader();
                backgroundDownloader.ServerCredential = new PasswordCredential("winHAB", this.sh_username, this.sh_passwort);
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
        public ImageCacheManager(String sh_url, String sh_path, Int32 sh_port, Boolean isHTTPSOn)
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
                backgroundDownloader = new BackgroundDownloader();
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
        public ImageCacheManager(String sh_url, String sh_path, Int32 sh_port, String sh_username, String sh_password)
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
            backgroundDownloader = new BackgroundDownloader();
            backgroundDownloader.ServerCredential = new PasswordCredential("winHAB", this.sh_username, this.sh_passwort);

        }
        /// <summary>
        /// Constructor without encryption, username and password
        /// </summary>
        /// <param name="sh_url"></param>
        /// <param name="sh_path"></param>
        /// <param name="sh_port"></param>
        public ImageCacheManager(String sh_url, String sh_path, Int32 sh_port)
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
            backgroundDownloader = new BackgroundDownloader();
            //httpClient = new HttpClient();
            //Windows.Web.Http.Headers.HttpNameValueHeaderValue cacheControl = new Windows.Web.Http.Headers.HttpNameValueHeaderValue("Cache-Control", "must-revalidate");

            //httpClient.DefaultRequestHeaders.CacheControl.Add(cacheControl);
        }
        #endregion



        /*#region all Methods to refresh the Imagecach and save them to Appfolder
        public static LinkedList<String> imageList = new LinkedList<String>();//{ get; set; }
        public async void loadImages()
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(JsonOpenHABDataContract));
            //String x = await httpClient.GetStringAsync(new Uri("http://demo.openhab.org:8080/rest/sitemaps/demo?type=json"));
            //MemoryStream mem = new MemoryStream(UTF8Encoding.UTF8.GetBytes(x));
            JsonOpenHABDataContract structs = (JsonOpenHABDataContract)json.ReadObject(mem);
            Windows.Storage.Streams.IInputStream inputStream;
            StorageFolder imageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("images", CreationCollisionOption.OpenIfExists);
            String imageName = "";
            Int32 counter = 0;
            BackgroundDownloader backgroundDownloader = new BackgroundDownloader();
            Queue<String> tmpImages = JsonOpenHABDataContract.icons;
            while (tmpImages.Count != 0)
            {
                try
                {
                    imageName = tmpImages.Dequeue();
                    //inputStream = await httpClient.GetInputStreamAsync(new Uri(uriToOpenHAB.Uri.ToString() + imageName));
                    StorageFile storageFile = await imageFolder.CreateFileAsync(imageName, CreationCollisionOption.ReplaceExisting);
                    DownloadOperation downloadOperation = backgroundDownloader.CreateDownload(new Uri(uriToOpenHAB.Uri.ToString() + imageName), storageFile);
                    await downloadOperation.StartAsync();
                    reportProgress(counter++);
                }
                catch (Exception ex)
                {
                    ErrorLogger.printErrorToLog(ex.StackTrace, ex.Message);
                }
            }
        }
        #endregion*/

        #region Checks if Image exits on the HDD
        /// <summary>
        /// Checks if the Image exists on the HDD
        /// </summary>
        /// <param name="imageName"></param>
        public async void checkIfImageExist(String imageName)
        {
            StorageFolder storageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("images",CreationCollisionOption.OpenIfExists);
            try
            {
                StorageFile imageFile = await storageFolder.GetFileAsync(imageName);
            }
            catch (FileNotFoundException ex)
            {
                //ErrorLogger.printErrorToLog(ex.StackTrace, ex.Message);
                loadImages(imageName);
            }
        }
        #endregion

        #region Method to load single Images
        public async void loadImages(String imageName)
        {
            Windows.Storage.Streams.IInputStream inputStream;
            StorageFolder imageFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("images", CreationCollisionOption.OpenIfExists);
            try
            {
                //imageName = tmpImages.Dequeue();
                StorageFile storageFile = await imageFolder.CreateFileAsync(imageName, CreationCollisionOption.ReplaceExisting);
                UriBuilder uriBuilder = new UriBuilder(uriToOpenHAB.Uri.ToString());
                Uri uri = new Uri(uriBuilder.Scheme+"://"+uriBuilder.Host+":"+uriBuilder.Port+"/"+"images/"+imageName);
                DownloadOperation downloadOperation = backgroundDownloader.CreateDownload(uri, storageFile);
                await downloadOperation.StartAsync();
                //reportProgress(counter++);
            }
            catch (Exception ex)
            {
                ErrorLogger.printErrorToLog(ex.StackTrace, ex.Message);
            }
        }

        #endregion
    }
}
