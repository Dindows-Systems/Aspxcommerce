<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryDetails.ascx.cs"
    Inherits="Modules_AspxDetails_AspxCategoryDetails_CategoryDetails" %>

<script type="text/javascript">
	var CategoryKey = "<%=Categorykey%>";
	var allowOutStockPurchase = '<%=AllowOutStockPurchase %>';
	var noImageCategoryDetailPath = '<%=NoImageCategoryDetailPath %>';
	var allowWishListCategory = '<%=AllowWishListCategory %>';
</script>

<div id="divHeader" class="cssClassSlider" >
    <%--<%=categorykey%>--%>
</div>
<div id='CategoryCaption' class='cssClassCategoryCaption'></div>
<div id="categoryListings">
</div>
<div id="divAjaxLoader" class="loading">
    <img id="imgLoader" src="" alt="loading...." />
</div>
<div id="divItemViewOptions" class="viewWrapper">
    <div id="divViewAs" class="view">
        View as:
        <select id="ddlViewAs" class="cssClassDropDown">
        </select>
    </div>
    <div id="divSortBy" class="sort">
        Sort by:
        <select id="ddlSortBy" class="cssClassDropDown">
        </select>
    </div>
</div>
<div id="divShowCategoryItemsList" class="cssClassDisplayResult">
</div>
<div class="cssClassClear">
</div>
<!-- TODO:: paging Here -->
<div class="cssClassPageNumber" id="divSearchPageNumber">
    <div class="cssClassPageNumberLeftBg">
        <div class="cssClassPageNumberRightBg">
            <div class="cssClassPageNumberMidBg">
                <div id="Pagination">
                </div>
                <div class="cssClassViewPerPage">
                    View Per Page
                    <select id="ddlPageSize" class="cssClassDropDown">
                        <option value="5">5</option>
                        <option value="10">10</option>
                        <option value="15">15</option>
                        <option value="20">20</option>
                        <option value="25">25</option>
                        <option value="40">40</option>
                    </select></div>
            </div>
        </div>
    </div>
</div>