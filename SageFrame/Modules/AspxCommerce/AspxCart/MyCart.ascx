<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MyCart.ascx.cs" Inherits="MyCart" %>

<script type="text/javascript">

    //<![CDATA[
    var showItemImagesOnCartSetting = '<%=ShowItemImagesOnCart%>';
    var allowOutStockPurchaseSetting = '<%=AllowOutStockPurchase %>';
    var minOrderAmountSetting = '<%=MinOrderAmount%>';
    var allowMultipleAddShippingSetting = '<%=AllowMultipleAddShipping%>';
    var noImageMyCartPathSetting = '<%=NoImageMyCartPath %>'; 
		//]]>
</script>

<%--<input type="button" id="btnSubmit" value="Pay through PayPal" />--%>
<div class="cssClassCartInformation cssClassCartTotal">
    <div class="cssClassBlueBtnWrapper">
        <div class="cssClassBlueBtn">
            <a id="lnkProceedToSingleCheckout" href="#"><span>Proceed to Checkout</span></a>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
    <div class="cssClassCartInformationDetails" id="divCartDetails">
    </div>
    <table class="cssClassSubTotalAmount">
        <tbody>
            <tr>
                <td>
                    <strong>Grand Sub Total:</strong>
                </td>
                <td>
                    <input type="text" class="total-box cssClassFormatCurrency" value="" id="product-subtotal"
                        readonly="readonly" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Discount:</strong>
                </td>
                <td>
                    <input type="text" class="cssClassFormatCurrency" id="txtDiscountAmount" readonly="readonly"
                        value="0.00" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Total Tax:</strong>
                </td>
                <td>
                    <input type="text" class="tax-box cssClassFormatCurrency" id="txtTotalTax" readonly="readonly"
                        value="0.00" />
                </td>
            </tr>
        </tbody>
    </table>
    <div class="cssClassLeftRightBtn">
        <div class="cssClassCartInformation">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="noborder">
                <tbody>
                    <tr class="cssClassHeadeTitle cssClassAlternativeEven">
                        <td class="cssClassSubTotalAmountWidth">
                            <strong>Grand Total:</strong>
                        </td>
                        <td class="cssClassGrandTotalAmountWidth">
                            <input type="text" readonly="readonly" class="cssClassFormatCurrency" id="txtTotalCost"
                                value="0" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="cssClassCouponHelp">
        <h3>
            <span class="cssClassRequired">*</span>Coupon amount is applied to your cart
            after you click the apply button. You can check your coupon code in your mail!</h3>
    </div>
    <div class="cssClassapplycoupon">
        <h3>
            Enter the Coupon Code if you have one:</h3>
        <input type="text" id="txtCouponCode" />
        <div class="cssClassButtonWrapper">
            <div class="cssClassButton">
                <a href="#"><span id="btnSubmitCouponCode">Apply Coupon</span></a>
            </div>
        </div>
        <div class="cssClassClear">
        </div>
    </div>
    <div class="cssClassBlueBtnWrapper">
        <div class="cssClassBlueBtn">
            <a id="lnkProceedToSingleChkout" href="#"><span>Proceed to Checkout</span> </a>
        </div>
        <div class="cssClassButtonWrapper">
            <div class="cssClassButton ">
                <button type="button" id="lnkContinueShopping" ><span><span>Continue Shopping</span></span></button>
            </div>
        </div>
        <div class="cssClassButtonWrapper">
            <div class="cssClassButton">
                <%-- <a href="#"><span id="btnUpdateShoppingCart">Update Shopping Cart</span></a>--%>
                <button type="button" id="btnUpdateShoppingCart">
                    <span><span>Update Shopping Cart</span></span></button>
            </div>
        </div>
        <div class="cssClassButtonWrapper">
            <div class="cssClassButton">
                <%-- <a href="#"><span id="btnClear">Clear Cart Items</span></a>--%>
                <button type="button" id="btnClear">
                    <span><span>Clear Cart Items</span></span></button>
            </div>
        </div>
        <%--   <div class="cssClassBlueBtn" id="divCheckOutMultiple">
            <a id="lnkProceedToMultiCheckout" href="#" onclick='SetSession();'><span>Checkout With Multiple Address</span>
            </a>
        </div>--%>
        <div class="cssClassClear">
        </div>
    </div>
</div>
