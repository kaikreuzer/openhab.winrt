using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.UI.Xaml.Data;
using openhab.winrt.winhab.communication.imageCacheControlManager;
using openhab.winrt.winhab.openHAB_definition.openHAB_Widgets;
using openhab.winrt.winhab.Parser.interfaces;
using Windows.UI.Xaml;
using Windows.Storage;
using openhab.winrt.winhab.Parser;
using openhab.winrt.winhab.other;

namespace JSON_Parser.Parser
{

    /// <summary>
    /// This class provies the standard-dataformat for the openhab.winrt.winhab json-file
    /// </summary>
    #region JsonOpenHABDataContract
    [DataContract]
    public class JsonOpenHABDataContract : IJsonOpenHABDataContract
    {
        [DataMember]
        public LinkedList<Sitemap> sitemap { get; set; }
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public String link { get; set; }
        [DataMember]
        public Homepage homepage { get; set; }
        public static Dictionary<String, Widget> widgets = new Dictionary<String, Widget>();
        //public static Int32 widgetCounter = 0;
        public static Queue<String> icons = new Queue<String>();
        //public static Dictionary<>
        //public static Int32 itemCounter = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    #endregion

    #region Sitemap
    public class Sitemap : ISitemap
    {
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public String link { get; set; }
        [DataMember]
        public LinkedList<Homepage> homepage { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    #endregion
    #region Homepage
    [DataContract]
    public class Homepage : IHomepage
    {
        [DataMember]
        public String id { get; set; }
        [DataMember]
        public String title { get; set; }
        [DataMember]
        public String link { get; set; }
        [DataMember]
        public LinkedList<Widget> widget { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

    }
    #endregion
    #region Widget
    [DataContract]
    public class Widget : IWidget
    {
        public Boolean guiState { get; set; }
        [DataMember]
        private String _widgetId { get; set; }
        [DataMember]
        public String widgetId
        {
            get
            {
                return _widgetId;
            }
            set
            {
                _widgetId = value;
                try
                {
                    JsonOpenHABDataContract.widgets.Add(this.widgetId, this);
                }
                catch
                { }
                NotifyPropertyChanged("widgetId");
            }
        }
        [DataMember]
        private String _type { get; set; }
        [DataMember]
        public String type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        [DataMember]
        private String _label { get; set; }
        [DataMember]
        public String label
        {
            get
            {
                guiText = _label;
                if (_label.Contains("Tempera"))
                {
                    guiText = "Temperature";
                }
                if (_label.Contains("open") || _label.Contains("closed"))
                {
                    guiText = guiText.Replace("[open]", "");
                    guiText = guiText.Replace("[closed]", "");
                }
                if (_label.Contains('['))
                {
                    guiText = guiText.Replace('[', ' ');
                    guiText = guiText.Replace(']', ' ');
                }
                return _label;
            }
            set
            {

                _label = value;
                NotifyPropertyChanged("label");
            }
        }
        [DataMember]
        private String _icon { get; set; }
        private Boolean _once = false;
        [DataMember]
        public String icon
        {
            get { return _icon; }
            set
            {
                if (!JsonOpenHABDataContract.icons.Contains(value + ".png") && !value.Equals("frame"))
                {
                    JsonOpenHABDataContract.icons.Enqueue(value + ".png");
                }
                if (_once != true)
                {
                    _icon = value;
                    _once = true;
                    AppProperties.setPath();
                    pathToIcon = AppProperties.path+"\\" + value + ".png";
                }
                else
                    _icon = value;
                NotifyPropertyChanged("icon");
            }
        }
        [DataMember]
        public LinkedList<Widget> widget { get; set; }
        [DataMember]
        public LinkedPage linkedPage { get; set; }

        private Item _item { get; set; }
        [DataMember(Name = "item")]
        public Item item { get { return _item; } set { _item = value; guiValue = value.state; } }
        [DataMember]
        private String _url { get; set; }
        [DataMember]
        public String url { get { return _url; } set { _url = value; } }
        [DataMember]
        private LinkedList<Mapping> _mapping { get; set; }
        [DataMember]
        public LinkedList<Mapping> mapping
        {
            get
            {
                return _mapping;
            }
            set
            {
                _mapping = value;
            }
        }
        public string _guiText { get; set; }
        public string guiText
        {
            get
            {
                return _guiText;
            }
            set
            {
                _guiText = value;
                NotifyPropertyChanged("guiText");
            }
        }

        private string _guiValue { get; set; }
        public string guiValue
        {
            get
            {
                try
                {
                    String tmp = _guiValue;
                    if (tmp.Contains('.'))
                    {
                        tmp = tmp.Replace('.', ',');
                    }
                    Double number = Convert.ToDouble(tmp);
                    _guiValue = Convert.ToString(number);
                }
                catch (Exception ex)
                {

                }
                return _guiValue;
            }
            set
            {
                _guiValue = value;
                NotifyPropertyChanged("guiValue");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _pathToIcon
        {
            get;
            set;
        }

        public string pathToIcon
        {
            get
            {
                return _pathToIcon;
            }
            set
            {
                _pathToIcon = value;
                NotifyPropertyChanged("pathToIcon");
            }
        }
    }
    #endregion
    #region Item
    [DataContract]
    public class Item : IItem
    {
        [DataMember]
        private String _type { get; set; }
        [DataMember]
        public String type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                //JsonOpenHABDataContract.icons.Add(JsonOpenHABDataContract.itemCounter++, this);
            }
        }
        [DataMember]
        private String _name { get; set; }
        [DataMember]
        public String name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public Boolean guiState { get; set; }
        [DataMember]
        private String _state { get; set; }
        [DataMember]
        public String state
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                NotifyPropertyChanged("state");
            }
        }
        [DataMember]
        private String _link { get; set; }
        [DataMember]
        public String link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    #endregion
    #region LinkedPage
    [DataContract]
    public class LinkedPage : ILinkedPage
    {
        [DataMember]
        public String id { get; set; }
        [DataMember]
        public String title { get; set; }
        [DataMember]
        public String icon { get; set; }
        [DataMember]
        public LinkedList<Widget> widget { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    #endregion

    #region Mapping
    [DataContract]
    public class Mapping : IMapping
    {
        [DataMember]
        private String _command { get; set; }
        [DataMember]
        public String command
        {
            get
            {
                return _command;
            }
            set
            {
                _command = value;
            }
        }
        [DataMember]
        private String _label { get; set; }
        [DataMember]
        public String label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
        //private async void getFolder()
        //{
        //    StorageFolder imageFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("images");
        //    pathToImages = imageFolder.Path;
        //}
        //public String pathToImages { get; set; }
    }


    #endregion
}
