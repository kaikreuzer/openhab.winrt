using System;
using System.ComponentModel;
namespace openhab.winrt.winhab.Parser.interfaces
{
    /// <summary>
    /// Interface to define a openhab.winrt.winhab-Sitemap
    /// </summary>
    interface ISitemap : INotifyPropertyChanged
    {
        /// <summary>
        /// Get or set the homepage of a Sitemap
        /// </summary>
        System.Collections.Generic.LinkedList<JSON_Parser.Parser.Homepage> homepage { get; set; }
        /// <summary>
        /// Get or sets the link of a Sitemap
        /// </summary>
        string link { get; set; }
        /// <summary>
        /// Gets or sets the Name of a Sitemap
        /// </summary>
        string name { get; set; }
        /// <summary>
        /// Function to call the PropertyChangedEvent
        /// </summary>
        /// <param name="propertyName"></param>
        void NotifyPropertyChanged(string propertyName);
    }
}
