var browseByCategory = '';
    $(function() {
    	var storeId = AspxCommerce.utils.GetStoreID();
    	var portalId = AspxCommerce.utils.GetPortalID();
    	var userName = AspxCommerce.utils.GetUserName();
    	var cultureName = AspxCommerce.utils.GetCultureName();
    	var customerId = AspxCommerce.utils.GetCustomerID();
    	var ip = AspxCommerce.utils.GetClientIP();
    	var countryName = AspxCommerce.utils.GetAspxClientCoutry();
    	var sessionCode = AspxCommerce.utils.GetSessionCode();
    	var userFriendlyURL = AspxCommerce.utils.IsUserFriendlyUrl();
    	
    	var categoryId = 0;
    	var categoryOptions = '';
    	browseByCategory = {
    		config: {
    			isPostBack: false,
    			async: false,
    			cache: false,
    			type: 'POST',
    			contentType: "application/json; charset=utf-8",
    			data: '{}',
    			dataType: 'json',
    			baseURL: aspxservicePath,
    			method: "",
    			url: ""
    		},
    		init: function() {
    			browseByCategory.GetShoppingOptionsByCategory();
    		},
    		GetShoppingOptionsByCategory: function() {
    			var param = JSON2.stringify({ storeID: storeId, portalID: portalId, categoryID: categoryId, userName: userName, cultureName: cultureName });
    			$.ajax({
    				type: "POST",
    				url: aspxservicePath + "AspxCommerceWebService.asmx/BindCategoryDetails",
    				data: param,
    				contentType: "application/json; charset=utf-8",
    				dataType: "json",
    				success: function(msg) {
    					if (msg.d.length > 0) {
    						categoryOptions += "<h2>Browse by</h2><ul>";
    						$.each(msg.d, function(index, item) {
    							browseByCategory.BindCategoryDetails(item, index);
    						});
    						categoryOptions += "</ul><div class=\"cssClassclear\"></div>";
    					} else {
    						categoryOptions += "<span class=\"cssClassNotFound\">No category with item is found!</span>";
    					}
    					$("#divCategoryItemsOptions").html(categoryOptions);
    				}
    			});
    		},

    		BindCategoryDetails: function(response, index) {
    			categoryOptions += "<li><a href='" + aspxRedirectPath + 'category/' + response.CategoryName + ".aspx' alt='" + response.CategoryName + "' title='" + response.CategoryName + "'>" + response.CategoryName + "</a></li>";
    		}
    	},
    	browseByCategory.init();
    });