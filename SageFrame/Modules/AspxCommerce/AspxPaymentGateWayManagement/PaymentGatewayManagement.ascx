<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaymentGatewayManagement.ascx.cs"
    Inherits="Modules_PaymentGatewayManagement_PaymentGatewayManagement" %>

<script type="text/javascript">
    //<![CDATA[
    var errorCode = '<%=ErrorCode %>';
    var lblLoadMessage = "<%=lblLoadMessage.ClientID %>";
    var lblPaymentGateWay = "<%=lblPaymentGateWay.ClientID %>";
    var lblPaymentGatewayEdit = "<%=lblPaymentGatewayEdit.ClientID %>";
    var urlPath = "<%=pageURL %>";
    var lblOrderDetailForm = "<%=lblOrderDetailForm.ClientID %>";
    //]]>
</script>

<div id="divPaymentGateWayManagement">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="PaymentGatewayManagement" runat="server" Text="Payment Methods"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper" style="display:none;">
                    <p>
                        <button type="button" id="btnDeletePayMethod">
                            <span><span>Delete All Selected</span></span></button>
                    </p>
                    <p>
                        <button type="button" id="btnAddNewPayGateWay">
                            <span><span>Add New Payment Method</span></span></button>
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
                                    Payment Gateway Name:</label>
                                <input type="text" id="txtSearchPaymentGateWayName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    IsActive:</label>
                                <select id="ddlIsActive" class="cssClassDropDown">
                                    <option value="">--All--</option>
                                    <option value="0">True</option>
                                    <option value="1">False</option>
                                </select>
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnSearchPaymentgateway">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxPaymentGateWayImage2" src="" title="loading...." alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvPaymentGateway" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divPaymentGateWayForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblPaymentGateWay" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table border="0" width="100%" id="tblPaymentGatewayForm" class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblLoadMessage" runat="server" CssClass="cssClassRed"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:FileUpload ID="fuPGModule" runat="server" />
                        <asp:Panel ID="pnlRepair" runat="server" Visible="true">
                            <asp:CheckBox ID="chkRepairInstall" runat="server" CssClass="cssClassCheckBox" />
                            <asp:Label ID="lblRepairInstallHelp" runat="server" CssClass="cssClassHelpTitle" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="btnBackFromAddNetPaymentForm" type="button" value="Back" class="cssClassButtonSubmit" />
                        <asp:Button ID="btnAddNew" runat="server" Text="Save" OnClick="btnAddNew_Click" class="cssClassButtonSubmit" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>

<div id="divPaymentGatewayEditForm" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblPaymentGatewayEdit" runat="server"></asp:Label>
            </h2>
        </div>
        <div class="cssClassFormWrapper">
            <table border="0" width="100%" id="tblPaymentGatewayEdit" class="cssClassPadding">
                <tr>
                    <td>
                        <asp:Label ID="lblGatewayName" Text="PaymentGatewayName:" runat="server" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="text" id="txtPaymentGatewayName" class="cssClassNormalTextBox" />
                    </td>
                </tr>
                <tr id="isActive" style="display:none;">
                    <td>
                        <asp:Label ID="lblIsActive" Text="Is Active:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="checkbox" id="chkIsActive" class="cssClassCheckBox" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIsUse" Text="Is Use:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td class="cssClassTableRightCol">
                        <input type="checkbox" id="chkIsUse" class="cssClassCheckBox" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnCancelPayEdit">
                    <span><span>Cancel</span></span></button>
            </p>
            <p>
                <button type="button" id="btnSubmitPayEdit">
                    <span><span>Save</span></span></button>
            </p>
             <p id="delete" style="display:none;">
                <button type="button" id="btnDeletePay">
                <span><span>Delete</span></span></button>
            </p>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
  <input type="hidden" id="hdnPaymentGatewayIDView" />
  <input type="hidden" id="hdnPaymentGatewayID" />
