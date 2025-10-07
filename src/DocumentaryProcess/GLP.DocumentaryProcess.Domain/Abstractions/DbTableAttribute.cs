namespace GLP.DocumentaryProcess.Domain.Abstractions;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class DbTableAttribute : Attribute
{
    public DbTableAttribute(string tableName) => TableName = tableName;
    public string TableName { get; }
}