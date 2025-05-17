using CTR.UI;
using DCR.Ui;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class RequestController : MonoBehaviour,
     IPointerDownHandler, IDragHandler, IPointerUpHandler
 {
     public const float DRAG_RATIO = 400f;
     public const float CORRECTION_VALUE = 1f;
     
     private Vector3 startMouseWorldPosition;
     private Vector3 startLocalPosition;
     private Vector2 startPointerPosition;
     private Vector2 startAnchoredPosition;
     
     private float originalLocalX;
     private float minLocalX;
     private float maxLocalX;
     
     private RectTransform rootRect;
     private Canvas canvas;

     [Header("Broadcasting on")]
     [SerializeField] private VoidEventChannelSO onAcceptRequest;
     [SerializeField] private VoidEventChannelSO onRejectRequest;
     private void Awake()
     {
         rootRect = transform.GetComponent<RectTransform>();
         canvas = GetComponentInParent<Canvas>();

         originalLocalX = rootRect.localPosition.x;
         minLocalX = originalLocalX - DRAG_RATIO;
         maxLocalX = originalLocalX + DRAG_RATIO;
     }
     
     public void OnPointerDown(PointerEventData eventData)
     {
         
     }

     public void OnDrag(PointerEventData eventData)
     {
         
         float deltaX = eventData.delta.x / canvas.scaleFactor;
         float newX = Mathf.Clamp(rootRect.localPosition.x + deltaX, minLocalX, maxLocalX);
         rootRect.localPosition = new Vector2(newX, rootRect.localPosition.y);
     }

     public void OnPointerUp(PointerEventData eventData)
     {
         float nowX = rootRect.localPosition.x;

         if (nowX > maxLocalX - CORRECTION_VALUE)
         {
             SelectRight();
         }
         else if (nowX < minLocalX + CORRECTION_VALUE)
         {
             SelectLeft();
         }
         else
         {
             rootRect.localPosition = new Vector3(originalLocalX, rootRect.localPosition.y, rootRect.localPosition.z);
         }
     }

     private void SelectRight()
     {
         onAcceptRequest.OnEventRaised();
     }

     private void SelectLeft()
     {
         onRejectRequest.OnEventRaised();
     }
 }
// public class RequestControllerUI : MonoBehaviour,
//         IPointerDownHandler, IDragHandler, IPointerUpHandler
// {
//     RectTransform rt;
//     Canvas canvas;
//     Vector2 startAnchoredPos;
//     Vector2 startPointerPos;
//
//     void Awake()
//     {
//         rt = GetComponent<RectTransform>();
//         canvas = GetComponentInParent<Canvas>();
//     }
//
//     public void OnPointerDown(PointerEventData eventData)
//     {
//         // 드래그 시작 시점
//         RectTransformUtility.ScreenPointToLocalPointInRectangle(
//             canvas.transform as RectTransform, 
//             eventData.position, 
//             canvas.worldCamera, 
//             out startPointerPos
//         );
//         startAnchoredPos = rt.anchoredPosition;
//     }
//
//     public void OnDrag(PointerEventData eventData)
//     {
//         Vector2 curPointerPos;
//         RectTransformUtility.ScreenPointToLocalPointInRectangle(
//             canvas.transform as RectTransform, 
//             eventData.position, 
//             canvas.worldCamera, 
//             out curPointerPos
//         );
//         Vector2 delta = curPointerPos - startPointerPos;
//         rt.anchoredPosition = startAnchoredPos + delta;
//     }
//
//     public void OnPointerUp(PointerEventData eventData)
//     {
//         // 드래그 끝
//     }
// }
