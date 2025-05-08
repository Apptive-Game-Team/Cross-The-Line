using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ContentData", menuName = "Content Database", order = -1)]
public class ContentDatabaseSO : ScriptableObject
{
    public List<ContentDataSO> Datas;
}
