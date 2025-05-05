using TMPro;
using UnityEngine;

public class UIContent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI contentText;

    public void SetContent(MailTextDataSO mail)
    {
        Debug.Log("Set Content");
        headerText.text = $"보낸 사람 : {mail.Sender}\n" +
            $"받는 사람 : {mail.Receiver}\n" +
            $"제목 : {mail.Title}";

        contentText.text = mail.Content;
    }
}
