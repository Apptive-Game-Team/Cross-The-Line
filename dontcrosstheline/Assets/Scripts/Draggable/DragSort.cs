namespace DCR.Ui
{
    using UnityEngine;

    public class DragSort : MonoBehaviour
    {
        public void SortChange()
        {
            transform.SetAsLastSibling();
        }
    }
}