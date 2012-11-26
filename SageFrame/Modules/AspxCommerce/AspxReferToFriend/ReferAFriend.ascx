<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReferAFriend.ascx.cs"
    Inherits="Modules_AspxReferToFriend_ReferAFriend" %>

<script type="text/javascript" language="javascript">
    //<![CDATA[
       var ReferAFriend="";
       $(function() {
       var storeId = AspxCommerce.utils.GetStoreID();
       var portalId = AspxCommerce.utils.GetPortalID();
       var cultureName = AspxCommerce.utils.GetCultureName();
           ReferAFriend = {
               config: {
                   isPostBack: false,
                   async: false,
                   cache: false,
                   type: "POST",
                   contentType: "application/json; charset=utf-8",
                   data: '{}',
                   dataType: "json",
                   baseURL: AspxCommerce.utils.GetAspxServicePath() + "AspxCommerceWebService.asmx/",
                   url: "",
                   method: ""
               },
               vars: {
                   totalPrice: ""
               },
               ajaxCall: function(config) {
                   $.ajax({
                       type: ReferAFriend.config.type,
                       contentType: ReferAFriend.config.contentType,
                       cache: ReferAFriend.config.cache,
                       async: ReferAFriend.config.async,
                       data: ReferAFriend.config.data,
                       dataType: ReferAFriend.config.dataType,
                       url: ReferAFriend.config.url,
                       success: ReferAFriend.ajaxSuccess,
                       error: ReferAFriend.ajaxFailure
                   });
               },
               init: function() {
                   ShowPopup('a.popupEmailAFriend');
                   $(".cssClassClose").bind("click", function() {
                       $('#fade, #popuprel').fadeOut();
                   });

                   if (AspxCommerce.utils.GetUserName().toLowerCase() != "anonymoususer") {
                       $("#txtYourName").val(ItemDetail.vars.userFullName);
                       $("#txtYourName").attr('disabled', 'disabled');
                       $("#txtYourName").attr('readonly', 'readonly');
                       $("#txtYourEmail").val(ItemDetail.vars.userEmail);
                       $("#txtYourEmail").attr('disabled', 'disabled');
                       $("#txtYourEmail").attr('readonly', 'readonly');
                   }
                   else {
                       $("#txtYourName").removeAttr('disabled');
                       $("#txtYourName").removeAttr('readonly');
                       $("#txtYourEmail").removeAttr('disabled');
                       $("#txtYourEmail").removeAttr('readonly');
                   }
                   var m = $("#EmailForm").validate({
                       ignore: ':hidden',
                       rules: {
                           yourname: {
                               minlength: 2
                           },
                           youremail: "required",
                           friendname: {
                               minlength: 2
                           },
                           friendemail: "required",
                           subj: {
                               minlength: 2
                           },
                           msg: "required"
                       },
                       messages: {
                           yourname: {
                               required: "*",
                               minlength: "(at least 2 chars)"
                           },
                           youremail: "*",
                           friendname: {
                               required: "*",
                               minlength: "(at least 2 chars)"
                           },
                           friendemail: "*",
                           subj: {
                               required: "*",
                               minlength: "(at least 2 chars)"
                           },
                           msg: "*"
                       }
                   });
                   $("#btnSendEmail").bind("click", function() {
                       if (m.form()) {
                           ReferAFriend.SendEmailToFriend();
                           return false;
                       }
                       else {
                           return false;
                       }
                   });
               },

               SendEmailToFriend: function() {
                   var senderName = $("#txtYourName").val();
                   var senderEmail = $.trim($("#txtYourEmail").val());
                   var receiverName = $("#txtFriendName").val();
                   var receiverEmail = $("#txtFriendEmail").val();
                   var subject = $("#txtSubject").val();
                   var message = $("#txtMessage").val();

                   var imgpath = $('.popupEmailAFriend').attr('imagepath');
                   imgpath = imgpath.replace('/uploads/', '/uploads/Small/');
                   var fullDate = new Date();
                   var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : (fullDate.getMonth() + 1);
                   if (twoDigitMonth.length == 2)
                   { } else if (twoDigitMonth.length == 1) { twoDigitMonth = '0' + twoDigitMonth; }
                   var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
                   var dateyear = fullDate.getFullYear();

                   var serverLocation = '<%=Request.ServerVariables["SERVER_NAME"]%>';
                   var serverHostLoc = 'http://' + serverLocation;
                   var itemprice = $('#spanPrice').html();

                   var messageBodyHtml = '';
                   messageBodyHtml += '<table style="font:12px Arial, Helvetica, sans-serif;" width="100%" border="0" cellspacing="0" cellpadding="0">  <tr>';
                   messageBodyHtml += '<td width="33%"><div style="border:1px solid #cfcfcf; background:#f1f1f1; padding:10px; text-align:center;"> ';
                   messageBodyHtml += '<img src="' + serverHostLoc + "/" + imgpath + '" alt="Picture" width="250" />';
                   messageBodyHtml += '<p style="margin:0; padding:5px 0 0 0; font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; line-height:18px;"> <span style="font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;">';
                   messageBodyHtml += '' + $('#spanItemName').html() + '</span><br />';
                   messageBodyHtml += '<span style="font-weight:bold; font-size:11px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;">Price:</span>';
                   messageBodyHtml += '' + itemprice + '<br />';
                   messageBodyHtml += '<span style="font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif;text-decoration:blink; text-shadow:1px 1px 0 #fff;"><a style="color: rgb(39, 142, 230);" href="' + window.location + '">click here to view all details</a></span> ';
                   messageBodyHtml += '</p> </div></td></tr> </table>';
                   this.config.url = this.config.baseURL + "SaveAndSendEmailMessage";
                   this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, itemID: ItemDetail.vars.itemId, senderName: senderName, senderEmail: senderEmail, receiverName: receiverName, receiverEmail: receiverEmail, subject: subject, message: message, messageBodyDetail: messageBodyHtml, cultureName: cultureName });
                   this.config.ajaxCallMode = 1;
                   this.ajaxCall(this.config);
               },

               ClearForm: function() {
                   if (AspxCommerce.utils.GetUserName().toLowerCase() != "anonymoususer") {
                       $("#txtYourName").val(ItemDetail.vars.userFullName);
                       $("#txtYourEmail").val(ItemDetail.vars.userEmail);
                   }
                   else {
                       $("#txtYourName").val('');
                       $("#txtYourEmail").val('');
                   }
                   $("#txtFriendName").val('');
                   $("#txtFriendEmail").val('');
                   $("#txtSubject").val('');
                   $("#txtMessage").val('');
               },
               ajaxSuccess: function() {
                   switch (ReferAFriend.config.ajaxCallMode) {
                       case 0:
                           break;
                       case 1:
                           csscody.info("<h2>Successful Message</h2><p>Email has been sent successfully.</p>");
                           ReferAFriend.ClearForm();
                           $('#fade , #popuprel').fadeOut();
                           break;
                   }
               }
           }
           ReferAFriend.init();

       });
    //]]>
