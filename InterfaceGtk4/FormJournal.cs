using Gtk;

namespace InterfaceGtk4;

[GObject.Subclass<Form>]
public partial class FormJournal : Form
{
    public UniqueID? SelectPointerItem { get; set; } = null;
    public ColumnView Grid { get; } = ColumnView.NewWithProperties([]);
    public virtual Gio.ListStore Store { get; } = Gio.ListStore.NewWithProperties([]);
    protected ScrolledWindow ScrollGrid { get; } = ScrolledWindow.New();

    partial void Initialize()
    {
        if (GetType().Namespace == "InterfaceGtk4") return;

        ScrollGrid.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
    }

    protected virtual void GridOnSelectionChanged(SelectionModel sender, SelectionModel.SelectionChangedSignalArgs args)
    {
        Bitset selection = Grid.Model.GetSelection();

        //Коли виділений один рядок
        if (selection.GetMinimum() == selection.GetMaximum())
        {
            uint position = selection.GetMaximum();
            if (Store.GetObject(position) is IRowSubclassJournal row)
                SelectPointerItem = row.UniqueID;
        }
    }

    protected async ValueTask Refresh() => await LoadRecords();

    protected virtual void GridModel() { }

    public virtual async ValueTask LoadRecords() => await ValueTask.FromResult(true);

    public virtual async ValueTask SetValue() => await ValueTask.FromResult(true);

    public virtual DirectoryHierarchicalRow LoadEmptyChildren() => DirectoryHierarchicalRow.New();

    public virtual async ValueTask<List<DirectoryHierarchicalRow>> LoadChildren(UniqueID[] parents) => await ValueTask.FromResult(new List<DirectoryHierarchicalRow>());


}