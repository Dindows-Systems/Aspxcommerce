(function(a) {
    a.createSageBanner = function(b) {
        b = a.extend({
            baseURL: "Services/Services.aspx",
            BannerId: 2,
            Auto_Direction: "",
            Auto_Hover: "True",
            Auto_Slide: "",
            DisplaySlideQty: 1,
            Easing: "",
            InfiniteLoop: "",
            NavigationImagePager: "",
            Pause_Time: 1,
            MoveSlideQty: 1,
            NumericPager: "",
            RandomStart: "",
            TransitionMode: "",
            Speed: 1,
            Starting_Slide: 1,
            Caption: ""
        }, b);
        if (b.Auto_Hover == "True") {
            b.Auto_Hover = true
        } else {
            b.Auto_Hover = false
        }
        if (b.Auto_Slide == "True") {
            b.Auto_Slide = true
        } else {
            b.Auto_Slide = false
        }
        if (b.Caption == "True") {
            b.Caption = true
        } else {
            b.Caption = false
        }
        if (b.InfiniteLoop == "True") {
            b.InfiniteLoop = true
        } else {
            b.InfiniteLoop = false
        }
        if (b.NumericPager == "True") {
            b.NumericPager = true
        } else {
            b.NumericPager = false
        }
        if (b.RandomStart == "True") {
            b.RandomStart = true
        } else {
            b.RandomStart = false
        }
        b.TransitionMode = jQuery.trim(b.TransitionMode);
        if (b.TransitionMode == "vertical") {
            b.Auto_Slide = false
        }
        a.ajax({
            type: "POST",
            url: b.baseURL + "Services/SageBannerService.asmx/GetBannerImages",
            data: JSON2.stringify({
                BannerID: b.BannerId
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(c) {
                var d = "";
                var e = "";
                a("#slider").html("");
                a.each(c.d, function(c, d) {
                    if (d.ImagePath.length == 0) {
                        a("#slider").append("<li>" + d.HTMLBodyText + "</li>")
                    } else {
                        if (d.ReadMorePage == "") {
                            e += '<li><div class="cssClassRightDiv"><img src=' + b.baseURL + "images/CroppedImages/" + d.ImagePath + ' /></div><div class="lof-main-item-desc"><p>' + d.Description + '</p><div class="cssClassReadMore"><a href=' + d.LinkToImage + "><span>" + d.ReadButtonText + "</span></a></div></div></li>"
                        } else {
                            var f = "#";
                            if (d.ReadMorePage != "#") {
                                f = d.ReadMorePage + ".aspx";
                                e += '<li><div class="cssClassRightDiv"><img src=' + b.baseURL + "images/CroppedImages/" + d.ImagePath + ' /></div><div class="lof-main-item-desc"><p>' + d.Description + '</p><div class="cssClassReadMore"><a href=' + f + "><span>" + d.ReadButtonText + "</span></a></div></div></li>"
                            }
                        }
                    }
                    a("#slider").html(e)
                });
                var f = a("#slider").bxSlider({
                    mode: b.TransitionMode,
                    infiniteLoop: b.InfiniteLoop,
                    speed: b.Speed,
                    easing: b.Easing,
                    pager: false,
                    pagerType: "full",
                    pagerLocation: "bottom",
                    pagerActiveClass: "pager-active",
                    nextText: "next",
                    prevText: "prev",
                    captions: b.Caption,
                    captionsSelector: null,
                    auto: b.Auto_Slide,
                    autoDirection: "next",
                    autoControls: false,
                    autoControlsSelector: null,
                    autoStart: true,
                    autoHover: b.Auto_Hover,
                    pause: b.Pause_Time,
                    startText: "start",
                    stopText: "stop",
                    stopImage: "",
                    wrapperClass: "",
                    startingSlide: b.Starting_Slide,
                    displaySlideQty: b.DisplaySlideQty,
                    moveSlideQty: b.MoveSlideQty,
                    randomStart: b.RandomStart,
                    controls: true
                });
                a(".thumbs a").live("click", function() {
                    var b = a(".thumbs a").index(this);
                    alert(b);
                    f.goToSlide(b);
                    a(".thumbs a").removeClass("pager-active");
                    a(this).addClass("pager-active");
                    return false
                });
                a(this).addClass("pager-active")
            },
            error: function() {
                alert("cant load js")
            }
        })
    };
    a.fn.SageBannerjs = function(b) {
        a.createSageBanner(b)
    }
})(jQuery)