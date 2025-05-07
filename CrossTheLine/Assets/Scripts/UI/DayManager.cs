using UnityEngine;

namespace CTR
{
    public class DayManager : MonoBehaviour
    {
        [Header("Listening to")]
        [SerializeField] private VoidEventChannelSO onDayChanged;
		
        public void GoNextDay()
        {
            onDayChanged.OnEventRaised();
        }
    }
}

