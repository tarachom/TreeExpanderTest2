using Gtk;

namespace InterfaceGtk4;

[GObject.Subclass<Box>]
public partial class Form : Box
{
    partial void Initialize()
    {
        if (GetType().Namespace == "InterfaceGtk4") return;
        
        SetOrientation(Orientation.Vertical);
    }
}