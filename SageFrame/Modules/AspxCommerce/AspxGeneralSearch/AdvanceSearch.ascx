<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvanceSearch.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxGeneralSearch_AdvanceSearch" %>

<script type="text/javascript">
    //<![CDATA[
    var noImageAdSearchPathSetting = '<%=NoImageAdSearchPath %>';
    var allowOutStockPurchaseSetting = '<%=AllowOutStockPurchase %>';
    var allowWishListAdvSearchSetting = '<%=AllowWishListAdvSearch %>';                             
//]]>                              
</script>

<div class="cssClassAdvenceSearch">
    <div class="cssClassHeader">
        <h2>
            <asp:Label ID="lblTitle" runat="server" Text="Advance Search:"></asp:Label>
        </h2>
    </div>
    <div class="cssClassFormWrapper">
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="tdpadding">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblSearchFor" runat="server" Text="Search For:" CssClass="cssClassLabel cssClasssearchFor"></asp:Label>
                    <input type="text" id="txtSearchFor" class="cssClassNormalTextBox searchForTextBox" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblSearchIn" runat="server" Text="Search In:" CssClass="cssClassLabel"></asp:Label>
                    <div id="divCheckBox">
                        <ul>
                        </ul>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="cssClassLabel"></asp:Label>
                    <select id="ddlCategory" class="">
                    </select>
                </td>
                <td>
                    <span class="pricebox">
                        <asp:Label ID="lblPriceIn" runat="server" Text="Price:" CssClass="cssClassLabel"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <span>From:</span><input class="cssClassNormalTextBox" id="txtPriceFrom" type="text" /><span
                            id="errmsgPriceFrom"></span> <span>To:</span><input class="cssClassNormalTextBox"
                                id="txtPriceTo" type="text" /><span id="errmsgPriceTo"></span> </span>
                </td>
            </tr>
        </table>
    </div>
    <div class="cssClassButtonWrapper cssClassPaddingNone">
        <p>
            <button type="button" id="btnAdvanceSearch">
                <span><span>Search</span></span></button>
        </p>
    </div>
</div>
<div id="divAjaxLoader">
    <img id="ajaxAdvanceSearchImage" alt="loading...." />
</div>
<div id="divItemViewOptions" class="viewWrapper">
    <div id="divViewAs" class="view">
        View as:
        <select id="ddlViewAs" class="cssClassDropDown">
        </select>
    </div>
    <div id="divSortBy" class="sort">
        Sort by:
        <select id="ddlSortBy" class="cssClassDropDown">
        </select>
    </div>
</div>
<div id="divShowAdvanceSearchResult" class="cssClassDisplayResult">
</div>
<div class="cssClassClear">
</div>
<!-- TODO:: paging Here -->
<div class="cssClassPageNumber" id="divSearchPageNumber">
    <div class="cssClassPageNumberLeftBg">
        <div class="cssClassPageNumberRightBg">
            <div class="cssClassPageNumberMidBg">
                <div id="Pagination">
                </div>
                <div class="cssClassViewPerPage">
                    View Per Page
                    <select id="ddlPageSize" class="cssClassDropDown">
                        <option value="5">5</option>
                        <option value="10">10</option>
                        <option value="15">15</option>
                        <option value="20">20</option>
                        <option value="25">25</option>
                        <option value="40">40</option>
                    </select></div>
                <%--<table width="84%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="40">
                            <a href="#">Prev</a>
                        </td>
                        <td>
                            <span>1</span> <a href="#">2</a> <a href="#">3</a> <a href="#">4</a> <a href="#">5</a>
                            <a href="#">6</a> <a href="#">7</a> <a href="#">8</a> <a href="#">9</a> <a href="#">
                                10</a> <a href="#">11</a> <a href="#">12</a> <a href="#">13</a> <a href="#">14</a>
                            <a href="#">15</a> <a href="#">16</a> <a href="#">17</a> <a href="#">18</a> <a href="#">
                                19</a> <a href="#">20</a>
                        </td>
                        <td width="40">
                            <a href="#">Next</a>
                        </td>
                    </tr>
                </table>--%>
            </div>
        </div>
    </div>
</div>
