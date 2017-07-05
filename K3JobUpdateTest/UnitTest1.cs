using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kerry.K3.JobUpdate.Scheduler.Job_Methor;
using System.Net.Mail;
using System.Configuration;
using Kerry.K3.JobUpdate.Scheduler.Models;
using System.Collections.Generic;

namespace K3JobUpdateTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            GetJob job = new GetJob();
            var result=job.GetK3JobLinkHouse();
        }

        [TestMethod]
        public void insetLinkActionJob()
        {
            GetJob job = new GetJob();
            var jobmodel = job.GetK3JobLinkHouse();
            foreach(var a in jobmodel)
            {
                if(a.JobUnid==3064612)
                {
                    var s='s';
                }
            }
            InsertActionJob injob = new InsertActionJob();

            var result = injob.InLinkJob(jobmodel, "L");
            JobUpdate jobupdate = new JobUpdate();


            var result1 = jobupdate.UpdateLinkJob();


        }

        [TestMethod]
        public void updateLinkjob()
        {
            var updateFlag = ConfigurationManager.AppSettings["updateFlag"];
            if (updateFlag == "Y")
            {
                var s = "s";
            }else
            {
                var s = "s";
            }
            JobUpdate job = new JobUpdate();


            var result = job.UpdateLinkJob();
        }

        [TestMethod]
        public void TestMethod3()
        {
           //log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
           //log.Info("test");
            JobUpdate jobu = new JobUpdate();
            var result = jobu.UpdateLinkJob();
        }

        [TestMethod]
        public void UnLinkJobUpdate()
        {
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("test");
            JobUpdate jobu = new JobUpdate();
            var result = jobu.UpdateUnLinkJob();
        }

        [TestMethod]
        public void TestSendEmail()
        {

            using (SmtpClient mailClient = new SmtpClient())
            {
                mailClient.Host = (ConfigurationManager.AppSettings["EmailServerHost"] ?? "192.168.90.250") as string;

                MailMessage mailMessage = null;
                mailClient.Port = Int32.Parse((ConfigurationManager.AppSettings["EmailServerPort"] ?? "25") as string);
                string mailfrom = ConfigurationManager.AppSettings["EmailFromAddress"].ToString();
                string mailto = ConfigurationManager.AppSettings["EmailToAddress"].ToString();
                string subject = "test";
                string content = "test content";
                mailMessage = new MailMessage(mailfrom, mailto, subject, content);

                mailMessage.IsBodyHtml = true;

                mailClient.Send(mailMessage);
                mailMessage.Dispose();
            }
        }

        [TestMethod]
        public void insertActionJob()
        {
            GetJob job = new GetJob();
            var jobLinkmodel = job.GetK3JobLinkHouse();
            InsertActionJob injob = new InsertActionJob();

            var resultLink = injob.InLinkJob(jobLinkmodel, "L");
            //log.Info("  LinkJob Insert ACTION_JOB count :" + resultLink);

            var jobUnLinkModel = job.GetK3JobUnLinkHouse();
            var resultUnLink = injob.InLinkJob(jobUnLinkModel, "U");
            ///log.Info("  UnLinkJob Insert ACTION_JOB count :" + resultUnLink);
        }
        [TestMethod]
        public void JobUpdated()
        {
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("prod update job");
            DateTime time = System.DateTime.Now;
            GetJob job = new GetJob();
            JobUpdate jobu = new JobUpdate();
            var result = jobu.UpdateLinkJob();
            log.Info("LinkJob count : "+result);
            var resultunlink = jobu.UpdateUnLinkJob();
            log.Info("UnLinkJob count : " + resultunlink);
        }

        [TestMethod]
        public void GetJobLinkVBO()
        {
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("prod update job");
            GetJob job = new GetJob();
            var jobLink = job.GetK3JobLinkVbo();
            List<JobModel> model = new List<JobModel>();
           // model.Add(new JobModel() {JobUnid=1 });
            InsertActionJob jobu = new InsertActionJob();
            jobu.InsertJobLinkVBO(jobLink);

        }



    }
}
