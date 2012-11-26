$(function() {
    var storeId = AspxCommerce.utils.GetStoreID();
    var portalId = AspxCommerce.utils.GetPortalID();
    var userName = AspxCommerce.utils.GetUserName();
    var cultureName = AspxCommerce.utils.GetCultureName();
    var Category = {
        vars: {
            itemPath: ""
        },
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
            ajaxCallMode: 0
        },
        ajaxCall: function(config) {
            $.ajax({
                type: Category.config.type,
                contentType: Category.config.contentType,
                cache: Category.config.cache,
                async: Category.config.async,
                url: Category.config.url,
                data: Category.config.data,
                dataType: Category.config.dataType,
                success: Category.ajaxSuccess,
                error: Category.ajaxFailure
            });
        },
        ajaxSuccess: function(msg) {
            switch (Category.config.ajaxCallMode) {
                case 1:
                    if (msg.d.length > 0) {
                        var categoryID = '';
                        var parentID = '';
                        var categoryLevel = '';
                        var attributeValue;
                        var catListmaker = '';
                        var catList = '';
                        catListmaker += "<div class=\"cssClassCategoryNav\"><ul class='cssClassCategoryUl'>";
                        $.each(msg.d, function(index, eachCat) {
                            var totalCategory = eachCat.length;
                            var css = '';
                            if (eachCat.ChildCount > 0) {
                                css = "class=cssClassCategoryParent";
                            } else {
                                css = "";
                            }
                            categoryID = eachCat.CategoryID;
                            parentID = eachCat.ParentID;
                            categoryLevel = eachCat.CategoryLevel;
                            attributeValue = eachCat.AttributeValue;
                            if (eachCat.CategoryLevel == 0) {
                                var hrefParentCategory = aspxRedirectPath + "category/" + Category.fixedEncodeURIComponent(eachCat.AttributeValue) + ".aspx";
                                catListmaker += "<li " + css + "><a href=" + hrefParentCategory + ">";
                                catListmaker += eachCat.AttributeValue;
                                catListmaker += "</a>";

                                if (eachCat.ChildCount > 0) {
                                    catListmaker += "<ul>";
                                    Category.vars.itemPath += eachCat.AttributeValue;
                                    catListmaker += Category.BindChildCategory(msg.d, categoryID);
                                    catListmaker += "</ul>";
                                }
                                catListmaker += "</li>";
                            }
                            Category.vars.itemPath = '';
                        });
                        catListmaker += "<div class=\"cssClassclear\"></div></ul></div>";
                        $("#divCategoryLister").html(catListmaker);
                        $('.cssClassCategoryNav>ul>li:last').addClass('cssClassLastMenu');
                        if ($('#breadcrumb').length > 0) {
                            if (AspxCommerce.utils.GetAspxRootPath() == "/") {
                                Breadcrum.getBreadcrumforlive();
                            } else {
                                // Breadcrum.getBreadcrum();
                            }
                        }
                    } else {
                        $("#divCategoryLister").html("<span class=\"cssClassNotFound\">This store has no category found!</span>");
                    }
                    break;
            }
        },
        fixedEncodeURIComponent: function(str) {
            return encodeURIComponent(str).replace(/!/g, '%21').replace(/'/g, '%27').replace(/\(/g, '%28').replace(/\)/g, '%29').replace(/-/g, '_').replace(/\*/g, '%2A').replace(/%26/g, 'ampersand').replace(/%20/g, '-');
        },
        BindCategory: function() {
            this.config.method = "AspxCommerceWebService.asmx/GetCategoryMenuList";
            this.config.url = this.config.baseURL + this.config.method;
            this.config.data = JSON2.stringify({ storeID: AspxCommerce.utils.GetStoreID(), portalID: AspxCommerce.utils.GetPortalID(), cultureName: cultureName });
            this.config.ajaxCallMode = 1;
            this.ajaxCall(this.config);
        },
        BindChildCategory: function(response, categoryID) {
            var strListmaker = '';
            var childNodes = '';
            var path = '';
            Category.vars.itemPath += "/";
            $.each(response, function(index, eachCat) {
                if (eachCat.CategoryLevel > 0) {
                    if (eachCat.ParentID == categoryID) {
                        var css = '';
                        if (eachCat.ChildCount > 0) {
                            css = "class=cssClassCategoryParent";
                        } else {
                            css = "";
                        }
                        var hrefCategory = aspxRedirectPath + "category/" + Category.fixedEncodeURIComponent(eachCat.AttributeValue) + ".aspx";
                        Category.vars.itemPath += eachCat.AttributeValue;
                        strListmaker += "<li " + css + "><a href=" + hrefCategory + ">" + eachCat.AttributeValue + "</a>";
                        childNodes = Category.BindChildCategory(response, eachCat.CategoryID);
                        Category.vars.itemPath = Category.vars.itemPath.replace(Category.vars.itemPath.lastIndexOf(eachCat.AttributeValue), '');
                        if (childNodes != '') {
                            strListmaker += "<ul>" + childNodes + "</ul>";
                        }
                        strListmaker += "</li>";
                    }
                }
            });
            return strListmaker;
        },
        Init: function() {
            Category.BindCategory();
        }
    };
    Category.Init();
});