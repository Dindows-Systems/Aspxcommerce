<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountDashboard.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxUserDashBoard_AccountDashboard" %>

<script type="text/javascript">
    //<![CDATA[
  
    var AccountDashboard = '';
    $(function() {
        AccountDashboard = {
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: AspxCommerce.utils.GetAspxServicePath(),
                method: "",
                url: "",
                ajaxCallMode: 0,
                error: 0
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: AccountDashboard.config.type,
                    contentType: AccountDashboard.config.contentType,
                    cache: AccountDashboard.config.cache,
                    async: AccountDashboard.config.async,
                    url: AccountDashboard.config.url,
                    data: AccountDashboard.config.data,
                    dataType: AccountDashboard.config.dataType,
                    success: AccountDashboard.ajaxSuccess,
                    error: AccountDashboard.ajaxFailure
                });
            }, GetAddressBookDetails: function() {
                this.config.method = "AspxCommerceWebService.asmx/GetAddressBookDetails";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, userName: userName, cultureName: cultureName });
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },
            ajaxSuccess: function(msg) {
                switch (AccountDashboard.config.ajaxCallMode) {
                    case 1:
                        $('#ddlUSState').show();
                        $('#txtState').hide();
                        $("#ddlUSState").html('');
                        $.each(msg.d, function(index, item) {
                            $("#ddlUSState").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                        });
                        break;
                    case 2:
                        var elements = '';
                        var tableElements = '';
                        var grandTotal = '';
                        var couponAmount = '';
                        var taxTotal = '';
                        var shippingCost = '';
                        var discountAmount = '';
                        $.each(msg.d, function(index, value) {
                            if (index < 1) {
                                var billAdd = '';
                                var arrBill;
                                arrBill = value.BillingAddress.split(',');
                                billAdd += '<li>' + arrBill[0] + ' ' + arrBill[1] + '</li>';
                                billAdd += '<li>' + arrBill[2] + '</li><li>' + arrBill[3] + '</li><li>' + arrBill[4] + '</li><li>' + arrBill[5] + '</li>';
                                billAdd += '<li>' + arrBill[6] + '</li><li>' + arrBill[7] + '</li>' + arrBill[8] + '<li>' + arrBill[9] + '</li><li>' + arrBill[10] + '</li><li>' + arrBill[11] + '</li><li>' + arrBill[12] + '</li>';
                                billAdd += '<li>' + arrBill[13] + '</li>';
                                $("#divOrderDetails").find('ul').html(billAdd);
                                $("#orderedDate").html(value.OrderedDate);
                                $("#paymentGatewayType").html(value.PaymentGatewayTypeName);
                                $("#paymentMethod").html(value.PaymentMethodName);
                            }
                            tableElements += '<tr>';
                            tableElements += '<td>' + value.ItemName + '<br/>' + value.CostVariants + '</td>';
                            tableElements += '<td>' + value.SKU + '</td>';
                            tableElements += '<td>' + value.ShippingAddress + '</td>';
                            tableElements += '<td><span class="cssClassFormatCurrency">' + value.ShippingRate + '</span></td>';
                            tableElements += '<td><span class="cssClassFormatCurrency">' + value.Price + '</span></td>';
                            tableElements += '<td>' + value.Quantity + '</td>';
                            tableElements += '<td><span class="cssClassFormatCurrency">' + value.Price * value.Quantity + '</span></td>';
                            tableElements += '</tr>';

                            if (index == 0) {
                                taxTotal = '<tr>';
                                taxTotal += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">Tax Total</td>';
                                taxTotal += '<td><span class="cssClassFormatCurrency">' + value.TaxTotal.toFixed(2) + '</span></td>';
                                taxTotal += '</tr>';
                                shippingCost = '<tr>';
                                shippingCost += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">Shipping Cost</td>';
                                shippingCost += '<td><span class="cssClassFormatCurrency">' + value.ShippingRate.toFixed(2) + '</span></td>';
                                shippingCost += '</tr>';
                                discountAmount = '<tr>';
                                discountAmount += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">Discount Amount</td>';
                                discountAmount += '<td><span class="cssClassFormatCurrency">' + value.DiscountAmount.toFixed(2) + '</span></td>';
                                discountAmount += '</tr>';
                                couponAmount = '<tr>';
                                couponAmount += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">Coupon Amount</td>';
                                couponAmount += '<td><span class="cssClassFormatCurrency">' + value.CouponAmount.toFixed(2) + '</span></td>';
                                couponAmount += '</tr>';
                                grandTotal = '<tr>';
                                grandTotal += '<td></td><td></td><td></td><td></td><td></td><td class="cssClassLabel">Grand Total</td>';
                                grandTotal += '<td><span class="cssClassFormatCurrency">' + value.GrandTotal.toFixed(2) + '</span></td>';
                                grandTotal += '</tr>';
                            }
                        });
                        $("#divOrderDetails").find('table>tbody').html(tableElements);
                        $("#divOrderDetails").find('table>tbody').append(taxTotal);
                        $("#divOrderDetails").find('table>tbody').append(shippingCost);
                        $("#divOrderDetails").find('table>tbody').append(discountAmount);
                        $("#divOrderDetails").find('table>tbody').append(couponAmount);
                        $("#divOrderDetails").find('table>tbody').append(grandTotal);
                        AccountDashboard.OrderHideAll();
                        $("#divOrderDetails").show();
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });

                        break;
                    case 3:
                        var defaultBillingAddressElements = '';
                        var defaultShippingAddressElements = '';
                        var addressElements = '';
                        var addressId = 0;
                        var defaultShippingExist = false;
                        var defaultBillingExist = false;
                        $.each(msg.d, function(index, value) {
                            if (value.DefaultBilling && value.DefaultShipping) {
                                addressId = value.AddressID;
                            }

                            if (!defaultShippingExist) {
                                if ((value.DefaultShipping != null || value.DefaultShipping)) {
                                    defaultShippingExist = true;
                                }
                                else {
                                    defaultShippingExist = false;
                                }
                            }

                            if (!defaultBillingExist) {
                                if (value.DefaultBilling != null || value.DefaultBilling) {
                                    defaultBillingExist = true;
                                }
                                else {
                                    defaultBillingExist = false;
                                }
                            }

                            if (value.DefaultBilling || value.DefaultShipping) {
                                if (value.DefaultShipping) {
                                    defaultShippingAddressElements += ' <h3>Default Shipping Address</h3>';
                                    defaultShippingAddressElements += '<p><label name="FirstName">' + value.FirstName + '</label>' + " " + '<label name="LastName">' + value.LastName + '</label><br>';
                                    defaultShippingAddressElements += '<label name="Email">' + value.Email + '</label><br>';
                                    defaultShippingAddressElements += '<label name="Company">' + value.Company + '</label><br/>';
                                    defaultShippingAddressElements += '<label name="Address1">' + value.Address1 + '</label><br>';
                                    defaultShippingAddressElements += '<label name="Address2">' + value.Address2 + '</label><br>';
                                    defaultShippingAddressElements += '<label name="City">' + value.City + '</label><br>';
                                    defaultShippingAddressElements += '<label name="State">' + value.State + '</label><br>';
                                    defaultShippingAddressElements += 'Zip:<label name="Zip">' + value.Zip + '</label><br>';
                                    defaultShippingAddressElements += '<label name="Country">' + value.Country + '</label><br>';
                                    defaultShippingAddressElements += 'P: <label name="Phone">' + value.Phone + '</label><br>M: <label name="Mobile">' + value.Mobile + '</label><br>';
                                    defaultShippingAddressElements += 'F: <label name="Fax">' + value.Fax + '</label><br>';
                                    defaultShippingAddressElements += '<label name="Website">' + value.Website + '</label></p>';
                                    defaultShippingAddressElements += '<p class="cssClassChange"><a href="#" rel="popuprel" name="EditAddress" Flag="1" value="' + value.AddressID + '" Element="Shipping">Change Shipping Address</a></p>';

                                    $("#liDefaultShippingAddress").html(defaultShippingAddressElements);
                                }
                                if (value.DefaultBilling) {
                                    defaultBillingAddressElements += '<h3>Default Billing Address</h3>';
                                    defaultBillingAddressElements += '<p><label name="FirstName">' + value.FirstName + '</label>' + " " + '<label name="LastName">' + value.LastName + '</label><br>';
                                    defaultBillingAddressElements += '<label name="Email">' + value.Email + '</label><br>';
                                    defaultBillingAddressElements += '<label name="Company">' + value.Company + '</label><br/>';
                                    defaultBillingAddressElements += '<label name="Address1">' + value.Address1 + '</label><br>';
                                    defaultBillingAddressElements += '<label name="Address2">' + value.Address2 + '</label><br>';
                                    defaultBillingAddressElements += '<label name="City">' + value.City + '</label><br>';
                                    defaultBillingAddressElements += '<label name="State">' + value.State + '</label><br>';
                                    defaultBillingAddressElements += 'Zip:<label name="Zip">' + value.Zip + '</label><br>';
                                    defaultBillingAddressElements += '<label name="Country">' + value.Country + '</label><br>';
                                    defaultBillingAddressElements += 'P: <label name="Phone">' + value.Phone + '</label><br>M: <label name="Mobile">' + value.Mobile + '</label><br>';
                                    defaultBillingAddressElements += 'F: <label name="Fax">' + value.Fax + '</label><br>';
                                    defaultBillingAddressElements += '<label name="Website">' + value.Website + '</label></p>';
                                    defaultBillingAddressElements += '<p class="cssClassChange"><a href="#" rel="popuprel" name="EditAddress" Flag="1" value="' + value.AddressID + '" Element="Billing">Change Billing Address</a></p>';
                                    $("#liDefaultBillingAddress").html(defaultBillingAddressElements);
                                }
                            }
                        });

                        if (defaultShippingExist) {
                            $("#hdnDefaultShippingExist").val('1');
                        }
                        else {
                            $("#hdnDefaultShippingExist").val('0');
                            $("#liDefaultShippingAddress").html("<span class=\"cssClassNotFound\">You have not set Default Shipping Adresss Yet!</span>");
                        }
                        if (defaultBillingExist) {
                            $("#hdnDefaultBillingExist").val('1');
                        }
                        else {
                            $("#hdnDefaultBillingExist").val('0');
                            $("#liDefaultBillingAddress").html("<span class=\"cssClassNotFound\">You have not set Default Billing Adresss Yet!</span>");
                        }

                        $("a[name='EditAddress']").bind("click", function() {
                            AccountDashboard.ClearAll();
                            $("#hdnAddressID").val($(this).attr("value"));
                            $("#txtFirstName").val($(this).parent('p').prev('p').find('label[name="FirstName"]').text());
                            $("#txtLastName").val($(this).parent('p').prev('p').find('label[name="LastName"]').text());
                            $("#txtEmailAddress").val($(this).parent('p').prev('p').find('label[name="Email"]').text());
                            $("#txtCompanyName").val($(this).parent('p').prev('p').find('label[name="Company"]').text());
                            $("#txtAddress1").val($(this).parent('p').prev('p').find('label[name="Address1"]').text());
                            $("#txtAddress2").val($(this).parent('p').prev('p').find('label[name="Address2"]').text());
                            $("#txtCity").val($(this).parent('p').prev('p').find('label[name="City"]').text());
                            $("#txtZip").val($(this).parent('p').prev('p').find('label[name="Zip"]').text());
                            var countryName = $(this).parent('p').prev('p').find('label[name="Country"]').text();
                            $('#ddlCountry').val($('#ddlCountry option:contains(' + countryName + ')').attr('value'));
                            if ($.trim(countryName) == 'United States') {
                                $('#ddlUSState').show(); $('#txtState').hide();

                                // alert($(this).parent('p').prev('p').find('label[name="State"]').text());
                                if ($(this).parent('p').prev('p').find('label[name="State"]').text() != '' || $(this).parent('p').prev('p').find('label[name="State"]').text() != null)
                                { AccountDashboard.GetStateList(); $('#ddlUSState').val($('#ddlUSState option:contains(' + $(this).parent('p').prev('p').find('label[name="State"]').text() + ')').attr('value')); }

                            } else {
                                $("#txtState").val($(this).parent('p').prev('p').find('label[name="State"]').text());
                            }
                            $("#txtPhone").val($(this).parent('p').prev('p').find('label[name="Phone"]').text());
                            $("#txtMobile").val($(this).parent('p').prev('p').find('label[name="Mobile"]').text());
                            $("#txtFax").val($(this).parent('p').prev('p').find('label[name="Fax"]').text());
                            $("#txtWebsite").val($(this).parent('p').prev('p').find('label[name="Website"]').text());

                            $("#chkShippingAddress").removeAttr("checked");
                            $("#chkBillingAddress").removeAttr("checked");

                            if ($(this).attr("value") == addressId) {

                                $('#trBillingAddress ,#trShippingAddress').hide();

                                $("#chkBillingAddress").attr("disabled", "disabled");
                                $("#chkShippingAddress").attr("disabled", "disabled");
                            }
                            else if ($(this).attr("Flag") == 1) {

                                if ($(this).attr("Element") == "Billing") {

                                    $("#chkBillingAddress").attr("disabled", "disabled");
                                    $("#chkShippingAddress").removeAttr("disabled");
                                }
                                else {
                                    $("#chkShippingAddress").attr("disabled", "disabled");
                                    $("#chkBillingAddress").removeAttr("disabled");
                                }
                            }
                            else {
                                $("#chkShippingAddress").removeAttr("disabled");
                                $("#chkBillingAddress").removeAttr("disabled");
                            }
                            ShowPopup(this);

                        });
                        break;
                    case 4:
                        AccountDashboard.GetAddressBookDetails();
                        $('#fade, #popuprel').fadeOut();
                        break;
                    case 5:
                        var countryElements = '';
                        $.each(msg.d, function(index, value) {
                            countryElements += '<option value=' + value.Value + '>' + value.Text + '</option>';
                        });
                        $("#ddlCountry").html(countryElements);
                        break;
                }
            }, ajaxFailure: function() {
            }, LoadAccountDashBoardStaticImage: function() {
                $('#ajaxAccountDashBoardImage').attr('src', '' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/ajax-loader.gif');
            }, GetStateList: function() {
                this.config.method = "AspxCommerceWebService.asmx/BindStateList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = "{}";
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);

            }, OrderHideAll: function() {
                $("#divMyOrders").hide();
                $("#divOrderDetails").hide();
                $("#popuprel").hide();
            }, GetMyOrders: function() {
                var offset_ = 1;
                var current_ = 1;
                var perpage = ($("#gdvMyOrder_pagesize").length > 0) ? $("#gdvMyOrder_pagesize :selected").text() : 10;
                $("#gdvMyOrders").sagegrid({
                    url: this.config.baseURL + "AspxCommerceWebService.asmx/",
                    functionMethod: 'GetMyOrderList',
                    colModel: [
                    { display: 'Order ID', name: 'order_id', cssclass: 'cssClassHeadNumber', coltype: 'label', align: 'left' },
                    { display: 'Invoice Number', name: 'invoice_number', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'CustomerID', name: 'customerID', cssclass: 'cssClassHeadNumber', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Customer Name', name: 'customer_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Email', name: 'email', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Order Status', name: 'order_status', cssclass: '', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Grand Total', name: 'grand_total', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Payment Gateway Type Name', name: 'payment_gateway_typename', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Payment Method Name', name: 'payment_method_name', cssclass: '', controlclass: '', coltype: 'label', align: 'left', hide: true },
                    { display: 'Ordered Date', name: 'ordered_date', cssclass: 'cssClassHeadDate', controlclass: '', coltype: 'label', align: 'left' },
                    { display: 'Actions', name: 'action', cssclass: 'cssClassAction', coltype: 'label', align: 'center' }
    				],

                    buttons: [
            { display: 'View', name: 'viewOrder', enable: true, _event: 'click', trigger: '1', callMethod: 'AccountDashboard.GetOrderDetails', arguments: '' }
    			    ],
                    rp: perpage,
                    nomsg: "No Records Found!",
                    param: { storeID: storeId, portalID: portalId, customerID: customerId, cultureName: cultureName },
                    current: current_,
                    pnew: offset_,
                    sortcol: { 10: { sorter: false} }
                });
            }, GetOrderDetails: function(tblID, argus) {
                switch (tblID) {
                    case "gdvMyOrders":
                        AccountDashboard.GetAllOrderDetails(argus[0]);
                        break;
                }
            }, GetAllOrderDetails: function(argus) {
                var orderId = argus;
                this.config.method = "AspxCommerceWebService.asmx/GetMyOrders";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ orderID: orderId, storeID: storeId, portalID: portalId, customerID: customerId, userName: userName, cultureName: cultureName });
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            }, GetCheckOutPage: function(tdlID, argus) {
                switch (tdlID) {
                    case "gdvMyOrders":
                        //checkoutpage.aspx;
                        break;
                    default:
                        break;
                }
            }, ClearAll: function() {
                $("#hdnAddressID").val(0);
                $("#txtFirstName").val('');
                $("#txtLastName").val('');
                $("#txtEmailAddress").val('');
                $("#txtCompanyName").val('');
                $("#txtAddress1").val('');
                $("#txtAddress2").val('');
                $("#txtCity").val('');
                $("#txtState").val('');
                $('#ddlCountry').val(1);
                $("#ddlUSState").val(1);
                $("#txtZip").val('');
                $("#txtPhone").val('');
                $("#txtMobile").val('');
                $("#txtFax").val('');
                $("#txtWebsite").val('');
                $("#chkShippingAddress").removeAttr("checked");
                $("#chkBillingAddress").removeAttr("checked");
                $("#chkShippingAddress").removeAttr("disabled");
                $("#chkBillingAddress").removeAttr("disabled");
            }, AddUpdateUserAddress: function() {
                var addressId = $("#hdnAddressID").val();
                var firstName = $("#txtFirstName").val();
                var lastName = $("#txtLastName").val();
                var email = $("#txtEmailAddress").val();
                var company = $("#txtCompanyName").val();
                var address1 = $("#txtAddress1").val();
                var address2 = $("#txtAddress2").val();
                var city = $("#txtCity").val();
                var state = '';
                if ($("#ddlCountry :selected").text() == 'United States') {
                    state = $("#ddlUSState :selected").text();
                }
                else {
                    state = $("#txtState").val();
                }
                var zip = $("#txtZip").val();
                var phone = $("#txtPhone").val();
                var mobile = $("#txtMobile").val();
                var fax = $("#txtFax").val();
                var webSite = $("#txtWebsite").val();
                var countryName = $("#ddlCountry :selected").text();
                var isDefaultShipping = $("#chkShippingAddress").attr("checked");
                var isDefaultBilling = $("#chkBillingAddress").attr("checked");
                this.config.method = "AspxCommerceWebService.asmx/AddUpdateUserAddress";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = JSON2.stringify({ addressID: addressId, customerID: customerId, firstName: firstName, lastName: lastName, email: email, company: company, address1: address1, address2: address2,
                    city: city, state: state, zip: zip, phone: phone, mobile: mobile, fax: fax, webSite: webSite, countryName: countryName, isDefaultShipping: isDefaultShipping, isDefaultBilling: isDefaultBilling, storeID: storeId,
                    portalID: portalId, userName: userName, cultureName: cultureName
                });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
                //return false;

            }, GetAllCountry: function() {

                this.config.method = "AspxCommerceWebService.asmx/BindCountryList";
                this.config.url = this.config.baseURL + this.config.method;
                this.config.data = "{}";
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            },
            Init: function() {
                var v1 = $("#form1").validate({
                    messages: {
                        FirstName: {
                            required: '*',
                            minlength: "* (at least 2 chars)", maxlength: "*"
                        },
                        LastName: {
                            required: '*',
                            minlength: "* (at least 2 chars)", maxlength: "*"
                        },
                        Email: {
                            required: '*',
                            email: 'Please enter valid email id'
                        },
                        Address1: {
                            required: '*',
                            minlength: "* (at least 2 chars)", maxlength: "*"
                        },
                        Address2: {
                            maxlength: "*"
                        },
                        Zip: {
                            required: '*',
                            minlength: "* (at least 5 chars)", maxlength: "*"
                        },
                        State: {
                            required: '*',
                            minlength: "* (at least 2 chars)", maxlength: "*"
                        },
                        Phone: {
                            required: '*',
                            minlength: "* (at least 7 digits)", maxlength: "*"
                        },
                        Mobile: {
                             minlength: "* (at least 10 digits)",maxlength: "*"
                        },
                        Fax: {
                        //number: true
                    },
                    Company: {
                        maxlength: "*"
                    },
                    City: {
                        required: '*',
                        minlength: "* (at least 2 chars)", maxlength: "*"
                    },
                    name: {
                        required: '*',
                        minlength: "* (at least 2 chars)", maxlength: "*"
                    }
                }, ignore: ":hidden"
            });

            $('#btnSubmitAddress').click(function() {
                if (v1.form()) {
                    AccountDashboard.AddUpdateUserAddress();
                    //alert("Address saved Successfully.");
                    csscody.info("<h2>Information Message</h2><p>Address saved Successfully.</p>");
                    return false;
                }
                else {
                    return false;
                }
            });

            $("#spanUserName").html(' ' + userName + '');
            $("#spanCustomerName").html('' + userName + '');
            $("#spanCustomerEmail").html('' + userEmail + '');
            //for GetMyOrders
            AccountDashboard.GetMyOrders();
            AccountDashboard.OrderHideAll();
            $("#divMyOrders").show();

            $("#lnkBack").bind("click", function() {
                AccountDashboard.OrderHideAll();
                $("#divMyOrders").show();
            });

            //End Of GetMYOrders
            //Start of AddressBook
            AccountDashboard.LoadAccountDashBoardStaticImage();
            AccountDashboard.GetAddressBookDetails();
            AccountDashboard.GetAllCountry();
            $("#lnkNewAddress").bind("click", function() {
                AccountDashboard.ClearAll();
                if ($("#hdnDefaultShippingExist").val() == "0") {
                    $("#chkShippingAddress").attr("checked", "checked");
                    $("#chkShippingAddress").attr("disabled", "disabled");
                }
                if ($("#hdnDefaultBillingExist").val() == "0") {
                    $("#chkBillingAddress").attr("checked", "checked");
                    $("#chkBillingAddress").attr("disabled", "disabled");
                }
                ShowPopup(this);
            });
            $(".cssClassClose").bind("click", function() {
                $('#fade, #popuprel').fadeOut();
            });
            $("#btnCancelAddNewAddress").bind("click", function() {
                $('#fade, #popuprel').fadeOut();
            });
            $("#btnAddNewAddress").bind("click", function() {
                $('#fade, #popuprel').fadeOut();
            });

            $('#ddlUSState').hide();
            $('#trBillingAddress ,#trShippingAddress').hide();
            $("#ddlCountry ").bind("change", function() {

                if ($("#ddlCountry :selected").text() == 'United States') {
                    AccountDashboard.GetStateList();
                }
                else {
                    $('#ddlUSState').hide();
                    $('#txtState').show();
                }
            });
        }
    }
    AccountDashboard.Init();
});
    //End of AddressBook
    //]]>
