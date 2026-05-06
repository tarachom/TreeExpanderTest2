using Gtk;

namespace InterfaceGtk4;

[GObject.Subclass<DirectoryFormJournalBase>]
public partial class DirectoryFormJournalBaseTree : DirectoryFormJournalBase
{
    public override Gio.ListStore Store { get; } = Gio.ListStore.New(DirectoryHierarchicalRow.GetGType());
    TreeListModel? TreeList { get; set; } = null;

    partial void Initialize()
    {
        if (GetType().Namespace == "InterfaceGtk4") return;
        
        GridModel();
    }

    /// <summary>
    /// Встановлення моделі
    /// </summary>
    protected override void GridModel()
    {
        //Модель для дерева
        TreeList = TreeListModel.New(Store, false, false, CreateFunc);

        //Модель
        MultiSelection model = MultiSelection.New(TreeList);
        model.OnSelectionChanged += GridOnSelectionChanged;

        Grid.Model = model;
    }

    private Gio.ListModel? CreateFunc(GObject.Object item)
    {
        DirectoryHierarchicalRow itemRow = (DirectoryHierarchicalRow)item;
        Gio.ListStore? store = null;

        if (itemRow.IsLoading)
        {
            if (itemRow.Store != null)
            {
                GLib.Functions.IdleAdd(0, () =>
                {
                    async void f()
                    {
                        try
                        {
                            itemRow.Store.RemoveAll();

                            var list = await LoadChildren([]);
                            foreach (var item in list)
                                itemRow.Store.Append(item);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    f();
                    return false;
                });
            }
        }
        else
        {
            store = Gio.ListStore.New(DirectoryHierarchicalRow.GetGType());

            DirectoryHierarchicalRow itemEmpty = LoadEmptyChildren();
            itemEmpty.UniqueID = itemRow.UniqueID;
            itemEmpty.IsLoading = true;
            itemEmpty.Store = store;

            store.Append(itemEmpty);
        }

        return store;
    }
}