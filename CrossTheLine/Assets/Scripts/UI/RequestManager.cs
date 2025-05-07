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
        // Day 바뀌는 이벤트 채널
        [Header("Broadcasting on")]
        [SerializeField] private VoidEventChannelSO onDayChanged;
        // UI Components
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private TextMeshProUGUI senderTMP;
        [SerializeField] private TextMeshProUGUI contentTMP;
        [SerializeField] private Button acceptBtn; // 우 슬라이드로 대체
        [SerializeField] private Button rejectBtn; // 좌 슬라이드
    
        private ScriptableObjects.RequestSO request; // current index, 
    
        // 이거 구현을 어케 하지. 일단 브루트포스로 -> 최적화 필요
        // 조건 : 스테이터스 만족? 이미 받은 의뢰? 선행 의뢰 수행?
        private ContentDataSO GetRandomRequest()
        {
            ContentDataSO content;
            return content;
        }
    
        private List<ContentDataSO> GetRandomRequests(int n)
        {
            List<ContentDataSO> contents = new List<ContentDataSO>();
            return contents;
        }

        private void OnEnable()
        {
            onDayChanged.OnEventRaised += Init;  
        }

        private void OnDisable()
        {
            onDayChanged.OnEventRaised -= Init;
        }
    
        private void Init()
        {
            request.CurrentIndex = 0;
            request.CurrentRequests = GetRandomRequests(N);
        }
    
        private void SetUI()
        {
            // if (currentIndex >= list.Count()) error;
            //
            // Title = list[currentIndex].title;
            // ...
        }
    
        private void OnAccept()
        {
            // 현재 의뢰 승락하면 스테이터스 상승 시키고 다음 의뢰 보여주기
        }
  
        private void OnReject()
        {
            // 다음 의뢰 보여주기
        }
    }
}

