<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerByNoOfOrder.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxCustomerManagement_CustomerByNoOfOrderl" %>

<script type="text/javascript">
    //<![CDATA[
    var storeId, portalId, userName, cultureName, customerId, ip, countryName, sessionCode, userFriendlyURL;


    $(document).ready(function() {
        storeId = AspxCommerce.utils.GetStoreID();
        portalId = AspxCommerce.utils.GetPortalID();
        userName = AspxCommerce.utils.GetUserName();
        cultureName = AspxCommerce.utils.GetCultureName();
        customerId = AspxCommerce.utils.GetCustomerID();
        ip = AspxCommerce.utils.GetClientIP();
        countryName = AspxCommerce.utils.GetAspxClientCoutry();
        sessionCode = AspxCommerce.utils.GetSessionCode();
        userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
        BindCustomerByNumberOrder();
        LoadCustomerByNoStaticImage();
        $("#btnExportToCSV").click(function() {
            $('#gdvCustomerByNumberOrder').table2CSV();
        });
    }); 

    function LoadCustomerByNoStaticImage() {
        $('#ajaxCustomerByNoOfOrderImage').attr('src', '' + aspxTemplateFolderPath + '/images/ajax-loader.gif');
    }
    
    function BindCustomerByNumberOrder(user) {
        var offset_ = 1;
        var current_ = 1;
        var perpage = ($("#gdvCustomerByNumberOrder_pagesize").length > 0) ? $("#gdvCustomerByNumberOrder_pagesize :selected").text() : 10;

        $("#gdvCustomerByNumberOrder").sagegrid({
            url: aspxservicePath + "AspxCommerceWebService.asmx/",
            functionMethod: 'GetCustomerOrderTotal',
            colModel: [
            //{ display: 'ItemID', name: 'itemId', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    {display: 'Customer Name', name: 'customer_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Number Of Orders', name: 'number_of_Orders', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Average Order Amount', name: 'average_order', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Total Order Amount', name: 'total_order', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center', hide: true }
    				],

            buttons: [
            //{ display: 'ShowReviews', name: 'showReviews', enable: true, _event: 'click', trigger: '1', callMethod: 'ShowItemReviewsList', arguments: '1,' },
    			    ],
            rp: perpage,
            nomsg: "No Records Found!",
            param: { storeID: storeId, portalID: portalId, cultureName: cultureName, user: user },
            current: current_,
            pnew: offset_,
            sortcol: { 4: { sorter: false} }
        });
    }

    function ExportDivDataToExcel() {
        var headerArr = $("#gdvCustomerByNumberOrder thead tr th");
        var header = "<tr>";
        $.each(headerArr, function() {
            if (!$(this).hasClass("cssClassAction")) {
                header += '<th>' + $(this).text() + '</th>';
            }
        });
        header += '</tr>'
        var data = $("#gdvCustomerByNumberOrder tbody tr");
        // var table = $("#Export1_lblTitle").text();
        var table = '<table>';
        table += header;
        $.each(data, function(index, item) {
            var cells = $(this).find("td");
            var td = "";
            $.each(cells, function(i, itm) {

                if ($(this).find("div").hasClass("cssClassActionOnClick")) {
                    //do not add
                }
                else {
                    td += '<td>' + $(this).text() + '</td>';
                }
            });
            table += '<tr>' + td + '</tr>';
        });

        table += '</tr></table>';
        table = $.trim(table);
        table = table.replace(/>/g, '&gt;');
        table = table.replace(/</g, '&lt;');
        $("input[id$='HdnValue']").val(table);
    }
    function SearchCustomerByNumberOrders() {
        var UserName = $.trim($("#txtSearchUserName").val());
        if (UserName.length < 1) {
            UserName = null;
        }
        BindCustomerByNumberOrder(UserName);
    }
    //]]>
</script>

<div id="divCustomerByNumberOrder">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblReviewHeading" runat="server" Text="Customer By Number Of Order"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnExportToExcel" CssClass="cssClassButtonSubmit" runat="server"
                            OnClick="Button1_Click" Text="Export to Excel" OnClientClick="ExportDivDataToExcel()" />
                    </p>
                    <p>
                        <button type="button" id="btnExportToCSV">
                            <span><span>Export to CSV</span></span></button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="cssClassSearchPanel cssClassFormWrapper">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <label class="cssClassLabel">
                                    User Name:</label>
                                <input type="text" id="txtSearchUserName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" onclick="SearchCustomerByNumberOrders()">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxCustomerByNoOfOrderImage" src=""  alt="loading...." />
                </div>
                <div class="log">
                </div>
                <table id="gdvCustomerByNumberOrder" width="100%" border="0" cellpadding="0" cellspacing="0">
                </table>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
