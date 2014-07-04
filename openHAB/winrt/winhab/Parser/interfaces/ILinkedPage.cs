using System;
using System.ComponentModel;
namespace openhab.winrt.winhab.Parser.interfaces
{
    /// <summary>
    /// Define the LinkedPage-Object of openhab.winrt.winhab
    /// </summary>
    interface ILinkedPage : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        string icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        System.Collections.Generic.LinkedList<JSON_Parser.Parser.Widget> widget { get; set; }
        /// <summary>
        /// Function to call the PropertyChangedEvent
        /// </summary>
        /// <param name="propertyName"></param>
        void NotifyPropertyChanged(string propertyName);
    }
}
