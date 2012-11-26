<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctl_SageSearchResult.ascx.cs"
    Inherits="Modules_SageSearch_ctl_SageSearchResult" %>

<div class="cssClassSearchPageWrapperBox">
    <div class="cssClassSearchPageWrapperBoxBtnBg">
        <div class="cssClassSearchPageWrapperBoxMidBg cssClassSearchPageWrapperBoxMidBgPadding">
            <asp:Label ID="lblSeachLable" Text="Search Keyword" runat="server"></asp:Label>
            <asp:Label ID="lblSearchKeyword" runat="server" CssClass="searchkey"></asp:Label>
        </div>
    </div>
</div>
<div class="cssClassSearchPageWrapper">
    <div class="cssClassSearchPageWrapperBox">
        <div class="cssClassSearchPageWrapperBoxBtnBg">
            <div class="cssClassSearchPageWrapperBoxMidBg">
                <div class="cssClassSearchFormWrapper">
                    <ul>
                        <li>
                            <asp:Label ID="lblOrdering" runat="server" Text="Ordering:" CssClass="cssClassFormLabel"
                                ToolTip="Select one of them"></asp:Label>
                        </li>
                        <li>
                            <asp:RadioButtonList ID="rblOrdering" ToolTip="Select one of them." RepeatLayout="Table"
                                runat="server" RepeatColumns="4" RepeatDirection="Horizontal" CssClass="cssClassDropDown" ValidationGroup="sage_searchresultfilter">
                            </asp:RadioButtonList>
                        </li>
                    </ul>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <ul class="searchonly">
                                    <li>
                                        <asp:Label ID="lblSearchOnly" runat="server" Text="Search Only:" CssClass="cssClassFormLabel"
                                            ToolTip="Select one of them"></asp:Label>
                                    </li>
                                    <li>
                                        <asp:CheckBoxList ID="cblResultSection" ToolTip="Chek those boxs that you want."
                                            RepeatLayout="Table" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                            CssClass="cssClassDropDown" ValidationGroup="sage_searchresultfilter">
                                        </asp:CheckBoxList>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ul class="searchpagedisplay">
                                    <li>
                                        <asp:Label ID="lblDisplay" runat="server" Text="Display:" CssClass="cssClassFormLabel"
                                            ToolTip="Select one of them"></asp:Label>
                                    </li>
                                    <li>
                                        <asp:DropDownList ID="ddlGridPager" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGridPager_SelectedIndexChanged" ValidationGroup="sage_searchresultfilter">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="75">75</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                    </table>
                    <div class="cssClassButtonWrapper">
                        <asp:ImageButton ID="imbFilter" runat="server" OnClick="imbFilter_Click" CausesValidation="true" ValidationGroup="sage_searchresultfilter"/>
                        <asp:Label ID="lblFilter" runat="server" Text="Filter" AssociatedControlID="imbFilter"
                            Style="cursor: pointer;"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="cssClassSearchPageWrapperBox">
        <div class="cssClassSearchPageWrapperBoxBtnBg">
            <div class="cssClassSearchPageWrapperBoxMidBg">
                <div class="cssClassSearchGridWrapper">
                    <asp:GridView Width="100%" runat="server" ID="gdvList" AutoGenerateColumns="False"
                        AllowPaging="True" EmptyDataText=".........No result found........." OnPageIndexChanging="gdvList_PageIndexChanging"
                        ShowHeader="False">
                        <EmptyDataRowStyle CssClass="cssClassNotFound" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <h2>
                                        <a href='<%# Eval("ResultUrl")%>' class="cssClassResultRedirect">
                                            <asp:Label ID="lblResultTitle" runat="server" Text='<%# Eval("ResultTitle")%>' CssClass="cssClassResultTitle"></asp:Label>
                                        </a>
                                    </h2>
                                    <h6>
                                        <asp:Label ID="lblResultSection" runat="server" Text='<%# Eval("ResultSection")%>'
                                            CssClass="cssClassResultSection"></asp:Label>
                                    </h6>
                                    <p>
                                        <asp:Label ID="lblResultDetail" runat="server" Text='<%# FormatResult(Eval("ResultDetail").ToString())%>'
                                            CssClass="cssClassResultDetail"></asp:Label>
                                    </p>
                                    <span class="cssClassUrlLink"><a href='<%# Eval("ResultUrl")%>' class="cssClassResultRedirect">
                                        '<%# Eval("ResultUrl")%>'</a></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="cssClassSearchPageNoOfPages" />
                        <HeaderStyle CssClass="cssClassHeadingOne" />
                        <RowStyle CssClass="cssClassAlternativeOdd" />
                        <AlternatingRowStyle CssClass="cssClassAlternativeEven" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</div>