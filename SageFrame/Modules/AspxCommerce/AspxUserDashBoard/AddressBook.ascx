<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddressBook.ascx.cs" Inherits="Modules_AspxUserDashBoard_AddressBook" %>

<script type="text/javascript">
    //<![CDATA[

    $(function() {
        var addressBook = {
            variables: {
                checkIfExist: '',
                addNewAddress: 0
            },
            config: {
                isPostBack: false,
                async: false,
                cache: false,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: 'json',
                baseURL: aspxservicePath + "AspxCommerceWebService.asmx/",
                method: "",
                url: "",
                ajaxCallMode: 0
            },
            ajaxCall: function(config) {
                $.ajax({
                    type: addressBook.config.type,
                    contentType: addressBook.config.contentType,
                    cache: addressBook.config.cache,
                    async: addressBook.config.async,
                    url: addressBook.config.url,
                    data: addressBook.config.data,
                    dataType: addressBook.config.dataType,
                    success: addressBook.ajaxSuccess,
                    error: addressBook.ajaxFailure
                });
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
                $("#txtZip").val('');
                $("#ddlUSState").val(1);
                $("#txtPhone").val('');
                $("#txtMobile").val('');
                $("#txtFax").val('');
                $("#txtWebsite").val('');
                $("#chkShippingAddress").removeAttr("checked");
                $("#chkBillingAddress").removeAttr("checked");
                $("#chkShippingAddress").removeAttr("disabled");
                $("#chkBillingAddress").removeAttr("disabled");
            },
            BindAddressDetails: function(msg) {

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
                            defaultBillingAddressElements += ' <h3>Default Billing Address</h3>';
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
                    else {
                        addressElements += '<li>';
                        addressElements += '<p><label name="FirstName">' + value.FirstName + '</label><label name="LastName">' + value.LastName + '</label><br>';
                        addressElements += '<label name="Email">' + value.Email + '</label><br>';
                        addressElements += '<label name="Company">' + value.Company + '</label><br/>';
                        addressElements += '<label name="Address1">' + value.Address1 + '</label><br>';
                        addressElements += '<label name="Address2">' + value.Address2 + '</label><br>';
                        addressElements += '<label name="City">' + value.City + '</label><br>';
                        addressElements += '<label name="State">' + value.State + '</label><br>';
                        addressElements += 'Zip:<label name="Zip">' + value.Zip + '</label><br>';
                        addressElements += '<label name="Country">' + value.Country + '</label><br>';
                        addressElements += 'P: <label name="Phone">' + value.Phone + '</label><br>M: <label name="Mobile">' + value.Mobile + '</label><br>';
                        addressElements += 'F: <label name="Fax">' + value.Fax + '</label><br>';
                        addressElements += '<label name="Website">' + value.Website + '</label></p>';
                        addressElements += ' <p class="cssClassChange"><a href="#" rel="popuprel" name="EditAddress" value="' + value.AddressID + '" Flag="0">Edit Address</a> <a href="#" name="DeleteAddress" value="' + value.AddressID + '">Delete Address</a></p></li>';
                    }
                });

                if (defaultShippingExist) {
                    $("#hdnDefaultShippingExist").val('1');
                }
                else {
                    $("#hdnDefaultShippingExist").val('0');
                    $("#liDefaultShippingAddress").html("<span class=\"cssClassNotFound\">You have not set default shipping adresss yet!</span>");
                }
                if (defaultBillingExist) {
                    $("#hdnDefaultBillingExist").val('1');
                }
                else {
                    $("#hdnDefaultBillingExist").val('0');
                    $("#liDefaultBillingAddress").html("<span class=\"cssClassNotFound\">You have not set default billing adresss yet!</span>");
                }
                $("#olAddtionalEntries").html(addressElements);
                $("a[name='EditAddress']").bind("click", function() {
                    addressBook.ClearAll();
                    $("#hdnAddressID").val($(this).attr("value"));
                    $("#txtFirstName").val($(this).parent('p').prev('p').find('label[name="FirstName"]').text());
                    $("#txtLastName").val($(this).parent('p').prev('p').find('label[name="LastName"]').text());
                    $("#txtEmailAddress").val($(this).parent('p').prev('p').find('label[name="Email"]').text());
                    $("#txtCompanyName").val($(this).parent('p').prev('p').find('label[name="Company"]').text());
                    $("#txtAddress1").val($(this).parent('p').prev('p').find('label[name="Address1"]').text());
                    $("#txtAddress2").val($(this).parent('p').prev('p').find('label[name="Address2"]').text());
                    $("#txtCity").val($(this).parent('p').prev('p').find('label[name="City"]').text());
                    $("#txtState").val($(this).parent('p').prev('p').find('label[name="State"]').text());
                    $("#txtZip").val($(this).parent('p').prev('p').find('label[name="Zip"]').text());
                    var countryName = $(this).parent('p').prev('p').find('label[name="Country"]').text();
                    $('#ddlCountry').val($('#ddlCountry option:contains(' + countryName + ')').attr('value'));
                    if ($.trim(countryName) == 'United States') {
                        $('#ddlUSState').show(); $('#txtState').hide();
                        if ($(this).parent('p').prev('p').find('label[name="State"]').text() != '' || $(this).parent('p').prev('p').find('label[name="State"]').text() != null) {
                            addressBook.GetStateList();
                            var st = $(this).parent('p').prev('p').find('label[name="State"]').text();
                            $('#ddlUSState').val($('#ddlUSState option:contains(' + st + ')').attr('value'));
                        }

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
                $("p >a[name='DeleteAddress']").bind("click", function() {
                    var addressId = $(this).attr("value");
                    var properties = { onComplete: function(e) {
                        addressBook.ConfirmAddressDelete(addressId, e);
                    }
                    }
                    // Ask user's confirmation before delete records        
                    csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this address?</p>", properties);
                });
            },
            GetAddressBookDetails: function() {
                this.config.url = this.config.baseURL + "GetAddressBookDetails";
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, userName: userName, cultureName: cultureName });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
            },
            GetAllCountry: function() {
                this.config.url = this.config.baseURL + "BindCountryList";
                this.config.data = "{}";
                this.config.ajaxCallMode = 2;
                this.ajaxCall(this.config);
            },
            GetStateList: function() {
                this.config.url = this.config.baseURL + "BindStateList";
                this.config.data = "{}";
                this.config.ajaxCallMode = 3;
                this.ajaxCall(this.config);
            },
            AddUpdateUserAddress: function() {
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

                this.config.url = this.config.baseURL + "AddUpdateUserAddress";
                this.config.data = JSON2.stringify({ addressID: addressId, customerID: customerId, firstName: firstName, lastName: lastName, email: email, company: company, address1: address1, address2: address2,
                    city: city, state: state, zip: zip, phone: phone, mobile: mobile, fax: fax, webSite: webSite, countryName: countryName, isDefaultShipping: isDefaultShipping, isDefaultBilling: isDefaultBilling, storeID: storeId,
                    portalID: portalId, userName: userName, cultureName: cultureName
                });
                this.config.ajaxCallMode = 4;
                this.ajaxCall(this.config);
                return false;
            },
            DeleteAddressBook: function(id) {
                this.config.url = this.config.baseURL + "DeleteAddressBook";
                this.config.data = JSON2.stringify({ addressID: id, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName });
                this.config.ajaxCallMode = 5;
                this.ajaxCall(this.config);
            },

            CheckAddressAlreadyExist: function() {               
                this.config.url = this.config.baseURL + "CheckAddressAlreadyExist";
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, sessionCode: sessionCode });
                this.config.ajaxCallMode = 6;
                this.ajaxCall(this.config);
                return addressBook.variables.checkIfExist;
            },

            ConfirmAddressDelete: function(id, event) {
                if (event) {
                    addressBook.DeleteAddressBook(id);
                }
            },

            ajaxSuccess: function(msg) {
                switch (addressBook.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        addressBook.BindAddressDetails(msg);
                        break;
                    case 2:
                        var countryElements = '';
                        $.each(msg.d, function(index, value) {
                            countryElements += '<option value=' + value.Value + '>' + value.Text + '</option>';
                        });
                        $("#ddlCountry").html(countryElements);
                        break;
                    case 3:
                        $('#ddlUSState').show();
                        $('#txtState').hide();
                        $("#ddlUSState").html('');
                        $.each(msg.d, function(index, item) {
                            $("#ddlUSState").append("<option value=" + item.Value + ">" + item.Text + "</option>");
                        });
                        break;
                    case 4:
                        addressBook.GetAddressBookDetails();
                        $('#fade, #popuprel').fadeOut();
                        break;
                    case 5:
                        addressBook.GetAddressBookDetails();
                          csscody.info("<h2>Successful Message</h2><p>Address has been deleted successfully.</p>");
                        break;
                    case 6:
                        addressBook.variables.checkIfExist = msg.d;
                        break;
                }
            },
            ajaxFailure: function(data) {
                switch (addressBook.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        csscody.error('<h2>Error Message</h2><p>Failed to load state list!</p>');
                        break;
                    case 4:
                        csscody.error('<h2>Error Message</h2><p>Failed to update address book!</p>');
                        break;
                    case 5:
                        csscody.error('<h2>Error Message</h2><p>Failed to delete!</p>');
                        break;
                }
            },

            init: function(config) {
           
                addressBook.GetAddressBookDetails();
                addressBook.GetAllCountry();
                $('#ddlUSState').hide();
                $("#lnkNewAddress").bind("click", function() {
                 addressBook.variables.addNewAddress=1;
                    if (allowMultipleAddress.toLowerCase() == 'false') {
                        var checkExist = addressBook.CheckAddressAlreadyExist();
                        if (checkExist) {
                            csscody.alert('<h2>Alert Message</h2><p>Multiple address book is disabled. Try again later!</p>');                           
                            return false;
                        }
                        else {
                            addressBook.ClearAll();
                            if ($("#hdnDefaultShippingExist").val() == "0") {
                                $("#chkShippingAddress").attr("checked", "checked");
                                $("#chkShippingAddress").attr("disabled", "disabled");
                            }
                            if ($("#hdnDefaultBillingExist").val() == "0") {
                                $("#chkBillingAddress").attr("checked", "checked");
                                $("#chkBillingAddress").attr("disabled", "disabled");
                            }
                            ShowPopup(this);
                        }
                    }

                    else {
                        addressBook.ClearAll();
                        if ($("#hdnDefaultShippingExist").val() == "0") {
                            $("#chkShippingAddress").attr("checked", "checked");
                            $("#chkShippingAddress").attr("disabled", "disabled");
                        }
                        if ($("#hdnDefaultBillingExist").val() == "0") {
                            $("#chkBillingAddress").attr("checked", "checked");
                            $("#chkBillingAddress").attr("disabled", "disabled");
                        }
                        ShowPopup(this);
                    }
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

                var v = $("#form1").validate({
                    //            rules: {
                    //                FirstName: { minlength: 2 },
                    //                LastName: { minlength: 2 },
                    //                Address1: { minlength: 2 },
                    //                Phone: { minlength: 7 },
                    //                State: { minlength: 2 },
                    //                Zip: { minlength: 5 },
                    //                City: { minlength: 2 }
                    //            },
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
                        Wedsite: {
                            url: '*'
                        },
                        Address1: {
                            required: '*',
                            minlength: "* (at least 2 chars)", maxlength: "*"
                        },
                        Address2: {
                            maxlength: "*"
                        },
                        Phone: {
                            required: '*',
                            minlength: "* (at least 7 digits)", maxlength: "*"
                        },
                        Mobile: {
                            minlength: "* (at least 10 digits)", maxlength: "*"
                        },
                        Fax: {
                            maxlength: "*"
                        },
                        State: {
                            required: '*',
                            minlength: "* (at least 2 chars)", maxlength: "*"
                        },
                        Zip: {
                            required: '*',
                            minlength: "* (at least 5 chars)", maxlength: "*"
                        },
                        City: {
                            required: '*',
                            minlength: "* (at least 2 chars)", maxlength: "*"
                        }
                    },
                    ignore: ":hidden"
                });

                $('#btnSubmitAddress').bind("click", function() {
                    if (v.form()) {
                        addressBook.AddUpdateUserAddress();
                        if(addressBook.variables.addNewAddress >0)
                        {
                        csscody.info("<h2>Successful Message</h2><p>Address has been saved successfully.</p>");
                        }
                        else{
                         csscody.info("<h2>Successful Message</h2><p>Address has been updated successfully.</p>");
                        }
                        addressBook.variables.addNewAddress=0;
                        return false;
                    }
                    else {
                        return false;
                    }
                });

                $("#ddlCountry ").bind("change", function() {

                    if ($("#ddlCountry :selected").text() == 'United States') {
                        addressBook.GetStateList();
                    }
                    else {
                        $('#ddlUSState').hide();
                        $('#txtState').show();
                    }
                });
            }
        }
        addressBook.init();
    });
 
   //]]>
