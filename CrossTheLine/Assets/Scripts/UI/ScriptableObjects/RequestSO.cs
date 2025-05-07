namespace CTR.UI.ScriptableObjects
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "RequestSO", menuName = "Request/RequestSO")]
    public class RequestSO : ScriptableObject
    {
        public List<ContentDataSO> CurrentRequests;
        public List<int> VisitedRequestIds;
        public int CurrentIndex;
    }
}


