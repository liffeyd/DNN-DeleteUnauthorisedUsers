// Schedule full class name:  DNN.DeleteUnauthorisedUsers.DeleteUnauthorisedUsers

namespace DNN.DeleteUnauthorisedUsers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;

    using DNN.DeleteUnauthorisedUsers.App_LocalResources;

    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Log.EventLog;
    using DotNetNuke.Services.Scheduling;

    public class DeleteUnauthorisedUsers : SchedulerClient
    {
        private const string STR_DeleteUsers = "DeleteUsers";
        private const string STR_RemoveDeletedUsers = "RemoveDeletedUsers";
        private const string STR_DefaultPortalAlias = "DefaultPortalAlias";
        private const string STR_UriString = "http://{0}/DesktopModules/DNN-DeleteUnauthorisedUsers/dwsDeleteUsersServices/API/DeleteUsers/";

        public DeleteUnauthorisedUsers(ScheduleHistoryItem oItem)
        {
            this.ScheduleHistoryItem = oItem;
        }

        public override void DoWork()
        {
                       
            try
            {
                //Perform required items for logging
                this.Progressing();

                
                // We need a portal ID in order to build a uri to access the service.
                // We could assume that there will always be a portal 0 but this may not always be true.
                // The only solution I can think of is to get a list of portals and take the first ID.

                //var portals = PortalController.Instance.GetPortals();
                var portals = PortalController.Instance.GetPortals().Cast<PortalInfo>().OrderBy(o => o.PortalID).ToList();

                var portalId = (from PortalInfo portalInfo in portals select portalInfo.PortalID).FirstOrDefault();

                // Now we can use the PortalID to get the default portal alias 
                var alias = PortalController.GetPortalSetting(STR_DefaultPortalAlias, portalId, "");

                // and hence build a valid uri to access the service
                var uri = String.Format(STR_UriString, alias);


                // Call the service to delete unauthorised users
                var responseString = CallDeleteUserService(uri + STR_DeleteUsers);

                this.ScheduleHistoryItem.AddLogNote(String.Format(Strings.PortalsProcessed, responseString));


                // Call the service to delete unauthorised users
                responseString = CallDeleteUserService(uri + STR_RemoveDeletedUsers);

                this.ScheduleHistoryItem.AddLogNote(String.Format(Strings.PortalsProcessed, responseString));

                this.ScheduleHistoryItem.Succeeded = true;
                
            }

            catch (Exception ex)
            {
                this.ScheduleHistoryItem.Succeeded = false;
                InsertLogEntry(Strings.Exception + ex.Message);
                this.Errored(ref ex);
                Exceptions.LogException(ex);
            }
        }

     

        private static void InsertLogEntry(string note)
        {
            var eventLog = new EventLogController();
            var logInfo = new LogInfo
            {
                LogUserID = -1,
                LogPortalID = -1,
                LogTypeKey = EventLogController.EventLogType.ADMIN_ALERT.ToString()
            };
            logInfo.AddProperty("KeyWord=", Strings.SpamUsersDelete);
            logInfo.AddProperty("KeyWordLike=", note);

            eventLog.AddLog(logInfo);
        }
    
        

        private static string CallDeleteUserService(string uri)
        {
            var retVal = string.Empty;
            try
            {
                var req = WebRequest.Create(uri);
                req.Proxy = new WebProxy(uri, true); //true means no proxy

                var resp = req.GetResponse();
                

                using (var stream = resp.GetResponseStream())
                {
                    if (stream != null)
                    {
                        var reader = new StreamReader(stream);
                        retVal = reader.ReadToEnd();
                        retVal = retVal.Remove(0, 1);
                        retVal = retVal.Remove(retVal.Length-1, 1);

                    }
                }

            }
            catch (Exception ex)
            {
                InsertLogEntry(ex.Message);
                
            }
            return retVal;
        }

    }
}
