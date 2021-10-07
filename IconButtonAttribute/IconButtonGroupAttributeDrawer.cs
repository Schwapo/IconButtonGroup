using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class IconButtonGroupAttributeDrawer : OdinGroupDrawer<IconButtonGroupAttribute>
{
	protected override void DrawPropertyLayout(GUIContent label)
	{
		EditorGUILayout.BeginHorizontal();

        switch (Attribute.Alignment)
        {
            case IconButtonGroupAlignment.Right:
            case IconButtonGroupAlignment.Center:
                GUILayout.FlexibleSpace();
                break;
        }

        for (var i = 0; i < Property.Children.Count; i++)
		{
			var style = i != 0 ? ((i != Property.Children.Count - 1) 
				? new GUIStyle("AppCommandMid") : new GUIStyle("AppCommandRight")) 
				: new GUIStyle("AppCommandLeft");

			style.imagePosition = ImagePosition.ImageLeft;

			var child = Property.Children[i];
			child.Context.GetGlobal("IconButtonStyle", SirenixGUIStyles.Button).Value = style;
			child.Draw(child.Label);
		}

        switch (Attribute.Alignment)
        {
            case IconButtonGroupAlignment.Left:
            case IconButtonGroupAlignment.Center:
                GUILayout.FlexibleSpace();
                break;
        }

		EditorGUILayout.EndHorizontal();
	}
}
