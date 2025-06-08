using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance { get; private set; }
    
    [SerializeField] private ItemManager itemManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (itemManager == null)
            {
                itemManager = FindObjectOfType<ItemManager>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanMergeItems(GridItem item1, GridItem item2)
    {
        if (item1 == null || item2 == null)
            return false;
        if (item2.OccupiedCell.IsCellBlocked)
            return false;
        
        return item1.properties.itemType == item2.properties.itemType &&
               item1.CurrentLevel == item2.CurrentLevel &&
               item1.CurrentLevel < item1.properties.maxLevel;
    }

    public GridItem PerformMerge(GridItem sourceItem, GridItem targetItem, Vector2Int mergePosition)
    {
        if (!CanMergeItems(sourceItem, targetItem))
            return null;

        sourceItem.SetMergeState(true);
        targetItem.SetMergeState(true);

        sourceItem.MarkForDestruction();
        targetItem.MarkForDestruction();

        Vector2Int sourcePos = sourceItem.GridPosition;
        Vector2Int targetPos = targetItem.GridPosition;

        GridManager.Instance.ClearCell(sourcePos);
        GridManager.Instance.ClearCell(targetPos);

        GridItem mergedItem = ItemManager.Instance.CreateMergedItem(
            mergePosition,
            sourceItem.properties.itemType,
            sourceItem.CurrentLevel + 1,
            sourceItem.properties
        );

        if (mergedItem != null)
        {
            itemManager.DestroyItem(sourceItem);
            itemManager.DestroyItem(targetItem);
            ItemMerged(mergedItem);
            return mergedItem;
        }
        else
        {
            sourceItem.SetMergeState(false);
            targetItem.SetMergeState(false);
            return null;
        }
    }

    private void ItemMerged(GridItem item)
    {
        UIManager.Instance.OpenItemDetailsPane(item);
        ParticleManager.Instance.SpawnParticle("itemMerged", item.transform.position);
        SoundManager.Instance.PlaySound("itemMerged");
        item.UnblockAdjacentCells();
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
