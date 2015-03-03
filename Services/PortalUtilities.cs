 using System.Collections.Generic;
 using System.Linq;
 using System.Web.Http;

 using DotNetNuke.Entities.Portals;
    
 namespace DNN.DeleteUnauthorisedUsers.Services
{
    public class PortalUtilities
    {

        [AllowAnonymous]
        [HttpGet]
        public static List<PortalInfo> GetPortals()
        {
            return PortalController.Instance.GetPortals().Cast<PortalInfo>().ToList();
        }


        public static List<PortalInfo> GetPortalsToDeleteUsers()
        {
            var portals = PortalController.Instance.GetPortals();
            var lstPortals = new List<PortalInfo>();
            foreach (PortalInfo portal in portals)
            {
                // get setting for portal
                var autoDeleteUsers = PortalController.GetPortalSettingAsBoolean("AutoDeleteUsers", portal.PortalID, false);

                // if AutoDelete = true
                if (autoDeleteUsers)
                {
                    lstPortals.Add(portal);
                }
            }
          
            return lstPortals;
        }

        public static List<PortalInfo> GetPortalsToRemoveUsers()
        {
            var portals = PortalController.Instance.GetPortals();
            var lstPortals = new List<PortalInfo>();
            foreach (PortalInfo portal in portals)
            {
                // get setting for portal
                var autoRemoveUsers = PortalController.GetPortalSettingAsBoolean("AutoPurgeUsers", portal.PortalID, false);

                // if AutoDelete = true
                if (autoRemoveUsers)
                {
                    lstPortals.Add(portal);
                }
            }

            return lstPortals;
        }


        public static void AddPortalSetting(int portalId, string settingName, string settingValue)
        {
            PortalController.UpdatePortalSetting(portalId,  settingName,  settingValue);
        }
    }
}
