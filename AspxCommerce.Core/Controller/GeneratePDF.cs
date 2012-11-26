using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;



namespace AspxCommerce.Core.Controller
{
    public class GeneratePDF
    {

        public void GenerateOrderDetailsPDF(string tableContent, string hdnDescriptionValue, string TemplateName)
        {
            var detailDataObj = JSONHelper.Deserialise<List<OrderDetailsData>>(tableContent);

            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition",
                                                   "attachment;filename=" + "MyReport_" +
                                                   DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".pdf");

            Document doc = new Document(iTextSharp.text.PageSize.A4, 0, 0, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Current.Response.OutputStream);
            writer.PageEvent = new MyPageEventHandler();

            doc.Open();

            //--- start of header----
            PdfPTable headerTbl = new PdfPTable(2);
            headerTbl.SetWidths(new int[2] {10, 15});

            headerTbl.TotalWidth = doc.PageSize.Width;
            iTextSharp.text.Image logo =
                iTextSharp.text.Image.GetInstance(
                    HttpContext.Current.Request.MapPath("~/Templates/" + TemplateName + "/images/AspxCommerce.png"));
            logo.ScalePercent(50f);
            PdfPCell cellH = new PdfPCell(logo);
            cellH.AddElement(logo);
            cellH.HorizontalAlignment = Element.ALIGN_LEFT;
            cellH.PaddingLeft = 5;

            cellH.Border = Rectangle.NO_BORDER;
            // cellH.Border = Rectangle.BOTTOM_BORDER;//for underline below logo
            headerTbl.AddCell(cellH);

            //--for second cell ----------
            Paragraph pa = new Paragraph("Order Details",
                                         FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16, Font.BOLD,
                                                             new BaseColor(0, 0, 255)));
            PdfPCell cell2 = new PdfPCell(pa);
            pa.Alignment = Element.ALIGN_BOTTOM;
            cell2.AddElement(pa);
            //cell2.HorizontalAlignment = Element.ALIGN_BASELINE;
            cell2.Border = Rectangle.NO_BORDER;
            //  cell2.Border = Rectangle.BOTTOM_BORDER;// for underline below logo
            headerTbl.AddCell(cell2);

            //for one line spacing
            PdfPCell blankCell = new PdfPCell();
            blankCell.Colspan = 2;
            blankCell.Border = Rectangle.NO_BORDER;
            headerTbl.AddCell(blankCell);
            var headingCellBackColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F4F8FC"));

            //for header description
            var r1 = JSONHelper.Deserialise<OrderDetailsData>(hdnDescriptionValue);
            string orderedDate = r1.OrderDate;
            string storeName = r1.StoreName;
            string storeDescription = r1.StoreDescription;
            string paymentGatewayType = r1.PaymentGatewayType;
            string paymentMethod = r1.PaymentMethod;
            string billingAddress = r1.BillingAddress;
            billingAddress = billingAddress.Replace("<b>", "");
            billingAddress = billingAddress.Trim();
            billingAddress = billingAddress.Replace("</b>", "");
            string[] splitter = new string[] {"<br>"};
            string[] billingAddColl = billingAddress.Split(splitter, StringSplitOptions.None);

