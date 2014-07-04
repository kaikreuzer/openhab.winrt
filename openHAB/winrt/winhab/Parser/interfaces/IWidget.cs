using System;
using System.ComponentModel;
namespace openhab.winrt.winhab.Parser.interfaces
{
    /// <summary>
    /// Interface to define a Widgetobject
    /// </summary>
    public interface IWidget : INotifyPropertyChanged
    {
        bool guiState { get; set; }
        /// <summary>
        /// Get or sets the iconname of a widget
        /// </summary>
        string icon { get; set; }
        /// <summary>
        /// Get or sets the Item of a Widget
        /// </summary>
        JSON_Parser.Parser.Item item { get; set; }
        /// <summary>
        /// Get or sets the label of a widget
        /// </summary>
        string label { get; set; }
        /// <summary>
        /// Get or sets the linkedPage of a Widget
        /// </summary>
        JSON_Parser.Parser.LinkedPage linkedPage { get; set; }
        /// <summary>
        /// Get or sets the mapping of a widget
        /// </summary>
        System.Collections.Generic.LinkedList<JSON_Parser.Parser.Mapping> mapping { get; set; }
        /// <summary>
        /// Function to call the PropertyChangedEvent
        /// </summary>
        /// <param name="propertyName"></param>
        void NotifyPropertyChanged(string propertyName);
        /// <summary>
        /// Get or sets the type of a widget
        /// </summary>
        string type { get; set; }
        /// <summary>
        /// Get or sets the url of a widget
        /// </summary>
        string url { get; set; }
        /// <summary>
        /// Get or sets the widget-list of a widget
        /// </summary>
        System.Collections.Generic.LinkedList<JSON_Parser.Parser.Widget> widget { get; set; }
        /// <summary>
        /// Get or sets the widgetId of a widget
        /// </summary>
        string widgetId { get; set; }
        /// <summary>
        /// Get or sets the guiText which is used in the DataTemplates
        /// </summary>
        string guiText { get; set; }
        /// <summary>
        /// Gets or sets the guivalue which is used in the DataTemplates
        /// </summary>
        string guiValue { get; set; }
        /// <summary>
        /// Gets or sets the Path to the Icon, wich is displayed to the User
        /// </summary>
        string pathToIcon { get; set; }
    }
}
