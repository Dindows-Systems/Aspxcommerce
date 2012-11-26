<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventViewer.ascx.cs" Inherits="SageFrame.Modules.Admin.EventViewer.EventViewer" %>
<%@ Register Src="~/Controls/sectionheadcontrol.ascx" TagName="sectionheadcontrol"
    TagPrefix="sfe" %>

<script language="javascript" type="text/javascript">
    function flipFlopException(eTarget) {
        if (document.getElementById(eTarget).style.display == 'none') {
            document.getElementById(eTarget).style.display = '';
        }
        else {
            document.getElementById(eTarget).style.display = 'none';
        }
    }
</script>
<h2 class="cssClassFormHeading">
        <asp:Label ID="lblEventViewerManagement" runat="server" 
            Text="Event Viewer Management" 
            meta:resourcekey="lblEventViewerManagementResource1"></asp:Label></h2>
<div class="cssClassEventViewerWrapper">
    <div class="cssClassFormWrapper">
        <table id="tblEventViewer" cellspacing="0" cellpadding="0" border="0" runat="server">
            <tr>
                <td width="10%">
                    <asp:Label ID="lblLogType" runat="server" CssClass="cssClassFormLabel" 
                        Text="Type" meta:resourcekey="lblLogTypeResource1" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlLogType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLogType_SelectedIndexChanged"
                        CssClass="cssClassDropDown" meta:resourcekey="ddlLogTypeResource1" />
                </td>
                <td>
                    <asp:Label ID="lblRecordsPage" runat="server" CssClass="cssClassFormLabel" 
                        Text="Show rows :" meta:resourcekey="lblRecordsPageResource1" />
                </td>
                <td>
                    <asp:DropDownList ID="ddlRecordsPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRecordsPerPage_SelectedIndexChanged"
                        CssClass="cssClassDropDown" meta:resourcekey="ddlRecordsPerPageResource1">
                        <asp:ListItem Value="10" meta:resourcekey="ListItemResource1">10</asp:ListItem>
                        <asp:ListItem Value="25" meta:resourcekey="ListItemResource2">25</asp:ListItem>
                        <asp:ListItem Value="50" meta:resourcekey="ListItemResource3">50</asp:ListItem>
                        <asp:ListItem Value="100" meta:resourcekey="ListItemResource4">100</asp:ListItem>
                        <asp:ListItem Value="150" meta:resourcekey="ListItemResource5">150</asp:ListItem>
                        <asp:ListItem Value="200" meta:resourcekey="ListItemResource6">200</asp:ListItem>
                        <asp:ListItem Value="250" meta:resourcekey="ListItemResource7">250</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div style="vertical-align: middle;">
        <asp:Label ID="lblClickRow" runat="server" CssClass="cssClassHelpTitle" 
            Text="Click on row for details" meta:resourcekey="lblClickRowResource1" />
    </div>
    <div class="cssClassButtonWrapper">
        <asp:ImageButton ID="imgLogClear" runat="server" ToolTip="Clear all Logs" OnClick="imgLogClear_Click"
            CausesValidation="False" meta:resourcekey="imgLogClearResource1" />
        <asp:Label ID="lblClearLog" runat="server" Text="Clear Log" AssociatedControlID="imgLogClear"
            Style="cursor: pointer;" meta:resourcekey="lblClearLogResource1"></asp:Label>
        <asp:ImageButton ID="imgLogDelete" runat="server" ToolTip="Delete Selected Logs"
            CausesValidation="False" OnClick="imgLogDelete_Click" 
            meta:resourcekey="imgLogDeleteResource1" />
        <asp:Label ID="lblLogDelete" runat="server" Text="Delete Selected Logs" AssociatedControlID="imgLogDelete"
            Style="cursor: pointer;" meta:resourcekey="lblLogDeleteResource1"></asp:Label>
    </div>
    <div class="cssClassGridWrapper">
        <asp:GridView Width="100%" runat="server" ID="gdvLog" OnSelectedIndexChanged="gdvLog_SelectedIndexChanged"
            GridLines="None" AutoGenerateColumns="False" AllowPaging="True" EmptyDataText="..........LogType Not Found.........."
            OnPageIndexChanging="gdvLog_PageIndexChanging" OnRowCommand="gdvLog_RowCommand"
            OnRowDataBound="gdvLog_RowDataBound" OnRowDeleting="gdvLog_RowDeleting" 
            meta:resourcekey="gdvLogResource1">
            <Columns>
                <asp:TemplateField HeaderStyle-CssClass="cssClassColumnCheckBox" 
                    meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdfLogID" runat="server" Value='<%# Eval("LogID") %>' />
                        <asp:CheckBox ID="chkSendEmail" runat="server" 
                            meta:resourcekey="chkSendEmailResource1" />
                    </ItemTemplate>
                    <HeaderStyle VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
                <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                    <HeaderTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <th style="width: 30%">
                                    <asp:Label ID="lblDateHeader" runat="server" 
                                        meta:resourcekey="lblDateHeaderResource1">Date</asp:Label>
                                </th>
                                <th width="20%">
                                    <asp:Label ID="lblTypeHeader" runat="server" 
                                        meta:resourcekey="lblTypeHeaderResource1">LogType</asp:Label>
                                </th>
                                <th width="25%">
                                    <asp:Label ID="lblPortalHeader" runat="server" 
                                        meta:resourcekey="lblPortalHeaderResource1">Portal Name</asp:Label>
                                </th>
                                <th width="25%">
                                    <asp:Label ID="lblClientHeader" runat="server" 
                                        meta:resourcekey="lblClientHeaderResource1">Client IP</asp:Label>
                                </th>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="">
                            <table style="width: 100%; border: none;">
                                <tr>
                                    <td style="cursor: pointer; width: 30%" 
                                        onclick='flipFlopException(&#039;_<%# Eval("LogID") %>&#039;)'>
                                        <span style="overflow: hidden;">&nbsp;
                                            <asp:Label runat="server" Text='<%# Eval("AddedOn") %>' ID="lblDate" 
                                            meta:resourcekey="lblDateResource1" />
                                        </span>
                                    </td>
                                    <td style="cursor: pointer; width: 20%" 
                                        onclick='flipFlopException(&#039;_<%# Eval("LogID") %>&#039;)'>
                                        <span style="overflow: hidden;">&nbsp;
                                            <asp:Literal ID="ltrLogType" runat="server" 
                                            Text='<%# Eval("LogTypeName") %>'></asp:Literal>
                                        </span>
                                    </td>
                                    <td style="cursor: pointer; width: 25%" 
                                        onclick='flipFlopException(&#039;_<%# Eval("LogID") %>&#039;)'>
                                        <span style="overflow: hidden;">&nbsp;
                                            <asp:Label runat="server" Text='<%# Eval("PortalName") %>' 
                                            ID="lblPortalName" meta:resourcekey="lblPortalNameResource1" />
                                        </span>
                                        <td style="cursor: pointer; width: 25%" 
                                            onclick='flipFlopException(&#039;_<%# Eval("LogID") %>&#039;)'>
                                            <span style="overflow: hidden;">&nbsp;
                                                <asp:Label runat="server" Text='<%# Eval("ClientIPAddress") %>' 
                                                ID="lblClientIP" meta:resourcekey="lblClientIPResource1" />
                                            </span>
                                        </td>
                                        <td style="cursor: pointer;" 
                                            onclick='flipFlopException(&#039;_<%# Eval("LogID") %>&#039;)'>
                                            <span style="overflow: hidden;">&nbsp;
                                                <asp:Label runat="server" Text='<%# Eval("PageURL") %>' ID="lblPageURL" 
                                                Visible="False" meta:resourcekey="lblPageURLResource1" />
                                            </span>
                                        </td>
                                    </td>
                                </tr>
                                <tr style="display: none;" id='_<%# Eval("LogID") %>' width="100%">
                                    <td colspan="4" class="cssClassEvenViewerInfo">
                                        <asp:Panel ID="pnlClientIP" runat="server" Width="100%" 
                                            meta:resourcekey="pnlClientIPResource1">
                                            <p>
                                                <asp:Label ID="lblClientIP1" runat="server" CssClass="cssClassBoldText" 
                                                    Text="Client IP:" meta:resourcekey="lblClientIP1Resource1"></asp:Label>
                                                <asp:Literal ID="ltrClientIP" runat="server" 
                                                    Text='<%# Eval("ClientIPAddress") %>'></asp:Literal></p>
                                            <p>
                                                <asp:Label ID="lblPageUrl1" runat="server" Text="PageUrl:" 
                                                    class="cssClassBoldText" meta:resourcekey="lblPageUrl1Resource1"></asp:Label>
                                                <asp:Literal ID="ltrPageUrl" runat="server" Text='<%# Eval("PageUrl") %>'></asp:Literal></p>
                                            <p>
                                                <asp:Label ID="lblException1" runat="server" Text="Exception:" 
                                                    class="cssClassBoldText" meta:resourcekey="lblException1Resource1"></asp:Label>
                                                <asp:Literal ID="ltrException" runat="server" Text='<%# Eval("Exception") %>'></asp:Literal></p>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="cssClassColumnDelete" 
                    meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:ImageButton ID="imbDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("LogID") %>'
                            CommandName="Delete" 
                            ImageUrl='<%# GetTemplateImageUrl("imgdelete.png", true) %>' 
                            meta:resourcekey="imbDeleteResource1" />
                    </ItemTemplate>
                    <HeaderStyle VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Top" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="cssClassPageNumber" />
            <HeaderStyle CssClass="cssClassHeadingOne" />
            <RowStyle CssClass="cssClassAlternativeOdd" />
            <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
        </asp:GridView>
    </div>
    <div class="cssClassFormWrapper">
        <asp:Panel ID="pnlSendExceptions" runat="server" 
            meta:resourcekey="pnlSendExceptionsResource1">
            <sfe:sectionheadcontrol ID="shcEventSettings" runat="server" Section="tblSendException"
                IncludeRule="true" IsExpanded="false" Text="Send Exceptions" />
                <div class="cssClassFormWrapper">
	                <table id="tblSendException" runat="server" style="width: 100%;" border="0">
                    <tr runat="server">
                        <td colspan="2" class="cssClasseventviewer_textexplanation" runat="server">
                            <asp:Label ID="Label4" runat="server" Text="<b>Please note:</b> By using these features below, you may be sending sensitive data over the Internet in clear text (not encrypted). Before sending your exception submission, please review the contents of your exception log to verify that no sensitive data is contained within it. The row that is checked is sent as an email along with the optional message."
                                CssClass="cssClassNormalHeading"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td style="width: 25;" runat="server">
                        </td>
                        <td style="vertical-align: top;" runat="server">
                            <table id="tblSendExceptionsInfo" runat="server">
                                <tr id="tr1" runat="server">
                                    <td class="SubHead" style="width: 175px" valign="top" runat="server">
                                        <asp:Label ID="lblEmailAddress1" runat="server" CssClass="cssClassFormLabel" Text="Email Address: "></asp:Label>
                                    </td>
                                    <td runat="server">
                                        <asp:TextBox ID="txtEmailAdd" runat="server" CssClass="cssClassNormalTextBox" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*"
                                            CssClass="cssClasssNormalRed" ControlToValidate="txtEmailAdd" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                                            ValidationGroup="sendMail" Display="Dynamic"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfvEmailAdd" runat="server" ControlToValidate="txtEmailAdd"
                                            CssClass="cssClasssNormalRed" ErrorMessage="Please Enter Your Email Address"
                                            SetFocusOnError="True" ValidationGroup="sendMail" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td align="left" width="200" runat="server">
                                        <asp:Label ID="Label2" runat="server" CssClass="cssClassFormLabel" Text="Subject:"></asp:Label>
                                    </td>
                                    <td runat="server">
                                        <asp:TextBox ID="txtSubject1" runat="server" CssClass="cssClassNormalTextBox" />
                                        <asp:RequiredFieldValidator ID="rfvsubject" runat="server" ControlToValidate="txtSubject1"
                                            CssClass="cssClasssNormalRed" ErrorMessage="Enter the subject" SetFocusOnError="True"
                                            ValidationGroup="sendMail"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td align="left" width="200" runat="server">
                                        <asp:Label ID="Label3" runat="server" CssClass="cssClassFormLabel" Text="Message(Optional):"></asp:Label>
                                    </td>
                                    <td runat="server">
                                        <asp:TextBox ID="txtMessage1" runat="server" Rows="6" Columns="25" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td align="left" runat="server">&nbsp;
                                        
                                    </td>
                                    <td runat="server">
                                        <div class="cssClassButtonWrapper">
                                            <asp:ImageButton ID="imgSendEmail" runat="server" ToolTip="Send Email" OnClick="imgSendEmail_Click"
                                                ValidationGroup="sendMail" />
                                            <asp:Label ID="lblSendEmail" runat="server" Text="Send" AssociatedControlID="imgSendEmail"
                                                Style="cursor: pointer;"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                </div>
        </asp:Panel>
    </div>
</div>
