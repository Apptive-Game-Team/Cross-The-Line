using TMPro;
using UnityEngine;

public class UIContent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _contentText;

    public void SetContent(MailTextDataSO mail)
    {
        Debug.Log("Set Content");
        _headerText.text = $"보낸 사람 : {mail.Sender}\n" +
            $"받는 사람 : {mail.Receiver}\n" +
            $"제목 : {mail.Title}";

        _contentText.text = mail.Content;
    }
}
