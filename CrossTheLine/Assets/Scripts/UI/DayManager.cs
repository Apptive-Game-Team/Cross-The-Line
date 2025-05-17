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
            CurrentDay += 1;
            currentDayTMP.text = $"Day - {CurrentDay}";
            onDayChanged.OnEventRaised(CurrentDay);
        }
    }
}