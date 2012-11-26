  var WishList ="";
  $(function() {
      var storeId = AspxCommerce.utils.GetStoreID();
      var portalId = AspxCommerce.utils.GetPortalID();
      var userName = AspxCommerce.utils.GetUserName();
      var cultureName = AspxCommerce.utils.GetCultureName();
      var ip = AspxCommerce.utils.GetClientIP();
      var countryName = countryName;
      var customerId = AspxCommerce.utils.GetCustomerID();
      var sessionCode = AspxCommerce.utils.GetSessionCode();
      var userEmailWishList = userEmailWishList;
      var serverLocation = serverNameVariables;
      var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();

      WishList = {
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
                  type: WishList.config.type,
                  contentType: WishList.config.contentType,
                  cache: WishList.config.cache,
                  async: WishList.config.async,
                  url: WishList.config.url,
                  data: WishList.config.data,
                  dataType: WishList.config.dataType,
                  success: WishList.ajaxSuccess,
                  error: WishList.ajaxFailure
              });
          },
          ajaxSuccess: function(msg) {
              switch (WishList.config.ajaxCallMode) {
                  case 1:
                      HeaderControl.GetWishListCount(); // for header wish counter increase for database
                      WishList.GetWishItemList(); // for rebinding the wishlist item 
                      csscody.info("<h2>Successful Message</h2><p>Wished item has been deleted successfully.</p>");
                      break;
                  case 2:
                      $("#tblWishItemList>tbody").html('');
                      if (msg.d.length > 0) {
                          $.each(msg.d, function(index, item) {
                              WishList.BindWishListItems(item, index);
                          });
                          $(".comment").each(function() {
                              if ($(this).val() == "") {
                                  $(this).addClass("lightText").val("enter a comment..");
                              }
                          });

                          $(".comment").bind("focus", function() {
                              if ($(this).val() == "enter a comment..") {
                                  $(this).removeClass("lightText").val("");
                              }
                              // focus lost action
                          });
                          $(".comment").bind("blur", function() {
                              if ($(this).val() == "") {
                                  $(this).val("enter a comment..").addClass("lightText");
                              }
                          });
                      }
                      else {
                          $("#tblWishItemList>thead").hide();
                          $("#wishitemBottom").hide();
                          $("#tblWishItemList").html("<tr><td class=\"cssClassNotFound\">Your wishlist is empty!</td></tr>");
                      }
                      break;
                  case 3:
                      HeaderControl.GetWishListCount(); // for header wish counter increase for database
                      WishList.GetWishItemList(); // for rebinding the wishlist item                         
                      csscody.info("<h2>Successful Message</h2><p>Wished item has been deleted successfully(2).</p>");

                      break;
                  case 4:
                      csscody.info("<h2>Successful Message</h2><p>Your wishlist has been updated successfully.</p>");
                      break;
                  case 5:
                      HeaderControl.GetWishListCount(); // for header wish counter increase for database
                      WishList.GetWishItemList(); // for rebinding the wishlist item                        
t
                      break;
                  case 6:
                      csscody.info("<h2>Successful Message</h2><p>Email has been sent successfully.</p>");
                      WishList.ClearShareWishItemForm();
                      $('#divWishListContent').show();
                      $('#divShareWishList').hide();
                      $('#fade, #popuprel5, .cssClassClose').fadeOut();
                      break;
                  case 7:
                      break;

              }
          },
          ajaxFailure: function() {
              switch (WishList.config.erroe) {
                  case 3:
                      csscody.error('<h2>Error Message</h2><p>Failed to delete wished item!</p>');
                      break;
                  case 4:
                      csscody.error('<h2>Error Message</h2><p>Failed to update wishlist!</p>');
                      break;
                  case 5:
                      csscody.error('<h2>Error Message</h2><p>Failed to clear wishlist!</p>');
                      break;
                  case 6:
                      WishList.ClearShareWishItemForm();
                      $('#fade, #popuprel5, .cssClassClose').fadeOut();
                      csscody.error('<h2>Error Message</h2><p>Failed to sending mail!</p>');
                      break;
              }
          },

          trim: function(str, chars) {
              return WishList.ltrim(WishList.rtrim(str, chars), chars);
          },
          ltrim: function(str, chars) {
              chars = chars || "\\s";
              return str.replace(new RegExp("^[" + chars + "]+", "g"), "");
          },
          rtrim: function(str, chars) {
              chars = chars || "\\s";
              return str.replace(new RegExp("[" + chars + "]+$", "g"), "");
          }, validateMultipleEmailsCommaSeparated: function(value) {
              var result = value.split(",");
              for (var i = 0; i < result.length; i++)
                  if (!WishList.validateEmail(result[i]))
                  return false;
              return true;
          }, validateEmail: function(field) {
              var regex = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
              return (regex.test(WishList.trim(field))) ? true : false;
          },
          ClearShareWishItemForm: function() {
              $('#txtEmailID').val('');
              $('#txtEmailMessage').val('');
              $('.cssClassWishComment textarea').val('');
              $(".comment").each(function() {
                  if ($(this).val() == "") {
                      $(this).addClass("lightText").val("enter a comment..");
                  }
              });
          },

          DeleteWishListItem: function(itemId) {
              var properties = { onComplete: function(e) {
                  WishList.ConfirmDeleteWishItem(itemId, e);
              }
              }
              // Ask user's confirmation before delete records        
              csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete this wished item?</p>", properties);
          },

          ConfirmDeleteWishItem: function(id, event) {
              if (event) {
                  this.config.method = "AspxCommerceWebService.asmx/DeleteWishItem";
                  this.config.url = this.config.baseURL + this.config.method;
                  this.config.data = JSON2.stringify({ ID: id, storeID: storeId, portalID: portalId, userName: userName });
                  this.config.ajaxCallMode = 1;
                  this.ajaxCall(this.config);
              }
          },

          GetWishItemList: function() {
              var count = 10;
              var isAll = 1;
              this.config.method = "AspxCommerceWebService.asmx/GetWishItemList";
              this.config.url = this.config.baseURL + this.config.method;
              this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName, flagShowAll: isAll, count: count });
              this.config.ajaxCallMode = 2;
              this.ajaxCall(this.config);
          },

          BindWishListItems: function(response, index) {
              if (response.ImagePath == "") {
                  response.ImagePath = noImageWishListSetting;
              }
              else if (response.AlternateText == "") {
                  response.AlternateText = response.ItemName;
              }
              ItemIDs = response.ItemID + "#";
              ItemComments = $("#comment" + response.ItemID + "").innerText;

              var WishDate = WishList.DateDeserialize(response.WishDate, "yyyy/M/d");

              var itemSKU = JSON2.stringify(response.SKU);
              if (index % 2 == 0) {

                  if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                      if (response.IsOutOfStock) {
                          //  Items = "<tr class='cssClassAlternativeEven' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt=' "+ response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2) + "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper cssClassOutOfStock'><a href='#'><span>Out Of Stock</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                          Items = '<tr class="cssClassAlternativeEven" id="tr_' + response.ItemID + '"><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                          Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                          Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                          Items += "<div class='cssClassButtonWrapper cssClassOutOfStock'><a href=\"#\"><span>Out Of Stock</span></a></div></td>";
                          Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';

                      }
                      else {
                          //  Items = "<tr class='cssClassAlternativeEven' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt='" + response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2) + "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper '><a href='#' onclick='AddToCartToJS(" + response.ItemID + "," + response.Price  + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                          Items = '<tr class="cssClassAlternativeEven" id="tr_' + response.ItemID + '"><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                          Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                          Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                          Items += "<div class='cssClassButtonWrapper'><a href=\"#\" onclick='WishList.AddToCartToJS(" + response.ItemID + "," + response.Price + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td>";
                          Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                      }
                  }
                  else {
                      //  Items = "<tr class='cssClassAlternativeEven' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt='" + response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2)+ "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper '><a href='#' onclick='AddToCartToJS(" + response.ItemID + "," + response.Price  + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                      Items = '<tr class="cssClassAlternativeEven" id="tr_' + response.ItemID + '"><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                      Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                      Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                      Items += "<div class='cssClassButtonWrapper'><a href=\"#\" onclick='WishList.AddToCartToJS(" + response.ItemID + "," + response.Price + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td>";
                      Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';

                  }
              }
              else {
                  if (allowOutStockPurchaseSetting.toLowerCase() == 'false') {
                      if (response.IsOutOfStock) {
                          // Items = "<tr class='cssClassAlternativeOdd' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt='" + response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2) + "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper cssClassOutOfStock'><a href='#'><span>Out Of Stock</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                          Items = '<tr class="cssClassAlternativeOdd" id="tr_' + response.ItemID + '"><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                          Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                          Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                          Items += "<div class='cssClassButtonWrapper cssClassOutOfStock'><a href=\"#\"><span>Out Of Stock</span></a></div></td>";
                          Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + ' /images/admin/btndelete.png"/></td></tr>';

                      }
                      else {
                          //  Items = "<tr class='cssClassAlternativeOdd' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt='" + response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2)+ "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper '><a href='#' onclick='AddToCartToJS(" + response.ItemID + "," + response.Price + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                          Items = '<tr class="cssClassAlternativeOdd" id="tr_' + response.ItemID + '"><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                          Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                          Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                          Items += "<div class='cssClassButtonWrapper'><a href=\"#\" onclick='WishList.AddToCartToJS(" + response.ItemID + "," + response.Price + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td>";
                          Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';
                      }
                  }
                  else {
                      //   Items = "<tr class='cssClassAlternativeOdd' id='tr_" + response.ItemID + "'><td class='cssClassWishItemDetails'><div class='cssClassImage'><img src='" + aspxRootPath + response.ImagePath.replace('uploads', 'uploads/Small') + "' alt='" + response.AlternateText + "' title='" + response.AlternateText + "'/></div><a href='" + aspxRedirectPath + "item/" + response.SKU + ".aspx'>" + response.ItemName + "</a><span class='cssClassPrice cssClassFormatCurrency'>" + (response.Price * rate).toFixed(2)+ "</span></td><td class='cssClassWishComment'><textarea maxlength='300' onkeyup="+ismaxlength(this)+" id='comment_" + response.ItemID + "' class='comment'>" + response.Comment + "</textarea></td><td class='cssClassWishDate'>" + WishDate + "</td><td class='cssClassWishToCart'><div class='cssClassButtonWrapper '><a href='#' onclick='AddToCartToJS(" + response.ItemID + "," + response.Price  + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td><td class='cssClassDelete'><img onclick='DeleteWishListItem(" + response.ItemID + ")' src='" + aspxTemplateFolderPath + "/images/admin/btndelete.png'/></td></tr>";
                      Items = '<tr class="cssClassAlternativeOdd" id="tr_' + response.ItemID + '"><td class="cssClassWishItemDetails"><div class="cssClassImage"><img src="' + AspxCommerce.utils.GetAspxRootPath() + response.ImagePath.replace('uploads', 'uploads/Small') + '" alt="' + response.AlternateText + '" title="' + response.AlternateText + '"/></div>';
                      Items += '<a href="' + AspxCommerce.utils.GetAspxRedirectPath() + 'item/' + response.SKU + '.aspx">' + response.ItemName + '</a>';
                      Items += '<span class="cssClassPrice cssClassFormatCurrency">' + (response.Price * rate).toFixed(2) + '</span></td><td class="cssClassWishComment"><textarea maxlength="300" onkeyup="' + WishList.ismaxlength(this) + '" id="comment_' + response.ItemID + '" class="comment">' + response.Comment + '</textarea></td><td class="cssClassWishDate">' + WishDate + '</td><td class="cssClassWishToCart">';
                      Items += "<div class='cssClassButtonWrapper'><a href=\"#\" onclick='WishList.AddToCartToJS(" + response.ItemID + "," + response.Price + "," + itemSKU + "," + 1 + ");'><span>Add To Cart</span></a></div></td>";
                      Items += '<td class="cssClassDelete"><img onclick="WishList.DeleteWishListItem(' + response.ItemID + ')" src="' + AspxCommerce.utils.GetAspxTemplateFolderPath() + '/images/admin/btndelete.png"/></td></tr>';

                  }
              }

              $("#tblWishItemList>tbody").append(Items);
              $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
              if (showImageInWishlistSetting.toLowerCase() == 'false') {
                  $('.cssClassWishItemDetails>img').hide();
              }
              $(".comment").keypress(function(e) {
                  if (e.which == 35) {
                      return false;
                  }
              });
          },
          ismaxlength: function(obj) {
              var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : ""
              if (obj.getAttribute && obj.value.length > mlength)
                  obj.value = obj.value.substring(0, mlength)
          },
          AddToCartToJS: function(itemId, itemPrice, itemSKU, itemQuantity) {
              AspxCommerce.RootFunction.AddToCartFromJS(itemId, itemPrice, itemSKU, itemQuantity, storeId, portalId, customerId, sessionCode, userName, cultureName);
          },
          ConfirmSingleDelete: function(id, event) {
              if (event) {
                  this.config.method = "AspxCommerceWebService.asmx/DeleteWishItem";
                  this.config.url = this.config.baseURL + this.config.method;
                  this.config.data = JSON2.stringify({ ID: id, storeID: storeId, portalID: portalId, userName: userName });
                  this.config.ajaxCallMode = 3;
                  this.ajaxCall(this.config);
              }
          },
          DeleteWishItem: function(itemId) {
              var properties = { onComplete: function(e) {
                  WishList.ConfirmSingleDelete(itemId, e);
              }
              }
              // Ask user's confirmation before delete records        
              csscody.confirm("<h2>Delete Confirmation</h2><p>Are you sure you want to delete wished item?</p>", properties);
          },
          UpdateWishList: function() {
              var comment = '';
              var itemId = '';
              $(".comment").each(function() {
                  comment += $(this).val() + '#';
                  itemId += parseInt($(this).attr("id").replace(/[^0-9]/gi, '')) + '#';
              });
              comment = comment.substring(0, comment.length - 1);
              itemId = itemId.substring(0, itemId.length - 1);

              this.config.method = "AspxCommerceWebService.asmx/UpdateWishList";
              this.config.url = this.config.baseURL + this.config.method;
              this.config.data = JSON2.stringify({ ID: itemId, comment: comment, storeID: storeId, portalID: portalId, userName: userName });
              this.config.ajaxCallMode = 4;
              this.config.error = 4;
              this.ajaxCall(this.config);

          },
          ClearWishList: function() {
              this.config.method = "AspxCommerceWebService.asmx/ClearWishList";
              this.config.url = this.config.baseURL + this.config.method;
              this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName });
              this.config.ajaxCallMode = 5;
              this.config.error = 5;
              this.ajaxCall(this.config);
          },
          DateDeserialize: function(content, format) {
              content = eval('new ' + content.replace(/[/]/gi, ''));
              return formatDate(content, format);
          },
          SendShareItemEmail: function() {
              var emailID = '';
              var message = '';
              var itemId = '';
              var arr = new Array;
              var elems = '';
              $(".comment").each(function() {
                  itemId += parseInt($(this).attr("id").replace(/[^0-9]/gi, '')) + ',';
              });
              itemId = itemId.substring(0, itemId.length - 1);
              emailID = $('#txtEmailID').val();
              message = $('#txtEmailMessage').val();
              var senderName = userName;
              var senderEmail = userEmailWishList;
              var receiverEmailID = emailID;
              var subject = "Take A Look At " + senderName + "'s " + " WishList";
              var msgbodyhtml = '';
              var msgCommenthtml = '';
              //        $('#tblWishItemList tbody .cssClassWishItemDetails img').each(function() {
              //            $(this).attr({ Width: "123px", Height: "81px" });
              //        });

              var serverHostLoc = 'http://' + serverLocation;
              var fullDate = new Date();
              var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : (fullDate.getMonth() + 1);
              if (twoDigitMonth.length == 2)
              { } else if (twoDigitMonth.length == 1) { twoDigitMonth = '0' + twoDigitMonth; }
              var currentDate = fullDate.getDate() + "/" + twoDigitMonth + "/" + fullDate.getFullYear();
              var dateyear = fullDate.getFullYear();

              var trLength = $('#tblWishItemList tbody tr').lenght;
              var tdContent = '';
              var tdContentArray = [];
              var shareWishMailHtml = '';
              shareWishMailHtml += '<table style="font:12px Arial, Helvetica, sans-serif;" width="100%" border="0" align="center" cellpadding="0" cellspacing="5" bgcolor="#e0e0e0"><tr><td align="center" valign="top"><table width="680" border="0" cellspacing="0" cellpadding="0"><tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr><tr><td>';
              shareWishMailHtml += '<table width="680" border="0" cellspacing="0" cellpadding="0"><tr>';
              shareWishMailHtml += ' <td width="300"><a href="' + serverHostLoc + '" target="_blank" style="outline:none; border:none;"><img src="' + serverHostLoc + aspxTemplateFolderPath + '/images/aspxcommerce.png' + '" width="143" height="62" alt="Aspxcommerce" title="Aspxcommerce"/></a></td>';
              shareWishMailHtml += '<td width="191" align="left" valign="middle">&nbsp;</td><td width="189" align="right" valign="middle"><b style="padding:0 20px 0 0; text-shadow:1px 1px 0 #fff;"> ' + currentDate + '</b></td></tr></table></td></tr>';
              shareWishMailHtml += '<tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr><tr><td bgcolor="#fff"><div style="border:1px solid #c7c7c7; background:#fff; padding:20px">';
              shareWishMailHtml += '<table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF"><tr>';
              shareWishMailHtml += ' <td><p style="font-family:Arial, Helvetica, sans-serif; font-size:17px; line-height:16px; color:#278ee6; margin:0; padding:0 0 10px 0; font-weight:bold; text-align:left;">';
              shareWishMailHtml += 'Your Friend <strong>' + userName + '</strong> Wants to Share these Wished Item with You !!</p>';
              shareWishMailHtml += '<p style="margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;"> Friend Email ID: ' + senderEmail + ' </p></td></tr>';
              shareWishMailHtml += '<tr><td><p style="margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;"> ' + message + '</p></td></tr></table>';
              shareWishMailHtml += '<table width="100%" border="0" cellspacing="0" cellpadding="0"><tr><td>';
              shareWishMailHtml += '  <table width="100%" border="0" cellspacing="0" cellpadding="0"><tr>';
              // loop function here
              $('#tblWishItemList tbody tr').each(function() {
                  var src = $(this).find('td div.cssClassImage img').attr('src');
                  var alt = $(this).find('td div.cssClassImage img').attr('alt');
                  var title = $(this).find('td div.cssClassImage img').attr('title');
                  var price = $(this).find('td.cssClassWishItemDetails span').html();
                  var href = $(this).find('td.cssClassWishItemDetails a').attr('href');
                  var hrefHtml = $(this).find('td.cssClassWishItemDetails a').html();
                  var htmlComment = $(this).find('td.cssClassWishComment textarea').val();
                  tdContent += '<td width="33%"><div style="border:1px solid #cfcfcf; background:#f1f1f1; padding:10px; text-align:center;"> <img src=' + serverHostLoc + src + ' alt="' + alt + '" width="80" />';
                  tdContent += ' <p style="margin:0; padding:5px 0 0 0; font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:normal; line-height:18px;">';
                  tdContent += '<span style="font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;">' + title + '</span><br />'; //item name
                  tdContent += '<span style="font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;"> <a href="' + serverHostLoc + href + '">' + hrefHtml + '</a></span><br />'; //item name
                  tdContent += '<span style="font-weight:bold; font-size:11px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;">Price:</span> ' + price + '<br />'; //price
                  tdContent += '<span style="font-weight:bold; font-size:12px; font-family:Arial, Helvetica, sans-serif; text-shadow:1px 1px 0 #fff;">Comments:</span> ' + htmlComment + '</p></div></td>'; //comment
                  tdContentArray.push(tdContent);
                  tdContent = '';
              }); //loop finishes
              for (var i in tdContentArray) {
                  if (i % 3 == 0) {
                      shareWishMailHtml += '</tr><tr>' + tdContentArray[i];
                  }
                  else {
                      shareWishMailHtml += tdContentArray[i];
                  }
              }
              shareWishMailHtml += '</tr></table></td></tr></table><p style="margin:0; padding:10px 0 0 0; font:bold 11px Arial, Helvetica, sans-serif; color:#666;"> Thank You,<br />';
              shareWishMailHtml += '<span style="font-weight:normal; font-size:12px; font-family:Arial, Helvetica, sans-serif;">AspxCommerce Team </span></p></div></td></tr>';
              shareWishMailHtml += '<tr><td><img src="' + serverHostLoc + '/blank.gif" width="1" height="20" alt=" "/></td></tr><tr>';
              shareWishMailHtml += ' <td align="center" valign="top"><p style="font-size:11px; color:#4d4d4d"> © ' + dateyear + ' AspxCommerce. All Rights Reserved.</p></td>';
              shareWishMailHtml += '</tr><tr><td align="center" valign="top"><img src="' + serverHostLoc + '/blank.gif" width="1" height="10" alt=" " /></td></tr></table></td></tr></table>';

            //  var functionName = "ShareWishListEmailSend";
              this.config.method = "AspxCommerceWebService.asmx/ShareWishListEmailSend";
              this.config.url = this.config.baseURL + this.config.method;
              this.config.data = JSON2.stringify({ storeID: storeId, portalID: portalId, itemID: itemId, senderName: senderName, senderEmail: userEmailWishList, receiverEmailID: receiverEmailID, subject: subject, message: message, link: shareWishMailHtml, cultureName: cultureName });
              this.config.ajaxCallMode = 6;
              this.config.error = 6;
              this.ajaxCall(this.config);
          },
          HideMessage: function() {
              $('.errorMessage').hide();
          },
          Init: function() {                  
              if (enableWishListSetting.toLowerCase() == 'true') {
                  WishList.GetWishItemList();
                  $("#divWishListContent").show();
                  $('.errorMessage').hide();
                  $('#divShareWishList').hide();
                  if (userFriendlyURL) {
                      $("#lnkContinueShopping").attr("href", '' + aspxRedirectPath + 'Home.aspx');
                  }
                  else {
                      $("#lnkContinueShopping").attr("href", '' + aspxRedirectPath + 'Home');
                  }
                  $("#continueInStore").bind("click", function() {
                      if (userFriendlyURL) {
                          window.location.href = aspxRedirectPath + 'Home.aspx';
                      }
                      else {
                          window.location.href = aspxRedirectPath + 'Home';
                      }
                      return false;
                  });

                  //		$('#btnShareWishBack').click(function() {
                  //            $('#divWishListContent').show();
                  //            $('#divShareWishList').hide();
                  //        });

                  $('#shareWishList').bind("click", function() {
                      //  $('#divWishListContent').hide();
                      $('#divShareWishList').show();
                      WishList.HideMessage();
                      WishList.ClearShareWishItemForm();
                      ShowPopupControl('popuprel5');
                  });

                  $(".cssClassClose").bind("click", function() {
                      $('#fade, #popuprel5').fadeOut();
                      WishList.ClearShareWishItemForm();
                  });

                  $('#btnShareWishItem').bind("click", function() {
                      var emailIDsColln = $('#txtEmailID').val();
                      if (WishList.validateMultipleEmailsCommaSeparated(emailIDsColln)) {
                          WishList.SendShareItemEmail();
                      }
                      else {
                          // alert('Eener Valid email with comma separated');
                          $('.errorMessage').show();
                      }
                  });
              }
              else {
                  csscody.alert('<h2>Information Alert</h2><p>WishList is not enabled.</p>');
              }
          }
      }
      WishList.Init();

  });