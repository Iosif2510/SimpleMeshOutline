using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _2510.SimpleMeshOutline.Editor
{
    [CustomPropertyDrawer(typeof(InlineToggleAttribute))]
    public class InlineToggleDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var attr = (InlineToggleAttribute)attribute;
        
            // 부모 경로를 통해 형제(bool) 프로퍼티를 찾습니다.
            string path = property.propertyPath;
            int lastDot = path.LastIndexOf('.');
            string parentPath = lastDot != -1 ? path.Substring(0, lastDot + 1) : "";
            var toggleProp = property.serializedObject.FindProperty(parentPath + attr.ToggleFieldName);

            var container = new VisualElement { style = { flexDirection = FlexDirection.Row, marginBottom = 2 } };

            if (toggleProp != null && toggleProp.propertyType == SerializedPropertyType.Boolean)
            {
                // 1. 토글 생성 (라벨 없이)
                var toggleField = new PropertyField(toggleProp, "");
                toggleField.style.width = 20;
                toggleField.style.marginRight = 5;

                // 2. 실제 데이터 필드 생성
                var dataField = new PropertyField(property, property.displayName);
                dataField.style.flexGrow = 1;

                // 3. 활성화 상태 동기화 (초기값 및 변경 시)
                void UpdateEnabled(bool val) => dataField.SetEnabled(val);
                UpdateEnabled(toggleProp.boolValue);
            
                toggleField.RegisterValueChangeCallback(evt => UpdateEnabled(evt.changedProperty.boolValue));

                container.Add(toggleField);
                container.Add(dataField);
            }
            else
            {
                // 토글을 못 찾았을 경우 경고 문구와 함께 기본 필드 출력
                container.Add(new Label($"[Error: {attr.ToggleFieldName} Not Found] ") { style = { color = Color.red } });
                container.Add(new PropertyField(property));
            }

            return container;
        }
    }
}