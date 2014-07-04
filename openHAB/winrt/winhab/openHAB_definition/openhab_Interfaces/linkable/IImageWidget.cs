using System;
using System.ComponentModel;
namespace openhab.winrt.winhab.openHAB_definition.openhab_Interfaces.linkable
{
    interface IImageWidget : INotifyPropertyChanged
    {
        string icon { get; set; }
        JSON_Parser.Parser.Item item { get; set; }
        string label { get; set; }
        JSON_Parser.Parser.LinkedPage linkedPage { get; set; }
        void NotifyPropertyChanged(string propertyName);
        string type { get; set; }
        System.Collections.Generic.LinkedList<JSON_Parser.Parser.Widget> widget { get; set; }
        string widgetId { get; set; }
        string pathToIcon { get; set; }
    }
}
