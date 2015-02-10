using System;
using System.Collections.Generic;

using DotNetNuke.Data;
using DotNetNuke.Services.Scheduling;
using DotNetNuke.Services.Log.EventLog;

namespace DNN.DeleteUnauthorisedUsers
{
    public class DeleteUnauthorisedUsers : SchedulerClient
    {
        public DeleteUnauthorisedUsers(ScheduleHistoryItem oItem)
            : base()
        {
            this.ScheduleHistoryItem = oItem;
        }

        public override void DoWork()
        {
                       
            try
            {
                //Perform required items for logging
                this.Progressing();


                int spammersCaught = NukeThem();
                                       
                
                this.ScheduleHistoryItem.AddLogNote(" - Deleted: " + spammersCaught.ToString());

                this.ScheduleHistoryItem.Succeeded = true;
            }

            catch (Exception ex)
            {
                this.ScheduleHistoryItem.Succeeded = false;
                InsertLogEntry("Exception: " + ex.ToString());
                this.Errored(ref ex);
                DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            }
        }

        private static int NukeThem()
        {
           //return Convert.ToInt32(DataProvider.Instance().ExecuteScalar("dws_DeleteSpamUsers", portalID));
            int retVal;
            using (IDataContext DBLoggingProvider = DataContext.Instance())
            {
                retVal = DBLoggingProvider.ExecuteScalar<int>(System.Data.CommandType.StoredProcedure, "dnn_DeleteUnauthorisedUsers");
            }
           
            return retVal;

        }

        private static void InsertLogEntry(string note)
        {
            EventLogController eventLog = new EventLogController();
            LogInfo logInfo = new LogInfo() 
            { 
                LogUserID = -1, 
                LogPortalID = -1, 
                LogTypeKey = EventLogController.EventLogType.ADMIN_ALERT.ToString() 
            };
            logInfo.AddProperty("KeyWord=", "SpamUserDelete");
            logInfo.AddProperty("KeyWordLike=", note);
            eventLog.AddLog(logInfo);
        }
    }
}
