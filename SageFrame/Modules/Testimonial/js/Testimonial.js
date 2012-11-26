
(function($) {
    $.createTestimonial = function(p) {
        p = $.extend
        ({
            baseUrl: '',
            PortalID: '',
            UserModuleID: '',
            NoOfListToDisplay: '',
            ActiveImage: '',
            ActiveDate: '',
            ActiveViewMore: ''

        }, p);
        GetLatestTestimonial();

        $('.cssClassReadMore').live('click', function() {
            ShowPopUp("dvDetails");
            GetDetails();
        });

        $('#fade').bind("click", function() {
            $('#fade,#dvDetails').fadeOut();
        });

        function ShowPopUp(popupid) {

            $('#' + popupid).fadeIn();

            $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn();
            var popuptopmargin = ($('#' + popupid).height() + 10) / 2;
            var popupleftmargin = ($('#' + popupid).width() + 10) / 2;
            $('#' + popupid).css({
                'margin-top': -popuptopmargin,
                'margin-left': -popupleftmargin
            });
        }

        function GetLatestTestimonial() {

            $.ajax({
                type: "POST",
                url: p.baseUrl + 'WebService.asmx/GetSelectedTestimonialList',
                data: JSON2.stringify({ PortalID: parseInt(p.PortalID), UserModuleID: parseInt(p.UserModuleID), NoOfList: parseInt(p.NoOfListToDisplay) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    var testml = msg.d;
                    if (testml.length > 0) {
                        $('#spnEmptyList').hide();
                        var html = '';
                        var Testimonia = '';
                        $('#ulTestInfo').html('');
                        $.each(msg.d, function(index, item) {
                            html += ('<li><div id="dvImageHolder" class=\"cssClassImageHolder"\>');
                            if (item.Image == '') {
                                html += ('<img alt="" src=' + p.baseUrl + 'image/Author.gif' + ' /></div>');
                            }
                            else {
                                html += ('<img alt="" src=' + p.baseUrl + 'image/UploadedImages/' + item.Image + ' /></div>');
                            }
                            html += ('<div class="cssClassAuthorDate"><span class="cssClassUser"><em class=\"cssClassUser\">' + item.UserName + '</em></span>');
                            html += ('<span class=\"cssClassDate\">' + item.AddedOn + '</span></div>')
                            html += ('<p class=\"cssClassTestmonialDetails\">');
                            html += item.Testimonial;
                            html += ('</p>');
                            html += ('<p class="cssClassLink"> <a href=\"' + item.WebUrl + '\" class=\"cssClasslink\" target="_blank">' + item.WebUrl + '</a></p>');
                            html += ('</li>');
                        });

                        $('#ulTestInfo').html(html);
                        GetSetting();
                    }
                    else {
                        $('.cssClassReadMore').attr('style', 'display:none');
                    }
                },
                error: function(error) {
                }
            });
        }

        function GetSetting() {
            if (p.ActiveImage == "False") {
                $('.cssClassImageHolder').attr('style', 'display:none');
            }
            if (p.ActiveDate == "False") {
                $('.cssClassDate').attr('style', 'display:none');
            }
            if (p.ActiveViewMore == "False") {
                $('.cssClassReadMore').attr('style', 'display:none');
            }
        }

        function GetDetails() {
            $.ajax({
                type: "POST",
                url: p.baseUrl + 'WebService.asmx/GetTestimonialList',
                data: JSON2.stringify({ PortalID: parseInt(p.PortalID), UserModuleID: parseInt(p.UserModuleID) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    var html = '';
                    $('#ulTestimonialDetails').html('');
                    $.each(msg.d, function(index, item) {
                        html += ('<li class=\"cssClassSep"\><div id="dvImg" class=\"cssClassImageHolder"\>');
                        if (item.Image == '') {
                            html += ('<img alt="" src=' + p.baseUrl + 'image/Author.gif' + ' /></div>');
                        }
                        else {
                            html += ('<img alt="" src=' + p.baseUrl + 'image/UploadedImages/' + item.Image + ' /></div>');
                        }

                        html += ('<p class="cssClassAuthorDate"> <em class=\"cssClassUser\">' + item.UserName + '</em>');
                        html += ('<span class=\"cssClassDate\">' + item.AddedOn + '</span></p>')
                        html += ('<p class=\"cssClassTestmonialDetails\">' + item.Testimonial + '</p>');
                        html += ('<p class="cssClassLink"> <a href=\"' + item.WebUrl + '\" class=\"cssClasslink\">' + item.WebUrl + '</a></p>');
                        html += ('</li>');
                    });

                    $('#ulTestimonialDetails').html(html);
                    GetSetting();
                },
                error: function(error) {
                }
            });
        }
    };
    $.fn.GetTestimonial = function(p) {
        $.createTestimonial(p);
    };
})(jQuery);


