using System;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;

namespace DCR.Ui
{
    public class ClickableRect : ClickEventHandler
    {
        public RectTransform clickRectTransform;

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
            if (clickRectTransform == null)
            {
                clickRectTransform = GetComponent<RectTransform>();
            }

            if (clickRectTransform == null || Canvas == null)
            {
                enabled = false;
                throw new Exception("There is no canvas");
            }
        }

        protected override void OnCanvasHierarchyChanged()
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }
}

