using System;
using UnityEngine;

//[CreateAssetMenu(fileName="ContentData", menuName = "Content Data", order = int.MaxValue)]
public class ContentDataSO : ScriptableObject
{
    // id | �ؽ�Ʈ | ����ġ | �ּ� ���� �������� | �ּ� �������ͽ�
    public int id;
    public string message;
    public int w;
    public int minStage;
    public Status minStatus;
}

[Serializable]
public struct Status
{
    public Status(int _bad, int _secret) { Bad = _bad; Secret = _secret; }
    public int Bad, Secret;
}
