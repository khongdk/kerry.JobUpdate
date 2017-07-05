using Kerry.K3.DB;
using Kerry.K3.DB.Utility;
using Kerry.K3.JobUpdate.Scheduler.Models;
using Kerry.K3.JobUpdate.Scheduler.Transform;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.K3.JobUpdate.Scheduler.Job_Methor
{
    public class InsertActionJob
    {
         private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public int InLinkJob(List<JobModel> jobModels,string flag)
        {
            ActionJobTF tf = new ActionJobTF();
            List<ACTION_JOB> entities = tf.ToEntity(jobModels, flag);
            //K3DEVEntities  k3 uat
            //K3Prod k3 prod
            using (K3Prod DB_K3dev = new K3Prod())
            {
                DB_K3dev.Configuration.AutoDetectChangesEnabled = false;

                
                foreach (var e in entities)
                {
                    var count = (decimal)0;
                    if(DB_K3dev.ACTION_JOB.Count()==0)
                    {
                        count = 0;
                    }else
                    {
                        count = DB_K3dev.ACTION_JOB.Max(x => x.UNID);
                    }
                    
                    int querycount = DB_K3dev.ACTION_JOB.Where(x => x.JOB_UNID == e.JOB_UNID && x.CONSOLNO.Equals(e.CONSOLNO)).Count();
                    if (e.CONSOLLOT_UNID != null && querycount == 0)
                    {
                        e.UNID = (decimal)(count + 1);
                        try
                        {
                            DB_K3dev.Entry<ACTION_JOB>(e).State = System.Data.EntityState.Added;

                            DB_K3dev.SaveChanges();
                            log.Info("job_unid [" + e.JOB_UNID + "] insert action_job success ! ");
                        }
                        catch (Exception ex)
                        {
                            log.Info("  Job insert action_job error :" + ex.Message+"job_unid : "+e.JOB_UNID);
                        }
                    }
                }
                return entities.Count;
            }

        }


        #region insert table job_link

        public void InsertJobLinkVBO(List<JobModel> jobModels)
        {           
            ActionJobTF tf = new ActionJobTF();
            List<JOBLINK> entities = tf.ToEntityJobLink(jobModels);
            #region sql insert into table joblink
            log.Info("------Start insert into table joblink data :------");
                #region uat 
            //using (K3DEVEntities DB_K3 = new K3DEVEntities())
            //{
            //    DB_K3.Configuration.AutoDetectChangesEnabled = false;


            //    foreach (var e in entities)
            //    {
            //        var count = DB_K3.JOBLINK.Where(x => x.JOB_UNID == e.JOB_UNID).Count();
            //        if (count > 0)
            //        {
            //            log.Info("job_unid [" + e.JOB_UNID + "] already insert joblink !");
            //        }
            //        else
            //        {
            //            DB_K3.Entry<JOBLINK>(e).State = System.Data.EntityState.Added;

            //            DB_K3.SaveChanges();
            //            log.Info("job_unid [" + e.JOB_UNID + "] insert joblink success !");
            //        }

            //    }
            #endregion
                #region prod
            using (K3Prod DB_K3 = new K3Prod())
            {
                foreach (JobModel jobLinkModel in jobModels)
                {

                    var usql = "select count(*) from kewillfwd.joblink where job_unid= " + jobLinkModel.JobUnid;
                    int query = DB_K3.Database.SqlQuery<int>(usql).FirstOrDefault();
                    if (query < 1)
                    {
                        DbHelper db = new DbHelper();
                        //db.GetSqlStringCommond = ""; kewillfwd.
                        string sql = string.Format(@"insert into kewillfwd.joblink values ({0},'B',{1}) ", jobLinkModel.JobUnid, jobLinkModel.JobUnid);
                        try
                        {
                            DbCommand dc = db.GetSqlStringCommond(sql);
                            int result = db.ExecuteNonQuery(dc);
                            if (result > 0)
                            {
                                log.Info("job_unid [" + jobLinkModel.JobUnid + "] insert joblink success !");
                            }else
                            {
                                log.Info("job_unid [" + jobLinkModel.JobUnid + "] insert joblink error !");
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Info("Add Command error :" + ex.Message);
                        }
                    }else
                    {
                        log.Info("job_unid [" + jobLinkModel.JobUnid + "] already insert joblink !");
                    }
                }
            }
                
                #endregion
            log.Info("------End insert into table joblink data :------");
            
            #endregion


        }
        #endregion
    }
}
