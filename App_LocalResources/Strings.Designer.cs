﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DNN.DeleteUnauthorisedUsers.App_LocalResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DNN.DeleteUnauthorisedUsers.App_LocalResources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;p&gt;AutoDeleteUsers and AutoPurgeUsers settings have been added to the portals listed above. The default value of each is false.&lt;/p&gt;&lt;p&gt;The default value of AutoDeleteUsersTimeSpan is 60, measured in minutes.&lt;/p&gt;&lt;p&gt;The default value of AutoPurgeUsersTimeSpan is 30, measured in days.&lt;/p&gt;.
        /// </summary>
        public static string AddSettings {
            get {
                return ResourceManager.GetString("AddSettings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exception: .
        /// </summary>
        public static string Exception {
            get {
                return ResourceManager.GetString("Exception", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Greetings from dwsDeleteUsersServices; DeleteUsersController!.
        /// </summary>
        public static string Greeting {
            get {
                return ResourceManager.GetString("Greeting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;h2&gt;Portal(s) updated:&lt;/h2&gt;.
        /// </summary>
        public static string PortalSettingsUpdated {
            get {
                return ResourceManager.GetString("PortalSettingsUpdated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;br /&gt;&amp;nbsp;&amp;nbsp; Portal(s) processed: &lt;br /&gt;{0}.
        /// </summary>
        public static string PortalsProcessed {
            get {
                return ResourceManager.GetString("PortalsProcessed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SpamUsersDelete.
        /// </summary>
        public static string SpamUsersDelete {
            get {
                return ResourceManager.GetString("SpamUsersDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Portal settings update error: .
        /// </summary>
        public static string UpdateError {
            get {
                return ResourceManager.GetString("UpdateError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;b&gt;Users Deleted&lt;/b&gt;.
        /// </summary>
        public static string UsersDeleted {
            get {
                return ResourceManager.GetString("UsersDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;b&gt;Users Removed&lt;/b&gt;.
        /// </summary>
        public static string UsersRemoved {
            get {
                return ResourceManager.GetString("UsersRemoved", resourceCulture);
            }
        }
    }
}
