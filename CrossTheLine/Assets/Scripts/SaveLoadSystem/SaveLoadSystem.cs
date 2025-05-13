using UnityEngine;

[CreateAssetMenu(menuName = "Scripts/SaveLoadSystem/SaveLoadSystem")]
public class SaveLoadSystem : ScriptableObject
{
    public string SaveFileName = "save.ctr";
    public string BackupSaveFileName = "save.ctr.bak";
    public Save SaveData = new Save();

    public void SaveDataToDisk()
    {
        if (FileManager.MoveFile(SaveFileName, BackupSaveFileName))
        {
            if (FileManager.WriteToFile(SaveFileName, SaveData.ToJson()))
            {
                Debug.Log($"Save successful {SaveFileName}");
            }
        }
    }

    public bool LoadSaveDataFromDisk()
    {
        if (FileManager.LoadFromFile(SaveFileName, out var json))
        {
            SaveData.LoadFromJson(json);
            return true;
        }

        return false;
    }

    public void WriteEmptySaveFile()
    {
        FileManager.WriteToFile(SaveFileName, "");
    }

    public void SetNewGameData()
    {
        FileManager.WriteToFile(SaveFileName, "");
        SaveDataToDisk();
    }
}
