using System;
using System.IO;
using UnityEngine;

public class FileManager
{
    public static bool WriteToFile(string fileName, string fileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            File.WriteAllText(fullPath, fileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }

    public static bool LoadFromFile(string fileName, out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);
        // 세이브 파일이 없으면 빈 파일로 생성
        if (!File.Exists(fullPath))
        {
            File.WriteAllText(fullPath, "");
        }

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read to {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }

    public static bool MoveFile(string fileName, string newFileName)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);
        var newFullPath = Path.Combine(Application.persistentDataPath, newFileName);

        try
        {
            // 기존의 백업 파일 삭제
            if (File.Exists(newFullPath))
            {
                File.Delete(newFullPath);
            }
            
            // 세이브 파일 없으면 false 반환
            if (!File.Exists(fullPath))
            {
                return false;
            }
            
            File.Move(fullPath, newFullPath);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to move file from {fullPath} to {newFullPath} with exception {e}");
            return false;
        }

        return true;
    }
}
