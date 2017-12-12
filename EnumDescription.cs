using System;
using System.Collections.Generic;
using System.Text;

namespace ShebaConverter
{
    public class EnumDescription : System.Attribute
    {
        public string Text;

        public EnumDescription(string text)
        {
            this.Text = text;
        }
    }
    public class EnumDescriptionList : System.Attribute
    {
        public string Text;

        public EnumDescriptionList(string text)
        {
            this.Text = text;
        }
    }
}
