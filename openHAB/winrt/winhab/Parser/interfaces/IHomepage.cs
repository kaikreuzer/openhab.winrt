using System;
using System.ComponentModel;
namespace openhab.winrt.winhab.Parser.interfaces
{
    /// <summary>
    /// Define the Homepage-Object of openhab.winrt.winhab
    /// </summary>
    interface IHomepage : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the id of a Homepage
        /// </summary>
        string id { get; set; }
        /// <summary>
        /// Gets or sets the link of a Homepage
        /// </summary>
        string link { get; set; }
        /// <summary>
        /// Gets or sets the title of a Homepage
        /// </summary>
        string title { get; set; }
        /// <summary>
        /// Gets or sets the widget-list of a Homepage
        /// </summary>
        System.Collections.Generic.LinkedList<JSON_Parser.Parser.Widget> widget { get; set; }
        /// <summary>
        /// Function to call the PropertyChangedEvent
        /// </summary>
        /// <param name="propertyName"></param>
        void NotifyPropertyChanged(string propertyName);
    }
}
