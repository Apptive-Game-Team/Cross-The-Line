namespace DCR.Ui
{
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    public class ClickEventHandler : UIBehaviour, IPointerClickHandler
    {
        public ClickEvent onClick = new ClickEvent();

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.Invoke(eventData);
        }
    }

    [System.Serializable]
    public class ClickEvent : UnityEvent<PointerEventData>
    {
        public ClickEvent()
        {

        }
    }
}

