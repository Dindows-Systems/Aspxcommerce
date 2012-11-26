<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AllCoupons.ascx.cs" Inherits="Modules_Admin_DetailsBrowse_AllCoupons" %>

<script type="text/javascript">
    //<![CDATA[

    $(function() {
        var storeId = AspxCommerce.utils.GetStoreID();
        var portalId = AspxCommerce.utils.GetPortalID();
        var userName = AspxCommerce.utils.GetUserName();
        var cultureName = AspxCommerce.utils.GetCultureName();
        var customerId = AspxCommerce.utils.GetCustomerID();
        var couponShowCount = 0;
        var couponList = new Array();
        var AllCoupon = {
            pageselectCallback: function(page_index, jq) {
                // Get number of elements per pagionation page from form
                var items_per_page = $('#ddlPageSize').val();
                var max_elem = Math.min((page_index + 1) * items_per_page, couponList.length);
                $("#divCouponList").html('');
                coupon = '';
                for (var i = page_index * items_per_page; i < max_elem; i++) {
                    AllCoupon.BindCouponListForDisplay(couponList[i]);
                    coupon += couponList[i].CouponID;
                }
                return false;
            }, Init: function() {
                AllCoupon.BindAllCouponList();
                $("#ddlPageSize").change(function() {
                    var optInit = AllCoupon.getOptionsFromForm();
                    $("#Pagination").pagination(couponList.length, optInit);
                });
            }, BindAllCouponList: function() {
                var coupon = '';
                $.ajax({
                    type: "POST",
                    url: aspxservicePath + "AspxCommerceWebService.asmx/GetCouponDetailListFront",
                    data: JSON2.stringify({ count: couponShowCount, storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName, customerID: customerId }),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        couponList = [];
                        if (msg.d.length > 0) {
                            $.each(msg.d, function(index, item) {
                               AllCoupon.BindCouponList(item, index);
                            });
                            var optInit = AllCoupon.getOptionsFromForm();
                            $("#Pagination").pagination(couponList.length, optInit);
                            $("#divSearchPageNumber").show();
                        }
                        else {
                            $("#divSearchPageNumber").hide();
                            $("#divCouponList").html("<span class=\"cssClassNotFound\">No Data Found!!</span>");
                        }
                    }                   
                });
            }, BindCouponList: function(item, index) {
                if (coupon.indexOf(item.CouponID) == -1) {
                    coupon += item.CouponID;
                } couponList.push(item);
            }, BindCouponListForDisplay: function(item) {
                var htmlListt = "";
                htmlListt += '<ul class="couponList"><li><span> Coupon Type: <span>' + item.CouponType + '</li>';
                htmlListt += '<li><span> Coupon Code: <span>' + item.CouponCode + '</li>';
                htmlListt += '<li><span> Amount: <span class="cssClassFormatCurrency">' + (item.CouponAmount * rate) + '</li>';
                htmlListt += '<li><span> Valid Till: <span>' + item.ValidateTo + '</li>';
                htmlListt += '</ul><br />';
                $("#divCouponList").append(htmlListt);
                $('.cssClassFormatCurrency').formatCurrency({ colorize: true, region: '' + region + '' });
            }, getOptionsFromForm: function() {
                var opt = { callback:AllCoupon.pageselectCallback };
                opt["items_per_page"] = $('#ddlPageSize').val();
                opt["prev_text"] = "Prev";
                opt["next_text"] = "Next";
                opt["prev_show_always"] = false;
                opt["next_show_always"] = false;
                return opt;
            }
        }

        AllCoupon.Init();
    });
   
//]]>
</script>

<div id="divCouponDetailFront">
    <div class="cssClassFormWrapper">
        <div class="couponlistheader">
            <h2>
                <asp:Label ID="lblWishHeading" runat="server" Text="Available Coupon List"></asp:Label></h2>
            <%-- <a class="btnPrevious" href="#">
                    <img alt="" src="<%=ResolveUrl("~/")%>Templates/AspxCommerce/images/admin/btnback.png" /></a>
                <a class="btnNext" href="#">
                    <img alt="" src="<%=ResolveUrl("~/")%>Templates/AspxCommerce/images/admin/imgforward.png" /></a>--%>
        </div>
        <div id="divCouponList">
        </div>
        <%-- <div>
            <p>
                <a href="#" class"btnSeeAllCoupon" onclick="SeeAllCoupon(0)">See all Coupons >></a>
                <a href="#" class"btnColapseAllCoupon" onclick="SeeAllCoupon(1)"> << </a>              
            </p>
        </div>--%>
    </div>
    <div class="cssClassPageNumber" id="divSearchPageNumber">
        <div class="cssClassPageNumberLeftBg">
            <div class="cssClassPageNumberRightBg">
                <div class="cssClassPageNumberMidBg">
                    <div id="Pagination">
                    </div>
                    <div class="cssClassViewPerPage">
                        View Per Page<select id="ddlPageSize" class="cssClassDropDown">
                            <option value="5">5</option>
                            <option value="10">10</option>
                            <option value="15">15</option>
                            <option value="20">20</option>
                            <option value="25">25</option>
                            <option value="40">40</option>
                        </select>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
