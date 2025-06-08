using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ContainerItemLevel
{
    public float rechargeTime;
    public ItemLevelCount[] itemCapacities;
    public bool canSpawnItems;

    public int TotalCapacity
    {
        get
        {
            int total = 0;
            if (itemCapacities != null)
            {
                foreach (var capacity in itemCapacities)
                {
                    total += capacity.count;
                }
            }
            return total;
        }
    }
}

[CreateAssetMenu(fileName = "ContainerItemProperties", menuName = "Game/Items/Container Properties")]
public class ContainerItemDefinition : BaseItemProperties
{
    public float energyCost;
    public ContainerItemLevel[] levels;

    protected virtual void OnEnable()
    {
        if (levels == null || levels.Length != maxLevel)
        {
            levels = new ContainerItemLevel[maxLevel];
            InitializeDefaultLevels();
        }
    }

    protected virtual void OnValidate()
    {
        if (levels == null || levels.Length != maxLevel)
        {
            levels = new ContainerItemLevel[maxLevel];
            InitializeDefaultLevels();
        }
    }

    protected virtual void InitializeDefaultLevels()
    {
    }

    public ContainerItemLevel GetLevelData(int level)
    {
        return levels[Mathf.Clamp(level - 1, 0, levels.Length - 1)];
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
