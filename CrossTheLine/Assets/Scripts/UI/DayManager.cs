using System;
using UnityEngine;

namespace CTR
{
    public class DayManager : MonoBehaviour
    {
        [Header("Broadcasting on")]
        [SerializeField] private IntEventChannelSO onDayChanged;

        public int CurrentDay { get; set; } = 0;
        public bool isChanged = false;

        private void Update()
        {
            if (isChanged)
            {
                isChanged = false;
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