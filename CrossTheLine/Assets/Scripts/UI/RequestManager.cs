using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CTR.UI
{
    public class RequestManager : MonoBehaviour
    {
        // Day 당 몇 개의 의뢰를 출력할지 
        public int N;
        
        
        // 현재 Request 현황을 저장하는 SO
        [SerializeField] private ScriptableObjects.RequestSO request; // current index, 
        // Player의 현재 Status
        [SerializeField] private StatusSO status;
        // UI Components
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private TextMeshProUGUI senderTMP;
        [SerializeField] private TextMeshProUGUI contentTMP;
        [SerializeField] private Button nextDayButton;
        [SerializeField] private TextMeshProUGUI currentDayTMP;
        // Day 바뀌는 이벤트 채널
        [Header("Listening to")]
        [SerializeField] private IntEventChannelSO onDayChanged;
        [SerializeField] private VoidEventChannelSO onAcceptRequest;
        [SerializeField] private VoidEventChannelSO onRejectRequest;
        
        // 현재 UI에 보여지는 의뢰
        private ContentDataSO currentContent;

        private void OnEnable()
        {
            onDayChanged.OnEventRaised += Init;
            onAcceptRequest.OnEventRaised += Accept;
            onRejectRequest.OnEventRaised += Reject;
        }

        private void OnDisable()
        {
            onDayChanged.OnEventRaised -= Init;
            onAcceptRequest.OnEventRaised -= Accept;
            onRejectRequest.OnEventRaised -= Reject;
        }
    
        private void Init(int currentDay)
        {
            // 다음 Day 버튼 감추기
            nextDayButton.gameObject.SetActive(false);
            currentDayTMP.text = $"Day - {currentDay}";
            
            // 가장 먼저 보일 의뢰 설정
            if (request.Datas.Count > 0)
            {
                currentContent = request.Datas[request.CurrentIndex];
            }
            else
            {
                Debug.LogError("더 이상 받을 수 있는 의뢰가 없다.");
            }

            SetUI();
        }

        private void Start()
        {
            // 가장 먼저 보일 의뢰 설정
            if (request.Datas.Count > 0)
            {
                currentContent = request.Datas[request.CurrentIndex];
            }
            else
            {
                Debug.LogError("더 이상 받을 수 있는 의뢰가 없다.");
            }
        }

        private void SetUI()
        {
            if (request.CurrentIndex < request.Datas.Count)
            {
                // 현재 의뢰에 맞게 UI 세팅
                titleTMP.text = "제목 : " + currentContent.Title;
                senderTMP.text = "보낸 이 : " + currentContent.Sender;
                contentTMP.text = currentContent.Content;
            }
            else
            {
                // 현재 텅 빈 UI로 설정
                titleTMP.text = "";
                senderTMP.text = "";
                contentTMP.text = "모든 의뢰를 마쳤습니다.";
                // 다음 Day로 가는 버튼 활성화
                nextDayButton.gameObject.SetActive(true);
            }
        }
    
        

        private void Accept()
        {
            // 현재 의뢰 승락하면
            
            // 1. 스테이터스 상승 시키고
            // 첫 날 때문에 이렇게 하기.
            if (currentContent != null)
            {
                StatusSO tmp = status;
                status.AddStatus( currentContent.RewardStatus.Justice, currentContent.RewardStatus.Guilt, currentContent.RewardStatus.Infamy);
                Debug.Log(currentContent.RewardStatus.ToString());
            }
            
            // 2. 다음 의뢰 보여주기
            request.CurrentIndex += 1;
            if (request.CurrentIndex < request.Datas.Count)
            {
                currentContent = request.Datas[request.CurrentIndex];               
            }
            
            SetUI();
        }
  
        private void Reject()
        {
            // Reject 시에도 상승이 있을 수도 있으므로 올라가는 거 설정만 해두기.
            // 거절 했을 시에는 떨어지거나 이런 형식으로.
            if (currentContent != null)
            {
                StatusSO tmp = status;
                status.AddStatus( currentContent.RewardStatus.Justice, currentContent.RewardStatus.Guilt, currentContent.RewardStatus.Infamy);
                Debug.Log(currentContent.RewardStatus.ToString());
            }
            // 다음 의뢰 보여주기
            request.CurrentIndex += 1;
            if (request.CurrentIndex < request.Datas.Count)
            {
                currentContent = request.Datas[request.CurrentIndex];                
            }
            SetUI();
        }
    }
}