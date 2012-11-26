using System;
using SageFrame;
using SageFrame.Web;
public partial class Modules_ItemBrowse_DisplayContainer : BaseAdministrationUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //SageFrameRoute parentPage = (SageFrameRoute)this.Page;

        //if (parentPage.ControlMode.Equals("category"))
        //{
        //    this.Label1.Text = "Category :  " + parentPage.Key;

        //}
        //else if (parentPage.ControlMode.Equals("item"))
        //{
        //    this.Label1.Text = "Item :  " + parentPage.Key;

        //}
        //else if (parentPage.ControlMode.Equals("item"))
        //{
        //    this.Label1.Text = "Tags :  " + parentPage.Key;

        //}
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            SageFrameRoute parentPage = (SageFrameRoute)this.Page;

            if (parentPage.ControlMode.Equals("category"))
            {
                SageUserControl itemDetails = (SageUserControl)LoadControl("~/Modules/Admin/DetailsBrowse/CategoryDetails.ascx");
                itemDetails.EnableViewState = true;
                itemDetails.SageUserModuleID = SageUserModuleID;
                phdetailBrowseholder.Controls.Add(itemDetails);
            }
            else if (parentPage.ControlMode.Equals("item"))
            {
                SageUserControl itemDetails = (SageUserControl)LoadControl("~/Modules/Admin/DetailsBrowse/ItemDetails.ascx");
                itemDetails.EnableViewState = true;
                itemDetails.SageUserModuleID = SageUserModuleID;
                phdetailBrowseholder.Controls.Add(itemDetails);
            }
            else if (parentPage.ControlMode.Equals("tags"))
            {
                SageUserControl itemDetails = (SageUserControl)LoadControl("~/Modules/Admin/DetailsBrowse/AllTags.ascx");
                itemDetails.EnableViewState = true;
                itemDetails.SageUserModuleID = SageUserModuleID;
                phdetailBrowseholder.Controls.Add(itemDetails);
            }
            else if (parentPage.ControlMode.Equals("tagsitems"))
            {
                SageUserControl itemDetails = (SageUserControl)LoadControl("~/Modules/Admin/DetailsBrowse/ViewTagsItems.ascx");
                itemDetails.EnableViewState = true;
                itemDetails.SageUserModuleID = SageUserModuleID;
                phdetailBrowseholder.Controls.Add(itemDetails);
            }
            else if (parentPage.ControlMode.Equals("search"))
            {
                SageUserControl itemDetails = (SageUserControl)LoadControl("~/Modules/Admin/DetailsBrowse/ItemLists.ascx");
                itemDetails.EnableViewState = true;
                itemDetails.SageUserModuleID = SageUserModuleID;
                phdetailBrowseholder.Controls.Add(itemDetails);
            }
            else if (parentPage.ControlMode.Equals("option"))
            {
                SageUserControl itemDetails = (SageUserControl)LoadControl("~/Modules/Admin/DetailsBrowse/ItemsListByIds.ascx");
                itemDetails.EnableViewState = true;
                itemDetails.SageUserModuleID = SageUserModuleID;
                phdetailBrowseholder.Controls.Add(itemDetails);
            }
            else if (parentPage.ControlMode.Equals("coupons"))
            {
                SageUserControl itemDetails = (SageUserControl)LoadControl("~/Modules/Admin/DetailsBrowse/AllCoupons.ascx");
                itemDetails.EnableViewState = true;
                itemDetails.SageUserModuleID = SageUserModuleID;
                phdetailBrowseholder.Controls.Add(itemDetails);
            }            
        }
        catch (Exception ex)
        {
            ProcessException(ex);
        }
    }
}