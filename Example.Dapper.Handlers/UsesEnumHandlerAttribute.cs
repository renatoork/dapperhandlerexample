using System;

namespace Example.Dapper.Handlers
{
    public class UsesEnumHandlerAttribute : Attribute
    {
        public bool UseCharAtEnumItem { get; }

        public UsesEnumHandlerAttribute(bool useCharAtEnumItem = false)
        {
            this.UseCharAtEnumItem = useCharAtEnumItem;
        }
    }
}