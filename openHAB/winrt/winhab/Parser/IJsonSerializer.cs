using System;
namespace openhab.winrt.winhab.Parser
{
    interface IJsonSerializer
    {
        string json { get; set; }
        JSON_Parser.Parser.JsonOpenHABDataContract parse(string json);
        event JSON_Parser.Parser.JsonSerializer.jsonReady sh_jsonReadyEvent;
        JSON_Parser.Parser.JsonOpenHABDataContract sh_jsonresult { get; set; }
        System.Collections.Generic.LinkedList<JSON_Parser.Parser.Widget> widgetList { get; set; }
    }
}
