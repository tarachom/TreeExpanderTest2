using Gtk;

namespace InterfaceGtk4;

[GObject.Subclass<Box>]
public partial class FormJournal : Box
{
    public ColumnView Grid { get; } = ColumnView.NewWithProperties([]);
    public virtual Gio.ListStore Store { get; } = Gio.ListStore.NewWithProperties([]);
    protected ScrolledWindow ScrollGrid { get; } = ScrolledWindow.New();

    partial void Initialize()
    {
        if (GetType().Namespace == "InterfaceGtk4") return;

        Console.WriteLine("FormJournal: " + GetType());

        SetOrientation(Orientation.Vertical);
        ScrollGrid.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
    }

    protected async Task Refresh() => await LoadRecords();
    protected virtual void GridModel() { }
    public virtual async Task LoadRecords() => await Task.FromResult(true);
    public virtual async Task SetValue() => await Task.FromResult(true);
    public virtual DirectoryHierarchicalRow LoadEmptyChildren() => DirectoryHierarchicalRow.New();
    public virtual async Task<List<DirectoryHierarchicalRow>> LoadChildren() => await Task.FromResult(new List<DirectoryHierarchicalRow>());


}