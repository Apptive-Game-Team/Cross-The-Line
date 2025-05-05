using UnityEngine;

[CreateAssetMenu]
public class CardSO : ScriptableObject
{
    public int Abc;
    public int cardID;
    public string cardName;
    public CardSprite cardSprite;
    public string leftQuote;
    public string rightQuote;

    public void Left()
    {
        Debug.Log(cardName + " swipe Left");
    }
    
    public void Right()
    {
        Debug.Log(cardName + " swipe Right");
    }
}
