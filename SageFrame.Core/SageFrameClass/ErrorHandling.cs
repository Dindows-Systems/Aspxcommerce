using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.ErrorLog;
using SageFrame.Web;
using System.Web;

namespace SageFrame.Web
{
    public class ErrorHandler
    {
        public bool LogCommonException(Exception exc)
        {
            string strIPaddress = string.Empty;
            string strPageUrl = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != string.Empty)
            {
                strIPaddress = HttpContext.Current.Request.UserHostAddress;
            }

            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.RawUrl != string.Empty)
            {
                strPageUrl = HttpContext.Current.Request.RawUrl;
            }
            ErrorLogDataContext db = new ErrorLogDataContext(SystemSetting.SageFrameConnectionString);
            System.Nullable<int> inID = 0;
            SageFrameConfig sfConfig = new SageFrameConfig();
            db.sp_LogInsert(ref inID, (int)SageFrameEnums.ErrorType.CommonError, 11, exc.Message, exc.ToString(),
                strIPaddress, strPageUrl, true, sfConfig.GetPortalID, sfConfig.GetUsername);

            return sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseCustomErrorMessages);

        }

        public bool LogPageMethodException(Exception exc)
        {
            string strIPaddress = string.Empty;
            string strPageUrl = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != string.Empty)
            {
                strIPaddress = HttpContext.Current.Request.UserHostAddress;
            }

            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.RawUrl != string.Empty)
            {
                strPageUrl = HttpContext.Current.Request.RawUrl;
            }
            ErrorLogDataContext db = new ErrorLogDataContext(SystemSetting.SageFrameConnectionString);
            System.Nullable<int> inID = 0;
            SageFrameConfig sfConfig = new SageFrameConfig();
            db.sp_LogInsert(ref inID, (int)SageFrameEnums.ErrorType.PageMethod, 11, exc.Message, exc.ToString(),
                strIPaddress, strPageUrl, true, sfConfig.GetPortalID, sfConfig.GetUsername);
            
            return sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseCustomErrorMessages);

        }

        public bool LogWCFException(Exception exc)
        {
            string strIPaddress = string.Empty;
            string strPageUrl = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != string.Empty)
            {
                strIPaddress = HttpContext.Current.Request.UserHostAddress;
            }

            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.RawUrl != string.Empty)
            {
                strPageUrl = HttpContext.Current.Request.RawUrl;
            }
            ErrorLogDataContext db = new ErrorLogDataContext(SystemSetting.SageFrameConnectionString);
            System.Nullable<int> inID = 0;
            SageFrameConfig sfConfig = new SageFrameConfig();
            db.sp_LogInsert(ref inID, (int)SageFrameEnums.ErrorType.WCF, 11, exc.Message, exc.ToString(),
                strIPaddress, strPageUrl, true, sfConfig.GetPortalID, sfConfig.GetUsername);            
            return sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseCustomErrorMessages);

        }

        public bool LogWebServiceException(Exception exc)
        {
            string strIPaddress = string.Empty;
            string strPageUrl = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.UserHostAddress != string.Empty)
            {
                strIPaddress = HttpContext.Current.Request.UserHostAddress;
            }

            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.RawUrl != string.Empty)
            {
                strPageUrl = HttpContext.Current.Request.RawUrl;
            }
            ErrorLogDataContext db = new ErrorLogDataContext(SystemSetting.SageFrameConnectionString);
            System.Nullable<int> inID = 0;
            SageFrameConfig sfConfig = new SageFrameConfig();
            db.sp_LogInsert(ref inID, (int)SageFrameEnums.ErrorType.WebService, 11, exc.Message, exc.ToString(),
                strIPaddress, strPageUrl, true, sfConfig.GetPortalID, sfConfig.GetUsername);
            
            return sfConfig.GetSettingBollByKey(SageFrameSettingKeys.UseCustomErrorMessages);

        }  
    }
}
