using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CardController : MonoBehaviour
{
    public CardSO card;
    public bool isMouseOver;
    
    private BoxCollider thisCard;
    private void Start()
    {
        thisCard = GetComponent<BoxCollider>(); 
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }
}

public enum CardSprite
{
    BOCCHI,
    NIJIKA,
    IKUYO,
    RYOU
}