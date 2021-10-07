using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ActionResolvers;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

[DrawerPriority(0, 0, 0.12)]
public class IconButtonAttributeDrawer : MethodDrawer
{
    private IconButtonAttribute attribute;
    private ActionResolver buttonActionResolver;
    private ValueResolver<string> buttonLabelResolver;
    private ValueResolver<Texture2D> iconResolver;
    
    private GUIStyle ButtonStyle
    {
        get
        {
            var style = Property.Context.GetGlobal("IconButtonStyle", SirenixGUIStyles.Button).Value;
            style.stretchWidth = attribute.Stretch;
            style.fixedHeight = attribute.Height;
            
            style.fixedWidth = attribute.LabelHasValue 
                && buttonLabelResolver.GetValue().IsNullOrWhitespace() ? attribute.Height : 0;

            style.padding = new RectOffset(
                attribute.HorizontalPadding, 
                attribute.HorizontalPadding, 
                attribute.VerticalPadding, 
                attribute.VerticalPadding);

            return style;
        }
    }

    protected override bool CanDrawMethodProperty(InspectorProperty property)
    {
        return property.GetAttribute<IconButtonAttribute>() != null;
    }

    protected override void Initialize()
    {
        attribute = Property.GetAttribute<IconButtonAttribute>();
        buttonActionResolver = ActionResolver.Get(Property, null);
        buttonLabelResolver = ValueResolver.GetForString(Property, attribute.Label);
        iconResolver = ValueResolver.Get<Texture2D>(Property, attribute.Icon);
    }

    protected override void DrawPropertyLayout(GUIContent label)                                   
    {
        ActionResolver.DrawErrors(buttonActionResolver);
        ValueResolver.DrawErrors(buttonLabelResolver, iconResolver);

        var buttonContent = new GUIContent(
            attribute.LabelHasValue ? buttonLabelResolver.GetValue() : label.text,
            iconResolver.GetValue(),
            attribute.Tooltip);

        if (GUILayout.Button(buttonContent, ButtonStyle))
        {
			InvokeButton();
        }
    }

    private void InvokeButton()
	{
        if (Property.ParentValueProperty != null)
        {
            Property.ParentValueProperty.RecordForUndo($"Clicked Button '{Property.NiceName}'", true);
        }

        if (buttonActionResolver != null)
        {
            buttonActionResolver.DoActionForAllSelectionIndices();
        }
	}
}