</script>

<div class="welcome-msg">
    <h2 class="sub-title">
        Hello, <span id="spanUserName"></span>
    </h2>
    <p>
        From your My Account Dashboard you have the ability to view a snapshot of your recent
        account activity and update your account information. Select a link below to view
        or edit information.</p>
</div>
<div id="divMyOrders">
    <div class="cssClassCommonBox Curve">
        <div class="cssClassHeader">
            <h2>
                <asp:Label ID="lblTitle" runat="server" Text="My Orders"></asp:Label>
            </h2>
            <div class="cssClassHeaderRight">
                <div class="cssClassButtonWrapper">
                    <div class="cssClassClear">
                    </div>
                </div>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
        <div class="cssClassGridWrapper">
            <div class="cssClassGridWrapperContent">
                <div class="loading">
                    <img id="ajaxAccountDashBoardImage" src=""  alt="loading...." title="loading...."/>
                </div>
                <div class="log">
                </div>
                <table id="gdvMyOrders" cellspacing="0" cellpadding="0" border="0" width="100%">
                </table>
            </div>
        </div>
    </div>
</div>
<div id="divOrderDetails" class="cssClassFormWrapper">
    <span class="cssClassLabel">Ordered Date: </span><span id="orderedDate"></span>
    <ul>
    </ul>
    <span class="cssClassLabel">Payment Gateway Type: </span><span id="paymentGatewayType"></span>
    <br />
    <span class="cssClassLabel">Payment Method: </span><span id="paymentMethod"></span>
    <br />
    <span class="cssClassLabel">Ordered Items: </span>
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <thead>
                        <tr class="cssClassHeading">
                            <td class="header">
                                Item Name
                            </td>
                            <td class="header">
                                SKU
                            </td>
                            <td class="header">
                                Shipping Address
                            </td>
                            <td class="header">
                                Shipping Rate
                            </td>
                            <td class="header">
                                Price
                            </td>
                            <td class="header">
                                Quantity
                            </td>
                            <td class="header">
                                Sub Total
                            </td>
                        </tr>
                    </thead>
        <tbody>
                    </tbody>
    </table>
    <a href="#" id="lnkBack" class="cssClassBack">Go back</a>
