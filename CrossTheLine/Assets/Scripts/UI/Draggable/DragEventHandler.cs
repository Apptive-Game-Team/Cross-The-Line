namespace DCR.Ui
{
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    public class DragEventHandler : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public DragEvent onBeginDrag = new DragEvent();
        public DragEvent onDrag = new DragEvent();
        public DragEvent onEndDrag = new DragEvent();
        

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag.Invoke(eventData);
        }

        
    }

    [System.Serializable]
    public class DragEvent : UnityEvent<PointerEventData>
    {
        public DragEvent()
        {

        }
    }
}

