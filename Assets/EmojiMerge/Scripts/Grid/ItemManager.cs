using UnityEngine;
using System;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject itemsContainer;

    [Header("Prefabs")]
    [SerializeField] private GameObject gridItemPrefab;

    public event Action<GridItem> OnGridItemCreated;
    public event Action<GridItem> OnGridItemDestroyed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NotifyItemCreated(GridItem item)
    {
        OnGridItemCreated?.Invoke(item);
    }

    private void NotifyItemDestroyed(GridItem item)
    {
        OnGridItemDestroyed?.Invoke(item);
    }

    public GameObject CreateGridItemBase(string name)
    {
        if (gridItemPrefab == null)
        {
            return null;
        }

        if (itemsContainer == null)
        {
            itemsContainer = new GameObject("Items Container");
            itemsContainer.transform.SetParent(transform);
            itemsContainer.transform.localPosition = Vector3.zero;
        }

        GameObject itemObj = Instantiate(gridItemPrefab, itemsContainer.transform);
        itemObj.name = name;
        
        itemObj.transform.localScale *= GridManager.Instance.GridScaleMultiplier;
        return itemObj;
    }

    public GridItem CreateProducedItem(Vector2Int gridPosition, int level, ProducedItemProperties producedItemProperties)
    {
        var emptyPos = GridManager.Instance.FindNearestEmptyCell(gridPosition);
        if (!emptyPos.HasValue || !GridManager.Instance.Cells.ContainsKey(emptyPos.Value)) 
            return null;

        GameObject itemObj = CreateGridItemBase("Produced Item");
        if (itemObj == null) return null;

        ProducedItem item = itemObj.AddComponent<ProducedItem>();
        item.Initialize(producedItemProperties, level);
        
        if (GridManager.Instance.TryPlaceItemInCell(emptyPos.Value, item))
        {
            var cell = GridManager.Instance.Cells[emptyPos.Value];
            cell.SetItem(item);
            item.SetGridPosition(emptyPos.Value, cell);
            NotifyItemCreated(item);
            return item;
        }
        
        Destroy(itemObj);
        return null;
    }

    public GridItem CreateProducedItemWithAnimation(Vector2Int gridPosition, int level, Vector3 startPosition, ProducedItemProperties producedItemProperties)
    {
        var emptyPos = GridManager.Instance.FindNearestEmptyCell(gridPosition);
        if (!emptyPos.HasValue || !GridManager.Instance.Cells.ContainsKey(emptyPos.Value)) 
            return null;

        GameObject itemObj = CreateGridItemBase("Produced Item");
        if (itemObj == null) return null;

        ProducedItem item = itemObj.AddComponent<ProducedItem>();
        ItemAnimator animator = itemObj.AddComponent<ItemAnimator>();
        item.Initialize(producedItemProperties, level);
        
        if (GridManager.Instance.TryPlaceItemInCell(emptyPos.Value, item))
        {
            var cell = GridManager.Instance.Cells[emptyPos.Value];
            cell.SetItem(item);
            item.SetGridPosition(emptyPos.Value, cell);
            
            animator.AnimateProduction(startPosition, cell.transform.position);
            NotifyItemCreated(item);
            return item;
        }
        
        Destroy(itemObj);
        return null;
    }

    public GridItem CreateResourceItem(Vector2Int gridPosition, BaseItemProperties itemProperties, int level)
    {
        var emptyPos = GridManager.Instance.FindNearestEmptyCell(gridPosition);
        if (!emptyPos.HasValue || !GridManager.Instance.Cells.ContainsKey(emptyPos.Value)) 
            return null;

        GameObject itemObj = CreateGridItemBase($"{itemProperties.itemType} Item");
        if (itemObj == null) return null;

        ResourceItem item = itemObj.AddComponent<ResourceItem>();
        item.Initialize(itemProperties, level);
        
        if (GridManager.Instance.TryPlaceItemInCell(emptyPos.Value, item))
        {
            var cell = GridManager.Instance.Cells[emptyPos.Value];
            cell.SetItem(item);
            item.SetGridPosition(emptyPos.Value, cell);
            NotifyItemCreated(item);
            return item;
        }
        
        Destroy(itemObj);
        return null;
    }

    public GridItem CreateMergedItem(Vector2Int gridPosition, ItemType type, int level, BaseItemProperties sourceProperties = null)
    {
        if (sourceProperties == null)
        {
            Debug.LogError("Source properties are null!");
            return null;
        }

        GameObject itemObj = CreateGridItemBase($"Merged {type} Item");
        if (itemObj == null) return null;

        GridItem item = null;
        switch (type)
        {
            case ItemType.ProducedItem:
                item = itemObj.AddComponent<ProducedItem>();
                break;
            case ItemType.Energy:
            case ItemType.Coin:
                item = itemObj.AddComponent<ResourceItem>();
                break;
            case ItemType.Producer:
                item = itemObj.AddComponent<ProducerItem>();
                break;
            case ItemType.Chest:
                item = itemObj.AddComponent<ChestItem>();
                break;
        }

        if (item != null)
        {
            item.Initialize(sourceProperties, level);

            if (GridManager.Instance.TryPlaceItemInCell(gridPosition, item, force: true))
            {
                NotifyItemCreated(item);
                return item;
            }
        }
        
        Destroy(itemObj);
        return null;
    }

    public GridItem CreateProducerItem(Vector2Int gridPosition, int level = 1, ProducerItemProperties producerProperties = null)
    {
        var emptyPos = GridManager.Instance.FindNearestEmptyCell(gridPosition);
        if (!emptyPos.HasValue || !GridManager.Instance.Cells.ContainsKey(emptyPos.Value)) 
            return null;

        GameObject itemObj = CreateGridItemBase("Producer Item");
        if (itemObj == null) return null;

        ProducerItem item = itemObj.AddComponent<ProducerItem>();
        item.Initialize(producerProperties, level);

        if (GridManager.Instance.TryPlaceItemInCell(emptyPos.Value, item))
        {
            var cell = GridManager.Instance.Cells[emptyPos.Value];
            cell.SetItem(item);
            item.SetGridPosition(emptyPos.Value, cell);
            NotifyItemCreated(item);
            return item;
        }
        
        Destroy(itemObj);
        return null;
    }

    public GridItem CreateChestItem(Vector2Int gridPosition, int level = 1, ChestItemProperties chestProperties = null)
    {
        var emptyPos = GridManager.Instance.FindNearestEmptyCell(gridPosition);
        if (!emptyPos.HasValue || !GridManager.Instance.Cells.ContainsKey(emptyPos.Value)) 
            return null;

        GameObject itemObj = CreateGridItemBase("Chest Item");
        if (itemObj == null) return null;

        ChestItem item = itemObj.AddComponent<ChestItem>();
        item.Initialize(chestProperties, level);

        if (GridManager.Instance.TryPlaceItemInCell(emptyPos.Value, item))
        {
            var cell = GridManager.Instance.Cells[emptyPos.Value];
            cell.SetItem(item);
            item.SetGridPosition(emptyPos.Value, cell);
            NotifyItemCreated(item);
            return item;
        }
        
        Destroy(itemObj);
        return null;
    }

    public void DestroyItem(GridItem item)
    {
        if (item != null)
        {
            item.MarkForDestruction();
            NotifyItemDestroyed(item);
            Destroy(item.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
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
