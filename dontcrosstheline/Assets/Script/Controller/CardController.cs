using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CardController : MonoBehaviour
{
    public Card card;
    BoxCollider thisCard;
    public bool isMouseOver;
    void Start()
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