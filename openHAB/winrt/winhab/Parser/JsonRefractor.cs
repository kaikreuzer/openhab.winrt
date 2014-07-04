using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openhab.winrt.winhab.Parser
{
    class JsonRefractor
    {
        private String _json { get; set; }
        private String _item { get; set; }
        public JsonRefractor(String json)
        {
            _json = json;
        }
        /// <summary>
        /// Returns the Json String with an Array, marked as List
        /// </summary>
        /// <param name="listToBeFixed"></param>
        /// <returns>the new json-String</returns>
        public String refractor(String listToBeFixed)
        {
            this._item = listToBeFixed;
            int found = 0;
            Queue<Int32> stellen = new Queue<Int32>();
            while (found != -1)
            {
                found = _json.IndexOf(_item, found);
                if (found > -1)
                {
                    //stellen.Enqueue(found);
                    //found = found + 1;
                    _refractor(found);
                    found = found + 1;
                }
            }
            return _json;
        }

        /// <summary>
        /// Refractors each found
        /// </summary>
        /// <param name="Found as Integer"></param>
        private void _refractor(int found)
        {
            int openBrackets = 0;
            int closedBrackets = 0;
            int first = found;
            int counter = 0;
            _json = _json.Insert(first + _item.Length - 1, "[");
            counter = first;
            Boolean next = false;
            while (!next)
            {
                // while (!next)
                //{
                if (_json[counter].Equals('{'))
                {
                    openBrackets++;
                }
                if (_json[counter].Equals('}'))
                {
                    closedBrackets++;
                }
                if (openBrackets != 0 && closedBrackets != 0 && (openBrackets == closedBrackets))
                {
                    _json = _json.Insert(counter + 1, "]");
                    next = true;
                }
                counter++;
                //}

            } next = false;
        }
    }
}