</script>

<div class="cssClasMyAccountInformation">
    <div class="cssClassHeading">
        <h1>
            Address Book</h1>
        <div class="cssClassButtonWrapper cssClassRight">
            <a href="#" id="lnkNewAddress" rel="popuprel"><span><span>Add New Address</span></span>
            </a>
        </div>
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
                    <li id="liDefaultBillingAddress"></li>
                </ol>
            </div>
        </div>
        <div class="cssClassCol2">
            <div class="cssClassAddressBook">
                <h3>
                    Additional Addresses Entries</h3>
                <ol id="olAddtionalEntries">
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
                        <input type="text" id="txtFirstName" name="FirstName" class="required" minlength="2" maxlength="40"/>
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
                        <input type="text" id="txtCompanyName" name="Company" maxlength="40" />
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
                        <input type="text" id="txtState" name="State" class="required" minlength="2" maxlength="250"/> <select id="ddlUSState" class="cssClassDropDown">
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
                        <input type="text" id="txtCity" name="City" class="required" minlength="2" maxlength="250" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPhone" Text="Phone:" runat="server" CssClass="cssClassLabel"></asp:Label><span
                            class="cssClassRequired">*</span>
                    </td>
                    <td>
                        <input type="text" id="txtPhone" name="Phone" class="required number" minlength="7" maxlength="20" />
                    </td>
                    <td>
                        <asp:Label ID="lblMobile" Text="Mobile:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtMobile" name="Mobile" class="number" minlength="10"  maxlength="20"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFax" Text="Fax:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtFax" name="Fax" class="number" maxlength="20" />
                    </td>
                    <td>
                        <asp:Label ID="lblWebsite" Text="Website:" runat="server" CssClass="cssClassLabel"></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="txtWebsite" name="Wedsite" class="url" maxlength="50"/>
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
