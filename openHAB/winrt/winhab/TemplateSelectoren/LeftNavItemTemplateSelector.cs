using JSON_Parser.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using openhab.winrt.winhab.openHAB_definition.openHAB_Widgets;
using openhab.winrt.winhab.openHAB_definition.openHAB_Widgets.openHAB_LinkableWidget;
using openhab.winrt.winhab.openHAB_definition.openHAB_Widgets.openHAB_NonLinkableWidget;

namespace openhab.winrt.winhab.TemplateSelectoren
{
    class LeftNavItemTemplateSelector : DataTemplateSelector
    {
        #region linkable Widgets
        public DataTemplate frameWidget { get; set; }
        public DataTemplate groupWidget { get; set; }
        public DataTemplate imageWidget { get; set; }
        public DataTemplate leftNavTextWidget { get; set; }
        #endregion


        #region unsupported Widget
        public DataTemplate unsupportedWidget { get; set; }
        #endregion

        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item,DependencyObject container)
        {
            if (item.GetType() == typeof(FrameWidget))
                return frameWidget;
            if (item.GetType() == typeof(GroupWidget))
                return groupWidget;
            if (item.GetType() == typeof(TextWidget))
                return leftNavTextWidget;
            return unsupportedWidget;
        }
    }
}
