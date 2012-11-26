using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Web.Utilities;

namespace SageFrame.Core.ListManagement
{
    public class ListManagementController
    {
        public static int AddNewList(ListInfo objList)
        {
            string sp = "[dbo].[usp_ListEntryAdd]";
            SQLHandler sagesql = new SQLHandler();
            try
            {
                List<KeyValuePair<string, object>> ParamCollInput = new List<KeyValuePair<string, object>>();
                ParamCollInput.Add(new KeyValuePair<string, object>("@ListName", objList.ListName));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Value", objList.Value));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Text", objList.Text));
                ParamCollInput.Add(new KeyValuePair<string, object>("@ParentID", objList.ParentID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Level", objList.Level));
                ParamCollInput.Add(new KeyValuePair<string, object>("@CurrencyCode", objList.CurrencyCode));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DisplayLocale", objList.DisplayLocale));
                ParamCollInput.Add(new KeyValuePair<string, object>("@EnableDisplayOrder", objList.EnableDisplayOrder));
                ParamCollInput.Add(new KeyValuePair<string, object>("@DefinitionID", objList.DefinitionID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Description", objList.Description));
                ParamCollInput.Add(new KeyValuePair<string, object>("@PortalID", objList.PortalID));
                ParamCollInput.Add(new KeyValuePair<string, object>("@IsActive", objList.IsActive));
                ParamCollInput.Add(new KeyValuePair<string, object>("@AddedBy", objList.AddedBy));
                ParamCollInput.Add(new KeyValuePair<string, object>("@Culture", objList.Culture));

                int ListID = sagesql.ExecuteNonQueryAsGivenType<int>(sp, ParamCollInput, "@ListID");
                return ListID;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
