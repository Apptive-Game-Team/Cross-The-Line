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
        
        // 엑셀에서 읽어온 SO를 저장하는 DB
        [SerializeField] private ContentDatabaseSO db;
        // 현재 Request 현황을 저장하는 SO
        [SerializeField] private ScriptableObjects.RequestSO request; // current index, 
        // Player의 현재 Status
        [SerializeField] private StatusSO status;
        // Day 바뀌는 이벤트 채널
        [Header("Listening to")]
        [SerializeField] private IntEventChannelSO onDayChanged;
        // UI Components
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private TextMeshProUGUI senderTMP;
        [SerializeField] private TextMeshProUGUI contentTMP;
        [SerializeField] private Button nextDayButton;
        [SerializeField] private TextMeshProUGUI currentDayTMP;
        
        public bool isAccept = false;
        public bool isReject = false;
        
        // 현재 UI에 보여지는 의뢰
        private ContentDataSO currentContent;

        private void OnEnable()
        {
            onDayChanged.OnEventRaised += Init;
        }

        private void OnDisable()
        {
            onDayChanged.OnEventRaised -= Init;
        }
    
        private void Init(int currentDay)
        {
            // 다음 Day 버튼 감추기
            nextDayButton.gameObject.SetActive(false);
            currentDayTMP.text = $"Day - {currentDay}";
            
            // 랜덤하게 배정받은 의뢰를 순서대로 화면에 출력하기 위한 세팅
            request.CurrentIndex = 0;
            request.Datas.Clear();
            request.Datas = GetRandomRequests(N);
            
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
    
        /// <summary>
        /// n개의 최소 스테이터스를 만족하는 ContentDataSO를 랜덤하게 배정해 반환
        /// 조건 : 최소 스테이터스 만족, 기존 의뢰와 중복 여부, 선행 의뢰 수행 여부
        /// </summary>
        private List<ContentDataSO> GetRandomRequests(int n)
        {
            List<ContentDataSO> contents = new List<ContentDataSO>();
            
            // db.Datas 에서 조건에 맞는 의뢰 필터링
            foreach (var content in db.Datas)
            {
                if (status.Status.Justice >= content.MinStatus.Justice &&
                    status.Status.Guilt >= content.MinStatus.Guilt &&
                    status.Status.Infamy >= content.MinStatus.Infamy &&
                    !request.VisitedRequestIds.Contains(content.Id) &&
                    request.Datas.Count < n)
                {
                    contents.Add(content);
                    request.VisitedRequestIds.Add(content.Id);
                }
                else
                {
                    Debug.Log("잘 걸렀네");
                }
            }
            
            return contents;
        }

        private void Update()
        {
            if (isAccept)
            {
                isAccept = false;
                OnAccept();
            }
            if (isReject)
            {
                isReject = false;
                OnReject();
            }
        }

        private void OnAccept()
        {
            // 현재 의뢰 승락하면
            
            // 1. 스테이터스 상승 시키고
            StatusSO tmp = status;
            status.AddStatus( currentContent.RewardStatus.Justice, currentContent.RewardStatus.Guilt, currentContent.RewardStatus.Infamy);
            Debug.Log(currentContent.RewardStatus.ToString());
            
            // 2. 다음 의뢰 보여주기
            request.CurrentIndex += 1;
            if (request.CurrentIndex < request.Datas.Count)
            {
                currentContent = request.Datas[request.CurrentIndex];               
            }
            
            SetUI();
        }
  
        private void OnReject()
        {
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