using Kerry.K3.DB;
using Kerry.K3.DB.Utility;
using Kerry.K3.JobUpdate.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.K3.JobUpdate.Scheduler.Job_Methor
{
    public class JobUpdate
    {
        List<string> status = new List<string>()
                {
                    "F","P"
                };
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public int UpdateLinkJob()
        {
            
            //K3DEVEntities  k3 uat
            //K3Prod k3 prod
            using (K3Prod k3dev = new K3Prod())
            {
                List<ACTION_JOB> actionJobModels = (from aj in k3dev.ACTION_JOB
                                                   where status.Contains(aj.STATUS) && aj.ACTION.Equals("L")&&aj.CONSOLLOT_UNID !=null
                                                   select aj).ToList();


                log.Info("Start update Link job :");
                foreach (ACTION_JOB actionJobModel in actionJobModels)
                {
                    DbHelper db = new DbHelper();
                    //db.GetSqlStringCommond = ""; kewillfwd.
                    string sql = string.Format(@"update kewillfwd.job set CONSOLLOT_UNID ={0},UPDATEDATE=sysdate where unid={1} and surefno like 'K35%' ", actionJobModel.CONSOLLOT_UNID, actionJobModel.JOB_UNID);
                    try
                    {
                        DbCommand dc = db.GetSqlStringCommond(sql);
                        int result = db.ExecuteNonQuery(dc);
                    
                    #region ADO
                    //var job = (from j in k3dev.JOB
                    //           where j.UNID == actionJobModel.JOB_UNID
                    //           && j.SUREFNO.Equals("K35")
                    //           select j).FirstOrDefault();
                    //if (job != null)
                    //{
                    //    job.CONSOLLOT_UNID = (long?)actionJobModel.CONSOLLOT_UNID;
                    //    k3dev.Entry<JOB>(job).State = System.Data.EntityState.Modified;

                    //    var result=k3dev.SaveChanges();
                        if(result>0)
                        {
                            actionJobModel.STATUS = "S";
                            k3dev.Entry<ACTION_JOB>(actionJobModel).State = System.Data.EntityState.Modified;
                            log.Info("update Link job Job_unid [success] :" + actionJobModel.JOB_UNID);

                        }else
                        {

                            //kewillfwd.
                            string sqlfail = string.Format(@"select count(1) from kewillfwd.job where unid={0} and surefno like 'K35%' ", actionJobModel.JOB_UNID);
                            DbCommand dc1 = db.GetSqlStringCommond(sqlfail);
                            var result1 = db.ExecuteScalar(dc1);
                            if (Convert.ToInt32(result1) == 0)
                            {
                                actionJobModel.STATUS = "N";
                                k3dev.Entry<ACTION_JOB>(actionJobModel).State = System.Data.EntityState.Modified;
                                log.Info("update Link job Fail Job_unid :" + actionJobModel.JOB_UNID);
                            }else
                            {
                                actionJobModel.STATUS = "F";
                                k3dev.Entry<ACTION_JOB>(actionJobModel).State = System.Data.EntityState.Modified;
                                log.Info("update Link job is null Job_unid :" + actionJobModel.JOB_UNID);
                            }

                        }

                        k3dev.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        log.Info("Update Command error :" + ex.Message);
                    }
                    //}
                    #endregion
                }
                
                return actionJobModels.Count;
            }
            
        }

        public int UpdateUnLinkJob()
        {
            //K3DEVEntities  k3 uat
            //K3Prod k3 prod
            using (K3Prod k3dev = new K3Prod())
            {
                List<ACTION_JOB> actionJobModels = (from aj in k3dev.ACTION_JOB
                                                    where status.Contains(aj.STATUS) && aj.ACTION.Equals("U")
                                                    select aj).ToList();
                log.Info("Start update UnLink job :");
                foreach (ACTION_JOB actionJobModel in actionJobModels)
                {                    
                    DbHelper db = new DbHelper();
                    //db.GetSqlStringCommond = ""; kewillfwd.
                    string sql = string.Format(@"update kewillfwd.job set CONSOLLOT_UNID =null where unid={0} and surefno like 'K35%' ", actionJobModel.JOB_UNID);

                    DbCommand dc = db.GetSqlStringCommond(sql);
                    int result = db.ExecuteNonQuery(dc);

                    #region ADO

                    //JOB job = k3dev.JOB.Find(actionJobModel.JOB_UNID);
                    //var job = (from j in k3dev.JOB
                    //           where j.UNID == actionJobModel.JOB_UNID
                    //           && j.SUREFNO.Equals("K35")
                    //           select  j ).FirstOrDefault();
                    //if (job != null)
                    //{
                    //    job.CONSOLLOT_UNID = null;
                    //    k3dev.Entry<JOB>(job).State = System.Data.EntityState.Modified;

                    //    var result = k3dev.SaveChanges();
                        if (result > 0)
                        {
                            actionJobModel.STATUS = "S";
                            k3dev.Entry<ACTION_JOB>(actionJobModel).State = System.Data.EntityState.Modified;

                        }
                        else
                        {
                            //kewillfwd.
                            string sqlfail = string.Format(@"select count(*) from kewillfwd.job where unid={0} and surefno like 'K35%' ", actionJobModel.JOB_UNID);
                            DbCommand dc1 = db.GetSqlStringCommond(sqlfail);
                            var result1 = db.ExecuteScalar(dc1);
                            if (Convert.ToInt32(result1) == 0)
                            {
                                actionJobModel.STATUS = "N";
                                k3dev.Entry<ACTION_JOB>(actionJobModel).State = System.Data.EntityState.Modified;
                                log.Info("update UnLink job Fail Job_unid :" + actionJobModel.JOB_UNID);
                            }
                            else
                            {
                                actionJobModel.STATUS = "F";
                                k3dev.Entry<ACTION_JOB>(actionJobModel).State = System.Data.EntityState.Modified;
                                log.Info("update UnLink job is null Job_unid :" + actionJobModel.JOB_UNID);
                            }
                        }

                        k3dev.SaveChanges();

                    //}
                    #endregion

                }
                return actionJobModels.Count;
            }

        }
    }
}
