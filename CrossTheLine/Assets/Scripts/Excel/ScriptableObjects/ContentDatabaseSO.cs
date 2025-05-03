using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContentData", menuName = "Content Database", order = int.MaxValue)]
public class ContentDatabaseSO : ScriptableObject
{
    public List<ContentDataSO> datas;
}
