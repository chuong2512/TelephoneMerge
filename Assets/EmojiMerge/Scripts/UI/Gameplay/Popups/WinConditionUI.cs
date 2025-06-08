using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WinConditionUI : MonoBehaviour
{
    [SerializeField] private UIDocument winConditionDocument;

    private VisualElement background;
    private Label winConditionText;
    private VisualElement popupBox;
    private BaseWinConditionData currentWinCondition;

    private void Awake()
    {
        background = winConditionDocument.rootVisualElement.Q<VisualElement>("Background");
        winConditionText = winConditionDocument.rootVisualElement.Q<Label>("WinText");
        popupBox = winConditionDocument.rootVisualElement.Q<VisualElement>("PopupBox");
    }

    public void ShowWinCondition(BaseWinConditionData winCondition)
    {
        if (winCondition == null) return;
        
        currentWinCondition = winCondition;
        
        string displayText = winCondition.GetWinConditionText();
        
        ShowWinPopup(displayText);
    }

    private void ShowWinPopup(string text)
    {
        winConditionText.text = text;
        background.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        popupBox.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        StartCoroutine(AnimateWinPopup());
    }

    private IEnumerator AnimateWinPopup()
    {
        background.style.opacity = 1;
        yield return new WaitForSeconds(0.3f);
        popupBox.style.scale = new StyleScale(new Scale(Vector2.one));
        yield return new WaitForSeconds(1.0f);
        popupBox.style.scale = new StyleScale(new Scale(Vector2.one));
        yield return new WaitForSeconds(0.5f);
        background.style.opacity = 0;
        yield return new WaitForSeconds(0.5f);
        background.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        GameManager.Instance.SetGameStarted();
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
