
public interface IRowSubclassJournal : IRowSubclass
{
    /// <summary>
    /// Колекція полів
    /// </summary>
    public Dictionary<string, string?> Fields { get; set; }
}
