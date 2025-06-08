using System;
using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts.CoinManager
{
    public class InAppButton : MonoBehaviour
    {
        public int price = 1;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(OnClickButton);
        }

        private void OnClickButton()
        {
            if (GameDataManager.Instance.playerData.intDiamond >= price)
            {
                GameDataManager.Instance.playerData.SubDiamond(price);
                //todo:
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
