using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.K3.JobUpdate.Scheduler.Models
{
    public class JobModel
    {
       // public long Unid { get; set; }
        public long JobUnid { get; set; }
        public string ShpNo { get; set; }
        public string ConsolNo { get; set; }
        public Nullable<long> consolLotunid { get; set; }
        public string BizType { get; set; }
        public string ShpType { get; set; }
        public string OwnerId { get; set; }
        //public string Action { get; set; }
        //public string Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime Createdate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? Updatedate { get; set; }
    }
}
