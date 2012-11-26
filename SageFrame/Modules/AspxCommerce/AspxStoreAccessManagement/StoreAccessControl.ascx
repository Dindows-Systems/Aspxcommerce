<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreAccessControl.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxStoreAccessManagement_StoreAccessControl" %>

<script type="text/javascript">
    //<![CDATA[
       var lblStoreAccessValueID = '<%=LblStoreAccessValueID %>';
       var lblAddEditStoreAccessTitleID = '<%=LblAddEditStoreAccessTitleID %>';
    //]]>
</script>

<script type="text/javascript">
    $(function() {
        $('#txtsrchEmail').autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: aspxservicePath + "AspxCommerceWebService.asmx/SearchStoreAccess",
                    data: JSON2.stringify({ text: request.term, keyID: $('input[ name="Email"]').val() }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function(data) { return data; },
                    success: function(data) {
                        response($.map(data.d, function(item) {
                            return {
                                value: item.StoreAccessData
                            };
                        }));
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus);
                    }
                });
            },
            minLength: 2
        });
        $('#txtStoreAccessValue').autocomplete("disable");

    });
</script>

<div class="cssClassTabPanelTable">
    <div id="dvTabPanel">
        <ul>
            <li><a href="#dvIP">
                <asp:Label ID="lblIP" runat="server" Text="IP"></asp:Label></a></li>
            <li><a href="#dvDomain">
                <asp:Label ID="lblDomain" runat="server" Text="Domain"></asp:Label></a></li>
            <li><a href="#dvEmail">
                <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></a></li>
            <li><a href="#dvCreditCard">
                <asp:Label ID="lblCreditCard" runat="server" Text="Credit Card"></asp:Label></a></li>
            <li><a href="#dvCustomer">
                <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></a></li>
        </ul>
        <div id="dvIP">
            <div class="cssClassIp">
                <div id="div1">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeaderIP" runat="server" CssClass="cssClassLabel" Text=" List of IP's Blocked"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="cssClassButtonWrapper">
                                    <p>
                                        <button type="button" id="btnDeleteSelectedIP">
                                            <span><span>Delete All Selected</span> </span>
                                        </button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnAddIP">
                                            <span><span>Add New IP</span></span></button>
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
                                                    IP:</label>
                                                <input type="text" id="txtsrchIP" class="cssClassTextBoxSmall" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel">
                                                    AddedOn:</label>
                                                <span class="label">From :</span>
                                                <input type="text" id="txtsrchIPDate" class="cssClassNormalTextBox" />
                                                <span class="label">To :</span>
                                                <input type="text" id="txtIPEndDate" class="cssClassNormalTextBox" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel">
                                                    Status:
                                                </label>
                                                <select id="SelectStatusIP">
                                                    <option value="">-- All -- </option>
                                                    <option value="True">Active </option>
                                                    <option value="False">Inactive </option>
                                                </select>
                                            </td>
                                            <td>
                                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                                    <p>
                                                        <button type="button" onclick="StoreAccessmanage.searchStoreAccess(this)" id="btnSrchIP">
                                                            <span><span>Search</span></span></button>
                                                    </p>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="loading">
                                    <img id="ajaxStoreAccessImage1" src="" alt="loading...." title="loading...." />
                                </div>
                                <div class="log">
                                </div>
                                <table id="gdvIP" cellspacing="0" cellpadding="0" border="0" width="100%">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvDomain" style="display: none">
            <div class="cssClassDomain">
                <div id="div2">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeadingDomain" runat="server" CssClass="cssClassLabel" Text=" List of Domains Blocked"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="cssClassButtonWrapper">
                                    <p>
                                        <button type="button" id="btnDeleteSelectedDomain">
                                            <span><span>Delete All Selected</span> </span>
                                        </button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnAddDomain">
                                            <span><span>Add New Domain</span></span></button>
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
                                                    Domain:</label>
                                                <input type="text" id="txtsrchDomain" class="cssClassTextBoxSmall" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel">
                                                    AddedOn:</label>
                                                <span class="label">From :</span>
                                                <input type="text" id="txtsrchDomainDate" class="cssClassNormalTextBox" />
                                                <span class="label">To :</span>
                                                <input type="text" id="txtDomainEndDate" class="cssClassNormalTextBox" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel">
                                                    Status:
                                                </label>
                                                <select id="SelectStatusDomain">
                                                    <option value="">-- All -- </option>
                                                    <option value="True">Active </option>
                                                    <option value="False">Inactive </option>
                                                </select>
                                            </td>
                                            <td>
                                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                                    <p>
                                                        <button type="button" onclick="StoreAccessmanage.searchStoreAccess(this)" id="btnSrchDomain">
                                                            <span><span>Search</span></span></button>
                                                    </p>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="loading">
                                    <img id="ajaxStoreAccessImage5" src="" alt="loading...." title="loading...." />
                                </div>
                                <div class="log">
                                </div>
                                <table id="gdvDomain" cellspacing="0" cellpadding="0" border="0" width="100%">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvEmail" style="display: none">
            <div class="cssClassEmail">
                <div id="div3">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeadingEmail" runat="server" CssClass="cssClassLabel" Text="List of Email IDs Blocked"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="cssClassButtonWrapper">
                                    <p>
                                        <button type="button" id="btnDeleteSelectedEmail">
                                            <span><span>Delete All Selected</span> </span>
                                        </button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnAddEmail">
                                            <span><span>Add New Email</span></span></button>
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
                                                    Email:</label>
                                                <input type="text" id="txtsrchEmail" class="cssClassTextBoxSmall" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel">
                                                    AddedOn:</label>
                                                <span class="label">From :</span>
                                                <input type="text" id="txtsrchEmailDate" class="cssClassNormalTextBox" />
                                                <span class="label">To :</span>
                                                <input type="text" id="txtEmailEndDate" class="cssClassNormalTextBox" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel">
                                                    Status:
                                                </label>
                                                <select id="SelectStatusEmail">
                                                    <option value="">-- All -- </option>
                                                    <option value="True">Active </option>
                                                    <option value="False">Inactive </option>
                                                </select>
                                            </td>
                                            <td>
                                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                                    <p>
                                                        <button type="button" onclick="StoreAccessmanage.searchStoreAccess(this)" id="btnSrchEmail">
                                                            <span><span>Search</span></span></button>
                                                    </p>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="loading">
                                    <img id="ajaxStoreAccessImage4" src="" alt="loading...." title="loading...." />
                                </div>
                                <div class="log">
                                </div>
                                <table id="gdvEmail" cellspacing="0" cellpadding="0" border="0" width="100%">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvCreditCard" style="display: none">
            <div class="cssClassCreditCard">
                <div id="div4">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeadingCreditCard" runat="server" CssClass="cssClassLabel" Text="List of Credit Cards Blocked"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="cssClassButtonWrapper">
                                    <p>
                                        <button type="button" id="btnDeleteSelectedCreditCard">
                                            <span><span>Delete All Selected</span> </span>
                                        </button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnAddCreditCard">
                                            <span><span>Add New Credit Card</span></span></button>
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
                                                    Credit Card No:</label>
                                                <input type="text" id="txtsrchCreditCard" class="cssClassTextBoxSmall" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel">
                                                    AddedOn:</label>
                                                <span class="label">From :</span>
                                                <input type="text" id="txtsrchCreditCardDate" class="cssClassNormalTextBox" />
                                                <span class="label">To :</span>
                                                <input type="text" id="txtCardEndDate" class="cssClassNormalTextBox" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel">
                                                    Status:
                                                </label>
                                                <select id="SelectStatusCreditCard">
                                                    <option value="">-- All -- </option>
                                                    <option value="True">Active </option>
                                                    <option value="False">Inactive </option>
                                                </select>
                                            </td>
                                            <td>
                                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                                    <p>
                                                        <button type="button" onclick="StoreAccessmanage.searchStoreAccess(this)" id="btnSrchCreditCard">
                                                            <span><span>Search</span></span></button>
                                                    </p>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="loading">
                                    <img id="ajaxStoreAccessImage3" src="" alt="loading...." title="loading...." />
                                </div>
                                <div class="log">
                                </div>
                                <table id="gdvCreditCard" cellspacing="0" cellpadding="0" border="0" width="100%">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="dvCustomer" style="display: none">
            <div class="cssClassCustomer">
                <div id="div6">
                    <div class="cssClassCommonBox Curve">
                        <div class="cssClassHeader">
                            <h2>
                                <asp:Label ID="lblHeadingCustomer" runat="server" CssClass="cssClassLabel" Text="List of Customers Blocked"></asp:Label>
                            </h2>
                            <div class="cssClassHeaderRight">
                                <div class="cssClassButtonWrapper">
                                    <p>
                                        <button type="button" id="btnDeleteSelectedCustomer">
                                            <span><span>Delete All Selected</span> </span>
                                        </button>
                                    </p>
                                    <p>
                                        <button type="button" id="btnAddCustomer">
                                            <span><span>Add New Customer</span></span></button>
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
                                                    Customer Name:</label>
                                                <input type="text" id="txtsrchCustomer" class="cssClassTextBoxSmall" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel">
                                                    AddedOn:</label>
                                                <span class="label">From :</span>
                                                <input type="text" id="txtsrchCustomerDate" class="cssClassNormalTextBox" />
                                                <span class="label">To :</span>
                                                <input type="text" id="txtCustomerDate" class="cssClassNormalTextBox" />
                                            </td>
                                            <td>
                                                <label class="cssClassLabel">
                                                    Status:
                                                </label>
                                                <select id="SelectStatusCustomer">
                                                    <option value="">-- All -- </option>
                                                    <option value="True">Active </option>
                                                    <option value="False">Inactive </option>
                                                </select>
                                            </td>
                                            <td>
                                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                                    <p>
                                                        <button type="button" onclick="StoreAccessmanage.searchStoreAccess(this)" id="btnSrchCustomer">
                                                            <span><span>Search</span></span></button>
                                                    </p>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="loading">
                                    <img id="ajaxStoreAccessImage2" src="" alt="loading...." title="loading...." />
                                </div>
                                <div class="log">
                                </div>
                                <table id="gdvCustomer" cellspacing="0" cellpadding="0" border="0" width="100%">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="popupbox" id="popuprel">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
    <div id="editAdd">
        <div class="cssClassCommonBox Curve">
            <div class="cssClassHeader">
                <h3>
                    <asp:Label ID="lblAddEditStoreAccessTitle" runat="server" Text=""></asp:Label>
                </h3>
            </div>
            <div class="cssClassFormWrapper">
                <table border="0" width="100%" id="tblAddEditStoreAccessForm" class="cssClassPadding">
                    <tr id="forIPonly">
                        <td>
                            <asp:Label ID="lblStoreAccessValue" Text="" runat="server" CssClass="cssClassLabel"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type="text" id="txtStoreAccessValue" name="txtNameValidate" class="cssClassNormalTextBox required" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblReason" Text="Reason:" runat="server" CssClass="cssClassLabel"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <textarea id="txtReason" cols="30" rows="6" name="msg" class="cssClassTextarea required"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStatus" Text="Status:" runat="server" CssClass="cssClassLabel"></asp:Label>
                            <span class="cssClassRequired">*</span>
                        </td>
                        <td class="cssClassTableRightCol">
                            <input type="radio" id="chkStatusActive" class="cssClassRadioBtn" name="status" checked="checked" />
                            <span>Active</span>
                            <input type="radio" id="chkStarusDisActive" class="cssClassRadioBtn" name="status" />
                            <span>Inactive</span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="cssClassButtonWrapper">
                <p>
                    <button type="button" id="btnSubmit" class="cssClassButtonSubmit">
                        <span><span>Save</span></span></button>
                </p>
                <p>
                    <button type="button" id="btnCancelSaveUpdate" class="cssClassButtonSubmit">
                        <span><span>Cancel</span></span></button>
                </p>
            </div>
        </div>
    </div>
</div>
<div id="hdnField">
</div>
