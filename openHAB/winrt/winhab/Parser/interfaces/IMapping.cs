using System;
using System.ComponentModel;
namespace openhab.winrt.winhab.Parser.interfaces
{
    /// <summary>
    /// Define the Mapping-Object of openhab.winrt.winhab
    /// </summary>
    interface IMapping : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the command of a Mapping-Object
        /// </summary>
        string command { get; set; }
        /// <summary>
        /// Gets or sets the Label of a Mapping-Object
        /// </summary>
        string label { get; set; }
        /// <summary>
        /// Function to call the PropertyChangedEvent
        /// </summary>
        /// <param name="propertyName"></param>
        void NotifyPropertyChanged(string propertyName);
    }
}
