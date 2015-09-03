using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;

namespace Models
{
    public class Item
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Position> PositionHistory { get; set; }
        public List<Job> JobHistory { get; set; }

        public Item()
        {
            PositionHistory = new List<Position>();
            JobHistory = new List<Job>();
        }

        static public Item GetItemFromString(string s)
        {
            Item item = new Item();
            List<string> properties = s.GetProperties();
            item.Id = Convert.ToInt32(properties[0]);
            item.FirstName = properties[1];
            item.LastName = properties[2];

            List<string> positions = properties[3].GetProperties();
            List<string> jobs = properties[4].GetProperties();

            foreach (string position in positions)
            {
                item.PositionHistory.Add(Position.GetPositionFromString(position));
            }

            foreach (string jb in jobs)
            {
                item.JobHistory.Add(Job.GetJobFromString(jb));
            }
            return item;
        }

        public override string ToString()
        {
            string sItem = "";
            string sPosHistory = "";
            foreach (Position position in PositionHistory)
            {
                sPosHistory = String.Format("{0},/{1}/,", sPosHistory, position);
            }
            string sJobHistory = "";
            foreach (Job job in JobHistory)
            {
                sJobHistory = String.Format("{0},/{1}/,", sJobHistory, job);
            }
            sItem = String.Format(",/{0}/,,/{1}/,,/{2}/,,/{3}/,,/{4}/,",
                Id, FirstName, LastName, sPosHistory, sJobHistory);
            return sItem;
        }
    }
}
