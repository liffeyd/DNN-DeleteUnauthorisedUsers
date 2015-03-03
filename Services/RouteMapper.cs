using System;
using DotNetNuke.Web.Api;

namespace DNN.DeleteUnauthorisedUsers.Services
{
    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            // http://localhost/Dnn_Platform/DesktopModules/DNN-DeleteUnauthorisedUsers/dwsDeleteUsersServices/API/DeleteUsers/HelloWorld
            //                              moduleFolderName, routeName, url, namespace
            mapRouteManager.MapHttpRoute("DNN-DeleteUnauthorisedUsers/dwsDeleteUsersServices", "default", "{controller}/{action}", new[] { "DNN.DeleteUnauthorisedUsers.Services" });

        }
    }

}
