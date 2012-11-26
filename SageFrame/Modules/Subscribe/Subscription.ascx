<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Subscription.ascx.cs"
    Inherits="Modules_Subscribe_Subscription" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="cssClassNewsLetter" id="newsLetter" runat="server">
    <asp:Label ID="lblHelpText" runat="server" Visible="false"></asp:Label>
    <p>
        <asp:Label ID="lblEmail" runat='server' Text=""></asp:Label>
        <asp:TextBox ID="txtNewsLetterEmail" runat="server" ValidationGroup="newsletter"
            CssClass="cssClassNewsLetterEmailBox"></asp:TextBox>
        <cc1:TextBoxWatermarkExtender TargetControlID="txtNewsLetterEmail" WatermarkText="Subscribe to our newsletter"
            ID="txtWatermarkExtender" runat="server">
        </cc1:TextBoxWatermarkExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewsLetterEmail"
            ErrorMessage="Email address is required" ValidationGroup="newsletter" Display="Dynamic"
            SetFocusOnError="True" ToolTip="Enter Email address">*</asp:RequiredFieldValidator>
    </p>
    <p class="cssClassValidator">
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNewsLetterEmail"
            ErrorMessage="Invalid email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            ValidationGroup="newsletter" Display="Dynamic" SetFocusOnError="True" ToolTip="Enter valid email Address"></asp:RegularExpressionValidator>
    </p>
    <p class="cssClassPaddingRemove">
        <asp:Button ID="btnSubscribe" runat="server" CssClass="cssClassNewsletterSubmitBtn"
            OnClick="btnSubscribe_Click" ValidationGroup="newsletter" />
    </p>
</div>
<div id="mainContainer" class="cssClassNewsLetter" runat="server">
    <h2>
        <asp:Label ID="lblTitleModal" runat="server"></asp:Label></h2>
    <p>
        <asp:Label ID="lblHelpTextModal" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblEmailModal" runat='server' Text=""></asp:Label>
        <asp:TextBox ID="txtEmailModal" runat="server" ValidationGroup="newsletterModal"
            CssClass="cssClassNewsLetterEmailBox"></asp:TextBox>
        <cc1:TextBoxWatermarkExtender TargetControlID="txtEmailModal" WatermarkText="Subscribe to our newsletter"
            ID="txtWatermarkExtenderModal" runat="server">
        </cc1:TextBoxWatermarkExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmailModal"
            ErrorMessage="Email address is required" ValidationGroup="newsletterModal" Display="Dynamic"
            SetFocusOnError="True" ToolTip="Enter Email address">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmailModal"
            ErrorMessage="Invalid email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            ValidationGroup="newsletterModal" Display="Dynamic" SetFocusOnError="True" ToolTip="Enter valid email Address"></asp:RegularExpressionValidator>
    </p>
    <div class="cssClassButton">
        <asp:Button ID="btnSubscribeModal" runat="server" CssClass="cssClassSubmitBtn" OnClick="btnSubscribe_Click"
            ValidationGroup="newsletterModal" />
    </div>
    <div class="cssClassClear">
    </div>
</div>
