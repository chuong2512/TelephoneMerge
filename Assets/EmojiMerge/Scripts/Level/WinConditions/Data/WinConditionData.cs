using UnityEngine;

public abstract class BaseWinConditionData : ScriptableObject
{
    public abstract bool IsCompleted(WinProgressData progressData);
    public abstract void UpdateProgress(WinProgressData progressData);
    public abstract int GetCurrentProgress(WinProgressData progressData);
    public abstract int GetRequiredProgress();
    
    public virtual string GetWinConditionText()
    {
        return $"Complete the objective to win!";
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
