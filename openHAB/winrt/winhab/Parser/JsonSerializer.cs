using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Networking.Sockets;
using Windows.Security.Cryptography;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System.Threading;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using openhab.winrt.winhab.ErrorManager;
using openhab.winrt.winhab.Parser;

namespace JSON_Parser.Parser
{
    public class JsonSerializer :openhab.winrt.winhab.Parser.IJsonSerializer
    {
        #region Variablen
        //private Windows.Web.Http.HttpClient httpClient;
        private HttpClientHandler httpClientHandler = new HttpClientHandler();
        private String sh_url { get; set; }
        private String sh_path { get; set; }
        private Int32 sh_port { get; set; }
        private String sh_username { get; set; }
        private String sh_passwort { get; set; }
        private UriBuilder sh_uriBuilder { get; set; }
        public delegate void jsonReady();
        public event jsonReady sh_jsonReadyEvent;
        public String json { get; set; }
        public JsonOpenHABDataContract sh_jsonresult { get; set; }
        public static JsonSerializer instance { get; set; }
        public LinkedList<Widget> widgetList { get; set; }
        public ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
         //ErrorVisualizer errorVisualizier = new ErrorVisualizer();
        #endregion





        public JsonSerializer()
        {

        }
        public JsonOpenHABDataContract parse(String json)
        {
            MemoryStream stream = new MemoryStream(UTF8Encoding.UTF8.GetBytes(json));
            stream.Position = 0;
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
            //settings.UseSimpleDictionaryFormat = true;
            DataContractJsonSerializer jsonSerielizer = new DataContractJsonSerializer(typeof(JsonOpenHABDataContract), settings);
            try
            {
                sh_jsonresult = (JsonOpenHABDataContract)jsonSerielizer.ReadObject(stream);  
                //MessageDialog m = new MessageDialog("hfihdsoi");
                //m.ShowAsync();
            }
            catch (Exception ex)
            {
                ErrorLogger.printErrorToLog(ex.Message, ex.StackTrace);
                //errorVisualizier.showError(ex.StackTrace, ex.Message);
            }
            return sh_jsonresult;
        }
    }
}
