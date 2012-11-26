<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountInformation.ascx.cs"
    Inherits="Modules_AspxCommerce_AspxUserDashBoard_AccountInformation" %>

<script type="text/javascript">
    //<![CDATA[
    
    $(function() {
        var accountInformation = {
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
                    type: accountInformation.config.type,
                    contentType: accountInformation.config.contentType,
                    cache: accountInformation.config.cache,
                    async: accountInformation.config.async,
                    url: accountInformation.config.url,
                    data: accountInformation.config.data,
                    dataType: accountInformation.config.dataType,
                    success: accountInformation.ajaxSuccess,
                    error: accountInformation.ajaxFailure
                });
            },
            UpdateCustomerInformation: function() {
                var customerFirstName = $("#txtFirstName").val();
                var customerLastName = $("#txtLastName").val();
                var customerEmail = $("#txtEmailAddress").val();

                this.config.url = this.config.baseURL + "UpdateCustomer";
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, userName: userName, firstName: customerFirstName, lastName: customerLastName, email: customerEmail });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
                return false;
            },
            ajaxSuccess: function(data) {
                switch (accountInformation.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        if (data.d == 1) {
                            csscody.alert("<h2>Information Alert</h2><p>Email address should be unique.</p>");
                        }
                        else if (data.d == -1) {
                            csscody.error('<h2>Error Message</h2><p>Failed to update account information!</p>');
                        }
                        else {
                            csscody.info("<h2>Successful Message</h2><p>Information has been updated successfully.</p>");
                        }
                        break;
                }
            },
            ajaxFailure: function(msg) {
                switch (accountInformation.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        csscody.error('<h2>Error Message</h2><p>Failed to update account information!</p>');
                        break;
                }
            },
            init: function(config) {

                $("#txtFirstName").val(userFirstName);
                $("#txtLastName").val(userLastName);
                $("#txtEmailAddress").val(userEmail);

                var v = $("#form1").validate({
                    rules: {
                        FirstName: "required",
                        LastName: "required",

                        Email: {
                            required: true,
                            email: true
                        }
                    },
                    messages: {
                        FirstName: {
                            required: '*',
                            minlength: "* (at least 2 chars)"
                        },
                        LastName: {
                            required: '*',
                            minlength: "* (at least 2 chars)"
                        },
                        Email: {
                            required: '*',
                            email: '*'
                        }
                    }
                });
                $('#btnSubmitInformation').click(function() {
                    if (v.form()) {
                        accountInformation.UpdateCustomerInformation();
                        return false;
                    }
                    else {
                        return false;
                    }
                });
            }
        }
        accountInformation.init();
    });  
    
    
//    $(document).ready(function() {       
//        // $("#form1").attr("onSubmit", "return false");

//        $("#txtFirstName").val(userFirstName);
//        $("#txtLastName").val(userLastName);
//        $("#txtEmailAddress").val(userEmail);

//        var v = $("#form1").validate({
//            rules: {
//                FirstName: "required",
//                LastName: "required",

//                Email: {
//                    required: true,
//                    email: true
//                }
//            },
//            messages: {
//                FirstName: {
//                    required: '*',
//                    minlength: "* (at least 2 chars)"
//                },
//                LastName: {
//                    required: '*',
//                    minlength: "* (at least 2 chars)"
//                },
//                Email: {
//                    required: '*',
//                    email: '*'
//                }
//            }

//        });
//        $('#btnSubmitInformation').click(function() {
//            if (v.form()) {
//                UpdateCustomerInformation();
//                return false;
//            }
//            else {
//                return false;
//            }
//        });
//    });

//    function UpdateCustomerInformation() {
//        var customerFirstName = $("#txtFirstName").val();
//        var customerLastName = $("#txtLastName").val();
//        var customerEmail = $("#txtEmailAddress").val();
//        var param = JSON2.stringify({ storeID: storeId, portalID: portalId, customerID: customerId, userName: userName, firstName: customerFirstName, lastName: customerLastName, email: customerEmail });
//        $.ajax({
//            type: "POST",
//            url: aspxservicePath + "AspxCommerceWebService.asmx/UpdateCustomer",
//            data: param,
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: function(data) {
//                if (data.d == 1) {
//                   // alert("Email Address Should Be Unique");
//                    csscody.alert("<h2>Information Message</h2><p>Email Address Should Be Unique.</p>");

//                }
//                else if (data.d == -1) {
//                //alert("Update Could Not Successed");
//                csscody.error('<h2>Error Message</h1><p>Update Could Not Successed</p>');
//                }
//                else {
//                    //alert("Customer Information Updated Successfully");
//                    csscody.alert("<h2>Information Message</h2><p>Customer Information Updated Successfully.</p>");
//                }
//            }
////            ,
////            error: function() {
////                alert("Error!");
////            }
//        });
//        return false;
    //    } 

    //]]>   
</script>

<div class="cssClassFormWrapper">
    <div class="cssClassHeading">
        <h1>
            <asp:Label ID="lblAddressTitle" runat="server" Text="Account Information"></asp:Label>
        </h1>
        <div class="cssClassClear">
        </div>
    </div>
    <table id="tblNewAddress" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td width="20%">
                    <asp:Label ID="lblFirstName" runat="server" Text="FirstName:" CssClass="cssClassLabel"></asp:Label>
                    <span class="cssClassRequired">*</span>
                </td>
                <td width="80%">
                    <input type="text" id="txtFirstName" name="FirstName" class="required" minlength="2" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLastName" runat="server" Text="LastName:" CssClass="cssClassLabel"></asp:Label><span
                        class="cssClassRequired">*</span>
                </td>
                <td>
                    <input type="text" id="txtLastName" name="LastName" class="required" minlength="2" />
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
            </tr>
        </tbody>
    </table>
    <div class="cssClassButtonWrapper">
        <button type="submit" name="btnSubmit" id="btnSubmitInformation" class="cssClassButtonSubmit">
            <span><span>Save</span></span></button>
    </div>
</div>
