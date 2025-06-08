using UnityEngine;

public class ProducedItem : BaseProducedItem
{    
    private ProducedItemProperties ProducedProperties => properties as ProducedItemProperties;

    public override void Initialize(BaseItemProperties props, int level = 1)
    {
        if (!(props is ProducedItemProperties))
        {
            Debug.LogError("Incorrect properties type provided to ProducedItem");
            return;
        }

        base.Initialize(props, level);
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
