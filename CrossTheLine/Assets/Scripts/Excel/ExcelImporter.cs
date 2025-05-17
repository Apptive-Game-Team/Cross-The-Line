using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using CsvHelper;

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
        TextReader reader = new StringReader(csvFile.text);
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
        
        List<ContentDataSO> allItems = new();
        
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            if (csv.Read())
            {
                csv.ReadHeader(); // Header 제끼기
            }
            while (csv.Read())
            {
                if (csv.Context.Parser is not { Record: { Length: < 15 } }) continue;
                var cols = csv.Context.Parser.Record;

                int id = int.Parse(cols[0]);
                int previousId = int.Parse(cols[1]);
                string title = cols[2];
                string sender = cols[3];
                string content = cols[4];
            
                // 최소 요구 스테이터스
                int minJustice = int.Parse(cols[5]);
                int minGuilt = int.Parse(cols[6]);
                int minInfamy = int.Parse(cols[7]);
            
                // 수락시 보상 스테이터스
                int rewardJustice = int.Parse(cols[8]);
                int rewardGuilt = int.Parse(cols[9]);
                int rewardInfamy = int.Parse(cols[10]);
            
                string acceptMessage = cols[11];
                string rejectMessage = cols[12];
                Sprite image = Resources.Load<Sprite>($"{imageDir}{cols[13]}");

                string assetPath = $"{contentOutDir}/Item_{id:D3}.asset";
                ContentDataSO data = AssetDatabase.LoadAssetAtPath<ContentDataSO>(assetPath);
                if (data == null)
                {
                    data = ScriptableObject.CreateInstance<ContentDataSO>();
                    AssetDatabase.CreateAsset(data, assetPath);
                    // data.hideFlags = HideFlags.HideAndDontSave;
                }

                data.Init(id, previousId, title, sender, content, 
                    minJustice, minGuilt, minInfamy,
                    rewardJustice, rewardGuilt, rewardInfamy,
                    acceptMessage, rejectMessage, image);

                EditorUtility.SetDirty(data);
                allItems.Add(data);
            
                Debug.Log($"{data}");
            }
        }

        // 3. Content Database 생성 or 갱신
        ContentDatabaseSO db = AssetDatabase.LoadAssetAtPath<ContentDatabaseSO>(dbAssetPath);
        if (db == null)
        {
            db = ScriptableObject.CreateInstance<ContentDatabaseSO>();
            AssetDatabase.CreateAsset(db, dbAssetPath);
            // db.hideFlags = HideFlags.HideAndDontSave;
        }

        db.Datas = allItems;
        EditorUtility.SetDirty(db);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("CSV → Content Datas + Content Database 생성 완료!");
    }
}