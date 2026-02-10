using UnityEngine;

namespace _2510.SimpleMeshOutline.Editor
{
    public class InlineToggleAttribute : PropertyAttribute
    {
        public readonly string ToggleFieldName;
        public InlineToggleAttribute(string toggleFieldName)
        {
            ToggleFieldName = toggleFieldName;
        }
    }
}