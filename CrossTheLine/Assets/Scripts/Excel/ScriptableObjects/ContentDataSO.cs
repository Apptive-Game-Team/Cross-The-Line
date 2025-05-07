using System;
using UnityEngine;

//[CreateAssetMenu(fileName="ContentData", menuName = "Content Data", order = int.MaxValue)]
public class ContentDataSO : ScriptableObject
{
    // id | 텍스트 | 가중치 | 최소 등장 스테이지 | 최소 스테이터스
    public int Id;
    public string Message;
    public int W;
    public int MinStage;
    public Status MinStatus;
    public Sprite Image;
    
    public void Init(int id, string message, int w, int minStage, int bad, int secret, Sprite image)
    {
        this.Id = id;
        this.Message = message;
        this.W = w;
        this.MinStage = minStage;
        this.MinStatus = new Status(bad, secret);
        this.Image = image;
    }
    
    [Serializable]
    public struct Status
    {
        public Status(int bad, int secret) { Bad = bad; Secret = secret; }
        public int Bad, Secret;
    }
}