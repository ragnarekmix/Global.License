﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Global.LicenseManager.Tests.Source {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Global.LicenseManager.Tests.Source.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;Customers&gt;
        ///  &lt;Customer&gt;
        ///    &lt;CustomerId&gt;1&lt;/CustomerId&gt;
        ///    &lt;FirstName&gt;Mihail&lt;/FirstName&gt;
        ///    &lt;LastName&gt;Podobivsky&lt;/LastName&gt;
        ///    &lt;Licenses&gt;
        ///      &lt;License&gt;
        ///        &lt;LicenseId&gt;1&lt;/LicenseId&gt;
        ///        &lt;Key&gt;lasdjflksdf&lt;/Key&gt;
        ///        &lt;CreationDate&gt;02 February 2011&lt;/CreationDate&gt;
        ///        &lt;ModificationDate&gt;02 February 2011&lt;/ModificationDate&gt;
        ///      &lt;/License&gt;
        ///    &lt;/Licenses&gt;
        ///  &lt;/Customer&gt;
        ///&lt;/Customers&gt;.
        /// </summary>
        internal static string SimpleSource {
            get {
                return ResourceManager.GetString("SimpleSource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        ///&lt;Customers&gt;
        ///  &lt;Customer&gt;
        ///    &lt;CustomerId&gt;1&lt;/CustomerId&gt;
        ///    &lt;FirstName&gt;Mihail&lt;/FirstName&gt;
        ///    &lt;LastName&gt;Podobivsky&lt;/LastName&gt;
        ///    &lt;Licenses&gt;
        ///      &lt;License&gt;
        ///        &lt;LicenseId&gt;1&lt;/LicenseId&gt;
        ///        &lt;Key&gt;lasdjflksdf&lt;/Key&gt;
        ///        &lt;CreationDate&gt;02 February 2011&lt;/CreationDate&gt;
        ///        &lt;ModificationDate&gt;02 February 2011&lt;/ModificationDate&gt;
        ///      &lt;/License&gt;
        ///    &lt;/Licenses&gt;
        ///  &lt;/Customer&gt;
        ///  &lt;Customer&gt;
        ///    &lt;CustomerId&gt;2&lt;/CustomerId&gt;
        ///    &lt;FirstName&gt;Joe&lt;/FirstNam [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Source {
            get {
                return ResourceManager.GetString("Source", resourceCulture);
            }
        }
    }
}