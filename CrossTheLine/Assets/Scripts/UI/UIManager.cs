using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MailTextDataSO[] textContents = default;
    [SerializeField] private UIScriptableTextManager textManager = default;
    [SerializeField] private UIContent content = default;

    private void OnEnable()
    {
        textManager.ViewMailAction += SetContent;
    }

    private void SetContent(int id)
    {
        content.SetContent(textContents[id]);
    }
}
