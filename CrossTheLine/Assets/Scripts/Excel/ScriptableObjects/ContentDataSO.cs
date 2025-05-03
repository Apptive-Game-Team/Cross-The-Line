using System;
using UnityEngine;

//[CreateAssetMenu(fileName="ContentData", menuName = "Content Data", order = int.MaxValue)]
public class ContentDataSO : ScriptableObject
{
    // id | 텍스트 | 가중치 | 최소 등장 스테이지 | 최소 스테이터스
    public int id;
    public string message;
    public int w;
    public int minStage;
    public Status minStatus;
    public Sprite image;
    
    public void Init(int id, string message, int w, int minStage, int bad, int secret, Sprite image)
    {
        this.id = id;
        this.message = message;
        this.w = w;
        this.minStage = minStage;
        this.minStatus = new Status(bad, secret);
        this.image = image;
    }
    
    [Serializable]
    public struct Status
    {
        public Status(int bad, int secret) { Bad = bad; Secret = secret; }
        public int Bad, Secret;
    }
}