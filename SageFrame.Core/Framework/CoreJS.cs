using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Common;
using SageFrame.Templating;
using System.IO;

namespace SageFrame.Core
{
    public class CoreJs
    {
        /// <summary>
        /// Get the list of Core js files to be included by default
        /// </summary>
        /// <param name="IsAdmin">Include only those files required for the admin mode i.e. the dashboard</param>
        /// <param name="IsUserLoggedIn">Scipts required for the top sticky bar and the edit buttons and popup</param>
        /// <returns>List of scripts to be included</returns>
        public static List<CssScriptInfo> GetList(bool IsAdmin,bool IsUserLoggedIn)
        {
             List<CssScriptInfo> lstJS = new List<CssScriptInfo>{                
                                                    new CssScriptInfo("Core","jquery-1.4.4.js","/js/",0),
                                                    new CssScriptInfo("Core","sageframecore.js","/js/SageFrameCorejs/",0),
                                                    new CssScriptInfo("Core","aspxcommercecore.js","/js/SageFrameCorejs/",0),
                                                    new CssScriptInfo("Core","json2.js","/js/",0),
                                                    new CssScriptInfo("Core","lazyload.js","/js/",0)      
            };
            if (IsAdmin && IsUserLoggedIn)
            {

            }
            else if (!IsAdmin && IsUserLoggedIn)
            {
            
            }

            return lstJS;
        }

    }
}