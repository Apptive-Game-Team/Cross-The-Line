using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "TextContentData", menuName = "Contents/TextContentData", order = int.MaxValue)]
public class MailTextDataSO : ScriptableObject
{
    [ReadOnly(true)] public string Sender;
    [ReadOnly(true)] public string Receiver;
    [ReadOnly(true)] public string Title;

    [TextArea(12, 20)]
    [ReadOnly(true)] public string Content; // runtime에 수정 방지
}
