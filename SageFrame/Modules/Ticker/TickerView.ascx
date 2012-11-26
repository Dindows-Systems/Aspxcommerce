<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TickerView.ascx.cs" Inherits="Modules_Ticker_TickerView" %>

<script type="text/javascript">
    //<![CDATA[
    var PortalId = '<%=PortalId %>';
    var StoreId = '<%=StoreId %>';
    $(function() {
        LoadTickerNews();
    });

    function LoadTickerNews() {
        $.ajax({
            type: "POST",
            url: TickerModulePath + "Services/TickerWebService.asmx/GetAllTickerItem",
            data: JSON2.stringify({ StoreID: StoreId, PortalID: PortalId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            aync: false,
            success: function(msg) {
                var html = "";
                var array = [];
                $.each(msg.d, function(index, Data) {
                    array[index] = '<li class=\"news-item\">' + Data.TickerNews + '</li>';
                });
                $('#js-news').html(array.join(''));
                $('#js-news').ticker();
            },
            error: function() {
                alert('Error loading news');
            }
        });
    }
    //]]>
</script>

<ul id="js-news" class="js-hidden">
</ul>
