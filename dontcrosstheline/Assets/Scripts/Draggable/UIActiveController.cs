namespace DCR.Ui
{
    using DG.Tweening;
    using UnityEngine;

    [RequireComponent(typeof(RectTransform))]
    public class UIActiveController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform activeOrigin;    // active 시작 위치 지정
        [SerializeField] private float duration = 0.25f;    // 애니메이션 시간
        [SerializeField] private Ease ease = Ease.OutQuad;

        [SerializeField] private GameObject root;

        // Animating toggle button to originalPos increasing its size
        private RectTransform rootRect;                     // 이 스크립트가 붙은 패널
        private Vector2 originalPos;
        private Vector3 originalScale;
        private Tween currentTween;

        void Awake()
        {
            if (root == null)
            {
                root = gameObject;
                rootRect = root.GetComponent<RectTransform>();
            }

            rootRect = (RectTransform)transform;
            originalPos = rootRect.anchoredPosition;
            originalScale = rootRect.localScale;
            root.gameObject.SetActive(false);   // 시작은 꺼진 상태
        }

        public void Toggle()
        {
            if (root.activeSelf) Close();
            else Open();
        }

        public void Open()
        {
            currentTween?.Kill();               // 진행 중이면 취소
            root.gameObject.SetActive(true);

            // 시작 : Toggle start point + Zero scale
            rootRect.anchoredPosition = ButtonLocalPos();
            rootRect.localScale = Vector3.zero;

            // 목표 : Original point and scale
            currentTween = DOTween.Sequence()
            .Join(rootRect.DOAnchorPos(originalPos, duration).SetEase(ease))
            .Join(rootRect.DOScale(originalScale, duration).SetEase(ease))
                .OnComplete(() => currentTween = null);
        }

        public void Close()
        {
            currentTween?.Kill();

            // Active start point + Zero scale
            currentTween = DOTween.Sequence()
            .Join(rootRect.DOAnchorPos(ButtonLocalPos(), duration).SetEase(ease))
            .Join(rootRect.DOScale(Vector3.zero, duration).SetEase(ease))
                .OnComplete(() =>
                {
                    rootRect.gameObject.SetActive(false); // 애니메이션 끝나면 꺼둠
                    currentTween = null;
                });
        }

        /// <summary>
        /// Active 시작점을 root 부모 공간(=rootRect.parent) 좌표로 변환
        /// </summary>
        Vector2 ButtonLocalPos()
        {
            var parentRect = rootRect.parent as RectTransform;

            // active 시작점의 월드 좌표
            Vector3 world = activeOrigin.position;

            // 부모 캔버스가 스크린 렌더 모드라면 Camera 지정 필요 없음
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                RectTransformUtility.WorldToScreenPoint(null, world),
                null,
                out Vector2 local);

            return local;
        }

        void OnDisable() => currentTween?.Kill();   // 안전 장치
    }
}

