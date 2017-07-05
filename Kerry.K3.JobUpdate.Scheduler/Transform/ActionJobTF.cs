using Kerry.K3.DB;
using Kerry.K3.JobUpdate.Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerry.K3.JobUpdate.Scheduler.Transform
{
    public class ActionJobTF
    {
        public List<ACTION_JOB> ToEntity(List<JobModel> jobModels,string flag)
        {
            var entities = new List<ACTION_JOB>();
            foreach (var model in jobModels)
            {
                entities.Add(new ACTION_JOB
                {
                    JOB_UNID=model.JobUnid,
                    SHPNO=model.ShpNo,
                    CONSOLLOT_UNID=model.consolLotunid,
                    CONSOLNO=model.ConsolNo,
                    BIZTYPE=model.BizType,
                    SHPTYPE=model.ShpType,
                    OWNERID=model.OwnerId,
                    CREATEBY=model.CreateBy,
                    CREATEDATE=model.Createdate,
                    UPDATEBY=model.UpdateBy,
                    UPDATEDATE=model.Updatedate,
                    ACTION=flag,
                    STATUS=model.consolLotunid==null?"N":"P"
                });
            }

            return entities;
        }

        public List<ACTION_JOB> ToEntity(List<JobModel> jobModels)
        {
            var entities = new List<ACTION_JOB>();
            foreach (var model in jobModels)
            {
                entities.Add(new ACTION_JOB
                {
                    JOB_UNID = model.JobUnid,
                    SHPNO = model.ShpNo,
                    CONSOLLOT_UNID = model.consolLotunid,
                    CONSOLNO = model.ConsolNo,
                    BIZTYPE = model.BizType,
                    SHPTYPE = model.ShpType,
                    OWNERID = model.OwnerId,
                    CREATEBY = model.CreateBy,
                    CREATEDATE = model.Createdate,
                    UPDATEBY = model.UpdateBy,
                    UPDATEDATE = model.Updatedate,
                    STATUS = model.consolLotunid == null ? "N" : "P"
                });
            }

            return entities;
        }

        public List<JOBLINK> ToEntityJobLink(List<JobModel> jobModels)
        {
            var entities = new List<JOBLINK>();
            foreach (var model in jobModels)
            {
                entities.Add(new JOBLINK
                {
                    JOB_UNID = model.JobUnid,
                    SOURCE_UNID = model.JobUnid,
                    TYPE="B"                    
                });
            }

            return entities;
        }


    }
}
