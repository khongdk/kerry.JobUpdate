using Kerry.K3.DB;
using Kerry.K3.JobUpdate.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.K3.JobUpdate.Scheduler.Job_Methor
{
    public class GetJob
    {
        public List<JobModel> GetK3JobLinkHouse()
        {
            //K3DEVEntities  k3 uat
            //K3ProdDrEntities k3 prod
            using (K3ProdDrEntities DB_K3 = new K3ProdDrEntities())
            {

                var usql = "select j.unid as JobUnid,j.shpno,j.consolno,j.createby,j.createdate,j.updateby,j.updatedate,j.ownerid, " +
                    "(select unid from job m WHERE m.shpno=j.consolno AND m.ownerid=j.ownerid AND m.biztype=j.biztype AND m.voidby is null AND m.voiddate is null and m.shptype='M' and m.surefno like 'K35%' ) as consolLotUnid,j.biztype,j.shpType " +
                "from job j " +
                "where j.ownerid LIKE  ('CN_CN%') " +
                    //"AND j.unid=732780 ";
                "AND j.CREATEDATE>TO_DATE('20160601','YYYYMMDD') " +
                "AND j.SHPTYPE ='H' " +
                "AND j.BIZTYPE IN ('AI','AE') " +
                "AND j.CONSOLNO IS NOT NULL " +
                "AND j.CONSOLLOT_UNID IS NULL " +
                "AND j.VOIDBY IS NULL " +
                "AND j.VOIDDATE IS NULL  " +
                "AND to_char(updatedate,'yyyyMMdd')>'20170625'  ";
                // "AND NVL(updatedate,createdate)>=sysdate-(1/24) ";

                List < JobModel > query = DB_K3.Database.SqlQuery<JobModel>(usql).ToList();
                return query;
            }
        }

        public List<JobModel> GetK3JobUnLinkHouse()
        {
            //K3DEVEntities  k3 uat
            //K3ProdDrEntities k3 prod
            using (K3ProdDrEntities DB_K3 = new K3ProdDrEntities())
            {

                var usql = "select j.unid as JobUnid,j.shpno,j.consolno,j.createby,j.createdate,j.updateby,j.updatedate,j.consollot_unid as consolLotUnid,j.biztype,j.ownerid from job j "+
                                "where  j.ownerid LIKE  ('CNECN%') "+
                                "AND j.CREATEDATE>TO_DATE('20160601','YYYYMMDD') "+
                                "AND J.SHPTYPE ='H' "+
                                "AND J.BIZTYPE IN ('AI','AE') "+
                                "AND J.CONSOLNO IS  NULL "+
                                "AND J.CONSOLLOT_UNID IS NOT NULL "+
                                "AND j.VOIDBY IS NULL "+
                                "AND j.VOIDDATE IS NULL " +
                                "AND to_char(updatedate,'yyyyMMdd')>'20170625'  ";
                               // "AND NVL(updatedate,createdate)>=sysdate-(1/24) ";

                List<JobModel> query = DB_K3.Database.SqlQuery<JobModel>(usql).ToList();
                return query;
            }
        }

        public List<JobModel> GetK3JobLinkVbo()
        {
            //K3DEVEntities  k3 uat
            //K3ProdDrEntities k3 prod
            using (K3ProdDrEntities DB_K3 = new K3ProdDrEntities())
            {
                //kewillfwd.
                var usql = "select j.unid as JobUnid,j.shpno,j.consolno,j.createby,j.createdate,j.updateby,j.updatedate,j.ownerid, " +
                    "1 as consolLotUnid,j.biztype,j.shpType " +
                "from job j " +
                "where j.ownerid LIKE  ('CNECN%') " +
                    //"AND j.unid=732780 ";
                "AND j.CREATEDATE>TO_DATE('20160601','YYYYMMDD') " +
                "AND j.SUREFNO ='K35VBO' " +
                "AND j.shptype='H' " +
                "AND j.VOIDBY IS NULL " +
                "AND j.VOIDDATE IS NULL " +
               // "AND to_char(updatedate,'yyyyMMdd')>'20170625'  ";
               "AND NVL(updatedate,createdate)>=sysdate-(4/24) ";

                List<JobModel> query = DB_K3.Database.SqlQuery<JobModel>(usql).ToList();
                return query;
            }
        }
    }
}
