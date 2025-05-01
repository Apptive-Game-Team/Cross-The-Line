namespace DCR.Ui
{
    using UnityEngine;

    public class UISort : MonoBehaviour
    {
        public void SortChange()
        {
            transform.SetAsLastSibling();
        }
    }
}