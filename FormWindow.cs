
using Gtk;
using static Gtk.Orientation;

namespace Program;

[GObject.Subclass<Window>]
partial class FormWindow : Window
{
    Notebook notebook = Notebook.New();
    public static new FormWindow New() => NewWithProperties([]);

    partial void Initialize()
    {
        Title = "Window";
        SetDefaultSize(800, 800);

        var vBox = Box.New(Vertical, 0);

        //Button
        {
            Button button = Button.NewWithLabel("Add");
            button.MarginStart = button.MarginEnd = button.MarginTop = button.MarginBottom = 5;
            button.OnClicked += async (_, _) => await Add();

            Box hBox = Box.New(Horizontal, 0);
            hBox.Append(button);

            vBox.Append(hBox);
        }

        vBox.Append(notebook);

        Child = vBox;
    }

    async Task Add()
    {
        DirectoryTest directoryTest = DirectoryTest.New();
        await directoryTest.LoadRecords();

        notebook.AppendPage(directoryTest, Label.New("Name"));
    }
}