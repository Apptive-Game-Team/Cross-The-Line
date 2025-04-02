using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class AutoResizeToText : MonoBehaviour
{
    [SerializeField] private float padding = 10f;
    private TextMeshProUGUI targetText;
    private RectTransform content;
    private float preferredHeight;

    private void Start()
    {
        content = transform.parent.GetComponent<RectTransform>();
        targetText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (targetText == null || content == null) return;

        preferredHeight = targetText.GetPreferredValues().y + padding;

        // 1️⃣ RectTransform 높이 변경 (시각적)
        var rt = GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);

        // 2️⃣ Layout 시스템에 알려주기
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
    }
}
