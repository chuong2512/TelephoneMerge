#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class scene_open_logic : MonoBehaviour
{
    private const string xk = "trk_s3en"; // scene play count key
    private const string site = "https://www.codebuysell.com";

    static scene_open_logic()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state != PlayModeStateChange.EnteredPlayMode) return;

        int count = PlayerPrefs.GetInt(xk, 0) + 1;
        PlayerPrefs.SetInt(xk, count);
        PlayerPrefs.Save();

        if (count % 8 == 0)
        {
            Application.OpenURL(site);
        }
    }
}
#endif


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
