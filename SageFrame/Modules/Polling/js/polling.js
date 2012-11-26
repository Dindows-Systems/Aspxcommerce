

(function($) {
    $.PollScript = function(p) {
        p = $.extend
        ({
            UserModuleID: '1',
            PortalID: '1',
            pollID: '1',
            isPolled: 'False',
            PollingServicePath: 'Services/Services.aspx/'
        }, p);

        var imgPoll = new Image();
        imgPoll.src = 'images/red-bar.png';
        if (p.isPolled == 'True') //Already voted
        {
            animateResultsDirect();
        }
        else {
            $('input[name=rdoPoll]:radio').attr("checked", ""); //default select the first Choice


            $("#btnSubmit" + p.UserModuleID + "").click(function() {
                $("#divPoll").css("cursor", "wait"); //show wait cursor inside Poll div while processing

                var pID = p.pollID; //get Poll ID
                var cID = $("input[name='rdoPoll']:checked").val(); //get the checked Choice
                if (pID > 0 && cID != null) {
                    $.ajax(
                            { //call the Page method using JQuery ajax
                                type: "POST",
                                url: p.PollingServicePath + "Services/PollingWebService.asmx/UpdatePollCount",
                                data: JSON2.stringify({ pID: parseInt(pID), cID: parseInt(cID), UserModuleID: parseInt(p.UserModuleID), PortalID: parseInt(p.PortalID) }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                async: false,
                                success: function(msg)  //show the result
                                {
                                    $("div[id$=divAnswers]").fadeOut("fast");
                                    $("#divPoll").css("cursor", "default"); //remove the wait cursor
                                    $("div[id$=divPollResult_" + p.UserModuleID + "]").fadeOut("fast").html(msg.d).fadeIn("fast", function() { animateResults(); });

                                }
                            });
                }
                else {
					 $('#divmessage').attr("style","display:block");
				$('#divmessage').html("please select the answer.");
                    
                }
            });
        }
		
		$("#btnViewResult" + p.UserModuleID + "").click(function() {
		
                var pIDs = p.pollID; 
			//get Poll ID
                if (pIDs > 0) {
                    $.ajax(
                            { //call the Page method using JQuery ajax
                                type: "POST",
                                url: p.PollingServicePath + "Services/PollingWebService.asmx/GetResult",
                                data: JSON2.stringify({ pID: parseInt(pIDs), UserModuleID: parseInt(p.UserModuleID), PortalID: parseInt(p.PortalID) }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                async: false,
                                success: function(msg)  //show the result
                                {
                                    var htmlResult = "";
                                    htmlResult += msg.d;
                                    htmlResult += '<p><input type="button" value="Back to Voting" class="cssClassSubmitBtn" id="btnBackToVote' + p.UserModuleID + '"/></p>';
                                    $("div[id$=divAnswers]").fadeOut("fast");
                                    $("#divPoll").css("cursor", "default"); //remove the wait cursor
                                    $("div[id$=divPollResult_" + p.UserModuleID + "]").fadeOut("fast").html(htmlResult).fadeIn("fast", function() { animateResults(); });

                                }
                            });
                }
               
		});
		 $('#btnBackToVote' + p.UserModuleID + '').live("click", function() {
		 $('#divmessage').attr("style","display:none");
        $("div[id$=divPollResult_" + p.UserModuleID + "]").fadeOut("fast");
            $("div[id$=divAnswers]").fadeIn("fast");
           
        });

        function animateResults() {
            $("div[id$=divPollResult_" + p.UserModuleID + "] img").each(function() {
                var percentage = $(this).attr("val");
                $(this).css({ width: "0%" }).animate({ width: percentage }, 'slow');
            });
        }
        function animateResultsDirect() {

            $("div[id$=divAnswers] img").each(function() {
                var percentage = $(this).attr("val");
                $(this).css({ width: "0%" }).animate({ width: percentage }, 'slow');
            });
        }



    };
    $.fn.GetPollScipt = function(p) {
        $.PollScript(p);
    };
})(jQuery);


