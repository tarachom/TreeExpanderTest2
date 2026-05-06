using Gtk;

namespace InterfaceGtk4;

[GObject.Subclass<FormJournal>]
public partial class DirectoryFormJournalBase : FormJournal
{
    public override Gio.ListStore Store { get; } = Gio.ListStore.New(DirectoryRowJournal.GetGType());
    protected Box HBoxTop { get; } = Box.New(Orientation.Horizontal, 0);

    partial void Initialize()
    {
        if (GetType().Namespace == "InterfaceGtk4") return;

        Append(HBoxTop);

        CreateToolbar();
        GridModel();

        ScrollGrid.SetChild(Grid);
        ScrollGrid.Vexpand = ScrollGrid.Hexpand = true;

        Append(ScrollGrid);
    }

    public override async ValueTask SetValue()
    {
        await LoadRecords();
    }

    protected override void GridModel()
    {
        //Модель
        MultiSelection model = MultiSelection.New(Store);
        model.OnSelectionChanged += GridOnSelectionChanged;

        Grid.Model = model;
    }

    void CreateToolbar()
    {
        Button button = Button.NewWithLabel("Refresh");
        button.MarginStart = button.MarginEnd = 5;
        button.TooltipText = "Refresh";
        button.OnClicked += async (_, _) => await Refresh();
        HBoxTop.Append(button);
    }
}