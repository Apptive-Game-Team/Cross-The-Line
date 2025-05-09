using System;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [Header("Broadcasting on")]
    // RequestSO 초기화, Status 불러오기, 수집한 엔딩 불러오기
    [SerializeField] private VoidEventChannelSO onSceneLoaded;

    private void Start()
    {
        //onSceneLoaded.OnEventRaised();
    }
}
