using System;
using UnityEngine;

[Serializable]
public class BaseData : MonoBehaviour {
    protected string prefString;

    public virtual void Init() {
        try {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(prefString), this);
        }
        catch (Exception e) {
            ResetData();
            Debug.LogError("Error On Load PlayerPrefs...");
            Debug.LogError("Error : " + e);
			
        }

        CheckAppendData();
    }

    public virtual void ResetData() { }
    protected virtual void CheckAppendData() { }

    protected void Save() {
        string json = JsonUtility.ToJson(this);
        // Debug.Log("json_______" + json);
        PlayerPrefs.SetString(prefString, json);
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
