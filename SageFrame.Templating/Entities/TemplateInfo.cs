using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SageFrame.Templating
{
    public class TemplateInfo
    {
        public string TemplateName { get; set; }
        public string Path { get; set; }
        public string ThumbImage { get; set; }
        public bool IsActive { get; set; }
        public PresetInfo DefaultPreset { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }

        public TemplateInfo() { }
        public TemplateInfo(string _TemplateName)
        {
            this.TemplateName = _TemplateName;
        }
        public string TemplateSeoName
        {
            get { return (TemplateName.Replace(' ', '_')); }            
        }
        public TemplateInfo(string _TemplateName, string _Path, string _ThumbImage,bool _IsActive)
        {
            this.TemplateName = _TemplateName;
            this.Path = _Path;
            this.ThumbImage = _ThumbImage;
            this.IsActive = _IsActive;
        }
    }
}
