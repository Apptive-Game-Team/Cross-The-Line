using UnityEngine;
using UnityEngine.Events;

public class UIScriptableTextManager : MonoBehaviour
{
    public UnityAction<int> ViewMailAction;

    public void ViewMailButton(int id)
    {
        ViewMailAction.Invoke(id);
    }
}
