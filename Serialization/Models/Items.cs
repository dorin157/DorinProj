using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;

namespace Models
{
    public class Items
    {
        public List<Item> CurItems { set; get; }
        public Items()
        {
            CurItems = new List<Item>();
        }

        static public Items GetItemsFromString(string s)
        {
            Items items = new Items();
            List<string> stringItems = new List<string>();
            stringItems = s.GetProperties();
            foreach (string item in stringItems)
            {
                items.CurItems.Add(Item.GetItemFromString(item));
            }
            return items;
        }

        public override string ToString()
        {
            string sItems = "";
            foreach (Item item in CurItems)
            {
                sItems = String.Format("{0},/{1}/,", sItems, item);
            }
            return sItems;
        }
    }
}
