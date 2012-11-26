<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestimonialSetting.ascx.cs"
    Inherits="Modules_Testimonial_TestimonialSetting" %>
    <script type="text/javascript">
        $(function() {
            var NoOfList = '#' + '<%=txtNoOfList.ClientID %>';
            $(NoOfList).numeric();
        });
    </script>
<div class="cssClassFormWrapper">
    <div id="dvSetting">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblNoOfList" runat="server" Text="No Of Testimonial To Display" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td>                    
                    :
                </td>
                <td>
                    <asp:TextBox ID="txtNoOfList" MaxLength="2" runat="server" CssClass="cssClassNormalTextBox"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNoOfList" ControlToValidate="txtNoOfList" ValidationGroup="saveSetting" runat="server" ErrorMessage="Field Is Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblImage" runat="server" Text="Enable Image" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:CheckBox ID="chkImage" runat="server" CssClass="cssClassCheckBox" />
                </td>
            </tr>
               <tr>
                <td>
                    <asp:Label ID="lblDatetime" runat="server" Text="Enable Date" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:CheckBox ID="chkDateTime" runat="server" CssClass="cssClassCheckBox" />
                </td>
            </tr>
             <tr>
                <td>
                    <asp:Label ID="lblViewMore" runat="server" Text="Enable View More" CssClass="cssClassFormLabel"></asp:Label>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:CheckBox ID="chkViewMore" runat="server" CssClass="cssClassCheckBox" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td>
                    <div class="cssClassButtonWrapper">
                        <asp:ImageButton ID="imbSaveSetting" ValidationGroup="saveSetting" runat="server" OnClick="imbSaveSetting_Click" />
                        <asp:Label ID="lblSavesetting" runat="server" Text="Save" AssociatedControlID="imbSaveSetting"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
