using UnityEngine;
using System;

[Serializable]
public struct ChestResourceDrop
{
    public ItemType resourceType;
    public int level;
    public int count;
}

[Serializable]
public struct ChestLevel
{
    public ChestResourceDrop[] resourceDrops;
    public float unlockTime;

    public int TotalCapacity
    {
        get
        {
            int total = 0;
            if (resourceDrops != null)
            {
                foreach (var drop in resourceDrops)
                {
                    total += drop.count;
                }
            }
            return total;
        }
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
