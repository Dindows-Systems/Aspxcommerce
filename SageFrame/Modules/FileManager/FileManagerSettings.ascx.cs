using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SageFrame.Web;
using SageFrame.FileManager;

public partial class Modules_FileManager_FileManagerSettings : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadSettings();
        }
    }
    protected void LoadSettings()
    {
        List<FileManagerSettingInfo> lstSettings = FileManagerController.GetFileManagerSettings(int.Parse(SageUserModuleID),GetPortalID);
        foreach (FileManagerSettingInfo obj in lstSettings)
        {
            switch (obj.SettingKey)
            {
                case "FileManagerExtensions":
                    txtExtensions.Text = obj.SettingValue;
                    break;
            }
        }
    }
    protected void btnAddExtension_Click(object sender, EventArgs e)
    {
        List<FileManagerSettingInfo> lstSettings=new List<FileManagerSettingInfo>();
        FileManagerSettingInfo objSetting=new FileManagerSettingInfo();
        objSetting.UserModuleID = int.Parse(SageUserModuleID.ToString());
        objSetting.SettingKey = "FileManagerExtensions";
        objSetting.SettingValue = txtExtensions.Text;
        objSetting.IsActive = true;
        objSetting.PortalID = GetPortalID;
        objSetting.UpdatedBy = GetUsername;
        objSetting.AddedBy = GetUsername;
        lstSettings.Add(objSetting);
        try
        {
            FileManagerController.AddUpdateSettings(lstSettings);
            LoadSettings();
            ShowMessage(SageMessageTitle.Information.ToString(), GetSageMessage("FileManager", "SettingsAddedSuccessfully"), "", SageMessageType.Success);

        }
        catch (Exception ex)
        {
                
           ProcessException(ex);
        }


    }

}