            Paragraph bodyPa =
                new Paragraph(
                    "Ordered Date: " + orderedDate + Environment.NewLine + "Store Name: " + storeName +
                    Environment.NewLine + "Store Descrption: " + storeDescription +
                    Environment.NewLine + "Payment Gateway Type: " + paymentGatewayType + Environment.NewLine +
                    "Payment Method: " + paymentMethod,
                    FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.NORMAL, new BaseColor(0, 0, 255)));

            PdfPCell bodyCell = new PdfPCell(bodyPa);
            bodyCell.HorizontalAlignment = Element.ALIGN_LEFT;
            bodyCell.PaddingLeft = 5;
            bodyCell.Border = Rectangle.NO_BORDER;

            bodyCell.HorizontalAlignment = Element.PARAGRAPH;
            bodyCell.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(bodyCell);

            string billAdd = string.Empty;

            for (int i = 0; i <= billingAddColl.Length - 1; i++)
            {
                if (billingAddColl[i] != "")
                {
                    if (billingAddColl[i].ToLower().Trim() == "billing address:")
                    {
                        billAdd += billingAddColl[i].ToUpper() + Environment.NewLine;
                    }
                    else
                    {
                        billAdd += billingAddColl[i].Trim() + Environment.NewLine;
                    }
                }
            }
            PdfPCell billingCell =
                new PdfPCell(new Paragraph(billAdd,
                                           FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.NORMAL,
                                                               new BaseColor(0, 0, 225))));
            billingCell.Border = Rectangle.NO_BORDER;
            billingCell.PaddingLeft = 50f;
            billingCell.PaddingRight = 20;
            billingCell.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(billingCell);

            //for one line spacing
            PdfPCell blankCell2 = new PdfPCell();
            blankCell2.Colspan = 2;
            blankCell2.Border = Rectangle.NO_BORDER;
            headerTbl.AddCell(blankCell2);
            headerTbl.SpacingAfter = 10f;
            doc.Add(headerTbl);

            //---end of header

            //--- Detail parts---
            PdfPTable bodyTbl = new PdfPTable(7);
            var headingFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, new BaseColor(0, 0, 225));
            var bodyFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, new BaseColor(0, 0, 0));
            PdfPCell headingCell =
                new PdfPCell(new Paragraph("Ordered Items:",
                                           FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL,
                                                               new BaseColor(0, 0, 225))));
            headingCell.Colspan = 7;
            headingCell.Border = Rectangle.NO_BORDER;
            bodyTbl.AddCell(headingCell);

            // bodyTbl.TotalWidth = doc.PageSize.Width-100;
            var cellHeadingColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#E3EDFA"));

            Paragraph middlePart = new Paragraph("Item Name", headingFont);
            PdfPCell middleCell = new PdfPCell(middlePart);
            middleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            middleCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(middleCell);

            var shippingMethodCell = new PdfPCell(new Paragraph("Shipping Method", headingFont));
            shippingMethodCell.HorizontalAlignment = Element.ALIGN_CENTER;
            shippingMethodCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(shippingMethodCell);

            var shippingAddressHeadingCell = new PdfPCell(new Paragraph("Shipping Address", headingFont));
            shippingAddressHeadingCell.HorizontalAlignment = Element.ALIGN_CENTER;
            shippingAddressHeadingCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(shippingAddressHeadingCell);

            var shippingRateHeadingCell = new PdfPCell(new Paragraph("Shipping Rate", headingFont));
            shippingRateHeadingCell.HorizontalAlignment = Element.ALIGN_CENTER;
            shippingRateHeadingCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(shippingRateHeadingCell);

            var priceHedingCell = new PdfPCell(new Paragraph("Price", headingFont));
            priceHedingCell.HorizontalAlignment = Element.ALIGN_CENTER;
            priceHedingCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(priceHedingCell);

            var quantityHeadingCell = new PdfPCell(new Paragraph("Quantity", headingFont));
            quantityHeadingCell.HorizontalAlignment = Element.ALIGN_CENTER;
            quantityHeadingCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(quantityHeadingCell);

            var subTotalHeadingCell = new PdfPCell(new Paragraph("SubTotal", headingFont));
            subTotalHeadingCell.HorizontalAlignment = Element.ALIGN_CENTER;
            subTotalHeadingCell.BackgroundColor = cellHeadingColor;
            bodyTbl.AddCell(subTotalHeadingCell);

            int length = detailDataObj.Count;
            for (int j = 0; j <= length - 1; j++)
            {
                var itemNameCell = new PdfPCell(new Paragraph(detailDataObj[j].ItemName, bodyFont));
                itemNameCell.PaddingLeft = 15;
                if (detailDataObj[j].ItemName == "")
                {
                    itemNameCell.Border = Rectangle.LEFT_BORDER;
                    if (j == length - 1)
                    {
                        itemNameCell.Border = Rectangle.LEFT_BORDER + Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl.AddCell(itemNameCell);

                var shippingMethodNameCell = new PdfPCell(new Paragraph(detailDataObj[j].ShippingMethodName, bodyFont));
                shippingMethodNameCell.PaddingLeft = 5;
                if (detailDataObj[j].ShippingMethodName == "")
                {
                    shippingMethodNameCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        shippingMethodNameCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl.AddCell(shippingMethodNameCell);

                var shippingAddressCell = new PdfPCell(new Paragraph(detailDataObj[j].ShippingAddress, bodyFont));
                shippingAddressCell.PaddingLeft = 5;
                if (detailDataObj[j].ShippingAddress == "")
                {
                    shippingAddressCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        shippingAddressCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl.AddCell(shippingAddressCell);

                var shippingRateCell = new PdfPCell(new Paragraph(detailDataObj[j].ShippingRate, bodyFont));
                shippingRateCell.PaddingRight = 15;
                shippingRateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                if (detailDataObj[j].ShippingRate == "")
                {
                    shippingRateCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        shippingRateCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl.AddCell(shippingRateCell);

                var priceCell = new PdfPCell(new Paragraph(detailDataObj[j].Price, bodyFont));
                priceCell.PaddingRight = 15;
                priceCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                if (detailDataObj[j].Price == "")
                {
                    priceCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        priceCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl.AddCell(priceCell);

                var quantityCell = new PdfPCell(new Paragraph(detailDataObj[j].Quantity, bodyFont));
                quantityCell.PaddingLeft = 15;
                if (detailDataObj[j].Quantity == "Tax Total:" || detailDataObj[j].Quantity == "Shipping Cost:" ||
                    detailDataObj[j].Quantity == "Discount Amount:" || detailDataObj[j].Quantity == "Coupon Amount:" ||
                    detailDataObj[j].Quantity == "Grand Total:")
                {
                    quantityCell.PaddingLeft = 3;
                }
                bodyTbl.AddCell(quantityCell);

                var subTotalCell = new PdfPCell(new Paragraph(detailDataObj[j].SubTotal, bodyFont));
                subTotalCell.PaddingRight = 15;
                subTotalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                bodyTbl.AddCell(subTotalCell);
            }
            doc.Add(bodyTbl);

            // StyleSheet styles = new StyleSheet();
            // HTMLWorker hw = new HTMLWorker(doc);
            //hw.Parse(new StringReader(tableContent));
            doc.Close();
        }

        public void GenerateInvoicePDF(string headerDetail, string tableContent, string hdnRemarksData, string hdnIsMultipleShipping, string TemplateName)
        {
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition",
                                                   "attachment;filename=" + "MyReport_Invoice_" +
                                                   DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".pdf");

            Document doc = new Document(iTextSharp.text.PageSize.A4, 0, 0, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Current.Response.OutputStream);
            writer.PageEvent = new MyPageEventHandler();
            doc.Open();

            //--- start of header----
            PdfPTable headerTbl = new PdfPTable(2);
            headerTbl.SetWidths(new int[2] {10, 15});
            //headerTbl.DefaultCell.Border = Rectangle.NO_BORDER;
            headerTbl.TotalWidth = doc.PageSize.Width;
            iTextSharp.text.Image logo =
                iTextSharp.text.Image.GetInstance(
                    HttpContext.Current.Request.MapPath("~/Templates/" + TemplateName + "/images/AspxCommerce.png"));
            logo.ScalePercent(50f);
            PdfPCell cellH = new PdfPCell(logo);
            cellH.AddElement(logo);
            cellH.HorizontalAlignment = Element.ALIGN_LEFT;
            cellH.PaddingLeft = 5;
            cellH.Border = Rectangle.NO_BORDER;
            // cellH.Border = Rectangle.BOTTOM_BORDER;// for underline below logo
            headerTbl.AddCell(cellH);

            //--for second cell ----------
            Paragraph pa = new Paragraph("Invoice",
                                         FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16, Font.BOLD,
                                                             new BaseColor(0, 0, 255)));
            PdfPCell cell2 = new PdfPCell(pa);
            pa.Alignment = Element.ALIGN_BOTTOM;
            cell2.AddElement(pa);

            cell2.Border = Rectangle.NO_BORDER;
            // cell2.Border = Rectangle.BOTTOM_BORDER;// for underline below logo
            headerTbl.AddCell(cell2);

            //for one line spacing
            PdfPCell blankCell = new PdfPCell();
            blankCell.Colspan = 2;
            blankCell.Border = Rectangle.NO_BORDER;
            headerTbl.AddCell(blankCell);

            var headingCellBackColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F4F8FC"));
            var headingFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.NORMAL, new BaseColor(0, 0, 255));

            //for header description
            var h1 = JSONHelper.Deserialise<InvoiceDetailDataTableInfo>(headerDetail);

            Paragraph headingPara =
                new Paragraph("Invoice No: " + h1.InvoiceNo + Environment.NewLine + "Invoice Date: " + h1.InvoiceDate,
                              headingFont);
            PdfPCell invoiceDateCell = new PdfPCell(headingPara);
            invoiceDateCell.Colspan = 2;
            invoiceDateCell.Border = Rectangle.NO_BORDER;
            invoiceDateCell.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(invoiceDateCell);

            //for one line spacing
            PdfPCell blankCell2 = new PdfPCell();
            blankCell2.Colspan = 2;
            blankCell2.Border = Rectangle.NO_BORDER;
            blankCell2.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(blankCell2);


            Paragraph invoiceStorePara =
                new Paragraph(
                    "StoreName: " + h1.StoreName + Environment.NewLine + "Store Description: " + h1.StoreDescription +
                    Environment.NewLine +
                    "Customer Name: " + h1.CustomerName + Environment.NewLine + "Customer Email: " + h1.CustomerEmail,
                    headingFont);
            PdfPCell invoiceStoreDetail = new PdfPCell(invoiceStorePara);
            invoiceStoreDetail.Border = Rectangle.NO_BORDER;
            invoiceStoreDetail.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(invoiceStoreDetail);

            Paragraph invoiceOrderDetailPara =
                new Paragraph(
                    "Order ID: " + h1.OrderId + Environment.NewLine + "Order Date: " + h1.OrderDate +
                    Environment.NewLine +
                    "Status: " + h1.Status +
                    Environment.NewLine + "Payment Method: " + h1.PaymentMethod + Environment.NewLine +
                    "Shipping Method: " + h1.ShippingMethod, headingFont);
            PdfPCell invoiceOrderDetail = new PdfPCell(invoiceOrderDetailPara);
            invoiceOrderDetail.Border = Rectangle.NO_BORDER;
            invoiceOrderDetail.PaddingLeft = 50f;
            invoiceOrderDetail.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(invoiceOrderDetail);

            //for one line spacing
            PdfPCell blankCell3 = new PdfPCell();
            blankCell3.Colspan = 2;
            blankCell3.Border = Rectangle.NO_BORDER;
            blankCell3.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(blankCell3);


            string[] splitter = new string[] {"<br>"};
            var shippingAddress = h1.ShippingAddress.Replace("<b>", ""); //.Replace(" ", "");
            shippingAddress = shippingAddress.Trim();
            shippingAddress = shippingAddress.Replace("</b>", "");
            string[] shippingAddColl = shippingAddress.Split(splitter, StringSplitOptions.None);
            string shipAdd = string.Empty;
            for (int i = 0; i <= shippingAddColl.Length - 1; i++)
            {
                if (shippingAddColl[i] != "")
                {
                    if (shippingAddColl[i].ToLower().Trim() == "shipping to:")
                    {
                        shipAdd += shippingAddColl[i].ToUpper() + Environment.NewLine;
                    }
                    else
                    {
                        shipAdd += shippingAddColl[i] + Environment.NewLine;
                    }
                }
            }

            Paragraph shippingAddPara = new Paragraph(shipAdd, headingFont);
            PdfPCell shippingAddCell = new PdfPCell(shippingAddPara);
            shippingAddCell.Border = Rectangle.NO_BORDER;
            shippingAddCell.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(shippingAddCell);

            var billingAddress = h1.BillingAddress.Replace("<b>", ""); //.Replace(" ", "");
            billingAddress = billingAddress.Trim();
            billingAddress = billingAddress.Replace("</b>", "");

            string[] billingAddColl = billingAddress.Split(splitter, StringSplitOptions.None);
            string billAdd = string.Empty;

            for (int i = 0; i <= billingAddColl.Length - 1; i++)
            {
                if (billingAddColl[i] != "")
                {
                    if (billingAddColl[i].ToLower().Trim() == "billing to:")
                    {
                        billAdd += billingAddColl[i].ToUpper() + Environment.NewLine;
                    }
                    else
                    {
                        billAdd += billingAddColl[i] + Environment.NewLine;
                    }
                }
            }

            Paragraph billinAddPara = new Paragraph(billAdd, headingFont);
            PdfPCell billingAddCell = new PdfPCell(billinAddPara);
            billingAddCell.Border = Rectangle.NO_BORDER;
            billingAddCell.PaddingLeft = 50f;
            billingAddCell.BackgroundColor = headingCellBackColor;
            headerTbl.AddCell(billingAddCell);

            //for one line spacing
            PdfPCell blankCell4 = new PdfPCell();
            blankCell4.Colspan = 2;
            blankCell4.Border = Rectangle.NO_BORDER;
            headerTbl.AddCell(blankCell4);
            headerTbl.SpacingAfter = 10f;
            doc.Add(headerTbl);

            //headerTbl.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height - 10), writer.DirectContent);
            //---end of header
            var bodyFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, new BaseColor(0, 0, 0));
            var cellHeadingColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#E3EDFA"));

            var invoiceDetailDataObj = JSONHelper.Deserialise<List<InvoiceDetailDataTableInfo>>(tableContent);
            PdfPTable bodyTbl1;
            string isMultipleShipping = hdnIsMultipleShipping;
            int noOfCol;
            if (isMultipleShipping == "true")
            {
                PdfPTable bodyTbl = new PdfPTable(6);
                PdfPCell headingCell =
                    new PdfPCell(new Paragraph("Ordered Items:",
                                               FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL,
                                                                   new BaseColor(0, 0, 225))));
                headingCell.Colspan = 6;
                headingCell.Border = Rectangle.NO_BORDER;
                bodyTbl.AddCell(headingCell);
                noOfCol = 6;
                bodyTbl1 = bodyTbl;
            }
            else
            {
                PdfPTable bodyTbl = new PdfPTable(4);
                PdfPCell headingCell =
                    new PdfPCell(new Paragraph("Ordered Items:",
                                               FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL,
                                                                   new BaseColor(0, 0, 225))));
                headingCell.Colspan = 4;
                headingCell.Border = Rectangle.NO_BORDER;
                bodyTbl.AddCell(headingCell);
                noOfCol = 4;
                bodyTbl1 = bodyTbl;
            }
            int length = invoiceDetailDataObj.Count;
            for (int j = 0; j <= length - 1; j++)
            {
                var itemNameCell = new PdfPCell(new Paragraph(invoiceDetailDataObj[j].ItemName, bodyFont));
                itemNameCell.PaddingLeft = 15;
                if (j == 0)
                {
                    itemNameCell.BackgroundColor = cellHeadingColor;
                    itemNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
                if (invoiceDetailDataObj[j].ItemName == "")
                {
                    itemNameCell.Border = Rectangle.LEFT_BORDER;
                    if (j == length - 1)
                    {
                        itemNameCell.Border = Rectangle.LEFT_BORDER + Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl1.AddCell(itemNameCell);

                if (invoiceDetailDataObj[j].ShippingMethodName != null)
                {
                    var shippingMethodNameCell =
                        new PdfPCell(new Paragraph(invoiceDetailDataObj[j].ShippingMethodName, bodyFont));
                    shippingMethodNameCell.PaddingLeft = 2;
                    if (j == 0)
                    {
                        shippingMethodNameCell.BackgroundColor = cellHeadingColor;
                        shippingMethodNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    if (invoiceDetailDataObj[j].ShippingMethodName == "" && invoiceDetailDataObj[j].ItemName == "")
                    {
                        shippingMethodNameCell.Border = Rectangle.NO_BORDER;
                        if (j == length - 1)
                        {
                            shippingMethodNameCell.Border = Rectangle.BOTTOM_BORDER;
                        }
                    }
                    bodyTbl1.AddCell(shippingMethodNameCell);
                }
                if (invoiceDetailDataObj[j].ShippingAddressDetail != null)
                {
                    var shippingAddressCell = invoiceDetailDataObj[j].ShippingAddressDetail;
                    shippingAddressCell = shippingAddressCell.Replace("<br>", " ").Replace(" ", "");

                    var shippingAddressDetailCell = new PdfPCell(new Paragraph(shippingAddressCell, bodyFont));
                    shippingAddressDetailCell.PaddingLeft = 2;
                    if (j == 0)
                    {
                        shippingAddressDetailCell.BackgroundColor = cellHeadingColor;
                        shippingAddressDetailCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    if (invoiceDetailDataObj[j].ShippingAddressDetail == "")
                    {
                        shippingAddressDetailCell.Border = Rectangle.NO_BORDER;
                        if (j == length - 1)
                        {
                            shippingAddressDetailCell.Border = Rectangle.BOTTOM_BORDER;
                        }
                    }
                    bodyTbl1.AddCell(shippingAddressDetailCell);
                }
                var priceCell = new PdfPCell(new Paragraph(invoiceDetailDataObj[j].Price, bodyFont));
                priceCell.PaddingRight = 30;
                priceCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                if (j == 0)
                {
                    priceCell.BackgroundColor = cellHeadingColor;
                    priceCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
                if (invoiceDetailDataObj[j].Price == "")
                {
                    priceCell.Border = Rectangle.NO_BORDER;
                    if (j == length - 1)
                    {
                        priceCell.Border = Rectangle.BOTTOM_BORDER;
                    }
                }
                bodyTbl1.AddCell(priceCell);

                var quantityCell = new PdfPCell(new Paragraph(invoiceDetailDataObj[j].Quantity, bodyFont));
                quantityCell.HorizontalAlignment = Element.ALIGN_CENTER;
                if (j == 0)
                {
                    quantityCell.BackgroundColor = cellHeadingColor;
                    quantityCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
                if (invoiceDetailDataObj[j].Quantity == "Sub Total:" || invoiceDetailDataObj[j].Quantity == "Taxes:" ||
                    invoiceDetailDataObj[j].Quantity == "Shipping Cost:" ||
                    invoiceDetailDataObj[j].Quantity == "Discount Amount:" ||
                    invoiceDetailDataObj[j].Quantity == "Coupon Amount:" ||
                    invoiceDetailDataObj[j].Quantity == "Total Cost:")
                {
                    quantityCell.PaddingLeft = 15;
                    quantityCell.HorizontalAlignment = Element.ALIGN_LEFT;
                }
                bodyTbl1.AddCell(quantityCell);

                var subTotalCell = new PdfPCell(new Paragraph(invoiceDetailDataObj[j].SubTotal, bodyFont));
                subTotalCell.PaddingRight = 15;
                subTotalCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                if (j == 0)
                {
                    subTotalCell.BackgroundColor = cellHeadingColor;
                    subTotalCell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
                bodyTbl1.AddCell(subTotalCell);
            }

            PdfPCell remarekCell = new PdfPCell(new Paragraph(hdnRemarksData, bodyFont));
            remarekCell.Border = Rectangle.NO_BORDER;
            remarekCell.Colspan = noOfCol;
            bodyTbl1.AddCell(remarekCell);
            doc.Add(bodyTbl1);

            //iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
            // iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
            //hw.Parse(new StringReader(tableContent));
            doc.Close();
        }

        private class MyPageEventHandler : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                var doc = document;
                var headerFooterFont = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.NORMAL, new BaseColor(0, 0, 255));

                PdfPTable footerTbl = new PdfPTable(1);
                footerTbl.TotalWidth = doc.PageSize.Width;
                footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;
                Paragraph para = new Paragraph("AspxCommerce Copyright © 2011", headerFooterFont);
                PdfPCell cell = new PdfPCell(para);
                cell = new PdfPCell(para);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingRight = 15;
                footerTbl.AddCell(cell);
                footerTbl.WriteSelectedRows(0, -1, 0, (doc.BottomMargin), writer.DirectContent);
            }
        }

        [DataContract]
        [Serializable]
        public class OrderDetailsData
        {
            [DataMember(Name = "orderDate")]
            public string OrderDate { get; set; }

            [DataMember(Name = "storeName")]
            public string StoreName { get; set; }

            [DataMember(Name = "storeDescription")]
            public string StoreDescription { get; set; }

            [DataMember(Name = "paymentGatewayType")]
            public string PaymentGatewayType { get; set; }

            [DataMember(Name = "paymentMethod")]
            public string PaymentMethod { get; set; }

            [DataMember(Name = "billingAddress")]
            public string BillingAddress { get; set; }

            [DataMember(Name = "itemName")]
            public string ItemName { get; set; }

            [DataMember(Name = "shippingMethodName")]
            public string ShippingMethodName { get; set; }

            [DataMember(Name = "shippingAddress")]
            public string ShippingAddress { get; set; }

            [DataMember(Name = "shippingRate")]
            public string ShippingRate { get; set; }

            [DataMember(Name = "price")]
            public string Price { get; set; }

            [DataMember(Name = "quantity")]
            public string Quantity { get; set; }

            [DataMember(Name = "subTotal")]
            public string SubTotal { get; set; }

        }

        [DataContract]
        [Serializable]
        public class InvoiceDetailDataTableInfo
        {
            [DataMember(Name = "InvoiceNo")]
            public string InvoiceNo { get; set; }

            [DataMember(Name = "InvoiceDate")]
            public string InvoiceDate { get; set; }

            [DataMember(Name = "StoreName")]
            public string StoreName { get; set; }

            [DataMember(Name = "StoreDescription")]
            public string StoreDescription { get; set; }

            [DataMember(Name = "CustomerName")]
            public string CustomerName { get; set; }

            [DataMember(Name = "CustomerEmail")]
            public string CustomerEmail { get; set; }

            [DataMember(Name = "OrderId")]
            public string OrderId { get; set; }

            [DataMember(Name = "OrderDate")]
            public string OrderDate { get; set; }

            [DataMember(Name = "Status")]
            public string Status { get; set; }

            [DataMember(Name = "PaymentMethod")]
            public string PaymentMethod { get; set; }

            [DataMember(Name = "ShippingMethod")]
            public string ShippingMethod { get; set; }

            [DataMember(Name = "ShippingAddress")]
            public string ShippingAddress { get; set; }

            [DataMember(Name = "BillingAddress")]
            public string BillingAddress { get; set; }
            [DataMember(Name = "ItemName")]
            public string ItemName { get; set; }

            [DataMember(Name = "ShippingMethodName")]
            public string ShippingMethodName { get; set; }

            [DataMember(Name = "ShippingAddressDetail")]
            public string ShippingAddressDetail { get; set; }

            [DataMember(Name = "Price")]
            public string Price { get; set; }

            [DataMember(Name = "Quantity")]
            public string Quantity { get; set; }

            [DataMember(Name = "SubTotal")]
            public string SubTotal { get; set; }

        }
    }
}
