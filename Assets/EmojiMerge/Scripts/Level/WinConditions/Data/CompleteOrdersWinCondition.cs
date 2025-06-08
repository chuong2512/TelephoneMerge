using UnityEngine;

[CreateAssetMenu(fileName = "CompleteOrdersWinCondition", menuName = "Game/Levels/Wins/Complete Orders Win Condition")]
public class CompleteOrdersWinCondition : BaseWinConditionData
{
    public int ordersToComplete;
    
    public override bool IsCompleted(WinProgressData progressData)
    {
        return progressData.ordersCompleted >= ordersToComplete;
    }
    
    public override void UpdateProgress(WinProgressData progressData)
    {
    }
    
    public override int GetCurrentProgress(WinProgressData progressData)
    {
        return progressData.ordersCompleted;
    }
    
    public override int GetRequiredProgress()
    {
        return ordersToComplete;
    }
    
    public override string GetWinConditionText()
    {
        string orderText = ordersToComplete == 1 ? "order" : "orders";
        return $"Complete {ordersToComplete} {orderText} to win!";
    }
}

//This source code is originally bought from www.codebuysell.com
// Visit www.codebuysell.com
//
//Contact us at:
//
//Email : admin@codebuysell.com
//Whatsapp: +15055090428
//Telegram: t.me/CodeBuySellLLC
//Facebook: https://www.facebook.com/CodeBuySellLLC/
//Skype: https://join.skype.com/invite/wKcWMjVYDNvk
//Twitter: https://x.com/CodeBuySellLLC
//Instagram: https://www.instagram.com/codebuysell/
//Youtube: http://www.youtube.com/@CodeBuySell
//LinkedIn: www.linkedin.com/in/CodeBuySellLLC
//Pinterest: https://www.pinterest.com/CodeBuySell/
