using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SageFrame.Templating.xmlparser;

namespace SageFrame.Templating
{
    public class BlockBuilder
    {
        public static string GetMiddleCustomMarkup(XmlTag middleBlock,List<CustomWrapper> lstWrapper,int Mode)
        {
            List<KeyValue> widths = Calculator.CalculateMiddleBlockWidth(middleBlock);
            StringBuilder sb = new StringBuilder();
            sb.Append(HtmlBuilder.GetMiddleWrappersBegin());
            if (Decide.HasBlock(Placeholders.FULLTOPSPAN, middleBlock))
            {
                sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.FULLTOPSPAN, middleBlock,lstWrapper,Mode));
            }
            //Check for Left Blocks
            foreach (CustomWrapper start in lstWrapper)
            {
                if (start.Type == "placeholder" && start.Start == "left")
                {
                    string style = "";
                    for (int i = 1; i <= start.Depth; i++)
                    {
                        if (i == 1)
                        {
                            style = start.Depth > 1 ? string.Format("sfBlockwrap {0}", start.Class) : string.Format("sfBlockwrap {0} clearfix", start.Class);
                            sb.Append("<div class='" + style + "'>");
                        }
                        else
                        {
                            style = start.Depth == i ? string.Format("sfBlockwrap {0} sf{1} clearfix", start.Class, i) : string.Format("sfBlockwrap {0} sf{1}", start.Class, i);
                            sb.Append("<div class='" + style + "'>");
                        }
                    }

                }
            }
            if (Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) || Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock) || (Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock)))
            {
                if (Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock))
                {
                    if ((Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock)))
                    {
                        //Every block is present on the left side
                        sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                        sb.Append(HtmlBuilder.GetLeftTop(Mode));
                        sb.Append(HtmlBuilder.GetLeftColsWrap(widths[1].Value, widths[2].Value, widths[0].Value,Mode));
                        sb.Append(HtmlBuilder.GetLeftBottom(Mode));
                        sb.Append(HtmlBuilder.GetLeftEnd());
                    }
                    else if (Decide.HasBlock(Placeholders.LEFTA, middleBlock) && !Decide.HasBlock(Placeholders.LEFTB, middleBlock))
                    {
                        sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                        sb.Append(HtmlBuilder.GetLeftTop(Mode));
                        sb.Append(HtmlBuilder.GetLeftA(widths[1].Value, Mode));
                        sb.Append(HtmlBuilder.GetLeftBottom(Mode));
                        sb.Append(HtmlBuilder.GetLeftEnd());
                    }
                    else if (!Decide.HasBlock(Placeholders.LEFTA, middleBlock) && Decide.HasBlock(Placeholders.LEFTB, middleBlock))
                    {
                        sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                        sb.Append(HtmlBuilder.GetLeftTop(Mode));
                        sb.Append(HtmlBuilder.GetLeftB(widths[2].Value, Mode));
                        sb.Append(HtmlBuilder.GetLeftBottom(Mode));
                        sb.Append(HtmlBuilder.GetLeftEnd());
                    }
                }
                else
                {
                    //Left Content Mass Blocks are not present
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftColsWrap(widths[1].Value, widths[2].Value, widths[0].Value, Mode));
                    sb.Append(HtmlBuilder.GetLeftEnd());
                }

            }
            else if (Decide.HasBlock(Placeholders.LEFTA, middleBlock))
            {
                if (Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock))
                {
                    //Every block is present on the left side
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftTop(Mode));
                    sb.Append(HtmlBuilder.GetLeftA(widths[1].Value, Mode));
                    sb.Append(HtmlBuilder.GetLeftBottom(Mode));
                    sb.Append(HtmlBuilder.GetLeftEnd());
                }
                else
                {
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftA(widths[1].Value, Mode));
                    sb.Append(HtmlBuilder.GetLeftEnd());
                }
            }
            else if (Decide.HasBlock(Placeholders.LEFTB, middleBlock))
            {
                if (Decide.HasBlock(Placeholders.LEFTTOP, middleBlock) && Decide.HasBlock(Placeholders.LEFTBOTTOM, middleBlock))
                {
                    //Every block is present on the left side
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftTop(Mode));
                    sb.Append(HtmlBuilder.GetLeftB(widths[2].Value, Mode));
                    sb.Append(HtmlBuilder.GetLeftBottom(Mode));
                    sb.Append(HtmlBuilder.GetLeftEnd());
                }
                else
                {
                    sb.Append(HtmlBuilder.GetLeftBegin(widths[0].Value));
                    sb.Append(HtmlBuilder.GetLeftB(widths[2].Value, Mode));
                    sb.Append(HtmlBuilder.GetLeftEnd());
                }
            }

            foreach (CustomWrapper start in lstWrapper)
            {
                if (start.Type == "placeholder" && start.End == "left")
                {

                    for (int i = 1; i <= start.Depth; i++)
                    {
                        sb.Append("</div>");
                    }

                }
            }


            //Check for Right Blocks
            foreach (CustomWrapper start in lstWrapper)
            {
                if (start.Type == "placeholder" && start.Start == "right")
                {
                    string style = "";
                    for (int i = 1; i <= start.Depth; i++)
                    {
                        if (i == 1)
                        {
                            style = start.Depth > 1 ? string.Format("sfBlockwrap {0}", start.Class) : string.Format("sfBlockwrap {0} clearfix", start.Class);
                            sb.Append("<div class='" + style + "'>");
                        }
                        else
                        {
                            style = start.Depth == i ? string.Format("sfBlockwrap {0} sf{1} clearfix", start.Class, i) : string.Format("sfBlockwrap {0} sf{1}", start.Class, i);
                            sb.Append("<div class='" + style + "'>");
                        }
                    }

                }
            }
            if (Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) || Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock) || (Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock)))
            {
                if (Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock))
                {
                    if (Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
                    {
                        //Every block is present on the left side
                        sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                        sb.Append(HtmlBuilder.GetRightTop(Mode));
                        sb.Append(HtmlBuilder.GetRightColsWrap(widths[4].Value, widths[5].Value, widths[3].Value, Mode));
                        sb.Append(HtmlBuilder.GetRightBottom(Mode));
                        sb.Append(HtmlBuilder.GetRightEnd());
                    }
                    else if (Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && !Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
                    {
                        sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                        sb.Append(HtmlBuilder.GetRightTop(Mode));
                        sb.Append(HtmlBuilder.GetRightA(widths[4].Value,Mode));
                        sb.Append(HtmlBuilder.GetRightBottom(Mode));
                        sb.Append(HtmlBuilder.GetRightEnd());
                    }
                    else if (!Decide.HasBlock(Placeholders.RIGHTA, middleBlock) && Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
                    {
                        sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                        sb.Append(HtmlBuilder.GetRightTop(Mode));
                        sb.Append(HtmlBuilder.GetRightB(widths[5].Value, Mode));
                        sb.Append(HtmlBuilder.GetRightBottom(Mode));
                        sb.Append(HtmlBuilder.GetRightEnd());
                    }

                }
                else
                {
                    //Left Content Mass Blocks are not present
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetRightColsWrap(widths[4].Value, widths[5].Value, widths[3].Value, Mode));
                    sb.Append(HtmlBuilder.GetRightEnd());
                }

            }
            else if (Decide.HasBlock(Placeholders.RIGHTA, middleBlock))
            {

                if (Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock))
                {
                    //Every block is present on the Right side
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetRightTop(Mode));
                    sb.Append(HtmlBuilder.GetRightA(widths[4].Value, Mode));
                    sb.Append(HtmlBuilder.GetRightBottom(Mode));
                    sb.Append(HtmlBuilder.GetRightEnd());
                }
                else
                {
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetRightA(widths[4].Value, Mode));
                    sb.Append(HtmlBuilder.GetRightEnd());
                }
            }
            else if (Decide.HasBlock(Placeholders.RIGHTB, middleBlock))
            {
                if (Decide.HasBlock(Placeholders.RIGHTTOP, middleBlock) && Decide.HasBlock(Placeholders.RIGHTBOTTOM, middleBlock))
                {
                    //Every block is present on the Right side
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetRightTop(Mode));
                    sb.Append(HtmlBuilder.GetRightB(widths[5].Value, Mode));
                    sb.Append(HtmlBuilder.GetRightBottom(Mode));
                    sb.Append(HtmlBuilder.GetRightEnd());
                }
                else
                {
                    sb.Append(HtmlBuilder.GetRightBegin(widths[3].Value));
                    sb.Append(HtmlBuilder.GetRightA(widths[5].Value, Mode));
                    sb.Append(HtmlBuilder.GetRightEnd());
                }
            }
            foreach (CustomWrapper start in lstWrapper)
            {
                if (start.Type == "placeholder" && start.End == "right")
                {

                    for (int i = 1; i <= start.Depth; i++)
                    {
                        sb.Append("</div>");
                    }
                }
            }


            //Create the default Middle Block
            foreach (CustomWrapper start in lstWrapper)
            {
                if (start.Type == "placeholder" && start.Start == "middle")
                {
                    string style = "";
                    for (int i = 1; i <= start.Depth; i++)
                    {
                        if (i == 1)
                        {
                            style = start.Depth > 1 ? string.Format("sfBlockwrap {0}", start.Class) : string.Format("sfBlockwrap {0} clearfix", start.Class);
                            sb.Append("<div class='" + style + "'>");
                        }
                        else
                        {
                            style = start.Depth == i ? string.Format("sfBlockwrap {0} sf{1} clearfix", start.Class, i) : string.Format("sfBlockwrap {0} sf{1}", start.Class, i);
                            sb.Append("<div class='" + style + "'>");
                        }
                    }

                }
            }
            sb.Append(HtmlBuilder.GetMiddleWrapperBegin(widths[6].Value));

            if (Decide.HasBlock(Placeholders.MIDDLETOP, middleBlock) || Decide.HasBlock(Placeholders.MIDDLEBOTTOM, middleBlock) || 1 == 1)
            {
                if (Decide.HasBlock(Placeholders.MIDDLETOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEBOTTOM, middleBlock))
                {
                    //Has outer top and bottom
                    sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLETOP, middleBlock,lstWrapper,Mode));
                    sb.Append(HtmlBuilder.GetMiddleMainContentBegin());
                    if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    sb.Append(HtmlBuilder.GetMiddleMainContentEnd());
                    sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEBOTTOM, middleBlock, lstWrapper, Mode));
                }
                else if (Decide.HasBlock(Placeholders.MIDDLETOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEBOTTOM, middleBlock))
                {
                    sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLETOP, middleBlock, lstWrapper, Mode));
                    sb.Append(HtmlBuilder.GetMiddleMainContentBegin());
                    if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    sb.Append(HtmlBuilder.GetMiddleMainContentEnd());
                }
                else if (!Decide.HasBlock(Placeholders.MIDDLETOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEBOTTOM, middleBlock))
                {

                    sb.Append(HtmlBuilder.GetMiddleMainContentBegin());
                    if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    sb.Append(HtmlBuilder.GetMiddleMainContentEnd());
                    sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEBOTTOM, middleBlock, lstWrapper, Mode));
                }
                else if (!Decide.HasBlock(Placeholders.MIDDLETOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEBOTTOM, middleBlock))
                {

                    sb.Append(HtmlBuilder.GetMiddleMainContentBegin());
                    if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINTOP, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));
                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINBOTTOM, middleBlock, lstWrapper, Mode));
                    }
                    else if (!Decide.HasBlock(Placeholders.MIDDLEMAINTOP, middleBlock) && !Decide.HasBlock(Placeholders.MIDDLEMAINBOTTOM, middleBlock))
                    {

                        sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.MIDDLEMAINCURRENT, middleBlock, lstWrapper, Mode));

                    }
                    sb.Append(HtmlBuilder.GetMiddleMainContentEnd());


                }
                foreach (CustomWrapper start in lstWrapper)
                {
                    if (start.Type == "placeholder" && start.End == "middle")
                    {

                        for (int i = 1; i <= start.Depth; i++)
                        {
                            sb.Append("</div>");
                        }
                    }
                }

            }
            else
            {
                //Generate Default Middle Block
                sb.Append(HtmlBuilder.GetMiddleDefaultBlock(widths[6].Value,Mode));
                foreach (CustomWrapper start in lstWrapper)
                {
                    if (start.Type == "placeholder" && start.End == "middle")
                    {

                        for (int i = 1; i <= start.Depth; i++)
                        {
                            sb.Append("</div>");
                        }
                    }
                }
            }

            sb.Append(HtmlBuilder.GetMiddleWrapperEnd());

            if (Decide.HasBlock(Placeholders.FULLBOTTOMSPAN, middleBlock))
            {
                sb.Append(BlockParser.ProcessMiddlePlaceholders(Placeholders.FULLBOTTOMSPAN, middleBlock,lstWrapper,Mode));
            }
            sb.Append(HtmlBuilder.EndSingleDiv());
            sb.Append(HtmlBuilder.EndSingleDiv());

            return sb.ToString();


        }

       
        public static string GetTopBlocks(XmlTag topBlock, List<CustomWrapper> lstWrappers, int Mode)
        {          
           
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(topBlock.LSTChildNodes.Count == 0 ? HtmlBuilder.GetTopBlockMarkupDefault() : BlockParser.ProcessPlaceholder(topBlock, lstWrappers,Mode));
                return sb.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static string GetBottomBlocks(XmlTag BottomBlock, List<CustomWrapper> lstWrappers, int Mode)
        {           
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append(BottomBlock.LSTChildNodes.Count == 0 ? HtmlBuilder.GetBottomBlockMarkupDefault() : BlockParser.ProcessPlaceholder(BottomBlock, lstWrappers,Mode));
                return sb.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


    }
}
