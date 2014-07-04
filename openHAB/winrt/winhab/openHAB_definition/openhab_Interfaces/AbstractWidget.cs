using JSON_Parser.Parser;
using openhab.winrt.winhab.openHAB_definition.openhab_Interfaces.nonlinkable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace openhab.winrt.winhab.openHAB_definition.openHAB_Widgets
{
    public abstract class AbstractWidget:IAbstractWidget
    {
        private String _widgetId { get; set; }

        public String widgetId
        {
            get
            {
                return _widgetId;
            }
            set
            {
                _widgetId = value;
                NotifyPropertyChanged("widgetId");
            }
        }

        private String _type { get; set; }

        public String type { get { return _type; } set { _type = value; } }

        private String _label { get; set; }

        public String label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                NotifyPropertyChanged("label");
            }
        }

        private String _icon { get; set; }
        public Boolean _once = false;

        public virtual String icon
        {
            get { return _icon; }
            set
            {
                if (_once != true)
                {
                    _icon = value;
                    _once = true;
                }
                else
                    _icon = value;
                NotifyPropertyChanged("icon");
            }
        }

        public LinkedList<Widget> widget { get; set; }

        public LinkedPage linkedPage { get; set; }

        public Item item { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool switchState
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
