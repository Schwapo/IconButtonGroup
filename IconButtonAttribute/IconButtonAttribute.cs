using Sirenix.OdinInspector;

public class IconButtonAttribute : ShowInInspectorAttribute
{
    public string Icon;
    public string Tooltip;
    public int Height;
    public bool Stretch;
    public int VerticalPadding = 3;
    public int HorizontalPadding = 3;
    
    private string label;
    public string Label
    {
        get => label;
        set
        {
            label = value;
            LabelHasValue = true;
        }
    }

    public bool LabelHasValue;

    public IconButtonAttribute(string icon) => Icon = icon;
}
