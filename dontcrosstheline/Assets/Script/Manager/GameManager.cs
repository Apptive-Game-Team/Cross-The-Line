using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    // GameObject
    // 사실 이 cardGameObject 가 걍 자신이라서 수정 필요할 수도.
    public GameObject cardGameObject;
    public CardController mainCardContorller;
    public SpriteRenderer cardSpriteRenderer;
    public ResourceManager resourceManager;

    public GameObject rightCardPosition;
    public GameObject leftCardPosition;
    public GameObject centerCardPosition;
    
    // Tweaking Variable
    public float fMovingSpeed = 2;
    private Vector3 pos;
    private Vector3 startPos;
    
    private bool isDelay = false;

    public float SIDE_MARGIN = 0.5f;
    public float SIDE_TRIGGER = 3;

    public float DIVIDE_VALUE = 3;
    
    private Collider cardCollider;
    
    // UI
    public TMP_Text displayText;
    public TMP_Text dialogueText;
    public TMP_Text characterName;
    
    // Card Variables
    private string leftQuote;
    private string rightQuote;
    public Card currentCard;
    public Card testCard;
    
    void Start()
    {
        // Set Card Variable
        Transform graphicTransform = cardGameObject.transform.Find("MainGraphic");
        mainCardContorller = cardGameObject.GetComponent<CardController>();
        cardCollider = cardGameObject.GetComponent<Collider>();
        cardSpriteRenderer = graphicTransform.GetComponent<SpriteRenderer>();
        //resourceManager = cardGameObject.GetComponent<ResourceManager>();
        
        LoadCard(testCard);
    }

    void Update()
    {
        if (!isDelay)
        {
            SetCardPosition(); 
        }
    }

    public void SetCardPosition() 
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (pos.x < 0)
        {
            cardGameObject.transform.position = Vector3.Lerp(centerCardPosition.transform.position, leftCardPosition.transform.position, Mathf.Abs(pos.x / (SIDE_MARGIN + 1)));
            cardGameObject.transform.rotation = Quaternion.Lerp(centerCardPosition.transform.rotation, leftCardPosition.transform.rotation, Mathf.Abs(pos.x / (SIDE_MARGIN + 1)));

            if (pos.x < -SIDE_MARGIN)
            {
                dialogueText.text = leftQuote;
                cardSpriteRenderer.color = Color.red;
                if (Input.GetMouseButtonDown(0))
                {
                    currentCard.Left();
                    NewCard();
                }
            }
            else
            {
                cardSpriteRenderer.color = Color.white;
            }
        }
        else if (pos.x > 0)
        {
            cardGameObject.transform.position = Vector3.Lerp(centerCardPosition.transform.position, rightCardPosition.transform.position, pos.x / (SIDE_MARGIN + 1));
            cardGameObject.transform.rotation = Quaternion.Lerp(centerCardPosition.transform.rotation, rightCardPosition.transform.rotation, pos.x / (SIDE_MARGIN + 1));
            if (pos.x > SIDE_MARGIN)
            {
                dialogueText.text = rightQuote;
                cardSpriteRenderer.color = Color.green;
                if (Input.GetMouseButtonDown(0))
                {
                    currentCard.Right();
                    NewCard();
                }
            }
            else
            {
                cardSpriteRenderer.color = Color.white;
            }
        }
        
        SetTextAlpha(pos);
    }

    public void SetTextAlpha(Vector3 mousePos)
    {
        dialogueText.alpha = Mathf.Min(Mathf.Sqrt(Mathf.Abs(mousePos.x / DIVIDE_VALUE)), 1);
    }

    public void NewCard()
    {
        int randomIndex = UnityEngine.Random.Range(0, resourceManager.cards.Length);
        LoadCard(resourceManager.cards[randomIndex]);
    }

    public void LoadCard(Card card)
    {
        cardSpriteRenderer.sprite = resourceManager.sprites[(int)card.cardSprite];
        leftQuote = card.leftQuote;
        rightQuote = card.rightQuote;
        currentCard = card;
        isDelay = true;

        dialogueText.text = "";
        cardSpriteRenderer.color = Color.white;
        StartCoroutine(MoveCenterDelay());
    }

    private IEnumerator MoveCenterDelay()
    {
        float elapsedTime = 0f;
        float duration = 1f;  // 이동하는 데 걸리는 시간 (1초)
        
        Quaternion backFlip = Quaternion.Euler(centerCardPosition.transform.rotation.eulerAngles.x, centerCardPosition.transform.rotation.eulerAngles.y -180, centerCardPosition.transform.rotation.eulerAngles.z);
        cardGameObject.transform.position = centerCardPosition.transform.position;
        cardGameObject.transform.rotation = backFlip;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float lerpFactor = elapsedTime / duration;
            
            cardGameObject.transform.rotation = Quaternion.Lerp(backFlip, centerCardPosition.transform.rotation, lerpFactor);

            yield return null;
        }
        /*
        Vector3 startPosition = cardGameObject.transform.position;
        Quaternion startRotation = cardGameObject.transform.rotation;

        // 1초 동안 부드럽게 이동
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float lerpFactor = Mathf.Clamp01(elapsedTime / duration); // lerpFactor는 0에서 1로 점진적으로 증가

            // 부드럽게 위치와 회전을 계산
            cardGameObject.transform.position = Vector3.Lerp(startPosition, centerCardPosition.transform.position, lerpFactor);
            cardGameObject.transform.rotation = Quaternion.Lerp(startRotation, centerCardPosition.transform.rotation, lerpFactor);

            yield return null;  // 한 프레임 기다린 후 계속 실행
        }

        // 이동이 완료된 후의 상태
        cardGameObject.transform.position = centerCardPosition.transform.position;
        cardGameObject.transform.rotation = centerCardPosition.transform.rotation;
        */

        // 딜레이 종료 처리
        
        yield return new WaitForSeconds(duration);
        isDelay = false;
    }
}
