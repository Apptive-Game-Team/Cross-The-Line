using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class AutoResizeToText : MonoBehaviour
{
    [SerializeField] private float padding = 10f;
    private TextMeshProUGUI targetText;
    private RectTransform content;
    private RectTransform rt;
    private float preferredHeight;
    private float previousHeight;

    private void Start()
    {
        content = transform.parent.GetComponent<RectTransform>();
        targetText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (targetText == null || content == null) 
            return;

        preferredHeight = targetText.GetPreferredValues().y + padding;

        if (preferredHeight != previousHeight)
        {
            // 1. RectTransform 높이 변경 (시각적)
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);

            // 2. Layout 시스템에 알려주기
            content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
        }

        previousHeight = preferredHeight;
    }
}