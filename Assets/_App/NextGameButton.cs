using UnityEngine;

public class NextGameButton : MonoBehaviour
{
	public GameObject shop;

	public void NextGame()
	{
		if(GameDataManager.Instance.playerData.intDiamond>=20)
		{
			GameDataManager.Instance.playerData.SubDiamond(20);
			GameManager.Instance.WinGame();
		}
		else
		{
			shop.SetActive(true);
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
