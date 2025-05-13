using System;
using UnityEngine;

[Serializable]
public class Save
{
    // 여기 저장할 것들을 넣으면 된다.
    
    public string ToJson() => JsonUtility.ToJson(this);
    public void LoadFromJson(string json) => JsonUtility.FromJsonOverwrite(json, this);
}
