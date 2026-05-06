using Gtk;
using InterfaceGtk4;

namespace Program;

[GObject.Subclass<DirectoryFormJournalBaseTree>]
partial class DirectoryTest : DirectoryFormJournalBaseTree
{
    partial void Initialize()
    {
        Console.WriteLine("DirectoryTest: " + GetType());

        AddColumn();
    }

    public static DirectoryTest New() => NewWithProperties([]);

    public override DirectoryHierarchicalRow LoadEmptyChildren()
    {
        DirectoryHierarchicalRow row = DirectoryHierarchicalRow.New();
        row.Fields.Add("Name", null);

        return row;
    }

    public override async Task<List<DirectoryHierarchicalRow>> LoadChildren()
    {
        List<DirectoryHierarchicalRow> list = [];

        for (int i = 0; i < 100; i++)
        {
            DirectoryHierarchicalRow row = DirectoryHierarchicalRow.New();
            row.Fields.Add("Name", "Name name");

            list.Add(row);
        }

        return list;
    }

    public override async Task LoadRecords()
    {
        Console.WriteLine("LoadRecords");

        Store.RemoveAll();

        for (int i = 0; i < 5000; i++)
        {
            DirectoryHierarchicalRow row = DirectoryHierarchicalRow.New();
            row.Fields.Add("Name", $"Name {i}");

            Store.Append(row);
        }
    }

    void AddColumn()
    {
        //TreeExpander and Image
        {
            SignalListItemFactory factory = SignalListItemFactory.New();
            factory.OnSetup += (_, args) =>
            {
                ListItem listItem = (ListItem)args.Object;
                TreeExpander expander = TreeExpander.New();
                expander.SetChild(ImageTablePartCell.New());
                listItem.SetChild(expander);
            };
            factory.OnBind += (_, args) =>
            {
                ListItem listItem = (ListItem)args.Object;
                TreeExpander? expander = (TreeExpander?)listItem.GetChild();
                TreeListRow? treeRow = (TreeListRow?)listItem.GetItem();
                if (expander != null && treeRow != null)
                {
                    expander.SetListRow(treeRow);
                    ImageTablePartCell? cell = (ImageTablePartCell?)expander.GetChild();
                    DirectoryHierarchicalRow? row = (DirectoryHierarchicalRow?)treeRow.Item;
                    if (cell != null && row != null)
                        if (row.IsLoading)
                            cell.SetSpinner();
                        else
                            cell.SetImage(null);
                }
            };
            ColumnViewColumn column = ColumnViewColumn.New("", factory);
            column.Resizable = true;
            Grid.AppendColumn(column);
        }

        //Name
        {
            SignalListItemFactory factory = SignalListItemFactory.New();
            factory.OnSetup += (_, args) =>
            {
                ListItem listItem = (ListItem)args.Object;
                listItem.Child = LabelTablePartCell.NewFromType("integer");
            };
            factory.OnBind += (_, args) =>
            {
                ListItem listItem = (ListItem)args.Object;
                TreeListRow? treeRow = (TreeListRow?)listItem.GetItem();
                if (treeRow != null)
                {
                    LabelTablePartCell? cell = (LabelTablePartCell?)listItem.Child;
                    DirectoryHierarchicalRow? row = (DirectoryHierarchicalRow?)treeRow.Item;
                    if (cell != null && row != null)
                        cell.SetText(row.Fields["Name"]);
                }
            };
            ColumnViewColumn column = ColumnViewColumn.New("Name", factory);
            column.Resizable = true;
            Grid.AppendColumn(column);
        }
    }
}