</div>
<div>
    &nbsp;&nbsp</div>
<div class="cssClassMyAccountInformation">
    <div class="cssClassHeading">
        <h1>
            Account Information</h1>
        <div class="cssClassClear">
        </div>
    </div>
    <div>
        <h3>
            Contact Information
        </h3>
        <p>
            <span id="spanCustomerName"></span>
            <br />
            <span id="spanCustomerEmail"></span>
        </p>
        <div class="cssClassClear">
        </div>
    </div>
</div>
<div>
    &nbsp;&nbsp</div>
<div class="cssClassMyAddressInformation">
    <div class="cssClassHeading">
        <h1>
            Address Book</h1>
        <div class="cssClassClear">
        </div>
    </div>
    <div class="cssClassCommonWrapper">
        <div class="cssClassCol1">
            <div class="cssClassAddressBook">
                <h3>
                    Default Addresses</h3>
                <ol>
                    <li id="liDefaultShippingAddress"></li>
                </ol>
            </div>
        </div>
        <div class="cssClassCol2">
            <div class="cssClassAddressBook">
                <h3>
                    &nbsp</h3>
                <ol>
                    <li id="liDefaultBillingAddress"></li>
                </ol>
            </div>
            <div class="cssClassClear">
            </div>
        </div>
    </div>
