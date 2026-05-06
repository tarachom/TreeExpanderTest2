using Gtk;
using GdkPixbuf;

/// <summary>
/// Клітинка табличної частини - Іконка
/// </summary>
[GObject.Subclass<Box>]
public partial class ImageTablePartCell : Box
{
    Box hBox = New(Orientation.Horizontal, 0);
    Image img = Image.NewFromPixbuf(null);

    partial void Initialize()
    {
        SetOrientation(Orientation.Vertical);

        hBox.Valign = hBox.Halign = Align.Center;
        //hBox.Vexpand = true;
        hBox.Append(img);

        Append(hBox);
        AddCssClass("base");
    }

    public static ImageTablePartCell New() => NewWithProperties([]);

    public static ImageTablePartCell NewFromPixbuf(Pixbuf? pixbuf)
    {
        ImageTablePartCell img = NewWithProperties([]);
        img.SetImage(pixbuf);

        return img;
    }

    public void SetImage(Pixbuf? pixbuf)
    {
        img.SetFromPixbuf(pixbuf);
    }

    /// <summary>
    /// Видаляє із hBox віджет img і замість нього ставить Spinner.
    /// 
    /// Використовується для вітки дерева в яку повинні завантажитись дані.
    /// Після завантаження даних, ця вітка видаляється
    /// </summary>
    public void SetSpinner()
    {
        Widget? firstChild = hBox.GetFirstChild();
        if (firstChild != null) hBox.Remove(firstChild);

        Spinner spinner = Spinner.New();
        spinner.Spinning = true;

        hBox.Append(spinner);
    }
}