namespace Vem.MyDev.SqlServer.SqlKataExtensions.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassAliasAttribute : Attribute
    {
        public ClassAliasAttribute(string alias, string nameReference)
        {
            Alias = alias;
            NameRef = nameReference;
        }

        public string Alias { get; set; }

        public string NameRef { get; set; }
    }
}
