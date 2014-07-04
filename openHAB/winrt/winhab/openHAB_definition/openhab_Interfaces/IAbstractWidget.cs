using System;
using System.ComponentModel;
namespace openHAB.openHAB_definition.openhab_Interfaces.nonlinkable
{
    /// <summary>
    /// This Interface provides the standard properties and Eventlisteners for propertiechanges
    /// </summary>
    interface IAbstractWidget : INotifyPropertyChanged
    {
        //string icon { get; set; }
        //JSON_Parser.Parser.Item item { get; set; }
        //string label { get; set; }
        //JSON_Parser.Parser.LinkedPage linkedPage { get; set; }
        //void NotifyPropertyChanged(string propertyName);
        //event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        //string type { get; set; }
        //System.Collections.Generic.LinkedList<JSON_Parser.Parser.Widget> widget { get; set; }
        //string widgetId { get; set; }
        bool switchState { get; set; }
        string icon { get; set; }
        JSON_Parser.Parser.Item item { get; set; }
        string label { get; set; }
        JSON_Parser.Parser.LinkedPage linkedPage { get; set; }
        void NotifyPropertyChanged(string propertyName);
        string type { get; set; }
        System.Collections.Generic.LinkedList<JSON_Parser.Parser.Widget> widget { get; set; }
        string widgetId { get; set; }
    }
}
