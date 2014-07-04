using System;
namespace openhab.winrt.winhab.openHAB_definition.openHAB_Widgets
{
    interface IAbstractWidget
    {
        string icon { get; set; }
        JSON_Parser.Parser.Item item { get; set; }
        string label { get; set; }
        JSON_Parser.Parser.LinkedPage linkedPage { get; set; }
        void NotifyPropertyChanged(string propertyName);
        event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        bool switchState { get; set; }
        string type { get; set; }
        System.Collections.Generic.LinkedList<JSON_Parser.Parser.Widget> widget { get; set; }
        string widgetId { get; set; }
    }
}
