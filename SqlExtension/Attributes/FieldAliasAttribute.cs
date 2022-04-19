namespace Vem.MyDev.SqlServer.SqlKataExtensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAliasAttribute : Attribute
    {
        public FieldAliasAttribute(string alias, string asAttr)
        {
            Alias = alias;
            As = asAttr;
        }

        public string Alias { get; set; }

        public string As { get; set; }
    }
}
