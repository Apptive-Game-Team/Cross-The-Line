using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ContentDatabaseSO))]
public class ContentDataEditor : Editor
{
    private SerializedProperty datasProperty;
    private ReorderableList datas;

    private readonly Dictionary<string, bool> isFoldoutExpandedsByTitle = new();

    private void OnEnable()
    {
        datasProperty = serializedObject.FindProperty("datas");
        datas = new ReorderableList(serializedObject, datasProperty, true, true, false, false)
        {
            drawHeaderCallback = rect =>
            {
                var headerRect = new Rect(rect.x, rect.y, rect.width, rect.height); // 위로 약간 띄우기
                EditorGUI.LabelField(headerRect, "Content Data List (Read-Only)", EditorStyles.boldLabel);
            },
            drawElementBackgroundCallback = (rect, index, isActive, isFocused) =>
            {
                // Disable highlight unless dragging
                if (DragAndDrop.activeControlID == 0)
                {
                    isActive = false;
                    isFocused = false;
                }
                if (Event.current.type == EventType.Repaint)
                {
                    GUIStyle style = (isActive || isFocused) ? "RL Element" : "RL Background";
                    style.Draw(rect, false, isActive, isFocused, false);
                }
            },
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = datasProperty.GetArrayElementAtIndex(index);
                if (element.objectReferenceValue == null) return;

                var content = (ContentDataSO)element.objectReferenceValue;
                string titleRaw = $"{content.id}. {content.message}";
                string title = titleRaw.Length > 40 ? titleRaw.Substring(0, 40) + "..." : titleRaw;
                string foldoutKey = $"Content_{index}_{content.name}";

                if (!isFoldoutExpandedsByTitle.ContainsKey(foldoutKey))
                    isFoldoutExpandedsByTitle[foldoutKey] = false;

                Rect foldoutRect = new Rect(rect.x + 15.0f, rect.y + 2.0f, rect.width - 20f, EditorGUIUtility.singleLineHeight);
                isFoldoutExpandedsByTitle[foldoutKey] = EditorGUI.Foldout(foldoutRect, isFoldoutExpandedsByTitle[foldoutKey], title, true);

                if (isFoldoutExpandedsByTitle[foldoutKey]) {
                    float y = rect.y + EditorGUIUtility.singleLineHeight + 4f;
                    float spacing = 5f;
                    float padding = 10f;
                    float imageSize = 40f;

                    float textBlockX = rect.x + padding + imageSize + spacing;
                    float textBlockWidth = rect.width - (textBlockX - rect.x) - 10f;

                    EditorGUI.BeginDisabledGroup(true);

                    if (content.image != null)
                    {
                        Rect imageRect = new Rect(rect.x + padding, y, imageSize, imageSize);
                        EditorGUI.DrawPreviewTexture(imageRect, content.image.texture);
                    }

                    // 첫 번째 줄: W / MS
                    float labelWidth = 25f;
                    float fieldWidth = 40f;

                    Rect wLabel = new Rect(textBlockX, y, labelWidth, EditorGUIUtility.singleLineHeight);
                    Rect wField = new Rect(wLabel.xMax, y, fieldWidth, EditorGUIUtility.singleLineHeight);
                    EditorGUI.LabelField(wLabel, "W:");
                    EditorGUI.TextField(wField, GUIContent.none, content.w.ToString());

                    Rect msLabel = new Rect(wField.xMax + spacing, y, labelWidth + 5f, EditorGUIUtility.singleLineHeight);
                    Rect msField = new Rect(msLabel.xMax, y, fieldWidth, EditorGUIUtility.singleLineHeight);
                    EditorGUI.LabelField(msLabel, "MS:");
                    EditorGUI.TextField(msField, GUIContent.none, content.minStage.ToString());

                    // 두 번째 줄: Status
                    y += EditorGUIUtility.singleLineHeight + 4f;
                    float statusLabelWidth = 50f;
                    float statusFieldWidth = textBlockWidth - statusLabelWidth;

                    Rect statusLabel = new Rect(textBlockX, y, statusLabelWidth, EditorGUIUtility.singleLineHeight);
                    Rect statusField = new Rect(statusLabel.xMax, y, statusFieldWidth, EditorGUIUtility.singleLineHeight);
                    EditorGUI.LabelField(statusLabel, "Status:");
                    EditorGUI.TextField(statusField, GUIContent.none, $"(Bad:{content.minStatus.Bad}, Secret:{content.minStatus.Secret})");

                    EditorGUI.EndDisabledGroup();
                }
            },
            elementHeightCallback = (index) =>
            {
                string key = $"Content_{index}_{((ContentDataSO)datasProperty.GetArrayElementAtIndex(index).objectReferenceValue).name}";
                if (!isFoldoutExpandedsByTitle.TryGetValue(key, out bool expanded) || !expanded)
                    return EditorGUIUtility.singleLineHeight + 6.0f;
                return EditorGUIUtility.singleLineHeight * 4;
            }
        };
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Space(20.0f);
        serializedObject.Update();
        
        datas.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
