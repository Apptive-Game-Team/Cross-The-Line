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
    
    public void Init(int _id, string _message, int _w, int _minStage, int _bad, int _secret)
    {
        id = _id;
        message = _message;
        w = _w;
        minStage = _minStage;
        minStatus = new Status(_bad, _secret);
    }
    
    [Serializable]
    public struct Status
    {
        public Status(int _bad, int _secret) { Bad = _bad; Secret = _secret; }
        public int Bad, Secret;
    }
}