using System;
using UnityEngine;

namespace CTR
{
    public class DayManager : MonoBehaviour
    {
        [Header("Broadcasting on")]
        [SerializeField] private IntEventChannelSO onDayChanged;

        public int CurrentDay { get; set; } = 0;
        public bool IsChanged = false;

        private void Update()
        {
            if (IsChanged)
            {
                IsChanged = false;
                GoNextDay();
            }
        }

        public void GoNextDay()
        {
            CurrentDay += 1;
            onDayChanged.OnEventRaised(CurrentDay);
        }
    }
}