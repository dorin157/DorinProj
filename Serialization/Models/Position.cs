using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;

namespace Models
{
    public class Position
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public int Accuracy { get; set; }
        public DateTime Date { get; set; }

        public Position()
        {

        }

        static public Position GetPositionFromString(string s)
        {
            Position position = new Position();
            List<string> properties = s.GetProperties();
            position.Latitude = Convert.ToInt64(properties[0]);
            position.Longitude = Convert.ToInt64(properties[1]);
            position.Accuracy = Convert.ToInt32(properties[2]);
            position.Date = Convert.ToDateTime(properties[3]);
            return position;
        }

        public override string ToString()
        {
            string sPosition = "";
            sPosition = String.Format(",/{0}/,,/{1}/,,/{2}/,,/{3}/,",
                Latitude, Longitude, Accuracy, Date);
            return sPosition;
        }
    }
}
