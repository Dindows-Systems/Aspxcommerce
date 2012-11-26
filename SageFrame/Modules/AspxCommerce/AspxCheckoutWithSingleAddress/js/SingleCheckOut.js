var CheckOut
$(function() {
    var $accor = '';
    CheckOut = {
        BillingAddress: {
            AddressID: 0,
            FirstName: "",
            LastName: "",
            CompanyName: "",
            EmailAddress: "",
            Address: "",
            Address2: "",
            City: "",
            State: "",
            Zip: "",
            Country: "",
            Phone: "",
            Mobile: "",
            Fax: "",
            Website: "",
            IsDefaultBilling: false,
            IsBillingAsShipping: false
        },
        ShippingAddress: {
            AddressID: 0,
            FirstName: "",
            LastName: "",
            CompanyName: "",
            EmailAddress: "",
            Address: "",
            Address2: "",
            City: "",
            State: "",
            Zip: "",
            Country: "",
            Phone: "",
            Mobile: "",
            Fax: "",
            Website: "",
            isDefaultShipping: false

        },
        Vars: {
            GatewayName: ""

        },
        UserCart: {
            isUserGuest: true,
            isActive: true,
            IsFShipping: IsFShipping,
            myAccountURL: myAccountURL,
            CartDiscount: 0,
            TotalDiscount: Discount,
            IsDownloadItemInCart: false,
            IsDownloadItemInCartFull: false,
            CountDownloadableItem: 0,
            CountAllItem: 0,
            paymentMethodName: "",
            paymentMethodCode: "",
            shippingRate: 0,
            amount: 0,
            lstItems: [],
            spMethodID: 0,
            spCost: 0,
            ID: 0,
            qty: 0,
            Tax: 0,
            price: 0,
            weight: 0,
            CartID: 0,
            ItemType: '',
            couponCode: CouponCode
        }, config: {
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
            ajaxCallMode: 0, ///0 for get categories and bind, 1 for notification,2 for versions bind
            error: 0,
            sessionValue: ""
        },
        ajaxCall: function(config) {
            $.ajax({
                type: CheckOut.config.type,
                contentType: CheckOut.config.contentType,
                cache: CheckOut.config.cache,
                async: CheckOut.config.async,
                url: CheckOut.config.url,
                data: CheckOut.config.data,
                dataType: CheckOut.config.dataType,
                success: CheckOut.ajaxSuccess,
                error: CheckOut.ajaxFailure
            });
        },
        CheckDownloadableOnlyInCart: function() {
            //        $.ajax({
            //            type: "POST",
            //            url: aspxservicePath + "AspxCommerceWebService.asmx/CheckDownloadableItemOnly",
            //            data: JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, sessionCode: sessionCode }),
            //            contentType: "application/json;charset=utf-8",
            //            dataType: "json",
            //            success: function(msg) {
            //                IsDownloadItemInCart = msg.d;
            if (CheckOut.UserCart.IsDownloadItemInCart) {
                if (AspxCommerce.utils.GetUserName() == 'anonymoususer') {
                    $('.cssClassCheckOutMethodLeft p:first').html('').html('Your cart contains Digital item(s)!<br/> Checkout as <b>Existing User</b> OR <b>Register </b>');
                    $('.cssClassCheckOutMethodLeft .cssClassPadding #rdbGuest ,.cssClassCheckOutMethodLeft .cssClassPadding  #lblguest').remove();
                    $('#btnCheckOutMethodContinue').hide();
                    $('#rdbRegister').attr('checked', true);
                    $('#dvLogin').show();
                    //  $('.cssClassCheckOutMethod').html('').html('Please Register <a href ="' + aspxRedirectPath + register + '">here</a> to continue your download..');

                }
            } else {
                $('#rdbGuest').attr('checked', true);
            }
            if (CheckOut.UserCart.CountAllItem == CheckOut.UserCart.CountDownloadableItem) {
                CheckOut.UserCart.IsDownloadItemInCartFull = true;
            }
            else {
                CheckOut.UserCart.IsDownloadItemInCartFull = false;
            }
            if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                $('#dvBilling .cssClassCheckBox').hide();
            }
            else {
                $('#dvBilling .cssClassCheckBox').show();
            }
            //            },
            //            error: function() {
            //                alert("error in database connection!");
            //            }
            //        });
        },

        AddUpdateUserAddress: function() {
            var addressIdX = $("#hdnAddressID").val();
            var firstNameX = $("#popuprel .cssClassFormWrapper table #txtFirstName").val();
            var lastNameX = $("#popuprel .cssClassFormWrapper table #txtLastName").val();
            var emailX = $("#popuprel .cssClassFormWrapper table #txtEmailAddress").val();
            var companyX = $("#popuprel .cssClassFormWrapper table #txtCompanyName").val();
            var address1X = $("#popuprel .cssClassFormWrapper table #txtAddress1").val();
            var address2X = $("#popuprel .cssClassFormWrapper table #txtAddress2").val();
            var cityX = $("#popuprel .cssClassFormWrapper table #txtCity").val();
            var stateX = '';
            if ($("#popuprel .cssClassFormWrapper table #ddlBLCountry :selected").text() == 'United States') {
                stateX = $("#popuprel .cssClassFormWrapper table #ddlBLState :selected").text();
            }
            else {
                stateX = $("#popuprel .cssClassFormWrapper table #txtState").val();
            }
            var zipX = $("#popuprel .cssClassFormWrapper table #txtZip").val();
            var phoneX = $("#popuprel .cssClassFormWrapper table #txtPhone").val();
            var mobileX = $("#popuprel .cssClassFormWrapper table #txtMobile").val();
            var faxX = '';
            if ($("#popuprel .cssClassFormWrapper table #txtFax").length > 0)
                faxX = $("#popuprel .cssClassFormWrapper table #txtFax").val();
            var webSiteX = '';
            if ($("#popuprel .cssClassFormWrapper table #txtFax").length > 0)
                webSiteX = $("#popuprel .cssClassFormWrapper table #txtWebsite").val();

            var countryNameX = $("#popuprel .cssClassFormWrapper table #ddlBLCountry :selected").text();
            var isDefaultShippingX = $("#popuprel .cssClassFormWrapper table #chkShippingAddress").attr("checked");
            var isDefaultBillingX = $("#popuprel .cssClassFormWrapper table #chkBillingAddress").attr("checked");

            this.config.method = "AspxCommerceWebService.asmx/AddUpdateUserAddress";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ addressID: addressIdX, customerID: AspxCommerce.utils.GetCustomerID(), firstName: firstNameX, lastName: lastNameX, email: emailX, company: companyX, address1: address1X, address2: address2X,
                city: cityX, state: stateX, zip: zipX, phone: phoneX, mobile: mobileX, fax: faxX, webSite: webSiteX, countryName: countryNameX, isDefaultShipping: isDefaultShippingX, isDefaultBilling: isDefaultBillingX, storeID: AspxCommerce.utils.GetStoreID(),
                portalID: AspxCommerce.utils.GetPortalID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName()
            });
            this.config.ajaxCallMode = 11;
            this.config.error = 11;
            this.ajaxCall(this.config);
        },

        ClearAll: function() {
            $("#hdnAddressID").val(0);
            $("#txtFirstName").val('');
            $("#txtLastName").val('');
            $("#txtEmailAddress").val('');
            $("#txtCompanyName").val('');
            $("#txtAddress1").val('');
            $("#txtAddress2").val('');
            $("#txtCity").val('');
            $("#txtState").val('');
            $('#ddlBLState').val(1);
            $("#ddlBLCountry").val(1);
            $("#txtZip").val('');
            $("#txtPhone").val('');
            $("#txtMobile").val('');
            $("#txtFax").val('');
            $("#txtWebsite").val('');
            //$(".error").hide();
        },
        ConfirmADDNewAddress: function(event) {
            if (event) {
                var route = '';
                if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                    route = AspxCommerce.utils.GetAspxRedirectPath() + CheckOut.UserCart.myAccountURL + '.aspx';
                }
                else {
                    route = AspxCommerce.utils.GetAspxRedirectPath() + CheckOut.UserCart.myAccountURL;
                }
                window.location.href = route;
                return false;
            }
            else { return false; }
        },

        BindBillingData: function() {
            $('#dvCPBilling').html('');
            var itemsarray = [];
            $('#dvBilling input:text,#dvBillingSelect option:selected').each(function() {
                var items = '';
                if ($(this).attr('class') == 'cssBillingShipping')
                    items = $(this).text();
                else
                    items = $(this).val();
                if (items != '') {
                    itemsarray.push(items);
                }
            });

            var html = '<ul>';
            $.each(itemsarray, function(index, item) {
                if (item != '') {
                    html += '<li>' + item + '</li>';
                }
            });

            html += '</ul>';
            html += '<div class="cssClassButtonWrapper"><p><button type="button" id="btnBillingChange"><span><span>Change</span></span></button></p><div class="cssClassClear"></div></div>';
            $('#dvCPBilling').html('').append(html);
            itemsarray = [];
            $('#btnBillingChange').bind("click", function() {
                $('#dvCPBilling').html('');
                itemsarray = [];
                $accor.accordion("activate", 1);
            });
        },
        QuantitityDiscountAmount: function() {
            this.config.method = "AspxCommerceWebService.asmx/GetDiscountQuantityAmount";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), userName: AspxCommerce.utils.GetUserName(), customerID: AspxCommerce.utils.GetCustomerID(), cultureName: AspxCommerce.utils.GetCultureName(), sessionCode: AspxCommerce.utils.GetSessionCode() });
            this.config.ajaxCallMode = 10;
            this.config.error = 10;
            this.ajaxCall(this.config);
        },

        BindShippingData: function() {
            $('#dvCPShipping').html('');
            var itemsarray = [];
            $('#dvShipping input:text, #dvShippingSelect option:selected').each(function() {
                var items = '';
                if ($(this).attr('class') == 'cssBillingShipping')
                    items = $(this).text();
                else
                    items = $(this).val();
                itemsarray.push(items);
            });

            var html = '<ul>';
            $.each(itemsarray, function(index, item) {
                if (item != '') {
                    html += '<li>' + item + '</li>';
                }
            });
            html += '</ul>';
            html += '<div class="cssClassButtonWrapper"><p><button type="button" id="btnShippingChange"><span><span>Change</span></span></button></p><div class="cssClassClear"></div></div>';
            $('#dvCPShipping').html('').append(html);
            itemsarray = [];
            $('#btnShippingChange').bind("click", function() {
                $('#dvCPShipping').html('');
                $accor.accordion("activate", 2);
            });
        },
        BindShippingMethodData: function() {
            $('#dvCPShippingMethod').html('');
            var itemsarray = [];
            var items = $('#divShippingMethod input:radio:checked').parents('tr').find('td div.cssClassCartPictureInformation h3').html();
            itemsarray.push(items);
            var html = '<ul>';
            $.each(itemsarray, function(index, item) {
                if (item != '') {
                    html += '<li>' + item + '</li>';
                }
            });
            html += '</ul>';
            html += '<div class="cssClassButtonWrapper"><p><button type="button" id="btnShippingMethodChange"><span><span>Change</span></span></button></p><div class="cssClassClear"></div></div>';
            $('#dvCPShippingMethod').html('').append(html);
            itemsarray = [];
            $('#btnShippingMethodChange').bind("click", function() {
                $('#dvCPShippingMethod').html('');
                itemsarray = [];
                $accor.accordion("activate", 3);
            });
        },
        BindPaymentData: function() {
            var itemsarray = [];
            var items = '';
            items = $('#dvPGList input[type=radio]:checked').attr('realname');
            itemsarray.push(items);
            if ($('#cardType').length > 0) {
                items = $.trim($('#AIMChild input:radio:checked').nextAll().find('label').html());
                itemsarray.push(items);
            }
            //alert(itemsarray);       

            var html = '<ul>';
            $('#dvCPPaymentMethod').html('');
            $.each(itemsarray, function(index, item) {
                if (item != '') {
                    html += '<li>' + item + '</li>';
                }
            });
            html += '</ul>';
            html += '<div class="cssClassButtonWrapper"><p><button type="button" id="btnPaymentChange"><span><span>Change</span></span></button></p><div class="cssClassClear"></div></div>';
            $('#dvCPPaymentMethod').html('').append(html);
            itemsarray = [];
            $('#btnPaymentChange').bind("click", function() {
                $('#dvCPPaymentMethod').html('');
                itemsarray = [];
                if ($('#cardType').length > 0) {
                    $('#cardType').remove();
                }
                $accor.accordion("activate", 4);
            });

        },

        AddBillingAsShipping: function() {

            if ($('#chkBillingAsShipping').attr('checked')) {
                if ($('#dvBillingInfo').is(':hidden')) {
                    $("#ddlShipping").val($("#ddlBilling").val());
                    $.cookies.set('ShippingDetails', $('#ddlShipping option').html());
                }
                else {
                    $('#txtSPFirstName').val($('#txtFirstName').val());
                    $('#txtSPLastName').val($('#txtLastName').val());
                    $('#txtSPEmailAddress').val($('#txtEmailAddress').val());

                    $('#txtSPCompany').val($('#txtCompanyName').val());
                    $('#txtSPAddress').val($('#txtAddress1').val());
                    $('#txtSPAddress2').val($('#txtAddress2').val());
                    $('#txtSPCity').val($('#txtCity').val());

                    if ($.trim($("#ddlBLCountry :selected").text()) == 'United States') {
                        $('#ddlSPState').show();
                        $('#txtSPState').hide();
                        $('#ddlSPState').val($('#ddlBLState').val());
                    }
                    else {
                        $('#ddlSPState').hide();
                        $('#txtSPState').show();
                        $('#txtSPState').val($('#txtState').val());

                    }
                    $('#txtSPZip').val($('#txtZip').val());
                    $('#ddlSPCountry').val($('#ddlBLCountry').val());
                    $('#txtSPPhone').val($('#txtPhone').val());
                    $('#txtSPMobile').val($('#txtMobile').val());
                    $.cookies.set('ShippingDetails', $('#txtSPFirstName').val() + ',' + $('#txtSPLastName').val() + ',' + $('#txtSPCompany').val() + ',' + $('#txtSPAddress').val() + ',' + $('#txtSPCity').val() + ',' + $('#ddlSPState').val() + ',' + $('#txtSPZip').val() + ',' + $('#ddlSPCountry').val() + ',' + $('#txtSPPhone').val() + ',' + $('#txtSPFax').val());
                }
            }
            else {
                $.cookies.set('ShippingDetails', '');
                $('#txtSPFirstName').val("");
                $('#txtSPLastName').val("");
                $('#txtSPCompany').val("");
                $('#txtSPAddress').val("");
                $('#txtSPAddress2').val("");
                $('#txtSPEmailAddress').val("");
                $('#txtSPMobile').val("");
                $('#txtSPCity').val("");
                $('#ddlSPState').val(1);
                $('#txtSPState').val("");
                $('#txtSPZip').val("");
                $('#ddlSPCountry').val(1);
                $('#txtSPPhone').val("");
                $('#txtSPFax').val("");
                $('#ddlSPState').hide();
                $('#txtSPState').show();
            }
        },

        GetCountry: function() {
            this.config.method = "AspxCommerceWebService.asmx/BindCountryList";
            this.config.url = this.config.baseURL + this.config.method;
            //this.config.async = false;
            //this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName(), sessionCode: AspxCommerce.utils.GetSessionCode() });
            this.config.ajaxCallMode = 9;
            this.config.error = 9;
            this.ajaxCall(this.config);
        },
        GetState: function() {

            this.config.method = "AspxCommerceWebService.asmx/BindStateList";
            this.config.url = this.config.baseURL + this.config.method;
            //this.config.async = false;
            //this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName(), sessionCode: AspxCommerce.utils.GetSessionCode() });
            this.config.ajaxCallMode = 8;
            this.config.error = 8;
            this.ajaxCall(this.config);
        },

        GetShippinMethodsFromWeight: function() {
            this.config.method = "AspxCommerceWebService.asmx/GetShippingMethodByWeight";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.async = false;
            this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName(), sessionCode: AspxCommerce.utils.GetSessionCode() });
            this.config.ajaxCallMode = 7;
            this.config.error = 7;
            this.ajaxCall(this.config);
        },

        GetDiscountCartPriceRule: function(CartID, SpCost) {
            this.config.method = "AspxCommerceWebService.asmx/GetDiscountPriceRule";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ cartID: CartID, storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName(), shippingCost: SpCost });
            this.config.ajaxCallMode = 6;
            this.config.error = 6;
            this.ajaxCall(this.config);
        },

        GetUserCartDetails: function() {
            this.config.method = "AspxCommerceWebService.asmx/GetCartDetails";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.async = false;
            this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName(), sessionCode: AspxCommerce.utils.GetSessionCode() });
            this.config.ajaxCallMode = 5;
            this.config.error = 5;
            this.ajaxCall(this.config);
        },

        AssignItemsDetails: function() {
            $('#tblCartList tr').not('.cssClassHeadeTitle').each(function(i, v) {
                CheckOut.UserCart.ID = $(this).find('a').attr("id").replace(/[^0-9]/gi, '');
                CheckOut.UserCart.ItemType = parseInt($(this).find('a').attr("itemType"));
                if ($(this).find("input[class='num-pallets-input']").val() != "null")
                    CheckOut.UserCart.qty = $(this).find("input[class='num-pallets-input']").val();
                else {
                    CheckOut.UserCart.qty = 0;
                }
                if ($(this).find("input[class='num-pallets-input']").attr('price') != "null") {
                    CheckOut.UserCart.price = $(this).find("input[class='num-pallets-input']").attr('price');

                }
                else {
                    CheckOut.UserCart.price = 0.00;
                }
                if ($(this).find("td.price-per-pallet").find('span').attr('id') != "null")
                    CheckOut.UserCart.weight = $(this).find("td.price-per-pallet").find('span').attr('id');
                else {
                    CheckOut.UserCart.weight = 0;
                }

                if ($('#dvShippingSelect option:selected').val() > 0) {
                    CheckOut.UserCart.spAddressID = $('#dvShippingSelect option:selected').val();
                } else {
                    CheckOut.UserCart.spAddressID = 0;
                }


                var costvariants = '';
                if ($.trim($(this).find(".row-variants").html()) != '') {
                    CheckOut.UserCart.costvariants = $.trim($(this).find(".row-variants").attr('varIDs'));
                } else { CheckOut.UserCart.costvariants = 0; }


                if (parseInt(CheckOut.UserCart.ItemType) == 2) {
                    CheckOut.UserCart.lstItems[i] = { "OrderID": 0, "ShippingAddressID": 0, "ShippingMethodID": 0, "ItemID": CheckOut.UserCart.ID, "Variants": CheckOut.UserCart.costvariants, "Quantity": CheckOut.UserCart.qty, "Price": parseFloat(CheckOut.UserCart.price), "Weight": 0, "Remarks": '', "orderItemRemarks": '', "ShippingRate": 0, 'IsDownloadable': true };

                } else {
                    CheckOut.UserCart.lstItems[i] = { "OrderID": 0, "ShippingAddressID": CheckOut.UserCart.spAddressID, "ShippingMethodID": CheckOut.UserCart.spMethodID, "ItemID": CheckOut.UserCart.ID, "Variants": CheckOut.UserCart.costvariants, "Quantity": CheckOut.UserCart.qty, "Price": parseFloat(CheckOut.UserCart.price), "Weight": parseFloat(CheckOut.UserCart.weight), "Remarks": '', "orderItemRemarks": '', "ShippingRate": CheckOut.UserCart.spCost, 'IsDownloadable': false };

                }


            });
        },

        BindUserAddress: function() {
            this.config.method = "AspxCommerceWebService.asmx/GetAddressBookDetails";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), customerID: AspxCommerce.utils.GetCustomerID(), userName: AspxCommerce.utils.GetUserName(), cultureName: AspxCommerce.utils.GetCultureName() });
            this.config.ajaxCallMode = 4;
            this.config.error = 4;
            this.ajaxCall(this.config);
        },

        SetSessionValue: function(sessionKey, sessionValue) {
            this.config.method = "AspxCommerceWebService.asmx/SetSessionVariable";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ key: sessionKey, value: sessionValue });
            this.config.ajaxCallMode = 2;
            this.config.error = 2;
            this.ajaxCall(this.config);
        },
        LoadPGatewayList: function() {
            this.config.method = "AspxCommerceWebService.asmx/GetPGList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), cultureName: AspxCommerce.utils.GetCultureName() });
            this.config.ajaxCallMode = 3;
            this.config.error = 3;
            this.ajaxCall(this.config);
        },
        LoadControl: function(ControlName, Name) {
            this.Vars.GatewayName = Name;
            this.config.method = "LoadControlHandler.aspx/Result";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = "{ controlName:'" + AspxCommerce.utils.GetAspxRootPath() + ControlName + "'}";
            this.config.ajaxCallMode = 1;
            this.config.error = 1;
            this.ajaxCall(this.config);

        }, ajaxSuccess: function(data) {
            switch (CheckOut.config.ajaxCallMode) {
                case 0:
                    break;
                case 1: //load control                    
                    if (CheckOut.Vars.GatewayName.toLowerCase() == 'aimauthorize') {
                        $('#dvCheque').remove();
                        $('#creditCard').remove();
                        $('#AIMChild').remove();
                        $('#dvPaymentInfo input[type="button"]').remove();
                        $('#dvPaymentInfo .cssClassButtonWrapper').before(data.d);
                        var button = $('#dvPaymentInfo').find("input[type=button]");
                        $('#dvPaymentInfo input[type="button"]').remove();
                        $('#dvPlaceOrder .cssClassButtonWrapper ').find('input').not("#btnPlaceBack").remove();
                        $('#dvPlaceOrder .cssClassButtonWrapper ').append(button);
                        $('#dvPlaceOrder .cssClassButtonWrapper div ').remove();
                    } else {
                        $('#dvPlaceOrder .cssClassButtonWrapper ').find('input').not("#btnPlaceBack").remove();
                        $('#dvPlaceOrder .cssClassButtonWrapper ').append(data.d);
                    }

                    break;
                case 2: //get session variable
                    CheckOut.config.sessionValue = parseFloat(data.d);
                    break;
                case 3:
                    if (data.d.length > 0) {
                        $.each(data.d, function(index, item) {
                            $('#dvPGList').append('<input id="rdb' + item.PaymentGatewayTypeName + '" name="PGLIST" type="radio" realname="' + item.PaymentGatewayTypeName + '" friendlyname="' + item.FriendlyName + '"  source="' + item.ControlSource + '" value="' + item.PaymentGatewayTypeID + '" class="cssClassRadioBtn" /><b><label> ' + item.PaymentGatewayTypeName + '</label></b><br />');
                        });
                        $('#dvPGList input[name="PGLIST"]').bind("click", function() {
                            CheckOut.SetSessionValue("Gateway", $(this).attr('value'));
                            if ('paypal' == $(this).attr('friendlyname').toLowerCase()) {
                                CheckOut.UserCart.paymentMethodCode = "Paypal";
                                CheckOut.UserCart.paymentMethodName = "Paypal";
                            }
                            else {
                            }
                            if ('aimauthorize' == $.trim($(this).attr('friendlyname').toLowerCase())) {

                                CheckOut.LoadControl($(this).attr('source'), $.trim($(this).attr('friendlyname')));
                            }
                            else {
                                CheckOut.LoadControl($(this).attr('source'), $(this).attr('friendlyname'));
                                $('#dvCheque').hide();
                                $('#creditCard').hide();
                                $('#AIMChild').hide();
                                $('#dvPaymentInfo input[type="button"]').remove();
                            }
                        });
                    }
                    break;
                case 4:
                    var option = '';
                    var optionBilling = '';
                    var pattern = ",", re = new RegExp(pattern, "g");

                    if (data.d.length > 0) {
                        $.each(data.d, function(index, item) {
                            if (item.DefaultShipping == 1) {
                                option += "<option value=" + item.AddressID + " selected='selected' class='cssBillingShipping'> ";
                                option += item.FirstName.replace(re, "-") + " " + item.LastName.replace(re, "-");
                                if (item.Address1 != "")
                                    option += ", " + item.Address1.replace(re, "-");

                                if (item.City != "")
                                    option += ", " + item.City.replace(re, "-");

                                if (item.State != "")
                                    option += ", " + item.State.replace(re, "-");

                                if (item.Country != "")
                                    option += ", " + item.Country.replace(re, "-");

                                if (item.Zip != "")
                                    option += ", " + item.Zip.replace(re, "-");

                                if (item.Email != "")
                                    option += ", " + item.Email.replace(re, "-");

                                if (item.Phone != "")
                                    option += ", " + item.Phone.replace(re, "-");

                                if (item.Mobile != "")
                                    option += ", " + item.Mobile.replace(re, "-");

                                if (item.Fax != "")
                                    option += ", " + item.Fax.replace(re, "-");

                                if (item.Website != "")
                                    option += ", " + item.Website.replace(re, "-");

                                if (item.Address2 != "")
                                    option += ", " + item.Address2.replace(re, "-");

                                if (item.Company != "")
                                    option += ", " + item.Company.replace(re, "-");
                            }
                            else {
                                option += "<option value=" + item.AddressID + " class='cssBillingShipping'> ";
                                option += item.FirstName.replace(re, "-") + " " + item.LastName.replace(re, "-");
                                if (item.Address1 != "")
                                    option += ", " + item.Address1.replace(re, "-");

                                if (item.City != "")
                                    option += ", " + item.City.replace(re, "-");

                                if (item.State != "")
                                    option += ", " + item.State.replace(re, "-");

                                if (item.Country != "")
                                    option += ", " + item.Country.replace(re, "-");

                                if (item.Zip != "")
                                    option += ", " + item.Zip.replace(re, "-");

                                if (item.Email != "")
                                    option += ", " + item.Email.replace(re, "-");

                                if (item.Phone != "")
                                    option += ", " + item.Phone.replace(re, "-");

                                if (item.Mobile != "")
                                    option += ", " + item.Mobile.replace(re, "-");

                                if (item.Fax != "")
                                    option += ", " + item.Fax.replace(re, "-");

                                if (item.Website != "")
                                    option += ", " + item.Website.replace(re, "-");

                                if (item.Address2 != "")
                                    option += ", " + item.Address2.replace(re, "-");

                                if (item.Company != "")
                                    option += ", " + item.Company.replace(re, "-");

                            }

                            if (item.DefaultBilling == 1) {
                                optionBilling += "<option value=" + item.AddressID + " selected='selected' class='cssBillingShipping'> ";
                                optionBilling += item.FirstName.replace(re, "-") + " " + item.LastName.replace(re, "-");
                                if (item.Address1 != "")
                                    optionBilling += ", " + item.Address1.replace(re, "-");

                                if (item.City != "")
                                    optionBilling += ", " + item.City.replace(re, "-");

                                if (item.State != "")
                                    optionBilling += ", " + item.State.replace(re, "-");

                                if (item.Country != "")
                                    optionBilling += ", " + item.Country.replace(re, "-");

                                if (item.Zip != "")
                                    optionBilling += ", " + item.Zip.replace(re, "-");

                                if (item.Email != "")
                                    optionBilling += ", " + item.Email.replace(re, "-");

                                if (item.Phone != "")
                                    optionBilling += ", " + item.Phone.replace(re, "-");

                                if (item.Mobile != "")
                                    optionBilling += ", " + item.Mobile.replace(re, "-");

                                if (item.Fax != "")
                                    optionBilling += ", " + item.Fax.replace(re, "-");

                                if (item.Website != "")
                                    optionBilling += ", " + item.Website.replace(re, "-");

                                if (item.Address2 != "")
                                    optionBilling += ", " + item.Address2.replace(re, "-");

                                if (item.Company != "")
                                    optionBilling += ", " + item.Company.replace(re, "-");
                            }
                            else {
                                optionBilling += "<option value=" + item.AddressID + " class='cssBillingShipping'> ";
                                optionBilling += item.FirstName.replace(re, "-") + " " + item.LastName.replace(re, "-");
                                if (item.Address1 != "")
                                    optionBilling += ", " + item.Address1.replace(re, "-");

                                if (item.City != "")
                                    optionBilling += ", " + item.City.replace(re, "-");

                                if (item.State != "")
                                    optionBilling += ", " + item.State.replace(re, "-");

                                if (item.Country != "")
                                    optionBilling += ", " + item.Country.replace(re, "-");

                                if (item.Zip != "")
                                    optionBilling += ", " + item.Zip.replace(re, "-");

                                if (item.Email != "")
                                    optionBilling += ", " + item.Email.replace(re, "-");

                                if (item.Phone != "")
                                    optionBilling += ", " + item.Phone.replace(re, "-");

                                if (item.Mobile != "")
                                    optionBilling += ", " + item.Mobile.replace(re, "-");

                                if (item.Fax != "")
                                    optionBilling += ", " + item.Fax.replace(re, "-");

                                if (item.Website != "")
                                    optionBilling += ", " + item.Website.replace(re, "-");

                                if (item.Address2 != "")
                                    optionBilling += ", " + item.Address2.replace(re, "-");

                                if (item.Company != "")
                                    optionBilling += ", " + item.Company.replace(re, "-");
                            }
                        });
                        $("#ddlShipping").html('');
                        $("#ddlBilling").html('');
                        $("#ddlShipping").html(option);
                        $("#ddlBilling").html(optionBilling);
                        if ($.trim($('#ddlBilling').text()) == "" || $.trim($('#ddlBilling').text()) == null) {

                            $('#addBillingAddress').show();
                        } else {
                            $('#addBillingAddress').hide();
                        }
                        if ($.trim($('#ddlShipping').text()) == "" || $.trim($('#ddlShipping').text()) == null) {
                            // alert("Please visit your Dashboard to add Shipping Address!!!");
                            $('#addShippingAddress').show();
                        } else { $('#addShippingAddress').hide(); }
                    }
                    break;
                case 5: //cart detail load
                    var cartHeading = '';
                    var cartElements = '';
                    cartHeading += '<table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblCartList">';
                    cartHeading += '<tbody><tr class="cssClassHeadeTitle">';
                    cartHeading += '<td class="cssClassSN"> Sn.';
                    cartHeading += ' </td><td class="cssClassProductImageWidth">';
                    cartHeading += 'Item Image';
                    cartHeading += '</td><td>';
                    cartHeading += 'Description';
                    cartHeading += '</td>';
                    cartHeading += '<td>';
                    cartHeading += 'Variants';
                    cartHeading += '</td>';
                    cartHeading += '<td class="cssClassQTY">';
                    cartHeading += ' Qty';
                    cartHeading += '</td>';
                    cartHeading += '<td class="cssClassTimes">';
                    cartHeading += 'X';
                    cartHeading += '</td>';
                    cartHeading += '<td class="cssClassProductPrice">';
                    cartHeading += 'Unit Price';
                    cartHeading += '</td>';
                    cartHeading += '<td class="cssClassEquals">';
                    cartHeading += '=';
                    cartHeading += '</td>';
                    cartHeading += '<td class="cssClassSubTotal">';
                    cartHeading += 'SubTotal';
                    cartHeading += '</td>';
                    cartHeading += '<td class="cssClassTaxRate">';
                    cartHeading += 'Unit Tax';
                    cartHeading += '</td>';
                    cartHeading += '</tr>';
                    cartHeading += '</table>';
                    $("#divCartDetails").html(cartHeading);
                    $.each(data.d, function(index, value) {
                        index = index + 1;
                        if (value.ImagePath == "") {
                            value.ImagePath = noImageCheckOutInfoPath;
                        }
                        else if (value.AlternateText == "") {
                            value.AlternateText = value.Name;
                        }
                        if (parseInt(value.ItemTypeID) == 2) {
                            CheckOut.UserCart.IsDownloadItemInCart = true;
                            CheckOut.UserCart.CountDownloadableItem++;
                        }
                        CheckOut.UserCart.CountAllItem++;
                        cartElements += '<tr >';
                        cartElements += '<td>';
                        cartElements += '<b>' + index + "." + '</b>';
                        cartElements += '</td>';
                        cartElements += '<td>';
                        cartElements += '<p class="cssClassCartPicture">';
                        cartElements += '<img src="' + AspxCommerce.utils.GetAspxRootPath() + value.ImagePath.replace('uploads', 'uploads/Small') + '" ></p>';
                        cartElements += '</td>';
                        cartElements += '<td>';
                        cartElements += '<div class="cssClassCartPictureInformation">';
                        cartElements += '<h3>';
                        cartElements += '<a class="cssClassLink" id="item_' + value.ItemID + '" itemType="' + value.ItemTypeID + '"  href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + value.SKU + '.aspx">' + value.ItemName + ' </a></h3>';
                        cartElements += '<p>';
                        //cartElements += '<textarea  id="itemDes" readonly="readonly" style="width: 153px; height: 75px;">' + Encoder.htmlDecode(value.ShortDescription) + '</textarea>';
                        cartElements += '' + Encoder.htmlDecode(value.ShortDescription) + '';
                        cartElements += '</p>';
                        cartElements += '</div>';
                        cartElements += '</td>';
                        cartElements += '<td class="row-variants" varIDs="' + value.CostVariantsValueIDs + '">';
                        cartElements += '' + value.CostVariants + '';
                        cartElements += '</td>';
                        cartElements += '<td class="cssClassPreviewQTY">';
                        cartElements += '<input class="num-pallets-input" taxrate="' + (value.TaxRateValue * rate).toFixed(2) + '" price="' + value.Price + '" id="txtQuantity_' + value.CartID + '" type="text" readonly="readonly" value="' + value.Quantity + '">';
                        cartElements += '</td>';
                        cartElements += '<td class="cssClassTimes">';
                        cartElements += ' X';
                        cartElements += '</td>';
                        cartElements += '<td class="price-per-pallet">';
                        cartElements += '<span id="' + value.Weight + '" class="cssClassFormatCurrency">' + (value.Price * rate).toFixed(2) + '</span>';
                        cartElements += '</td>';
                        cartElements += '<td class="cssClassEquals">';
                        cartElements += '=';
                        cartElements += '</td>';
                        cartElements += '<td class="row-total">';
                        cartElements += '<input class="row-total-input cssClassFormatCurrency" id="txtRowTotal_' + value.CartID + '"  value="' + (value.TotalItemCost * rate).toFixed(2) + '"  readonly="readonly" type="text" />';
                        cartElements += '</td>';
                        cartElements += '<td class="row-taxRate">';
                        cartElements += '<span class="cssClassFormatCurrency">' + (value.TaxRateValue * rate).toFixed(2) + '</span>';
                        cartElements += '</td>';
                        cartElements += '</tr>';
                        CheckOut.UserCart.CartID = value.CartID;
                    });
                    $("#tblCartList").append(cartElements);
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    $("#tblCartList tr:even ").addClass("cssClassAlternativeEven");
                    $("#tblCartList tr:odd ").addClass("cssClassAlternativeOdd");

                    var subTotalAmount = 0.00;
                    $(".row-total-input").each(function() {
                        subTotalAmount = parseFloat(subTotalAmount) + parseFloat($(this).val().replace(/[^-0-9\.]+/g, ""));
                        $.cookies.set('Total', subTotalAmount.toFixed(2).replace(/[^-0-9\.]+/g, ""));
                    });

                    CheckOut.UserCart.Tax = 0.00;
                    $(" .cssClassPreviewQTY .num-pallets-input").each(function() {
                        CheckOut.UserCart.Tax += $(this).val() * $(this).attr("taxrate");
                    });
                    CheckOut.SetSessionValue("TaxAll", CheckOut.UserCart.Tax);
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    $(".total-box").val('').attr("value", subTotalAmount.toFixed(2));
                    //  alert(subTotalAmount.toFixed(2));
                    $(".num-pallets-input").bind("keypress", function(e) {
                        if ($(this).val() == "") {
                            if (e.which != 8 && e.which != 0 && (e.which < 49 || e.which > 57)) {
                                return false;
                            }
                        }
                        else {
                            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                                return false;
                            }
                        }
                    });
                    $(".num-pallets-input").bind("keyup", function(e) {
                        var subTotalAmount = 0;
                        var cartId = parseInt($(this).attr("id").replace(/[^0-9]/gi, ''));
                        $(this).closest('tr').find("#txtRowTotal_" + cartId + "").val($(this).val() * $(this).attr("price"));
                        $(".row-total-input").each(function() {
                            subTotalAmount = parseFloat(subTotalAmount) + parseFloat($(this).val().replace(/[^0-9]/gi, ''));
                        });
                        $(".total-box").val('').attr("value", subTotalAmount.toFixed(2));
                        if (CheckOut.UserCart.IsFShipping.toLowerCase() == 'true') {
                            $('#Fshipping p b').html('');
                            $('#Fshipping p b').html('Shipping Cost: 0.00 (free shipping)');
                            $('#txtShippingTotal').val('0.00');
                        }
                        $('#txtTax').val(CheckOut.UserCart.Tax.toFixed(2));
                        $('#txtDiscountAmount').val(parseFloat(CheckOut.UserCart.TotalDiscount).toFixed(2) + parseFloat(CheckOut.UserCart.CartDiscount).toFixed(2));
                        $("#txtTotalCost").val(parseFloat($("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, "")) + parseFloat(CheckOut.UserCart.Tax) + parseFloat($(".total-box").val().replace(/[^-0-9\.]+/g, "")) - parseFloat(CheckOut.UserCart.TotalDiscount) - parseFloat(CheckOut.UserCart.CartDiscount));
                        var gt = $("#txtTotalCost").val().replace(/[^-0-9\.]+/g, "");
                        if (gt == 'NaN')
                            gt = 0;
                        CheckOut.SetSessionValue("GrandTotalAll", parseFloat(gt));
                        $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    });
                    if ($('#lnkMyCart').find('.cssClassTotalCount').text().replace('[', '').replace(']', '') == '0') {
                        $('.cssClassAccordionWrapper').hide();
                        $('.cssClassRightAccordainTab').hide();
                        $('.cssClassBodyContentWrapper').append("<div id ='msgnoitem' class='cssClassCommonBox Curve'><div class='cssClassHeader'><h2> <span id='spnheader'>Message </span></h2> <div class='cssClassClear'> </div></div><div class='cssClassGridWrapper'><div class='cssClassGridWrapperContent'><h3> No Items found in your Cart</h3><div class='cssClassButtonWrapper'><button type='button' id='btnContinueInStore'><span><span>Continue to Shopping</span></span></button></div><div class='cssClassClear'></div></div></div></div>");

                        $("#btnContinueInStore").bind("click", function() {
                            if (AspxCommerce.utils.IsUserFriendlyUrl()) {
                                window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Home.aspx';
                            }
                            else {
                                window.location.href = AspxCommerce.utils.GetAspxRedirectPath() + 'Home';
                            }
                            return false;
                        });
                    }
                    CheckOut.AssignItemsDetails();
                    CheckOut.CheckDownloadableOnlyInCart();
                    break;
                case 6: //cart discount
                    if (data.d.length > 0) {
                        //alert(msg.d);
                        CheckOut.UserCart.CartDiscount = parseFloat(data.d).toFixed(2);
                    }
                    break;
                case 7:
                    var shippingmethodId = 0;
                    var shippingHeading = '';
                    var shippingMethodElements = '';
                    shippingHeading += '<table width="100%" cellspacing="0" cellpadding="0" border="0">';
                    shippingHeading += '<tbody><tr class="cssClassHeadeTitle">';
                    shippingHeading += '<td colspan="4">Shipping Method(s)</td>';
                    shippingHeading += '</tr></tbody></table>';
                    $("#divShippingMethod").html(shippingHeading);

                    if (data.d.length == 0) {
                        $('#divShippingMethod table>tbody').append("<tr><td>Items' weight in your cart doesn't meet the shipping provider weight criteria Or<br /> Shipping providers are unable to ship items!</td></tr>");
                        $('#btnShippingMethodContinue').hide();
                    }

                    $.each(data.d, function(index, value) {
                        shippingMethodElements += '<tr ><td class=""><b><input name="shippingRadio" type="radio" value="' + value.ShippingMethodID + '" shippingCost=" ' + value.ShippingCost + '"/></b></td>';
                        shippingMethodElements += '<td class="">';
                        if (value.ImagePath != "") {
                            shippingMethodElements += '<p class="cssClassCartPicture"><img  alt="' + value.AlternateText + '" src="' + aspxRootPath + value.ImagePath.replace('uploads', 'uploads/Small') + '" height="83px" width="124px" /></p>';
                        }
                        shippingMethodElements += '</td>';
                        shippingMethodElements += '<td>';
                        shippingMethodElements += '<div class="cssClassCartPictureInformation">';
                        shippingMethodElements += '<h3>' + value.ShippingMethodName + '</h3>';
                        shippingMethodElements += '<p><b>Delivery Time: ' + value.DeliveryTime + '</b></p>';
                        shippingMethodElements += '</div></td>';
                        shippingMethodElements += '<td id="Fshipping"><p><b>Shipping Cost: <span class="cssClassFormatCurrency">' + value.ShippingCost + '</span></b></p></td>'
                        shippingMethodElements += '</tr>';
                        if (index == 0) {
                            shippingmethodId = value.ShippingMethodID
                        }
                    });

                    $("#divShippingMethod").find("table>tbody").append(shippingMethodElements);
                    $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
                    $("#divShippingMethod").find("table>tbody tr:even").addClass("cssClassAlternativeEven");
                    $("#divShippingMethod").find("table>tbody tr:odd").addClass("cssClassAlternativeOdd");
                    if (CheckOut.UserCart.IsFShipping.toLowerCase() == 'true') {
                        $('#Fshipping p b').html('');
                        $('#Fshipping p b').html('Shipping Cost: 0.00 (free shipping)');
                        $('#txtShippingTotal').val('0.00');
                    }
                    //$("input[type='radio'][name='shippingRadio'][value=" + shippingmethodId + "]").attr("checked", "checked");
                    // $("#txtShippingTotal").val('').val($("input[type='radio'][name='shippingRadio'][value=" + shippingmethodId + "]").attr("shippingCost"));
                    CheckOut.UserCart.spMethodID = shippingmethodId;

                    $("input[type='radio'][name='shippingRadio']").bind("change", function() {
                        $(this).attr("checked", "checked");
                        CheckOut.UserCart.spMethodID = $(this).attr("value");
                        CheckOut.UserCart.spCost = $(this).attr("shippingCost");
                        CheckOut.GetDiscountCartPriceRule(CheckOut.UserCart.CartID, CheckOut.UserCart.spCost);
                        $("#txtShippingTotal").val('').val($(this).attr("shippingCost"));
                        if (CheckOut.UserCart.IsFShipping.toLowerCase() == 'true') {
                            $('#Fshipping p b').html('');
                            $('#Fshipping p b').html('Shipping Cost: 0.00 (free shipping)');
                            $('#txtShippingTotal').val('0.00');
                        }
                        $("#txtTotalCost").val(parseFloat($("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, "")) + CheckOut.UserCart.Tax + parseFloat($(".total-box").val().replace(/[^-0-9\.]+/g, "")) - eval(CheckOut.UserCart.TotalDiscount) - eval(CheckOut.UserCart.CartDiscount));
                        CheckOut.UserCart.spCost = $("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, "");
                    });
                    $('#txtDiscountAmount').val(parseFloat(CheckOut.UserCart.TotalDiscount + CheckOut.UserCart.CartDiscount).toFixed(2));
                    $("#txtTotalCost").val(parseFloat($("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, "")) + CheckOut.UserCart.Tax + parseFloat($(".total-box").val().replace(/[^-0-9\.]+/g, "")) - eval(CheckOut.UserCart.TotalDiscount) - eval(CheckOut.UserCart.CartDiscount));

                    break;
                case 8: //bind state
                    if (data.d.length > 0) {
                        $('#ddlBLState').html('');
                        $.each(data.d, function(index, item) {
                            var option = '';
                            option += "<option class='cssBillingShipping'> " + item.Text + "</option>";
                            $('#ddlBLState').append(option);
                            $('#ddlSPState').append(option);
                        });
                    }
                    break;
                case 9: //bind country
                    if (data.d.length > 0) {
                        $('#ddlBLCountry').html('');
                        $('#ddlSPCountry').html('');
                        $.each(data.d, function(index, item) {
                            var option = '';
                            option += "<option class='cssBillingShipping'> " + item.Text + "</option>";
                            $('#ddlBLCountry').append(option);
                            $('#ddlSPCountry').append(option);
                        });
                    }
                    break;
                case 10:
                    CheckOut.UserCart.TotalDiscount = parseFloat(data.d).toFixed(2);
                    break;
                case 11:
                    CheckOut.BindUserAddress();
                    $('#addBillingAddress ,#addShippingAddress').hide();
                    $('#fade, #popuprel').fadeOut();
                    break;


            }
        }, ajaxFailure: function() {
            switch (CheckOut.config.error) {
                case 0:
                    break;
                case 1: //load control                   
                    csscody.error("<h2>Error Message</h2><p>Failed to load payment gateway!</p>");
                    break;
                case 4: //address bind                  
                    csscody.error("<h2>Error Message</h2><p>Failed loading address...please hit refresh</p>");
                    break;
                case 6:
                    csscody.error("<h2>Error Message</h2><p>Error occured while using cart discount</p>");
                    break;
                case 7: //shipping method by weight                 
                    csscody.error("<h2>Error Message</h2><p>Failed to load shipping method</p>");
                    break;
            }
        },
        Init: function() {
            $accor = $("#accordion").accordion({ autoHeight: false, event: false });
            var $billingSelect = $('#dvBillingSelect');
            var $billingInfo = $('#dvBillingInfo');
            var $shippingSelect = $('#dvShippingSelect');
            var $shippingInfo = $('#dvShippingInfo');

            CheckOut.GetCountry();
            $('#ddlBLState').hide();
            $('#ddlSPState').hide();
            CheckOut.GetState();
            CheckOut.GetUserCartDetails();
            CheckOut.GetShippinMethodsFromWeight();
            CheckOut.LoadPGatewayList();
            $('#dvLogin').hide();
            $('#lblAuthCode').hide();
            $('#txtAuthCode').hide();
            $billingSelect.hide();
            $shippingSelect.hide();
            //$('#txtPhone').mask("99-99999999");

            if (AspxCommerce.utils.GetUserName() != 'anonymoususer') {

                CheckOut.BindUserAddress();
                $billingInfo.hide();
                $billingSelect.show();
                CheckOut.UserCart.isUserGuest = false;
                $accor.accordion("activate", 1);
                $('#btnBillingBack').hide();
                if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                    $('#dvBilling .cssClassCheckBox').hide();
                }
                else {
                    $('#dvBilling .cssClassCheckBox').show();
                }

            }
            else {
                $('#btnBillingBack').show();

            }


            if (CheckOut.UserCart.TotalDiscount == 0) {
                CheckOut.QuantitityDiscountAmount();
            }

            $('#rdbRegister').bind("click", function() {
                $('#btnCheckOutMethodContinue').hide();
                $('#dvLogin').show();
                CheckOut.UserCart.isUserGuest = false;
            });
            $('#rdbGuest').bind("click", function() {
                $('#btnCheckOutMethodContinue').show();
                $('#dvLogin').hide();
                $('#txtLoginEmail').val('');
                $('#loginPassword').val('');
                CheckOut.UserCart.isUserGuest = true;
            });
            var v = $("#form1").validate({
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
                        minlength: "* (at least 2 chars)"
                    },
                    Address2: {
                        required: '*',
                        minlength: "* (at least 2 chars)"
                    },
                    Phone: {
                        required: '*',
                        maxlength: "*",
                        minlength: "* (at least 7 digits)"
                        //number: true
                    },
                    Company: {
                        maxlength: "*"
                    },
                    mobile: {
                        maxlength: "*",
                        minlength: "* (at least 10 digits)"
                        //number: true
                    }, Fax: {
                    //number: true
                },
                City: {
                    required: '*',
                    minlength: "* (at least 2 chars)",
                    maxlength: "*"
                },
                stateprovince: {
                    required: '*',
                    minlength: "* (at least 2 chars)",
                    maxlength: "*"
                },
                biZip: {
                    required: '*',
                    minlength: "* (at least 5 chars)",
                    maxlength: "*" //number: true

                },
                spFName: {
                    required: '*',
                    minlength: "* (at least 2 chars)", maxlength: "*"
                },
                spLName: {
                    required: '*',
                    minlength: "* (at least 2 chars)", maxlength: "*"
                },
                spAddress1: {
                    required: '*',
                    minlength: "* (at least 2 chars)",
                    maxlength: "*"
                },
                spAddress2: {
                    required: '*',
                    minlength: "* (at least 2 chars)",
                    maxlength: "*"
                },
                spCountry: {
                    required: '*',
                    minlength: "* (at least 4 chars)"
                },
                spCity: {
                    required: '*',
                    minlength: "* (at least 2 chars)",
                    maxlength: "*"
                },
                SPCompany: {
                    maxlength: "*"
                },
                spZip: {
                    required: '*',
                    minlength: "* (at least 5 chars)", maxlength: "*"
                },
                spstateprovince: {
                    required: '*',
                    minlength: "* (at least 2 chars)",
                    maxlength: "*"
                },
                spPhone: {
                    required: '*',
                    minlength: "* (at least 7 chars)"
                },
                spmobile: {
                    maxlength: "*",
                    minlength: "* (at least 10 digits)"
                },
                cardCode: {
                    required: '*',
                    minlength: "* (at least 3 chars)"
                }
            },
            rules:
            {
                creditCard: {
                    required: true,
                    creditcard: true
                },
                FirstName: { minlength: 2 },
                LastName: { minlength: 2 },
                Address1: { minlength: 2 },
                Address2: { minlength: 2 },
                Phone: { minlength: 7 },
                mobile: { minlength: 10 },
                City: { minlength: 2 },
                stateprovince: { minlength: 2 },
                biZip: { minlength: 5 },
                spFName: { minlength: 2 },
                spLName: { minlength: 2 },
                spAddress1: { minlength: 2 },
                spAddress2: { minlength: 2 },
                spCountry: { minlength: 4 },
                spCity: { minlength: 2 },
                spZip: { minlength: 5 },
                spstateprovince: { minlength: 2 },
                spPhone: { minlength: 7 },
                spmobile: { minlength: 10 },
                cardCode: { minlength: 3 }
            },
            ignore: ":hidden"

        });

        $('#ddlTransactionType').bind("change", function() {
            if ($('#ddlTransactionType option:selected').text() == " CAPTURE_ONLY") {
                $('#lblAuthCode').show();
                $('#txtAuthCode').show();
            }
            else {
                $('#lblAuthCode').hide();
                $('#txtAuthCode').hide();
            }
        });

        $('#btnCheckOutMethodContinue').bind("click", function() {
            if (v.form()) {
                if ($('#rdbGuest').attr('checked') == true) {
                    $accor.accordion("activate", 1);
                    $billingInfo.show();
                    $billingSelect.hide();
                    if ($('#rdbGuest').is(':checked')) {
                        $('#password').hide();
                        $('#confirmpassword').hide();
                    }
                    else {
                        $('#password').show();
                        $('#confirmpassword').show();
                    }
                }
            }
            else {
                csscody.alert("<h2>Information Alert</h2><p>Please fill the form correctly.</p>");
            }
        });


        $('#btnBillingContinue').click(function() {

            if (CheckOut.UserCart.CountAllItem == CheckOut.UserCart.CountDownloadableItem) {
                CheckOut.UserCart.IsDownloadItemInCartFull = true;
            }
            else {
                CheckOut.UserCart.IsDownloadItemInCartFull = false;
            }
            if (v.form()) {
                // alert('');                                    
                CheckOut.AssignItemsDetails();
                if (CheckOut.UserCart.isUserGuest == false) {
                    if ($.trim($('#ddlBilling').text()) == '' || $.trim($('#ddlBilling').text()) == null) {
                        $('#addBillingAddress').show();
                        csscody.alert("<h2>Information Alert</h2><p>Please add billing address to checkout.</p>");
                        return false;
                    } else {
                        CheckOut.BindBillingData();
                        if ($('#txtFirstName').val() == '') {
                            $shippingInfo.hide();
                            $shippingSelect.show();
                        }
                        else {
                            $shippingInfo.show();
                            $shippingSelect.hide();
                        }
                        if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                            CheckOut.AssignItemsDetails();
                            $accor.accordion("activate", 4);
                            $("#txtShippingTotal").val(0);
                            CheckOut.SetSessionValue("ShippingCostAll", $("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, ""));
                        }
                        if ($('#chkBillingAsShipping').is(':checked')) {
                            CheckOut.BindShippingData();
                            if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                                $accor.accordion("activate", 4);
                                $("#txtShippingTotal").val(0);
                                CheckOut.SetSessionValue("ShippingCostAll", $("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, ""));
                            } else {
                                $accor.accordion("activate", 3);
                            }
                        }
                        else {
                            $('#dvCPShipping').html('');
                            $accor.accordion("activate", 2);
                        }
                        // return false;
                    }
                }
                else {
                    CheckOut.BindBillingData();
                    if ($('#txtFirstName').val() == '') {
                        $shippingInfo.hide();
                        $shippingSelect.show();
                    }
                    else {
                        $shippingInfo.show();
                        $shippingSelect.hide();
                    }
                    if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                        $accor.accordion("activate", 4);
                        $("#txtShippingTotal").val(0);
                        CheckOut.SetSessionValue("ShippingCostAll", $("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, ""));
                    }
                    if ($('#chkBillingAsShipping').is(':checked')) {
                        CheckOut.BindShippingData();
                        if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                            CheckOut.AssignItemsDetails();
                            $accor.accordion("activate", 4);
                            $("#txtShippingTotal").val(0);
                            CheckOut.SetSessionValue("ShippingCostAll", $("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, ""));
                        } else {
                            $accor.accordion("activate", 3);
                        }
                    }
                    else {
                        $('#dvCPShipping').html('');
                        $accor.accordion("activate", 2);
                    }
                    // return false;
                }
            }
            else {
                //  alert('Please fill the form correctly');
            }

        });

        $('#btnBillingBack').bind("click", function() {
            if (AspxCommerce.utils.GetUserName() != 'anonymoususer') {
                $accor.accordion("activate", 1);
            } else {

                $('#dvCPBilling').html('');
                $accor.accordion("activate", 0);
            }
        });

        $('#btnShippingContinue').bind("click", function() {
            if (CheckOut.UserCart.CountAllItem == CheckOut.UserCart.CountDownloadableItem) {
                CheckOut.UserCart.IsDownloadItemInCartFull = true;
            }
            else {
                CheckOut.UserCart.IsDownloadItemInCartFull = false;
            }
            if (v.form()) {
                if (CheckOut.UserCart.isUserGuest == false) {
                    if ($.trim($('#ddlShipping').text()) == '' || $.trim($('#ddlShipping').text()) == null) {
                        // alert("Please visit your Dashboard to add Shipping Address!!!");
                        $('#addShippingAddress').show();
                        return false;
                    }
                    else {
                        $.cookies.set('ShippingDetails', $('#ddlShipping option').html());
                        CheckOut.BindShippingData();
                        if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                            $accor.accordion("activate", 4);
                            $("#txtShippingTotal").val(0);
                        } else {
                            $accor.accordion("activate", 3);
                        }
                    }
                }
                else {
                    $.cookies.set('ShippingDetails', $('#ddlShipping option').html());
                    CheckOut.BindShippingData();
                    if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                        $accor.accordion("activate", 4);
                        $("#txtShippingTotal").val(0);
                        CheckOut.SetSessionValue("ShippingCostAll", $("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, ""));
                    } else {
                        $accor.accordion("activate", 3);
                    }
                }
            }
            else {
                // csscody.alert("<h2>Information Alert</h2><p>Please fill the form correctly.</p>");
            }
        });

        $('#btnShippingBack').bind("click", function() {
            $('#dvCPShipping').html('');
            $accor.accordion("activate", 1);
            if ($('#chkBillingAsShipping').attr('checked') == 'true' || $('#chkBillingAsShipping').attr('checked') == true) {
                $('#chkBillingAsShipping').attr('checked', false);
            }
        });

        $('#btnShippingMethodBack').bind("click", function() {
            if ($('#chkBillingAsShipping').is(':checked')) {
                $accor.accordion("activate", 1);
            }
            else {
                $accor.accordion("activate", 2);
            }
        });
        $('#btnShippingMethodContinue').bind("click", function() {
            $('#btnShippingMethodContinue').show();
            var count = 0;
            CheckOut.AssignItemsDetails();

            $("input[type='radio'][name='shippingRadio']").each(function() {
                if ($(this).is(':checked') == true)
                    count = 1;
            });
            if (count == 1) {
                CheckOut.AssignItemsDetails();
                var discountTotal = eval(CheckOut.UserCart.TotalDiscount) + eval(CheckOut.UserCart.CartDiscount);
                CheckOut.SetSessionValue("DiscountAll", discountTotal);
                //   $.cookie('Discount', eval(TotalDiscount) + eval(CartDiscount));
                CheckOut.SetSessionValue("ShippingCostAll", $("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, ""));
                var gt = $("#txtTotalCost").val().replace(/[^-0-9\.]+/g, "");
                if (gt == 'NaN')
                    gt = 0;
                CheckOut.SetSessionValue("GrandTotalAll", parseFloat(gt));
                CheckOut.BindShippingMethodData();
                $accor.accordion("activate", 4);

            }

            else {
                csscody.alert("<h2>Information Alert</h2><p>Please check at least one shipping method.</p>");
                // $('#divShippingMethod table tr td').html('').html("  Either Items weight in your cart doesn't meet the shipping provider weight criteria or No shipping Method is set in this store!!");
                // $('#btnShippingMethodContinue').hide();
            }
            var gt1 = $("#txtTotalCost").val().replace(/[^-0-9\.]+/g, "");
            if (gt1 == 'NaN')
                gt1 = 0;
            CheckOut.SetSessionValue("GrandTotalAll", parseFloat(gt1));
        });


        $('#btnPaymentInfoContinue').bind("click", function() {
            var Total = 0;
            if (v.form()) {
                if ($.trim(CheckOut.BillingAddress.country) == "United States") {
                    CheckOut.BillingAddress.state = $('#ddlBLState :selected').text();
                }
                if ($.trim(CheckOut.ShippingAddress.spCountry) == "United States") {
                    CheckOut.ShippingAddress.spState = $('#ddlSPState :selected').text();
                }
                if ($('#dvPGList input:radio:checked').attr('checked') == 'checked' || $('#dvPGList input:radio:checked').attr('checked') == true) {
                    if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                        CheckOut.GetDiscountCartPriceRule(CheckOut.UserCart.CartID, 0);
                    }
                    if ($.trim($('#dvPGList input:radio:checked').attr('friendlyname')) == 'aimauthorize') {
                        if ($('#AIMChild').length > 0) {
                            CheckOut.BindPaymentData();
                            $('#txtDiscountAmount').val(parseFloat(eval(CheckOut.UserCart.TotalDiscount) + eval(CheckOut.UserCart.CartDiscount)).toFixed(2));
                            CheckOut.UserCart.Total = parseFloat(eval($("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, ""))) + CheckOut.UserCart.Tax + parseFloat(eval($(".total-box").val().replace(/[^-0-9\.]+/g, ""))) - eval(CheckOut.UserCart.TotalDiscount) - eval(CheckOut.UserCart.CartDiscount);
                            $("#txtTotalCost").val(Total.toFixed(2));
                            // $("#txtTotalCost").val(parseFloat($("#txtShippingTotal").val()) + Tax + parseFloat($(".total-box").val()) - eval(TotalDiscount) - eval(CartDiscount));
                            $('#txtTax').val(CheckOut.UserCart.Tax.toFixed(2));
                            CheckOut.SetSessionValue("TaxAll", CheckOut.UserCart.Tax);
                            var gt = $("#txtTotalCost").val().replace(/[^-0-9\.]+/g, "");
                            if (gt == 'NaN')
                                gt = 0;
                            CheckOut.SetSessionValue("GrandTotalAll", parseFloat(gt));
                            $accor.accordion("activate", 5);

                            if ($("#txtTotalCost").val().replace(/[^-0-9\.]+/g, "") < 0) {
                                csscody.alert("<h2>Information Alert</h2><p>Your cart is not eligible to checkout due to a negatve total amount!</p>");
                                $('#dvPlaceOrder .cssClassButtonWrapper ').find('input').not("#btnPlaceBack").remove();
                                $("#dvPGList input:radio").attr("disabled", "disabled");
                            } else {
                                $("#dvPGList input:radio").attr("disabled", false);
                            }

                        }
                        else {
                        }

                    }
                    else {
                        CheckOut.BindPaymentData();
                        $('#txtDiscountAmount').val(parseFloat(eval(CheckOut.UserCart.TotalDiscount) + eval(CheckOut.UserCart.CartDiscount)).toFixed(2));
                        CheckOut.UserCart.Total = parseFloat($("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, "")) + CheckOut.UserCart.Tax + parseFloat($(".total-box").val().replace(/[^-0-9\.]+/g, "")) - eval(CheckOut.UserCart.TotalDiscount) - eval(CheckOut.UserCart.CartDiscount);
                        $("#txtTotalCost").val(CheckOut.UserCart.Total.toFixed(2));
                        //$("#txtTotalCost").val(parseFloat($("#txtShippingTotal").val()) + Tax + parseFloat($(".total-box").val()) - eval(TotalDiscount) - eval(CartDiscount));
                        $('#txtTax').val(CheckOut.UserCart.Tax.toFixed(2).replace(/[^-0-9\.]+/g, ""));
                        CheckOut.SetSessionValue("TaxAll", CheckOut.UserCart.Tax);
                        var gt = $("#txtTotalCost").val().replace(/[^-0-9\.]+/g, "");
                        if (gt == 'NaN')
                            gt = 0;
                        CheckOut.SetSessionValue("GrandTotalAll", parseFloat(gt));
                        $accor.accordion("activate", 5);
                        if ($("#txtTotalCost").val().replace(/[^-0-9\.]+/g, "") < 0) {
                            csscody.alert("<h2>Information Alert</h2><p>Your cart is not eligible to checkout due to a negatve total amount!</p>");
                            $('#dvPlaceOrder .cssClassButtonWrapper ').find('input').not("#btnPlaceBack").remove();
                            $("#dvPGList input:radio").attr("disabled", "disabled");
                        } else {
                            $("#dvPGList input:radio").attr("disabled", false);
                        }

                    }
                }
                else {
                    csscody.alert("<h2>Information Message</h2><p>Please select your payment system.</p>");
                }
            }
            else {
                csscody.alert("<h2>Information Alert</h2><p>Please fill the form correctly.</p>");
            }
            $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
        });

        $('#btnPaymentInfoBack').bind("click", function() {
            $('#dvCPPaymentMethod').html('');
            if (CheckOut.UserCart.IsDownloadItemInCartFull) {
                $accor.accordion("activate", 1);
                $("#txtShippingTotal").val(0);
                CheckOut.SetSessionValue("ShippingCostAll", $("#txtShippingTotal").val().replace(/[^-0-9\.]+/g, ""));
            } else {
                $accor.accordion("activate", 3);
            }
        });

        $('#btnPlaceBack').bind("click", function() {
            $accor.accordion("activate", 4);
        });

        $('#chkBillingAsShipping').bind("click", function() {
            CheckOut.AddBillingAsShipping();
        });

        $("#ddlSPCountry ,#ddlBLCountry ").bind("change", function() {
            if ($.trim($("#ddlSPCountry :selected").text()) == 'United States') {
                $('#ddlSPState').show();
                $('#txtSPState').hide();
            }
            else {
                $('#ddlSPState').hide();
                $('#txtSPState').show();
            }

            if ($.trim($("#ddlBLCountry :selected").text()) == 'United States') {
                $('#ddlBLState').show();
                $('#txtState').hide();
            }
            else {
                $('#ddlBLState').hide();
                $('#txtState').show();
            }
        });

        $(".cssClassClose").bind("click", function() {
            $('#fade, #popuprel').fadeOut();
            $('#popuprel .cssClassFormWrapper table').empty();
        });

        $('#addBillingAddress ,#addShippingAddress').bind("click", function() {
            $('#popuprel .cssClassFormWrapper table').empty();
            $('<table  width="100%" border="0" cellpadding="0" cellspacing="0">' + $('#dvBillingInfo table').html() + '</table>').insertBefore('#popuprel .cssClassFormWrapper .cssClassButtonWrapper');
            CheckOut.ClearAll();
            switch ($(this).attr('id')) {
                case "addBillingAddress":
                    ShowPopupControl("popuprel");
                    $('#trBillingAddress ,#trShippingAddress').show();
                    $("#popuprel .cssClassFormWrapper table #chkShippingAddress").attr("checked", "checked");
                    $("#popuprel .cssClassFormWrapper table #chkShippingAddress").attr("disabled", "disabled");
                    $("#popuprel .cssClassFormWrapper table #chkBillingAddress").attr("checked", "checked");
                    $("#popuprel .cssClassFormWrapper table #chkBillingAddress").attr("disabled", "disabled");
                    break;
                case "addShippingAddress":
                    $('#popuprel .cssClassFormWrapper table tr:nth-child(7)').remove();
                    ShowPopupControl("popuprel");
                    break;
            }
            // ShowPopupControl("popuprel");
            $("#popuprel .cssClassFormWrapper table #ddlSPCountry ,#popuprel .cssClassFormWrapper table #ddlBLCountry").bind("change", function() {

                if ($.trim($("#popuprel .cssClassFormWrapper table #ddlSPCountry :selected").text()) == 'United States') {
                    $('#popuprel .cssClassFormWrapper table #ddlSPState').show();
                    $('#popuprel .cssClassFormWrapper table #txtSPState').hide();
                }
                else {
                    $('#popuprel .cssClassFormWrapper table #ddlSPState').hide();
                    $('#popuprel .cssClassFormWrapper table #txtSPState').show();
                }

                if ($.trim($("#popuprel .cssClassFormWrapper table #ddlBLCountry :selected").text()) == 'United States') {
                    $('#popuprel .cssClassFormWrapper table #ddlBLState').show();
                    $('#popuprel .cssClassFormWrapper table #txtState').hide();
                }
                else {
                    $('#popuprel .cssClassFormWrapper table #ddlBLState').hide();
                    $('#popuprel .cssClassFormWrapper table #txtState').show();
                }
            });

        });

        $('#btnSubmitAddress').bind("click", function() {
            if (v.form()) {
                CheckOut.AddUpdateUserAddress();
            }
        });
        $('#trBillingAddress , #trShippingAddress').hide();
        $('#addBillingAddress , #addShippingAddress').hide();
        if ($.trim($('#ddlBilling').text()) == "" || $.trim($('#ddlBilling').text()) == null) {

            $('#addBillingAddress').show();
        } else {
            $('#addBillingAddress').hide();
        }
        if ($.trim($('#ddlShipping').text()) == "" || $.trim($('#ddlShipping').text()) == null) {
            // alert("Please visit your Dashboard to add Shipping Address!!!");
            $('#addShippingAddress').show();
        } else { $('#addShippingAddress').hide(); }

        CheckOut.CheckDownloadableOnlyInCart();

        var register = "";
        var checkouturl = "";
        if (AspxCommerce.utils.IsUserFriendlyUrl())
        { register = 'User-Registration.aspx'; checkouturl = "Single-Address-Checkout.aspx"; }
        else { register = 'User-Registration'; checkouturl = "Single-Address-Checkout"; }
        //$('.cssClassRegisterlnk').html('<a href ="' + AspxCommerce.utils.GetAspxRedirectPath() + register + '?ReturnUrl=' + AspxCommerce.utils.GetAspxRedirectPath() + checkouturl + '"><span class="cssClassRegisterLink">Register</span></a>');

        if (CheckOut.UserCart.IsDownloadItemInCartFull) {
            $('#dvBilling .cssClassCheckBox').hide();
        }
        else {
            $('#dvBilling .cssClassCheckBox').show();
        }
    }
};
CheckOut.Init();
});