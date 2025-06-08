using UnityEngine;

[CreateAssetMenu(fileName = "ResourceItemProperties", menuName = "Game/Items/Resource Properties")]
public class ResourceItemProperties : BaseItemProperties
{
    public float[] resourceAmounts;
    
    private void OnEnable()
    {
        maxLevel = 3;
        if (resourceAmounts == null || resourceAmounts.Length != maxLevel)
        {
            resourceAmounts = new float[maxLevel];
            for (int i = 0; i < maxLevel; i++)
            {
                resourceAmounts[i] = (i + 1) * 10f;
            }
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