</script>

<form class="cmxform" id="EmailForm" method="post" action="">
<div class="popupbox" id="popuprel">
    <div class="cssClassCloseIcon">
        <button type="button" class="cssClassClose">
            <span>Close</span></button>
    </div>
    <h2>
        <asp:Label ID="lblTitle" runat="server" Text="Email A Friend"></asp:Label>
    </h2>
    <div class="cssClassFormWrapper">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tblEmailAFriend">
            <tr>
                <td width="20%">
                    <label id="lblYourName" class="cssClassLabel">
                        Your Name:<span class="cssClassRequired">*</span></label>
                </td>
                <td width="80%">
                    <input type="text" id="txtYourName" name="yourname" class="required"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblYourEmail" class="cssClassLabel">
                        Your Email:<span class="cssClassRequired">*</span></label>
                </td>
                <td>
                    <input type="text" id="txtYourEmail" name="youremail" class="required email" />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblFriendName" class="cssClassLabel">
                        Friend Name:<span class="cssClassRequired">*</span></label>
                </td>
                <td>
                    <input type="text" id="txtFriendName" name="friendname" class="required" />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblFriendEmail" class="cssClassLabel">
                        Friend Email:<span class="cssClassRequired">*</span></label>
                </td>
                <td>
                    <input type="text" id="txtFriendEmail" name="friendemail" class="required email" />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblSubject" class="cssClassLabel">
                        Subject:<span class="cssClassRequired">*</span></label>
                </td>
                <td>
                    <input type="text" id="txtSubject" name="subj" class="required"/>
                </td>
            </tr>
            <tr>
                <td>
                    <label id="lblMessage" class="cssClassLabel">
                        Message:<span class="cssClassRequired">*</span></label>
                </td>
                <td>
                    <textarea id="txtMessage" cols="30" rows="6" name="msg" class="cssClassTextarea required" onkeydown="limitMaxText(this);" 
                            onkeyup="limitMaxText(this);"></textarea>
                </td>
            </tr>
        </table>
        <div class="cssClassButtonWrapper">
            <button type="button" id="btnSendEmail">
                <span><span>Send</span></span></button>
        </div>
    </div>
</div>
</form>
