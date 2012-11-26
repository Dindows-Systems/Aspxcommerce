using System;
using SageFrame.Web;
using AspxCommerce.Core;

public partial class Modules_AspxCommerce_AspxStoreLogo_StoreLogo : BaseAdministrationUserControl
{
    public int StoreID, PortalID;
    public string UserName, CultureName, StoreLogoImg;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                StoreID = GetStoreID;
                PortalID = GetPortalID;
                UserName = GetUsername;
                CultureName = GetCurrentCultureName;
                StoreSettingConfig ssc = new StoreSettingConfig();
                StoreLogoImg = ssc.GetStoreSettingsByKey(StoreSetting.StoreLogoURL, StoreID, PortalID, CultureName);
            }
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}
