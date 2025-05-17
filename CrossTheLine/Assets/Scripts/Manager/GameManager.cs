using System;
using System.Collections.Generic;
using CTR;
using CTR.UI.ScriptableObjects;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 엑셀에서 읽어온 SO를 저장하는 DB
    [SerializeField] private ContentDatabaseSO db;
    // 현재 Request 현황을 저장하는 SO
    [SerializeField] private RequestSO request;
    // Player의 현재 Status
    [SerializeField] private StatusSO status;
    //[SerializeField] private DayManager dayManager; // 이게 너무 싫음
    
    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO onSceneLoaded;
    [SerializeField] private IntEventChannelSO onDayChanged;

    private void OnEnable()
    {
        onDayChanged.OnEventRaised += Init;
        onSceneLoaded.OnEventRaised += Init;
    }

    private void OnDisable()
    {
        onDayChanged.OnEventRaised -= Init;
        onSceneLoaded.OnEventRaised -= Init;
    }

    private void Init()
    {
        Init(0);
    }

    private void Init(int currentDay)
    {
        // 랜덤하게 배정받은 의뢰를 순서대로 화면에 출력하기 위한 세팅
        request.CurrentIndex = 0;
        request.Datas.Clear();
        request.Datas = GetRandomRequests(currentDay);
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
                (request.VisitedRequestIds.Contains(content.PreviousId) || content.PreviousId == -1) &&
                request.Datas.Count < n)
            {
                contents.Add(content);
                request.VisitedRequestIds.Add(content.Id);
            }
            else
            {
                Debug.Log($"잘 걸렀네 {content.Id}");
            }
        }
            
        return contents;
    }
}
