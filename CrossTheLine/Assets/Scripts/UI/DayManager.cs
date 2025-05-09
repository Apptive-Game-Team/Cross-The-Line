using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CTR
{
    public class DayManager : MonoBehaviour
    {
        [SerializeField] private Button nextDayButton;
        [SerializeField] private TextMeshProUGUI currentDayTMP;
        
        [Header("Broadcasting on")]
        [SerializeField] private IntEventChannelSO onDayChanged;

        public int CurrentDay { get; set; } = 0;

        public void GoNextDay()
        {
            nextDayButton.gameObject.SetActive(false);
            currentDayTMP.text = $"Day - {CurrentDay}";
            CurrentDay += 1;
            onDayChanged.OnEventRaised(CurrentDay);
        }
    }
}