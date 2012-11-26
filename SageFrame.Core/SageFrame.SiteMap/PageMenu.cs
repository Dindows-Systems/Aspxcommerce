using System;
namespace SageFrame.Web
{
    public class PageMenu
    {
        private string _SEOName;
        private string _PageName;
        private string _Description;

        public string SEOName
        {
            get { return _SEOName; }
            set { _SEOName = value; }
        }
        public string PageName
        {
            get { return _PageName; }
            set { _PageName = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
    }
}