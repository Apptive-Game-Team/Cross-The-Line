using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExcelImporter : MonoBehaviour
{
    private const string csvPath = "Assets/Data/ContentTable";
    private const string contentOutDir = "Assets/Data/Generated/Contents";
    private const string dbAssetPath = "Assets/Data/Generated/ContentDB.asset";

    [MenuItem("Tools/CSV �� ScriptableObjects + Database")]
    public static void ConvertCsvAndGenerateDatabase()
    {
        // 1. CSV �б�
        TextAsset csvFile = Resources.Load<TextAsset>(csvPath);
        if (csvFile == null)
        {
            Debug.LogError($"CSV ������ ã�� �� �����ϴ�: Resources/{csvPath}.csv");
            return;
        }

        // 2. ��� ���� �غ�
        if (!AssetDatabase.IsValidFolder(contentOutDir))
        {
            Directory.CreateDirectory(contentOutDir);
            AssetDatabase.Refresh();
        }

        string[] lines = csvFile.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        List<ContentDataSO> allItems = new();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] cols = line.Split(',');

            if (cols.Length < 4)
            {
                Debug.LogWarning($"�߸��� ����: {line}");
                continue;
            }

            int id = int.Parse(cols[0]);
            string message = cols[1];
            int w = int.Parse(cols[2]);
            int minStage = int.Parse(cols[3]);
            int bad = int.Parse(cols[4]);
            int secret = int.Parse(cols[5]);

            string assetPath = $"{contentOutDir}/Item_{id:D3}.asset";
            ContentDataSO item = AssetDatabase.LoadAssetAtPath<ContentDataSO>(assetPath);
            if (item == null)
            {
                item = ScriptableObject.CreateInstance<ContentDataSO>();
                AssetDatabase.CreateAsset(item, assetPath);
            }

            item.id = id;
            item.message = message;
            item.w = w;
            item.minStage = minStage;
            item.minStatus = new Status(bad, secret);

            EditorUtility.SetDirty(item);
            allItems.Add(item);
        }

        // 3. Content Database ���� or ����
        ContentDBSO db = AssetDatabase.LoadAssetAtPath<ContentDBSO>(dbAssetPath);
        if (db == null)
        {
            db = ScriptableObject.CreateInstance<ContentDBSO>();
            AssetDatabase.CreateAsset(db, dbAssetPath);
        }

        db.datas = allItems;
        EditorUtility.SetDirty(db);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("CSV �� ItemData + ItemDatabase ���� �Ϸ�!");
    }
}
