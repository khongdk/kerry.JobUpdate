using Kerry.K3.JobUpdate.Scheduler.Job_Methor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Kerry.K3.JobUpdate.Scheduler
{
    public partial class JobUpdateScheduler : ServiceBase
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        Timer timer = new Timer();
        public JobUpdateScheduler()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //get time interval from app.config
            int timeInterval = Convert.ToInt32(ConfigurationManager.AppSettings["timerInterval"]);
            this.timer.Interval = timeInterval;
            this.timer.Enabled = true;
            this.timer.Start();

            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(SchedulerTimerEvent);

        }

        //create Timer Event
        private void SchedulerTimerEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            this.timer.Stop();
            log.Info("  [Schdeuler] [SchedulerTimerEvent] : Start up Scheduler Task");
            #region
            try
            {
                log.Info("   JOBLINK starting :");
                GetJob job = new GetJob();
                var jobLink = job.GetK3JobLinkVbo();
                InsertActionJob jobu = new InsertActionJob();
                jobu.InsertJobLinkVBO(jobLink);
            }
            catch (Exception ex)
            {
                log.Info("  Job insert JOBLINK error :" + ex.Message);
                SendEmail("insert JOBLINK Error", ex.Message);
            }
            #endregion
            #region insert action_job
            try
            {
                log.Info("   unpdate consotlot_unid starting :");
                GetJob job = new GetJob();
                var jobLinkmodel = job.GetK3JobLinkHouse();
                InsertActionJob injob = new InsertActionJob();

                var resultLink = injob.InLinkJob(jobLinkmodel, "L");
                log.Info("  LinkJob Insert ACTION_JOB count :" + resultLink);

                var jobUnLinkModel = job.GetK3JobUnLinkHouse();
                var resultUnLink = injob.InLinkJob(jobUnLinkModel, "U");
                log.Info("  UnLinkJob Insert ACTION_JOB count :" + resultUnLink);
            }
            catch(Exception ex)
            {
                log.Info("  Job insert ACTION_JOB error :" + ex.Message);
                SendEmail("Get Job Error", ex.Message);
            }
            #endregion
            var updateFlag = ConfigurationManager.AppSettings["updateFlag"];
            if (updateFlag == "Y")
            {
                #region update job
                try
                {
                    log.Info("  start insert job :");
                    Kerry.K3.JobUpdate.Scheduler.Job_Methor.JobUpdate jobupdateIF = new Kerry.K3.JobUpdate.Scheduler.Job_Methor.JobUpdate();
                    var linkjob = jobupdateIF.UpdateLinkJob();  
                    log.Info("  LinkJob update job count :" + linkjob);

                    var unlinkjob = jobupdateIF.UpdateUnLinkJob();
                    log.Info("  UnLinkJob update job count :" + unlinkjob);

                }
                catch (Exception ex)
                {
                    log.Info("  Job update error :" + ex.Message);
                    SendEmail("Update Job Error", ex.Message);
                }
                #endregion
            }else
            {
                log.Info("The update job status is N so don't update job !!");
            }
            //start counting
            this.timer.Start();
        }

        private void SendEmail(string subject, string content)
        {
            using (SmtpClient mailClient = new SmtpClient())
            {
                mailClient.Host = (ConfigurationManager.AppSettings["EmailServerHost"] ?? "192.168.83.231") as string;

                MailMessage mailMessage = null;
                mailClient.Port = Int32.Parse((ConfigurationManager.AppSettings["EmailServerPort"] ?? "25") as string);
                string mailfrom = ConfigurationManager.AppSettings["EmailFromAddress"].ToString();
                string mailto = ConfigurationManager.AppSettings["EmailToAddress"].ToString();
                
                content = content.Replace("\r\n", "<br/>");
                mailMessage = new MailMessage(mailfrom, mailto, subject, content);

                mailMessage.IsBodyHtml = true;

                mailClient.Send(mailMessage);
                mailMessage.Dispose();
            }
        }
        protected override void OnStop()
        {
        }
    }
}