</div>
<div id="popuprel2" class="popupbox adminpopup" style="display:none">
</div>
<div id="divPaymentEdit">
</div>
<div id="divOrderDetailForm" style="display: none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblOrderDetailForm" runat="server" Text="Order Details"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <asp:Button ID="btnSavePDFForm2" runat="server" Text="Save As Pdf" OnClick="btnSavePDFForm2_Click"
                            OnClientClick="PaymentGatewayManage.GenerateOrderDetailsPDF()" CssClass="cssClassButtonSubmit" />
                    </p>
                    <p>
                        <button type="button" id="btnPrint">
                            <span><span>Print</span></span></button>
                    </p>
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
        </div>
        <div id="divPrintOrderDetail" class="cssClassFormWrapper">
            <div class="cssItemHeadingDetail">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tbody>                    
                        <tr >
                            <td valign="top">
                                <br />
                                <b><span class="cssClassLabel">Ordered Date: </span></b><span id="OrderDate"></span>
                                <br />
                                <b><span class="cssClassLabel">Store Name: </span></b><span id="storeName"></span><br />
                                <b><span class="cssClassLabel">Store Description: </span></b><span id="storeDescription"></span><br />
                                <b><span class="cssClassLabel">Payment Gateway Type: </span></b><span id="PaymentGatewayType">
                                </span>
                                <br />
                                <b><span class="cssClassLabel">Payment Method: </span></b><span id="PaymentMethod">
                                </span>
                            </td>
                            <td>
                                <br />
                                <div class="cssClassLabel" id="divBillingAddressDetail">
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="cssClassCommonBox Curve">
                <div class="cssClassHeader">
                    <h2 style="font-size:14px">
                        Ordered Items:</h2>
                </div>
                <div class="cssClassGridWrapper">
                    <div id="orderItemDetail" class="cssClassGridWrapperContent">
                        <span class="remarks"></span>
                    </div>
                </div>
            </div>
        </div>
        <div class="cssClassButtonWrapper">
            <p>
                <button type="button" id="btnBackOrder">
                    <span><span>Back</span></span></button>
            </p>
        </div>
    </div>
</div>
<asp:HiddenField ID="HdnValue" runat="server" />
<div id="divPaymentGateWayManagementEdit" style="display:none">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblPaymentGatewayManagement" runat="server" Text="Order Lists"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <p>
                        <button type="button" id="btnBackPaymentEdit">
                            <span><span>Back</span></span></button>
                    </p>
                  <%--  <p>
                        <button type="button" id="Button1">
                            <span><span>Delete All Selected</span></span></button>
                    </p>--%>
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
                                    Bill To Name:</label>
                                <input type="text" id="txtSearchBillToName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Ship To Name:</label>
                                <input type="text" id="txtSearchShipToName" class="cssClassTextBoxSmall" />
                            </td>
                            <td>
                                <label class="cssClassLabel">
                                    Order Status:</label>
                                <select id="ddlOrderStatus" class="cssClassDropDown">
                                    <option value="0">--All--</option>
                                </select>
                            </td>
                            <td>
                                <div class="cssClassButtonWrapper cssClassPaddingNone">
                                    <p>
                                        <button type="button" id="btnSearchOrders">
                                            <span><span>Search</span></span></button>
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="loading">
                    <img id="ajaxPayementGatewayImage" src="" title="loading...." alt="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvPaymentGatewayEdit" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>

 <div id="dvTransactionDetail" class="cssClassFormWrapper" style="display:none;" >  
     <div class="cssClassCommonBox Curve"> 
         <div class="cssClassHeader">
             <h2>
                 <asp:Label ID="lblTransactionLogDetail" runat="server" Text="Transaction Detail "></asp:Label>
             </h2>
         </div>
         <span id="spanNodata" class="cssNodata"></span> 
          <div id="divTransactionDetail"> 
         <table cellspacing="0" cellpadding="0" border="0" width="100%">
             <tr>
                 <td>
                     <asp:Label ID="lblTpaymentGateway" runat="server" Text="Payment Gateway Name:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindPName" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTorderID" runat="server" Text="OrderID:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindOrderId" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTtransactionId" runat="server" Text="Transaction ID:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindtransactionId" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTstatus" runat="server" Text="Order Completed AS:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindstatus" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTtotal" runat="server" Text="Order Amount:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindtotal" class="cssClassLabel cssClassFormatCurrency">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTpayerEmail" runat="server" Text="Payer Email:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindpayerEmail" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTcreditCard" runat="server" Text="CreditCard No:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindcreditCard" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTauthCode" runat="server" Text="Auth Code:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindAuthCode" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTresponsetext" runat="server" Text="Response Text:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindresponseText" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTCustomerName" runat="server" Text="Customer Name:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindCustomerName" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTcustomerID" runat="server" Text="CustomerID:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindcustomerId" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTsessionCode" runat="server" Text="Session Code:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindsessionCode" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblTAddedDate" runat="server" Text="Added On:" CssClass="cssClassLabel"></asp:Label>
                 </td>
                 <td>
                     <label id="lblBindAddedon" class="cssClassLabel">
                     </label>
                 </td>
             </tr>
         </table>        
         </div>
          <div class="cssClassButtonWrapper">
             <p>
                 <button type="button" id="btnBacktoOrderView">
                     <span><span>Back</span></span></button>
             </p>
         </div>
     </div>
</div>
<asp:HiddenField ID="hdnDescriptionValue" runat="server" />