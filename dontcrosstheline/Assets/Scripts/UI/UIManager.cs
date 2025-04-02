using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MailTextDataSO[] _textContents = default;
    [SerializeField] private UIScriptableTextManager _textManager = default;
    [SerializeField] private UIContent _content = default;

    private void OnEnable()
    {
        _textManager.ViewMailAction += SetContent;
    }

    private void SetContent(int id)
    {
        _content.SetContent(_textContents[id]);
    }
}
