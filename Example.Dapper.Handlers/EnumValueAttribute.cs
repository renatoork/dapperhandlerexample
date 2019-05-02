using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Dapper.Handlers
{
    public class EnumValueAttribute : Attribute
    {

        public string Value { get; }
        
        public EnumValueAttribute(string value)
        {
            this.Value = value;
        }


    }
}
