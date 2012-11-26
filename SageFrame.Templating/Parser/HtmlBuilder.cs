using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Templating.xmlparser;

namespace SageFrame.Templating
{
    public class HtmlBuilder
    {
        public static string GeneratePlaceholderStart(XmlTag placeholder)
        {
            //1.Check for classes like no-wrap and no-main
            //2.Check for wrap-inner and main-inner classes
            //3.If Wrap-inner is not defined simply create a wrap class and a id from the key
            //4.Check for main inner and other wrappers
            //5.If we are using special markup tags this is the place to begin the markups
            //6.Generate main-inner wrappers if present

            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("<div id=\"id-{0}\" class=\"sf{1}\">", Utils.GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME), Utils.GetAttributeValueByName(placeholder, XmlAttributeTypes.NAME)));
            return sb.ToString();
        }


        public static string GeneratePlaceholderEnd()
        {
            //1.Check the above conditions and generate closing divs
            return "";
        }
        public static string GetMiddleDefaultMarkup()
        {
            return "";
        }

        public static string GenerateMiddleBlockEnd()
        {
            return "";
        }

        public static string GetMiddleWrappersBegin()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='sfBodyContent' class='sfOuterwrapper sfCurve clearfix'><div class='sfInnerwrapper clearfix'>");
            return sb.ToString();
        }

        public static string GetTopBlockMarkupDefault()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='#sfHeader' class='sfOuterwrapper sfCurve'><div class='sfContainer sfCurve'>");
            sb.Append(AppendDroppableArea("top"));
            sb.Append("</div></div>");
            return sb.ToString();
        }
        public static string GetCommonWrapper(string position)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfInnerwrapper clearfix'><div class='sfWrapper sfCurve'>");
            sb.Append(AppendDroppableArea(position));
            sb.Append("</div></div>");

            return sb.ToString();
        }
        public static string GetBottomBlockMarkupDefault()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='#sfFooter' class='sfOuterwrapper sfCurve'><div class='sfContainer sfCurve'>");
            sb.Append(AppendDroppableArea("footer"));
            sb.Append("</div></div>");
            return sb.ToString();
        }
        #region LeftBlocks
        //Get the Left Blocks

        public static string GetLeftBegin(string Left)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='sfLeft' style='width:" + Left + "'><div class='sfContainer sfCurve'>");
            return sb.ToString();
        }

        public static string GetLeftEnd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div></div>");
            return sb.ToString();
        }

        public static string GetLeftTop(int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfLeftTop sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("lefttop",Mode));
            sb.Append("</div>");
            return sb.ToString();
        }
        public static string GetLeftBottom(int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfLeftBottom sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("leftbottom", Mode));
            sb.Append("</div>");
            return sb.ToString();
        }

        public static string GetLeftColsWrap(string LeftA, string LeftB, string Left,int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfCurve sfDouble clearfix'><div class='sfLeftA' style='width:" + LeftA + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("leftA", Mode));
            sb.Append("</div></div><div class='sfLeftB' style='width:" + LeftB + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("leftB", Mode));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }

        public static string GetLeftA(string LeftA,int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfSingle sfCurve clearfix'><div class='sfLeftA' style='width:" + LeftA + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("leftA", Mode));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }
        public static string GetLeftB(string LeftB,int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfSingle sfCurve clearfix'><div class='sfLeftB' style='width:" + LeftB + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("leftB", Mode));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }
        #endregion

        #region RightBlocks

        public static string GetRightBegin(string Right)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='sfRight' style='width:" + Right + "'><div class='sfContainer sfCurve'>");
            return sb.ToString();
        }

        public static string GetRightEnd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div></div>");
            return sb.ToString();
        }

        public static string GetRightTop(int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfRightTop sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("righttop", Mode));
            sb.Append("</div>");
            return sb.ToString();
        }
        public static string GetRightBottom(int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfRightBottom sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("rightBottom", Mode));
            sb.Append("</div>");
            return sb.ToString();
        }

        public static string GetRightColsWrap(string RightA, string RightB, string Right,int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfDouble sfCurve clearfix'><div  class='sfRightA' style='width:" + RightA + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("rightA", Mode));
            sb.Append("</div></div><div  class='sfRightB' style='width:" + RightB + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("rightB", Mode));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }

        public static string GetRightA(string RightA,int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfCurve sfSingle clearfix'><div class='sfRightA' style='width:" + RightA + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("rightA", Mode));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }
        public static string GetRightB(string RightB,int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfColswrap sfSingle sfCurve'><div  class='sfRightB' style='width:" + RightB + "'><div class='sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("rightB", Mode));
            sb.Append("</div></div></div>");
            return sb.ToString();
        }
        #endregion

        #region MiddleBlocks
        public static string GetMiddleDefaultBlock(string Center,int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMainContent sfCurve'><div class='sfMiddleMainCurrent sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("middlemaincurrent", Mode));
            sb.Append("</div></div>");
            return sb.ToString();
        }
        public static string GetMiddleTopContent(int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMiddleTopContent sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("middletop", Mode));
            sb.Append("</div>");
            return sb.ToString();
        }
        public static string GetMiddleWrapperBegin(string Center)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='sfMainWrapper'  style='width:" + Center + "'><div class='sfContainer sfCurve'>");
            return sb.ToString();
        }
        public static string GetMiddleWrapperEnd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div></div>");
            return sb.ToString();
        }
        public static string GetMiddleBottomContent(int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div  class='sfMiddleBottomContent sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("middlebottom", Mode));
            sb.Append("</div>");
            return sb.ToString();
        }
        public static string GetMiddleMainContentBegin()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMainContent sfCurve'>");
            return sb.ToString();
        }
        public static string GetMiddleMainContentEnd()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div>");
            return sb.ToString();
        }
        public static string EndSingleDiv()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("</div>");
            return sb.ToString();
        }
        public static string GetMiddleMainTop(int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMiddleMainTop sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("middlemaintop", Mode));
            sb.Append("</div>");
            return sb.ToString();
        }
        public static string GetMiddleMainCurrent(int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMiddleMainCurrent sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("middlemaincurrent", Mode));
            sb.Append("</div>");
            return sb.ToString();
        }
        public static string GetMiddleMainBottom(int Mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfMiddleMainBottom sfWrapper sfCurve'>");
            sb.Append(AddPlaceholder("middlemainbottom", Mode));
            sb.Append("</div>");
            return sb.ToString();
        }

        public static string GetClearFix()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div style='clear:both'></div>");
            return sb.ToString();
        }

        #endregion
        public static string GenerateBlockWrappersEnd(List<int> wrapperdepth)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= wrapperdepth[0]; i++)
            {
                sb.Append("</div>");
            }
            if (wrapperdepth[0] == 0)
            {
                sb.Append("</div>");
            }
            for (int i = 1; i <= wrapperdepth[1]; i++)
            {
                sb.Append("</div>");
            }
            return sb.ToString();

        }

        public static string AppendDroppableArea(string position)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='sfPosition'>" + position + "</div>");
           // if (_Mode == 1)
                sb.Append("<div class='sfblocks'></div>");
            return sb.ToString();
        }

        public static string AddPlaceholder(string position, int _Mode)
        {
            StringBuilder sb = new StringBuilder();
            switch (_Mode)
            {
                case 0:
                    sb.Append("<div class='sfPosition'>" + position + "</div>");
                    break;
                case 1:
                    sb.Append("<div class='sfPosition'>" + position + "</div>");
                    sb.Append("<div class='sfblocks'></div>");
                    break;
                case 2:
                    string PlaceHolderID = string.Format("pch_{0}",position.ToLower()); ;
                    sb.Append("<asp:PlaceHolder ID='" + PlaceHolderID + "' runat='server'></asp:Placeholder>");
                    break;
            }

            return sb.ToString();
        }

        public static string AppendPlaceHolder(string position)
        {
            string PlaceHolderID = "pch_" + position;
            StringBuilder sb = new StringBuilder();
            sb.Append("<asp:PlaceHolder ID=" + PlaceHolderID + " runat=\"server\">");
            return sb.ToString();
        }

        public static string GenerateMiddleBlockStart(XmlTag middleBlock, List<CustomWrapper> lstWrappers, int Mode)
        {
          
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(middleBlock.LSTChildNodes.Count == 0 ? HtmlBuilder.GetMiddleWrapperBegin("100%") + HtmlBuilder.GetMiddleDefaultBlock("100%",Mode) + HtmlBuilder.GetMiddleWrapperEnd() : BlockBuilder.GetMiddleCustomMarkup(middleBlock,lstWrappers,Mode));
                return sb.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
