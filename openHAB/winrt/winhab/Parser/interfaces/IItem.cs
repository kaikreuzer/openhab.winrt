using System;
using System.ComponentModel;
namespace openhab.winrt.winhab.Parser.interfaces
{
    /// <summary>
    /// Define the Item-Object of openhab.winrt.winhab
    /// </summary>
    interface IItem : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the guiState of an Item
        /// </summary>
        bool guiState { get; set; }
        /// <summary>
        /// Gets or sets the Link of an Item
        /// </summary>
        string link { get; set; }
        /// <summary>
        /// Gets or sets the Name of an Item
        /// </summary>
        string name { get; set; }
        /// <summary>
        /// Gets or sets the state of an Item
        /// </summary>
        string state { get; set; }
        /// <summary>
        /// Gets or sets the state of an item
        /// </summary>
        string type { get; set; }
        /// <summary>
        /// This function is used to call the PropertyChangeEvent of an Item
        /// </summary>
        /// <param name="propertyName"></param>
        void NotifyPropertyChanged(string propertyName);
    }
}
