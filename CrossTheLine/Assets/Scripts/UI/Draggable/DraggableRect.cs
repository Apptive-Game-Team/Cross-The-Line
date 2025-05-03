namespace DCR.Ui
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class DraggableRect : DragEventHandler
    {
        public RectTransform dragRectTransform;

        private Canvas canvas;
        public Canvas Canvas
        {
            get
            {
                if (canvas == null)
                {
                    canvas = GetComponentInParent<Canvas>();
                }
                return canvas;
            }
        }

        protected override void OnEnable()
        {
            if (dragRectTransform == null)
            {
                dragRectTransform = GetComponent<RectTransform>();
            }

            if (dragRectTransform == null || Canvas == null)
            {
                enabled = false;
                throw new Exception("There is no canvas");
            }

            onDrag.AddListener(OnDragMove);
        }

        protected override void OnDisable()
        {
            onDrag.RemoveListener(OnDragMove);
        }

        public void OnDragMove(PointerEventData eventData)
        {
            dragRectTransform.anchoredPosition += eventData.delta / Canvas.scaleFactor;
        }

        protected override void OnCanvasHierarchyChanged()
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }
}