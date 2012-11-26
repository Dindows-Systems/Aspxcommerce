<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_SqlQueryAnalyser.ascx.cs"
    Inherits="SageFrame.Modules.Admin.SQL.ctl_SqlQueryAnalyser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<h2 class="cssClassFormHeading">
        <asp:Label ID="lblSqlQueryAnalyser" runat="server" Text="SQL Query Analyser"></asp:Label></h2>
<div class="cssClassFormWrapper">    
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>&nbsp;
                
            </td>
            <td class="cssClassSqlPage">&nbsp;
                
            </td>
            <td>&nbsp;
                
            </td>
        </tr>
        <tr>
            <td width="30%">
                <asp:Label ID="lblSelectSqlScriptFile" runat="server" CssClass="cssClassFormLabel"
                    Text="SQL File:" ToolTip="Upload a file into the SQL Query window (Optional)."></asp:Label>
            </td>
            <td class="cssClassSqlPage cssClassButtonWrapper">
                <asp:FileUpload ID="fluSqlScript" runat="server" />
                <asp:ImageButton ID="imbUploadSqlScript" runat="server" OnClick="imbUploadSqlScript_Click"
                    ToolTip="Load the selected file." ImageUrl='<%# GetTemplateImageUrl("imgload.png", true) %>' />
                <asp:Label ID="lblUploadSqlScript" runat="server" Text="Load" AssociatedControlID="imbUploadSqlScript"
                    Style="cursor: pointer;"></asp:Label>
            </td>
            <td width="50%">&nbsp;
                
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtSqlQuery" runat="server" TextMode="MultiLine" Rows="10" CssClass="cssClassTextArea1"
                    EnableViewState="false"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>
<div class="cssClassButtonWrapper">
    <asp:ImageButton ID="imbExecuteSql" runat="server" OnClick="imbExecuteSql_Click"
        ToolTip="can include {directives} and /*comments*/" ImageUrl='<%# GetTemplateImageUrl("imgexecute.png", true) %>' />
    <asp:Label ID="lblExecuteSql" runat="server" Text="Execute" AssociatedControlID="imbExecuteSql"
        CssClass="cssClassFormLabel"></asp:Label>
    <asp:CheckBox ID="chkRunAsScript" runat="server" Text="Run as Script" TextAlign="Left"
        ToolTip="include 'GO' directives; for testing &amp; update scripts" CssClass="cssClassCheckBox cssClassButtonCheck"/>
</div>
<div class="cssClassGridWrapper">
    <asp:GridView ID="gdvResults" runat="server" AutoGenerateColumns="True" EnableViewState="False">
        <EmptyDataTemplate>
            <asp:Label ID="lblEmptyText" runat="server" Text="The query did not return any data" />
        </EmptyDataTemplate>
        <HeaderStyle CssClass="cssClassHeadingOne" />
        <RowStyle CssClass="cssClassAlternativeOdd" />
        <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
    </asp:GridView>
</div>
