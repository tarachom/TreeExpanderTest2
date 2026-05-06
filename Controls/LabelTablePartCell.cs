using Gtk;

/// <summary>
/// Клітинка табличної частини - Текст
/// </summary>
[GObject.Subclass<Box>]
public partial class LabelTablePartCell : Box
{
    Box hBox = New(Orientation.Horizontal, 0);
    Label label = Label.New(null);

    partial void Initialize()
    {
        SetOrientation(Orientation.Vertical);

        hBox.Valign = Align.Center;
        //hBox.Vexpand = true;

        hBox.Append(label);

        Append(hBox);
        AddCssClass("base");
    }

    public static LabelTablePartCell New() => NewWithProperties([]);

    public static LabelTablePartCell NewWithString(string? text)
    {
        LabelTablePartCell lbl = NewWithProperties([]);
        lbl.SetText(text);

        return lbl;
    }

    public static LabelTablePartCell NewFromType(string type)
    {
        LabelTablePartCell lbl = NewWithProperties([]);
        lbl.SetType(type);

        return lbl;
    }

    public string Text
    {
        get => label.GetText();
        set => label.SetText(value);
    }

    public void SetText(string? text)
    {
        label.SetText(text ?? "");
    }

    public void SetText(object? text)
    {
        label.SetText(text?.ToString() ?? "");
    }

    public void SetType(string type)
    {
        if (type == "integer" || type == "numeric")
        {
            hBox.Halign = Align.End;
            AddCssClass("numeric");
        }
    }
}