</div>
<div class="popupbox" id="popuprel">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
    <h2>
        <asp:Label ID="lblAddressTitle" runat="server" Text="Address Details"></asp:Label>
    </h2>
    <div class="cssClassFormWrapper">
        <table id="tblNewAddress" width="100%" border="0" cellpadding="0" cellspacing="0">
            <tbody>
                <tr>
                    <td width="20%">
                        <asp:Label ID="lblFirstName" runat="server" Text="FirstName:" CssClass="cssClassLabel"></asp:Label>
                        <span class="cssClassRequired">*</span>
                    </td>
                    <td width="80%">
                        <input type="text" id="txtFirstName" name="FirstName" class="required" minlength="2" maxlength="40" />
                    </td>
                    <td>
                        <asp:Label ID="lblLastName" runat="server" Text="LastName:" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtLastName" name="LastName" class="required" minlength="2" maxlength="40" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtEmailAddress" name="Email" class="required email" minlength="2" />
                    </td>
                     <td>
                        <asp:Label ID="lblCompany" Text="Company:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtCompanyName" name="Company" maxlength="40"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAddress1" Text="Address1:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtAddress1" name="Address1" class="required" minlength="2" maxlength="250"/>
                    </td>
                     <td>
                        <asp:Label ID="lblAddress2" Text="Address2:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtAddress2" name="Address2" maxlength="250"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCountry" Text="Country:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <select id="ddlCountry" class="cssClassDropDown">
                        </select>
                    </td>
                   
                     <td>
                        <asp:Label ID="lblState" Text="State/Province:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtState" name="State"  class="required" minlength="2"  maxlength="250"/> <select id="ddlUSState" class="cssClassDropDown">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblZip" Text="Zip/Postal Code:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtZip" name="Zip" class="required number" minlength="5" maxlength="10"/>
                    </td>
                    <td>
                        <asp:Label ID="lblCity" Text="City:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtCity" name="City" class="required" minlength="2" maxlength="250"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPhone" Text="Phone:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtPhone" name="Phone" class="required number" minlength="7" maxlength="20"/>
                    </td>
                    <td>
                        <asp:Label ID="lblMobile" Text="Mobile:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtMobile" class="number" name="Mobile" minlength="10" maxlength="20"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFax" Text="Fax:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtFax" name="Fax" class="number" maxlength="20"/>
                    </td>
                    <td>
                        <asp:Label ID="lblWebsite" Text="Website:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtWebsite" class="url" maxlength="50"/>
                    </td>
                </tr>
                <tr id="trShippingAddress">
                    <td>
                        <input type="checkbox" id="chkShippingAddress" />
                    </td>
                    <td>
                        <asp:Label ID="lblDefaultShipping" Text=" Use as Default Shipping Address" runat="server"
                            CssClass="cssClassLabel"></asp:Label>
                    </td>
                </tr>
                <tr id="trBillingAddress">
                    <td>
                        <input type="checkbox" id="chkBillingAddress" />
                    </td>
                    <td>
                        <asp:Label ID="lblDefaultBilling" Text="Use as Default Billing Address" runat="server"
                            CssClass="cssClassLabel"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="cssClassButtonWrapper">
            <button type="submit" id="btnSubmitAddress" class="cssClassButtonSubmit">
                <span><span>Save</span></span></button>
        </div>
    </div>
</div>
<input type="hidden" id="hdnAddressID" />
<input type="hidden" id="hdnDefaultShippingExist" />
<input type="hidden" id="hdnDefaultBillingExist" />
