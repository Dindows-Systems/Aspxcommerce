<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProductReviews.ascx.cs"
    Inherits="Modules_AspxUserDashBoard_UserProductReviews" %>

<script type="text/javascript">
    //<![CDATA[
    var arrReview = new Array();
    var arrResultToBind = new Array();
    var arrayNew = new Array();

    $(document).ready(function() {
        GetUserProductReviewsDetails();
        $(".cssClassEditUserReview").live("click", function() {
            var itemId = parseInt($(this).attr("id").replace(/[^0-9]/gi, ''));
            var itemReviewId = $(this).attr("itemreviewid");
            var ratingIds = '';
            var ratingValues = '';
            var reviewSummary = $("#txtReviewSummary_" + itemId + "").val();
            var review = $("#txtReview_" + itemId + "").val();
            var itemColl = "input[type='radio'][nameid='" + itemId + "']";
            $(itemColl).each(function() {
                var itemRatingCriteriaID = $(this).attr("ratingcriteriaid");
                if ($(this).attr("checked")) {
                    ratingIds += itemRatingCriteriaID + ',';
                    ratingValues += $("input[type='radio'][ratingcriteriaid=" + itemRatingCriteriaID + "][nameid='" + itemId + "']:checked").attr("value") + ',';
                }

            });

            UpdateUserProductReview(itemId, itemReviewId, ratingIds, ratingValues, reviewSummary, review);
        });

        $(".cssClassDeleteUserReview").live("click", function() {
            var itemId = parseInt($(this).attr("id").replace(/[^0-9]/gi, ''));
            var itemReviewId = $(this).attr("itemreviewid");
            var properties = { onComplete: function(e) {
                DeleteUserReview(itemId, itemReviewId, e);
            }
            }
            // Ask user's confirmation before delete records        
            csscody.confirm("<h2>Delete Confirmation</h2><p>Do you want to delete this review?</p>", properties);
        });

        $("#ddlPageSize").change(function() {
            var items_per_page = $(this).val();
            $("#Pagination").pagination(arrReview.length, {
                callback: pageselectCallback,
                items_per_page: items_per_page,
                //num_display_entries: 10,
                //current_page: 0,
                prev_text: "Prev",
                next_text: "Next",
                prev_show_always: false,
                next_show_always: false
            });
        });
    });

    function DeleteUserReview(itemId, itemReviewId, e) {
        if (e) {
            $.ajax({
                type: "POST",
                url: aspxservicePath + "AspxCommerceWebService.asmx/DeleteUserProductReview",
                data: JSON2.stringify({ itemID: itemId, itemReviewID: itemReviewId, storeID: storeId, portalID: portalId, userName: userName }),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function() {
                    GetUserProductReviewsDetails();
                },
                error: function() {
                    alert("error");
                }
            });
        }
    }

    function GetUserProductReviewsDetails() {
        $.ajax({
            type: "POST",
            url: aspxservicePath + "AspxCommerceWebService.asmx/GetUserProductReviews",
            data: JSON2.stringify({ storeID: storeId, portalID: portalId, userName: userName, cultureName: cultureName }),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function(msg) {
                $("#divUserProductReviews").html('');
                arrResultToBind.length = 0;
                arrReview.length = 0;
                arrayNew.length = 0;
                var itemNames = '';
                var arr = new Array();
                if (msg.d.length > 0) {
                    $.each(msg.d, function(index, value) {
                        if (value.ImagePath == "") {
                            value.ImagePath = aspxRootPath + "Modules/AspxCommerce/AspxItemsManagement/uploads/noitem.png";
                        }
                        var userProductReviewElements = '';
                        arr = itemNames.split(',');
                        var status = jQuery.inArray(value.ItemName, arr);
                        if (status == -1) {
                            arrReview.push(value);
                            itemNames += value.ItemName + ',';
                        }
                        else {
                            arrayNew.push(value.ItemName + '+' + value.ItemRatingCriteriaID + '+' + value.ItemRatingCriteria + '+' + value.RatingValue + '+' + value.ItemID);
                        }
                    });
                    var items_per_page = $('#ddlPageSize').val();
                    $("#Pagination").pagination(arrReview.length, {
                        callback: pageselectCallback,
                        items_per_page: items_per_page,
                        //num_display_entries: 10,
                        //current_page: 0,
                        prev_text: "Prev",
                        next_text: "Next",
                        prev_show_always: false,
                        next_show_always: false
                    });
                }
                else {
                    $("#divUserProductReviews").html("You have not reviewed any items yet!");
                    $("#divReviewPageNumber").html('');
                }

            },
            error: function() {
                alert("error");
            }
        });
    }
    function UpdateUserProductReview(itemId, itemReviewId, ratingIds, ratingValues, reviewSummary, review) {
        $.ajax({
            type: "POST",
            url: aspxservicePath + "AspxCommerceWebService.asmx/UpdateUserProductReview",
            data: JSON2.stringify({ itemID: itemId, itemReviewID: itemReviewId, ratingIDs: ratingIds, ratingValues: ratingValues, reviewSummary: reviewSummary, review: review, storeID: storeId, portalID: portalId, userName: userName }),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function() {
                GetUserProductReviewsDetails();
            },
            error: function() {
                alert("error");
            }
        });
    }

    function pageselectCallback(page_index, jq) {
        // Get number of elements per pagionation page from form
        var items_per_page = $('#ddlPageSize').val();
        var max_elem = Math.min((page_index + 1) * items_per_page, arrReview.length);
        arrResultToBind.length = 0;

        // Iterate through a selection of the content and build an HTML string
        for (var i = page_index * items_per_page; i < max_elem; i++) {
            //newcontent += '<dt>' + arrItemListType[i]._Name + '</dt>';
            arrResultToBind.push(arrReview[i]);
        }
        BindReviews();
    }

    // Replace old content with new content
    //$('#Searchresult').html(newcontent);

    // Prevent click event propagation
    function BindReviews() {
        $("#divUserProductReviews").html('');
        var tableElements = '';
        tableElements += '<table width\="100%"><tbody><tr class="cssClassHeadeTitle cssClassAlternativeEven">';
        tableElements += '<td>Product</td>';
        tableElements += '<td>ReviewSummary</td>';
        tableElements += '<td>Review</td>';
        tableElements += '<td>Ratings</td>';
        tableElements += '<td>Function</td>';
        tableElements += '</tr></tbody><table>';
        $("#divUserProductReviews").html(tableElements);
        $.each(arrResultToBind, function(index, value) {
            var myitems = '';
            var functionElements = '';
            var userProductReviewElements = '';
            userProductReviewElements += '<tr id="tr' + index + '">';
            userProductReviewElements += "<td><a href='item/" + value.SKU + ".aspx'>" + value.ItemName + "</a>";
            userProductReviewElements += "<img src='" + value.ImagePath + "' alt='" + value.ItemName + "'/></td>";
            userProductReviewElements += '<td><input type="text" id="txtReviewSummary_' + value.ItemID + '" value="' + value.ReviewSummary + '"/></td>';

            userProductReviewElements += '<td><textarea id="txtReview_' + value.ItemID + '">' + value.Review + '</textarea></td>';
            userProductReviewElements += '<td><label>RatingAverage:</label>';
            userProductReviewElements += '<label>' + value.RatingAverage + '</label><br>';
            userProductReviewElements += '<label  class="cssClassLabel">' + value.ItemRatingCriteria + ':<span class="cssClassRequired">*</span></label>';
            userProductReviewElements += '<input name="star' + value.ItemRatingCriteriaID + '_' + value.ItemName + '" type="radio" class="auto-submit-star" value="1"  ratingcriteriaid="' + value.ItemRatingCriteriaID + '"   nameid="' + value.ItemID + '"  title="Worst" validate="required:true" />';
            userProductReviewElements += '<input name="star' + value.ItemRatingCriteriaID + '_' + value.ItemName + '" type="radio" class="auto-submit-star" value="2"  ratingcriteriaid="' + value.ItemRatingCriteriaID + '"   nameid="' + value.ItemID + '"  title="Bad" />';
            userProductReviewElements += '<input name="star' + value.ItemRatingCriteriaID + '_' + value.ItemName + '" type="radio" class="auto-submit-star" value="3"  ratingcriteriaid="' + value.ItemRatingCriteriaID + '"   nameid="' + value.ItemID + '"  title="OK" />';
            userProductReviewElements += '<input name="star' + value.ItemRatingCriteriaID + '_' + value.ItemName + '" type="radio" class="auto-submit-star" value="4"  ratingcriteriaid="' + value.ItemRatingCriteriaID + '"   nameid="' + value.ItemID + '"  title="Good" />';
            userProductReviewElements += '<input name="star' + value.ItemRatingCriteriaID + '_' + value.ItemName + '" type="radio" class="auto-submit-star" value="5"  ratingcriteriaid="' + value.ItemRatingCriteriaID + '"   nameid="' + value.ItemID + '"  title="Best" /><br></td>';
            userProductReviewElements += '</tr>';
            $("#divUserProductReviews>table tbody").append(userProductReviewElements);

            $.each(arrayNew, function(i, item) {
                var reviewElements = '';
                myitems = item;
                var arr = new Array();
                arr = myitems.split('+');

                if (value.ItemName == arr[0]) {
                    reviewElements += '<label  class="cssClassLabel">' + arr[2] + ':<span class="cssClassRequired">*</span></label>';
                    reviewElements += '<input name="star' + arr[1] + '_' + arr[0] + '" type="radio" class="auto-submit-star" value="1"  ratingcriteriaid="' + arr[1] + '"   nameid="' + arr[4] + '"  title="Worst" validate="required:true" />';
                    reviewElements += '<input name="star' + arr[1] + '_' + arr[0] + '" type="radio" class="auto-submit-star" value="2"  ratingcriteriaid="' + arr[1] + '"   nameid="' + arr[4] + '"  title="Bad" />';
                    reviewElements += '<input name="star' + arr[1] + '_' + arr[0] + '" type="radio" class="auto-submit-star" value="3"  ratingcriteriaid="' + arr[1] + '"   nameid="' + arr[4] + '"   title="OK" />';
                    reviewElements += '<input name="star' + arr[1] + '_' + arr[0] + '" type="radio" class="auto-submit-star" value="4"  ratingcriteriaid="' + arr[1] + '"   nameid="' + arr[4] + '"  title="Good" />';
                    reviewElements += '<input name="star' + arr[1] + '_' + arr[0] + '" type="radio" class="auto-submit-star" value="5"  ratingcriteriaid="' + arr[1] + '"   nameid="' + arr[4] + '"   title="Best" /><br>';

                    $("#divUserProductReviews table>tbody tr:last td:last").append(reviewElements);
                    $("input[name='star" + arr[1] + "_" + arr[0] + "'][value='" + arr[3] + "']").attr("checked", "checked");
                }
            });

            $("input[name='star" + value.ItemRatingCriteriaID + "_" + value.ItemName + "'][value='" + value.RatingValue + "']").attr("checked", "checked");
            functionElements += '<td>';
            functionElements += '<div class="cssClassButtonWrapper">';
            functionElements += '<button type="button" id="btnEditUserReview_' + value.ItemID + '" class="cssClassEditUserReview" itemreviewid="' + value.ItemReviewID + '" ><span><span>Save Changes</span></span></button>';
            functionElements += '<button type="button" id="btnDeleteUserReview_' + value.ItemID + '" class="cssClassDeleteUserReview" itemreviewid="' + value.ItemReviewID + '" ><span><span>Delete this review</span></span></button></div></td> ';
            $("#divUserProductReviews table>tbody tr:last").append(functionElements);
        });

        $('.auto-submit-star').rating();
        ("#divUserProductReviews table tr:even").addClass("cssClassAlternativeEven");
        ("#divUserProductReviews table tr:even").addClass("cssClassAlternativeOdd");
    }
    //]]>
</script>

<div id="divUserProductReviews" class="cssClassCartInformationDetails">
</div>
<div class="cssClassPageNumber" id="divReviewPageNumber">
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
