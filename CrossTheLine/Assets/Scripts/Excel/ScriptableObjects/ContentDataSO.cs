using System;
using UnityEngine;

public class ContentDataSO : ScriptableObject
{
    public int Id;
    public int PreviousId;
    public string Title;
    public string Sender;
    public string Content;
    public Status MinStatus;      // 최소 요구 스테이터스
    public Status RewardStatus;   // 수락시 보상 스테이터스
    public string AcceptMessage;
    public string RejectMessage;
    public Sprite Image;

    public void Init(int id, int previousId, string title, string sender, string content, 
        int minJustice, int minGuilt, int minInfamy,
        int rewardJustice, int rewardGuilt, int rewardInfamy,
        string acceptMessage, string rejectMessage, Sprite image)
    {
        this.Id = id;
        this.PreviousId = previousId;
        this.Title = title;
        this.Sender = sender;
        this.Content = content;
        this.MinStatus = new Status(minJustice, minGuilt, minInfamy);
        this.RewardStatus = new Status(rewardJustice, rewardGuilt, rewardInfamy);
        this.AcceptMessage = acceptMessage;
        this.RejectMessage = rejectMessage;
        this.Image = image;
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
}