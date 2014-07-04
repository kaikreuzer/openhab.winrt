using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace openhab.winrt.winhab.other
{
    static class AppProperties
    {
        public static String _path { get; set; }
        public static String path
        {
            get;
            set;
        }
        public static async void setPath()
        {
            StorageFolder appFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync("images");
            AppProperties.path = appFolder.Path;
        }
    }
}
