<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountPassword.ascx.cs" Inherits="Modules_AspxCommerce_AspxUserDashBoard_AccountPassword" %>

<script type="text/javascript">
    //<![CDATA[
    $(function() {
        var accountPassword = {
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
                    type: accountPassword.config.type,
                    contentType: accountPassword.config.contentType,
                    cache: accountPassword.config.cache,
                    async: accountPassword.config.async,
                    url: accountPassword.config.url,
                    data: accountPassword.config.data,
                    dataType: accountPassword.config.dataType,
                    success: accountPassword.ajaxSuccess,
                    error: accountPassword.ajaxFailure
                });
            },
            UpdateUserPassword: function() {
                var newPassword = $("#txtNewPassword").val();
                var retypePassword = $("#txtConfirmPassword").val();
                this.config.url = this.config.baseURL + "ChangePassword";
                this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, newPassword: newPassword, retypePassword: retypePassword });
                this.config.ajaxCallMode = 1;
                this.ajaxCall(this.config);
                return false;
            },
            ajaxSuccess: function(data) {
                switch (accountPassword.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        if (data.d) {
                            $("#txtNewPassword").val(''); $("#txtConfirmPassword").val('');                           
                            csscody.info("<h2>Successful Message</h2><p>Password has been changed successfully.</p>");
                        }
                        else {                           
                            csscody.error('<h2>Error Message</h2><p>Failed to change password!</p>');
                        }
                        break;
                }
            },
            ajaxFailure: function(data) {
                switch (accountPassword.config.ajaxCallMode) {
                    case 0:
                        break;
                    case 1:
                        csscody.error('<h2>Error Message</h2><p>Failed to change password!</p>');
                        break;
                }
            },
            init: function(config) {
                $('#txtConfirmPassword').change(function() {
                    if ($("#txtNewPassword").val() != $("#txtConfirmPassword").val()) {
                        $('#lblnotification').html(' Password not matched.');

                    } else {
                        $('#lblnotification').html('');
                    }
                });

                $('#btnSubmitPassword').bind("click", function() {                   
                    var d = $("#form1").validate({
                        rules: {
                            Password: { required: true },
                            ConfirmPassword: { required: true }
                        }

                    });
                    if (d.form()) {
                        if ($("#txtNewPassword").val() == $("#txtConfirmPassword").val()) {
                           accountPassword.UpdateUserPassword();
                            return false;
                        }
                    }

                });
            }
        }
        accountPassword.init();
    });
</script>

<div class="cssClassFormWrapper">
    <div class="cssClassHeading">
        <h1>
            <asp:Label ID="lblAccountTitle" runat="server" Text="Change Password"></asp:Label>
        </h1>
        <div class="cssClassClear">
        </div>
    </div>
    <table id="tblAccountPassword" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td width="20%">
                    <asp:Label ID="lblPassword" runat="server" Text="New Password:" CssClass="cssClassLabel"></asp:Label>
                    <span class="cssClassRequired">*</span>
                </td>
                <td width="80%">
                    <input type="password" id="txtNewPassword" name="Password" class="required" minlength="4"  />
                </td>
             <%--   <td>
                    <div class="password-meter">
                        <div class="password-meter-message">
                        </div>
                        <div class="password-meter-bg">
                            <div class="password-meter-bar">
                            </div>
                        </div>
                    </div>
                </td>--%>
                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbRetypePassword" runat="server" Text="Confirm Password:" CssClass="cssClassLabel"></asp:Label><span
                        class="cssClassRequired">*</span>
                </td>
                <td>
                    <input type="password" id="txtConfirmPassword" name="ConfirmPassword" class="required" /><label id='lblnotification' style ="color:#FF0000;"></label>
                </td>
            </tr>
           
        </tbody>
    </table>
    <div class="cssClassButtonWrapper">
        <button type="button" name="btnSubmitPassword" id="btnSubmitPassword" class="cssClassButtonSubmit">
            <span><span>Save Password</span></span></button>
    </div>
</div>
