using System;
using System.ComponentModel;
namespace openhab.winrt.winhab.Parser.interfaces
{
    /// <summary>
    /// Define the JsonOpenHABDataContract of openhab.winrt.winhab
    /// </summary>
    interface IJsonOpenHABDataContract : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the homepage of JsonOpenHABDataContract
        /// </summary>
        JSON_Parser.Parser.Homepage homepage { get; set; }
        /// <summary>
        /// Gets or sets the link of JsonOpenHABDataContract
        /// </summary>
        string link { get; set; }
        /// <summary>
        /// Gets or sets the name of JsonOpenHABDataContract
        /// </summary>
        string name { get; set; }
        /// <summary>
        /// Gets or sets the sitemap of JsonOpenHABDataContract
        /// </summary>
        System.Collections.Generic.LinkedList<JSON_Parser.Parser.Sitemap> sitemap { get; set; }
        /// <summary>
        /// Function to call the PropertyChangedEvent
        /// </summary>
        /// <param name="propertyName"></param>
        void NotifyPropertyChanged(string propertyName);
    }
}
