using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusSO", menuName = "Gameplay/StatusSO", order = -1)]
public class StatusSO : ScriptableObject
{
    public Status Status = new Status(0, 0, 0);
    
    // Status 초기화를 위한 reset 버튼
    public bool reset;
    
    public void SetStatus(int justice, int guilt, int infamy)
    {
        Status.Justice = justice;
        Status.Guilt = guilt;
        Status.Infamy = infamy;
    }

    public void AddStatus(int justice, int guilt, int infamy)
    {
        Status.Justice += justice;
        Status.Guilt += guilt;
        Status.Infamy += infamy;
    }

    // 에디터 상에서 Status 초기화를 위해 사용
    private void OnValidate()
    {
        Status = new Status(0, 0, 0);
    }
}

[Serializable]
public struct Status
{
    public Status(int justice, int guilt, int infamy)
    { 
        Justice = justice;
        Guilt = guilt;
        Infamy = infamy;
    }
        
    public int Justice, Guilt, Infamy;

    public override string ToString()
    {
        return $"정의감:{Justice}, 죄책감:{Guilt}, 악명:{Infamy}";
    }
}