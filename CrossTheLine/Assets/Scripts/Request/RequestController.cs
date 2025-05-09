using CTR.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class RequestController : MonoBehaviour
{
    public const static float DRAG_RATIO = 4f;
    public const static float CORRECTION_VALUE = 1f;
    private bool isDragging = false;
    
    private Vector3 startMouseWorldPosition;
    private Vector3 startLocalPosition;
    
    private float originalLocalX;
    private float minLocalX;
    private float maxLocalX;
   
    private RequestManager requestManager;

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO onAcceptRequest;
    [SerializeField] private VoidEventChannelSO onAcceptRequest;
    private void Awake()
    {
        requestManager = transform.parent.GetComponent<RequestManager>();
        
        startLocalPosition = transform.localPosition;
        originalLocalX = transform.localPosition.x;
        minLocalX = originalLocalX - DRAG_RATIO;
        maxLocalX = originalLocalX + DRAG_RATIO;
    }
    
    private void OnMouseDown()
    {
        var screenPoint = Input.mousePosition;
        if (Camera.main != null) startMouseWorldPosition = Camera.main.ScreenToWorldPoint(screenPoint);

        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;
        
        var screenPoint = Input.mousePosition;
        if (Camera.main != null)
        {
            var currentMouseWorldPosition = Camera.main.ScreenToWorldPoint(screenPoint);
        
            float deltaX = currentMouseWorldPosition.x - startMouseWorldPosition.x;
            float clampedX = Mathf.Clamp(startLocalPosition.x + deltaX, minLocalX, maxLocalX);
            
        
            transform.localPosition = new Vector3(clampedX, startLocalPosition.y, startLocalPosition.z);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        
        var nowX = transform.localPosition.x;
        
        Debug.Log("Drag End");

        // 어떤 시각적인 효과로 넘어갔으면 좋겠다.
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
            transform.localPosition = startLocalPosition;
        }
    }

    private void SelectRight()
    {
        Debug.Log("SelectRight");
        onAcceptRequest.Invoke;
    }

    private void SelectLeft()
    {
        Debug.Log("SelectLeft"); 
        requestManager.isReject = true;
        onRejectRequest.Invoke;
    }
}