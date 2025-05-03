using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExcelImporter : MonoBehaviour
{
    private const string csvPath = "Assets/Data/ContentTable";
    private const string imageDir = "Assets/Images/";
    private const string contentOutDir = "Assets/Data/Generated/Contents";
    private const string dbAssetPath = "Assets/Data/Generated/ContentDB.asset";

    [MenuItem("Tools/CSV → ScriptableObjects + Database")]
    public static void ConvertCsvAndGenerateDatabase()
    {
        // 1. CSV 읽기
        TextAsset csvFile = Resources.Load<TextAsset>(csvPath);
        if (csvFile == null)
        {
            Debug.LogError($"CSV 파일을 찾을 수 없습니다: Resources/{csvPath}.csv");
            return;
        }

        // 2. 출력 폴더 준비
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
                Debug.LogWarning($"잘못된 라인: {line}");
                continue;
            }
            
            // Data 다 읽은 경우 반복문 중단
            if (!int.TryParse(cols[0], out int id)) break;
            string message = cols[1];
            int w = int.Parse(cols[2]);
            int minStage = int.Parse(cols[3]);
            int bad = int.Parse(cols[4]);
            int secret = int.Parse(cols[5]);
            Sprite image = Resources.Load<Sprite>($"{imageDir}{cols[6]}");

            string assetPath = $"{contentOutDir}/Item_{id:D3}.asset";
            ContentDataSO data = AssetDatabase.LoadAssetAtPath<ContentDataSO>(assetPath);
            if (data == null)
            {
                data = ScriptableObject.CreateInstance<ContentDataSO>();
                AssetDatabase.CreateAsset(data, assetPath);
            }

            data.Init(id, message, w, minStage, bad, secret, image);

            EditorUtility.SetDirty(data);
            allItems.Add(data);
        }

        // 3. Content Database 생성 or 갱신
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
        Debug.Log("CSV → Content Datas + Content Database 생성 완료!");
    }
}
