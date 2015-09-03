using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;

namespace Models
{
    public class Job
    {
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }

        public Job() { }

        static public Job GetJobFromString(string s)
        {
            Job job = new Job();
            List<string> properties = s.GetProperties();
            job.Time = Convert.ToDateTime(properties[0]);
            job.Description = properties[1];
            job.Phone = properties[2];
            job.UserId = Convert.ToInt32(properties[3]);
            return job;
        }

        public override string ToString()
        {
            string sJob = "";
            sJob = String.Format(",/{0}/,,/{1}/,,/{2}/,,/{3}/,",
                Time, Description, Phone, UserId);
            return sJob;
        }
    }
}
