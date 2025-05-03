using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContentData", menuName = "Content Database", order = int.MaxValue)]
public class ContentDBSO : ScriptableObject
{
    public List<ContentDataSO> datas;
}
