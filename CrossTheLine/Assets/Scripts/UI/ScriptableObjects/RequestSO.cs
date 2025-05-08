using System;
using UnityEngine.Serialization;

namespace CTR.UI.ScriptableObjects
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "RequestSO", menuName = "Request/RequestSO")]
    public class RequestSO : ScriptableObject
    {
        [FormerlySerializedAs("CurrentRequests")]
        public List<ContentDataSO> Datas;
        public List<int> VisitedRequestIds;
        public int CurrentIndex;

        public bool reset;

        private void OnValidate()
        {
            Datas = new List<ContentDataSO>();
            VisitedRequestIds = new List<int>();
            CurrentIndex = 0;
        }

    }
}