using Sirenix.OdinInspector;
using System;
using System.Diagnostics;

public enum IconButtonGroupAlignment
{
	Center,
	Left,
	Right,
}

[IncludeMyAttributes]
[ShowInInspector]
[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class IconButtonGroupAttribute : PropertyGroupAttribute
{
	private IconButtonGroupAlignment alignment;
	public IconButtonGroupAlignment Alignment
    {
		get => alignment;
		set
        {
			alignment = value;
			AlignmentHasValue = true;
        }
    }

	public bool AlignmentHasValue;

	public IconButtonGroupAttribute(string group = "_DefaultGroup", float order = 0f)
		: base(group, order) { }

	public IconButtonGroupAttribute(IconButtonGroupAlignment alignment)
		: base("_DefaultGroup", 0f) => Alignment = alignment;

	public IconButtonGroupAttribute(string group, IconButtonGroupAlignment alignment, float order = 0f)
		: base(group, order) => Alignment = alignment;

	protected override void CombineValuesWith(PropertyGroupAttribute other)
    {
        var otherAttribute = (IconButtonGroupAttribute)other;
        Alignment = otherAttribute.AlignmentHasValue ? otherAttribute.Alignment : Alignment;
    }
}
