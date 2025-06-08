using UnityEngine;

[CreateAssetMenu(fileName = "GetGoldWinCondition", menuName = "Game/Levels/Wins/Get Gold Win Condition")]
public class GetGoldWinCondition : BaseWinConditionData
{
    public int goldNeeded;
    
    public override bool IsCompleted(WinProgressData progressData)
    {
        return progressData.goldEarned >= goldNeeded;
    }
    
    public override void UpdateProgress(WinProgressData progressData)
    {
    }
    
    public override int GetCurrentProgress(WinProgressData progressData)
    {
        return progressData.goldEarned;
    }
    
    public override int GetRequiredProgress()
    {
        return goldNeeded;
    }
    
    public override string GetWinConditionText()
    {
        return $"Get {goldNeeded} gold to win!";
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
