<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestimonialView.ascx.cs"
    Inherits="Modules_Testimonial_TestimonialView" %>

<script type="text/javascript">

    $(document).ready(function() {
        $(this).GetTestimonial({
            PortalID: '<%=PortalID%>',
            UserModuleID: '<%=UserModuleID%>',
            baseUrl: '<%=baseUrl %>',
            NoOfListToDisplay: '<%=NoOListToDisplay %>',
            ActiveImage: '<%=ActiveImage %>',
            ActiveDate: '<%=ActiveDate %>',
            ActiveViewMore: '<%=ActiveViewMore %>'
        });
    });    
</script>

<div class="cssClassCommonSideBox" style="display: block;">
    <div id="fade">
    </div>
    <h2>
        <span class="cssClassTestimonial">What our Customers Say?</span></h2>
    <div class="cssClassCommonSideBoxTable">
        <div id="divTestList" class="cssClassTestimonialList">
            <ul id="ulTestInfo" class="cssClassTestimonial">
            <li></li>
            </ul>
            <a class="cssClassReadMore">View More Testimonials</a> <span id="spnEmptyList" class="cssClassNotFound">
                Testimonial list is Empty</span>
        </div>
        <div id="dvDetails" class="Testpopupbox">
            <ul id="ulTestimonialDetails">
             <li></li>
            </ul>
        </div>
    </div>
</div>
