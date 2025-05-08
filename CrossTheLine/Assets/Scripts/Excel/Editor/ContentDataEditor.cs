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
        datasProperty = serializedObject.FindProperty("Datas");
        datas = new ReorderableList(serializedObject, datasProperty, true, true, false, false)
        {
            drawHeaderCallback = rect =>
            {
                var headerRect = new Rect(rect.x, rect.y, rect.width, rect.height);
                EditorGUI.LabelField(headerRect, "Content Data List (Read-Only)", EditorStyles.boldLabel);
            },
            drawElementBackgroundCallback = (rect, index, isActive, isFocused) =>
            {
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
                string titleRaw = $"{content.Id}. {content.Title}";
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
                    float imageSize = 60f;

                    float textBlockX = rect.x + padding + imageSize + spacing;
                    float textBlockWidth = rect.width - (textBlockX - rect.x) - 10f;

                    EditorGUI.BeginDisabledGroup(true);

                    if (content.Image != null)
                    {
                        Rect imageRect = new Rect(rect.x + padding, y, imageSize, imageSize);
                        EditorGUI.DrawPreviewTexture(imageRect, content.Image.texture);
                    }

                    // 첫 번째 줄: Sender & Previous ID
                    float labelWidth = 50f;
                    float valueWidth = 100f;
                    
                    Rect senderLabel = new Rect(textBlockX, y, labelWidth, EditorGUIUtility.singleLineHeight);
                    Rect senderField = new Rect(senderLabel.xMax, y, valueWidth, EditorGUIUtility.singleLineHeight);
                    EditorGUI.LabelField(senderLabel, "보낸이:");
                    EditorGUI.TextField(senderField, GUIContent.none, content.Sender);

                    Rect prevIdLabel = new Rect(senderField.xMax + spacing, y, labelWidth, EditorGUIUtility.singleLineHeight);
                    Rect prevIdField = new Rect(prevIdLabel.xMax, y, valueWidth, EditorGUIUtility.singleLineHeight);
                    EditorGUI.LabelField(prevIdLabel, "선행ID:");
                    EditorGUI.TextField(prevIdField, GUIContent.none, content.PreviousId.ToString());

                    // 두 번째 줄: 최소 요구 스테이터스
                    y += EditorGUIUtility.singleLineHeight + 4f;
                    Rect minStatusLabel = new Rect(textBlockX, y, labelWidth + 30f, EditorGUIUtility.singleLineHeight);
                    Rect minStatusField = new Rect(minStatusLabel.xMax, y, textBlockWidth - (labelWidth + 30f), EditorGUIUtility.singleLineHeight);
                    EditorGUI.LabelField(minStatusLabel, "최소 요구:");
                    EditorGUI.TextField(minStatusField, GUIContent.none, content.MinStatus.ToString());

                    // 세 번째 줄: 보상 스테이터스
                    y += EditorGUIUtility.singleLineHeight + 4f;
                    Rect rewardStatusLabel = new Rect(textBlockX, y, labelWidth + 30f, EditorGUIUtility.singleLineHeight);
                    Rect rewardStatusField = new Rect(rewardStatusLabel.xMax, y, textBlockWidth - (labelWidth + 30f), EditorGUIUtility.singleLineHeight);
                    EditorGUI.LabelField(rewardStatusLabel, "수락 보상:");
                    EditorGUI.TextField(rewardStatusField, GUIContent.none, content.RewardStatus.ToString());

                    EditorGUI.EndDisabledGroup();
                }
            },
            elementHeightCallback = (index) =>
            {
                string key = $"Content_{index}_{((ContentDataSO)datasProperty.GetArrayElementAtIndex(index).objectReferenceValue).name}";
                if (!isFoldoutExpandedsByTitle.TryGetValue(key, out bool expanded) || !expanded)
                    return EditorGUIUtility.singleLineHeight + 6.0f;
                return EditorGUIUtility.singleLineHeight * 5;
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