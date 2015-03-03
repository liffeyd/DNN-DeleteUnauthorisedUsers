namespace DNN.DeleteUnauthorisedUsers.Services
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;

    using DNN.DeleteUnauthorisedUsers.App_LocalResources;

    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Web.Api;

    public class DeleteUsersController : DnnApiController
    {


        

        /// <summary>
        /// Deletes the users.
        /// 
        /// Called from schedule task.
        /// First get a list of portals to be processed by reading Portal settings.
        /// Loop through the list and delete users where the setting AutoDelete = true
        /// 
        /// </summary>
        /// <returns>Returns a list of portals processed.</returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage DeleteUsers()
        {

            var usersDeletedCount = 0;
            var portalsProcessed = new StringBuilder();
            
            portalsProcessed.Append(Strings.UsersDeleted);
            portalsProcessed.Append("<ul>");

            var lstPortals = PortalUtilities.GetPortalsToDeleteUsers();

            foreach (var portal in lstPortals)
            {
                // Portal setting AutoDeleteTimeSpan since user was registered. Default 60 minutes
                var deleteTimeSpan = PortalController.GetPortalSetting("AutoDeleteUsersTimeSpan", portal.PortalID, "60");
               
                var users = UserController.GetUsers(portal.PortalID);

                usersDeletedCount += users.Cast<UserInfo>().Where(
                    user => !user.IsDeleted && !user.Membership.Approved && (DateTime.Now - user.Membership.CreatedDate).TotalMinutes 
                        >= Convert.ToDouble(deleteTimeSpan)).Count(userToDelete => UserController.DeleteUser(ref userToDelete, false, false));
                
                // return the name of the portal(s) processed with the number of users deleted.
                // this string will be used in the scheduled task log.
                portalsProcessed.Append(string.Format("<li>{0} ({1})</li>", portal.PortalName, usersDeletedCount));

            }

            portalsProcessed.Append("</ul>");
            
            return this.Request.CreateResponse(HttpStatusCode.OK, portalsProcessed.ToString());
            
        }


        /// <summary>
        /// Removes deleted users. x days after they registered if they have not been approved.
        /// Default time span is 7 days.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage RemoveDeletedUsers()
        {

            var usersRemovedCount = 0;
            var portalsProcessed = new StringBuilder();
            const string DefaultTimespan = "7";
                        
            portalsProcessed.Append(Strings.UsersRemoved);
            portalsProcessed.Append("<ul>");

            var lstPortals = PortalUtilities.GetPortalsToRemoveUsers();
            foreach (var portal in lstPortals)
            {
                var removeDeletedUsersTimeSpan = PortalController.GetPortalSetting("AutoRemoveDeletedUsersTimeSpan", portal.PortalID, DefaultTimespan);

                var users = UserController.GetDeletedUsers(portal.PortalID);
                usersRemovedCount += users.Cast<UserInfo>().Where(
                    user => user.IsDeleted && !user.Membership.Approved && (DateTime.Now - user.Membership.CreatedDate).TotalMinutes 
                        > Convert.ToDouble(removeDeletedUsersTimeSpan)).Count(UserController.RemoveUser);

                // return the name of the portal(s) processed with the number of users deleted.
                // this string will be used in the scheduled task log.
                portalsProcessed.Append(string.Format("<li>{0}({1})</li>", portal.PortalName, usersRemovedCount));
            }

            portalsProcessed.Append("</ul>");
            return this.Request.CreateResponse(HttpStatusCode.OK, portalsProcessed.ToString());
        }



        /// <summary>
        /// Used to test service is accessible.
        /// </summary>
        /// <returns>Returns Hello World!</returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage HelloWorld()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, Strings.Greeting);
        }




        /// <summary>
        /// Adds some test users to portal 0.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage AddUsers()
        {
            string retVal;
            try
            {
                for (var x = 1; x <= 2; x++)
                {
                    var user = new UserInfo
                                   {
                                       Username = "user" + x,
                                       PortalID = 0,
                                       FirstName = "Test",
                                       LastName = "User" + x,
                                       Email = "declan@dws.ie",
                                       Membership =
                                           {
                                               Password = "Pa$$w0rd", 
                                               Approved = false
                                           }
                                   };

                    UserController.CreateUser(ref user);

                }
                retVal = "Users created";
            }
            catch (Exception ex)
            {

                retVal = "Error creating user: " + ex.Message;
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, retVal);

        }
    }
}
