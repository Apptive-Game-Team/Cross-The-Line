using System;
using UnityEngine;

namespace CTR
{
    public class DayManager : MonoBehaviour
    {
        [Header("Broadcasting on")]
        [SerializeField] private VoidEventChannelSO onDayChanged;

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
            onDayChanged.OnEventRaised();
        }
    }
}