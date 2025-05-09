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

            if (cols.Length < 13)
            {
                Debug.LogWarning($"잘못된 라인: {line}");
                continue;
            }
            
            // Data 다 읽은 경우 반복문 중단
            if (!int.TryParse(cols[0], out int id)) break;
            
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
                data.hideFlags = HideFlags.HideAndDontSave;
            }

            data.Init(id, previousId, title, sender, content, 
                minJustice, minGuilt, minInfamy,
                rewardJustice, rewardGuilt, rewardInfamy,
                acceptMessage, rejectMessage, image);

            EditorUtility.SetDirty(data);
            allItems.Add(data);
        }

        // 3. Content Database 생성 or 갱신
        ContentDatabaseSO db = AssetDatabase.LoadAssetAtPath<ContentDatabaseSO>(dbAssetPath);
        if (db == null)
        {
            db = ScriptableObject.CreateInstance<ContentDatabaseSO>();
            AssetDatabase.CreateAsset(db, dbAssetPath);
            db.hideFlags = HideFlags.HideAndDontSave;
        }

        db.Datas = allItems;
        EditorUtility.SetDirty(db);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("CSV → Content Datas + Content Database 생성 완료!");
    }
}