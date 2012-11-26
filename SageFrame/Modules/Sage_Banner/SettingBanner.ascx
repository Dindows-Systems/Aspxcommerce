<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SettingBanner.ascx.cs"
    Inherits="Modules_Sage_Banner_SettingBanner" %>
<div id="divBannerSetting" runat="server" class="sfFormwrapper">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="25%">
                <asp:Label ID="lblBannerToUse" runat="server" CssClass="cssClassFormLabel" Text="Bannner Use"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:DropDownList ID="ddlBannerListToUse" runat="server" AutoPostBack="False" CssClass="cssClassDropDown">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMode" runat="server" CssClass="cssClassFormLabel" Text="Transition Mode"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:DropDownList ID="ddlTransitionMode" runat="server" CssClass="cssClassDropDown">
                    <asp:ListItem Value="0">horizontal</asp:ListItem>
                    <asp:ListItem Value="2"> fade</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblInfiniteLoop" runat="server" CssClass="cssClassFormLabel" Text="Infinite loop"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:CheckBox ID="chkInfiniteLoop" CssClass="cssClassCheckBox" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSpeed" runat="server" CssClass="cssClassFormLabel" Text="Speed(in ms)"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:TextBox ID="txtSpeed" runat="server" MaxLength="4" CssClass="cssClassNormalTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtSpeed" runat="server" ControlToValidate="txtSpeed"
                    SetFocusOnError="true" ValidationGroup="bannersetting" ErrorMessage="*" CssClass="cssClasssNormalRed"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtSpeed" runat="server" ControlToValidate="txtSpeed"
                    ErrorMessage="*" ValidationExpression="^\d+$">
                </asp:RegularExpressionValidator>
                <asp:RangeValidator runat="server" id="rngSpeed" controltovalidate="txtSpeed" type="Integer" minimumvalue="0" maximumvalue="3000" errormessage="Please enter integer upto 3000 " />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPager" runat="server" CssClass="cssClassFormLabel" Text="PagerType"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:Label ID="lblNumeric" runat="server" CssClass="cssClassFormLabel" Text="Numeric"></asp:Label>
                <asp:CheckBox ID="chkNumeric" CssClass="cssClassCheckBox" runat="server" />
            </td>
        </tr>
        <tr id="tdAutoslide" runat="server">
            <td>
                <asp:Label ID="lblAuto" runat="server" CssClass="cssClassFormLabel" Text="Auto Slide"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:CheckBox ID="chkAutoSlide" CssClass="cssClassCheckBox" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPause" runat="server" CssClass="cssClassFormLabel" Text="Pause Time(in ms)"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:TextBox ID="txtPauseTime" runat="server" Text="0" MaxLength="4" CssClass="cssClassNormalTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtPauseTime" runat="server" ControlToValidate="txtPauseTime"
                    SetFocusOnError="true" ValidationGroup="bannersetting" ErrorMessage="*" CssClass="cssClasssNormalRed"
                    Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPauseTime"
                ErrorMessage="*"  ValidationExpression="^\d+$" ValidationGroup="bannersetting"></asp:RegularExpressionValidator>
                <asp:RangeValidator runat="server" id="rngvPause" controltovalidate="txtPauseTime" type="Integer" minimumvalue="0" maximumvalue="3000" errormessage="Please enter integer upto 3000 " />
            </td>
            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" CssClass="cssClassFormLabel" Text="Enable Next/Prev Button"></asp:Label>
            </td>
            <td width="30">
                :
            </td>
            <td>
                <asp:CheckBox ID="chkEnableControl" runat="server" CssClass="cssClassCheckBox" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
                <div class="cssClassButtonWrapper" runat="server">
                    <asp:ImageButton ID="imbSaveBannerSetting" runat="server" ValidationGroup="bannersetting"
                        OnClick="imbSaveBannerSetting_Click" />
                    <asp:Label ID="lblSaveBannerSetting" runat="server" Text="Save" AssociatedControlID="imbSaveBannerSetting"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblSettingMessage" runat="server" CssClass="cssClassFormLabel"></asp:Label>
</div>
