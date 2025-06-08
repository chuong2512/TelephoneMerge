using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private RandomLevelData[] allLevels;
    private static RandomLevelData currentLevelData;

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < allLevels.Length)
        {
            RandomLevelData level = allLevels[levelIndex];
            currentLevelData = level;
        }
        else
        {
            GameManager.Instance.ResetLevelIndex();
            currentLevelData = allLevels[0];
        }
        
        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator LoadLevelAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Gameplay");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        InitializeLevel();
    }

    void InitializeLevel() 
    {
        if (GridManager.Instance != null && OrderManager.Instance != null)
        {
            GridManager.Instance.Initialize(currentLevelData);
            OrderManager.Instance.Initialize(currentLevelData);
            WinManager winManager = new GameObject("WinManager").AddComponent<WinManager>();
            winManager.InitializeWinCondition(currentLevelData);
            UIManager.Instance.WinCondition.ShowWinCondition(currentLevelData.winCondition);
        }
        else
        {
            Debug.LogError("Failed to find GridManager or OrderManager instances!");
        }
    }

    public static RandomLevelData GetCurrentLevelData()
    {
        return currentLevelData;
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
