
/// <summary>
/// Рядок для табличного списку журналу довідника
/// </summary>
[GObject.Subclass<GObject.Object>]
public partial class DirectoryRowJournal : IRowSubclassJournal
{
    public static DirectoryRowJournal New() => NewWithProperties([]);

    /// <summary>
    /// Унікальний ідентифікатор
    /// </summary>
    public UniqueID UniqueID { get; set; } = new();

    /// <summary>
    /// Помітка на видалення
    /// </summary>
    public bool DeletionLabel { get; set; } = false;

    /// <summary>
    /// Колекція полів
    /// </summary>
    public Dictionary<string, string?> Fields { get; set; } = [];
}