
/// <summary>
/// Ієрархічний рядок
/// </summary>
[GObject.Subclass<GObject.Object>]
public partial class DirectoryHierarchicalRow : IRowSubclassJournal
{
    public static DirectoryHierarchicalRow New() => NewWithProperties([]);

    #region Динамічне довантаження

    /// <summary>
    /// Цей елемент для завантаження даних
    /// </summary>
    public bool IsLoading { get; set; }

    /// <summary>
    /// Сховище
    /// </summary>
    public Gio.ListStore? Store { get; set; } = null;

    #endregion

    /// <summary>
    /// Унікальний ідентифікатор
    /// </summary>
    public UniqueID UniqueID { get; set; } = UniqueID.NewEmpty();

    /// <summary>
    /// Колекція полів
    /// </summary>
    public Dictionary<string, string?> Fields { get; set; } = [];
}
