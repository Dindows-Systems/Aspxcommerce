<%@ Control Language="C#" AutoEventWireup="true" Inherits="SageFrame.Modules.Admin.WebAnalytics.ctl_WebAnalytics"
    CodeFile="ctl_WebAnalytics.ascx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>
    <h2 class="cssClassFormHeading">
        <asp:Label ID="lblWebAnalyticsManagement" runat="server" Text="Web Analytics Management"></asp:Label></h2>
<div class="cssClassFormWrapper">
    <table border="0" cellpadding="0" cellspacing="0">        
        <tr>
            <td valign='top' style="width:15%;"><strong>
                <asp:Label ID="lblvalue" runat="server" Text="Google JS :"
                    ToolTip="Javascript code provided by google, paste here."></asp:Label>
                    </strong>
            </td>
            <td>
                <asp:TextBox ID="txtvalue" runat="server" TextMode="MultiLine" Rows="10" ValidationGroup="GJSC"  CssClass="cssClassTextArea1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvJS" runat="server" ControlToValidate="txtvalue"
                                    ErrorMessage="*" ValidationGroup="GJSC" CssClass="cssClasssNormalRed"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td valign='top'>
                <asp:Label ID="lblisActive" runat="server" Text="Is Active:" CssClass="cssClassFormLabel"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chkIsActive" runat='server' ValidationGroup="GJSC" CssClass="cssClassCheckBox" />
            </td>
        </tr>        
    </table>
</div>
<div class="cssClassButtonWrapper">
    <asp:ImageButton ID="imbSave" runat="server" OnClick="imbSave_Click" ToolTip="Save"
        CausesValidation="false" ValidationGroup="GJSC" />
    <asp:Label ID="lblSave" runat="server" Text="Update" ToolTip="Save" AssociatedControlID="imbSave"
        Style="cursor: pointer;"></asp:Label>
    <asp:ImageButton ID="imbRefresh" runat="server" ToolTip="Refresh" OnClick="imbRefresh_Click"
        CausesValidation="false" />
    <asp:Label ID="lblRefresh" runat="server" Text="Refresh" ToolTip="Refresh" AssociatedControlID="imbRefresh"
        Style="cursor: pointer;"></asp:Label>
</div>
