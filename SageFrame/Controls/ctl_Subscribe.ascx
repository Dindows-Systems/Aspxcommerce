<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_Subscribe.ascx.cs"
    Inherits="SageFrame.Controls.ctl_Subscribe" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="cssClassRightBox_TopBg">
    <div class="cssClassRightBox_BtnBg">
        <div class="cssClassRightBox_MidBg">
            <h1>
                Subscribe Newsletter</h1>
            <div class="cssClassNewsLetter">
                <p>
                    Receive security bulletins and our monthly newsletter with special offers.
                </p>
                <p>
                    <label>
                        Email :</label>
                    <%--<input type="text"/>--%>
                    <%--<input id="txtNewsLetterEmail" type="text" runat="server" value="Subscribe to our newsletter" />--%>
                    <asp:TextBox ID="txtNewsLetterEmail" runat="server" ValidationGroup="newsletter"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender  TargetControlID="txtNewsLetterEmail" WatermarkText="Subscribe to our newsletter"
                        ID="txtWatermarkExtender" runat="server">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewsLetterEmail"
                        ErrorMessage="Email addres is required" ValidationGroup="newsletter">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNewsLetterEmail"
                        ErrorMessage="Invalid email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="newsletter">*</asp:RegularExpressionValidator>
                    <%-- <input type="submit" value="" class="cssClasssubmitbtn" style="width: 14px" />--%>
                </p>
                <p>
                    <asp:Button ID="btnSubscribe" runat="server" CssClass="cssClasssubmitbtn" OnClick="btnSubscribe_Click"
                        ToolTip="subscribe" ValidationGroup="newsletter" />
                    <%--<button type="submit" class="SubmitBtn" id="submit"> <span><span>Subscribe</span></span> </button>--%>
                </p>
            </div>
        </div>
    </div>
</div>
