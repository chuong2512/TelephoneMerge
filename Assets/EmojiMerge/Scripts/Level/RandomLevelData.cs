using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomLevelData", menuName = "Game/Levels/Random Level Data")]
public class RandomLevelData : ScriptableObject
{
    [Header("Win Condition")]
    public BaseWinConditionData winCondition;
    
    [Header("Grid Data")]
    public Vector2Int MinGridSize;
    public Vector2Int MaxGridSize;

    public bool blockCells;
    public Vector2Int unblockCenter;

    [Header("Items Config")]
    [Header("Forced Spawn Items")]
    public ItemLevelCount[] forcedSpawnItems;

    [Header("Producer Config")]
    public ProducerItemProperties[] ProducerProperties;
    public float ProducerProbability;
    public int MinProducerCount;
    public int MaxProducerCount;

    [Header("Produced Item Config")]
    public ProducedItemProperties[] ProducedItemProperties;
    public float ProducedItemProbability;

    [Header("Chest Config")]
    public ChestItemProperties[] ChestProperties;
    public float ChestProbability;
    public int MinChestCount;
    public int MaxChestCount;

    [Header("Orders Config")]
    public OrderData[] possibleOrders;
    public int maxOrders = 3;
    public float orderCooldown = 5f;
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
