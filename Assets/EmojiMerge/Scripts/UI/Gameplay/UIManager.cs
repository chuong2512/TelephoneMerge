using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ItemDetailsUI itemDetails;
    [SerializeField] private StatsUI stats;
    [SerializeField] private WinConditionUI winCondition;
    [SerializeField] private WinLosePopup winPopup;


    public StatsUI Stats => stats;
    public WinLosePopup WinPopup => winPopup;
    public ItemDetailsUI ItemDetails => itemDetails;
    public WinConditionUI WinCondition => winCondition;
    
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenItemDetailsPane(GridItem item)
    {
        itemDetails.ShowDetails(item);
    }

    public void CloseItemDetailsPane()
    {
        itemDetails.Hide();
    }

    public void UpdateGold(int newGoldValue)
    {
        stats.UpdateGold(newGoldValue);
    }

    public void UpdateEnergy(int newEnergyValue)
    {
        stats.UpdateEnergy(newEnergyValue);
    }

    public void ShowEnergyWarning()
    {
        stats.ShowEnergyWarning();
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
