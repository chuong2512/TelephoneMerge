using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ContainerItem : GridItem
{
    public event Action OnContainerStateChanged;
    
    protected Dictionary<int, Dictionary<BaseItemProperties, int>> inventoryItemCounts = new Dictionary<int, Dictionary<BaseItemProperties, int>>();
    protected bool isRecharging = false;
    protected float rechargeProgress = 0f;
    protected int spawnsSinceLastRecharge = 0;
    
    protected ContainerItemDefinition ContainerProperties => properties as ContainerItemDefinition;
    protected ContainerItemLevel CurrentLevelData => ContainerProperties.GetLevelData(currentLevel);

    public float RechargeProgress => rechargeProgress / CurrentLevelData.rechargeTime;
    public bool IsRecharging => isRecharging;
    
    public IEnumerable<(BaseItemProperties properties, int level, int count)> GetContainedItems()
    {
        if (CurrentLevelData.itemCapacities == null) 
            yield break;
            
        foreach (var capacity in CurrentLevelData.itemCapacities)
        {
            if (inventoryItemCounts.TryGetValue(capacity.level, out var items))
            {
                if (items.TryGetValue(capacity.itemDefinition, out int count))
                {
                    yield return (capacity.itemDefinition, capacity.level, count);
                }
            }
        }
    }

    public override void Initialize(BaseItemProperties props, int level = 1)
    {
        if (!(props is ContainerItemDefinition))
        {
            Debug.LogError($"Incorrect properties type provided to {GetType().Name}");
            return;
        }

        base.Initialize(props, level);
        InitializeInventory();
        
        isReadyToMerge = currentLevel < props.maxLevel;

        if (CurrentLevelData.canSpawnItems)
        {
            FillInventory();
        }
    }

    protected virtual void InitializeInventory()
    {
        inventoryItemCounts.Clear();
        if (CurrentLevelData.itemCapacities != null)
        {
            foreach (var capacity in CurrentLevelData.itemCapacities)
            {
                if (!inventoryItemCounts.ContainsKey(capacity.level))
                {
                    inventoryItemCounts[capacity.level] = new Dictionary<BaseItemProperties, int>();
                }
                inventoryItemCounts[capacity.level][capacity.itemDefinition] = 0;
            }
        }
    }

    public override bool CanPerformAction()
    {
        if (InputManager.IsDragging)
            return false;
            
        if (CanSpawnItems())
        {
            bool hasItems = false;
            foreach (var levelItems in inventoryItemCounts.Values)
            {
                foreach (var count in levelItems.Values)
                {
                    if (count > 0)
                    {
                        hasItems = true;
                        break;
                    }
                }
                if (hasItems) break;
            }
            if (!hasItems) return false;
            
            if (!GameManager.Instance.HasEnoughEnergy(ContainerProperties.energyCost)) 
                return false;
                
            return true;
        }
        
        return false;
    }

    protected virtual bool CanSpawnItems()
    {
        return CurrentLevelData.canSpawnItems;
    }

    protected bool HasEmptyInventorySlots()
    {
        foreach (var capacity in CurrentLevelData.itemCapacities)
        {
            if (inventoryItemCounts[capacity.level][capacity.itemDefinition] < capacity.count)
            {
                return true;
            }
        }
        return false;
    }

    protected virtual void FillInventory()
    {
        bool inventoryChanged = false;
        
        foreach (var capacity in CurrentLevelData.itemCapacities)
        {
            int currentCount = inventoryItemCounts[capacity.level][capacity.itemDefinition];
            if (currentCount < capacity.count)
            {
                inventoryItemCounts[capacity.level][capacity.itemDefinition] = capacity.count;
                inventoryChanged = true;
            }
        }

        if (inventoryChanged)
        {
            NotifyStateChanged();
        }

        if (HasEmptyInventorySlots())
        {
            StartRecharge();
        }
        else if (inventoryChanged)
        {
            ParticleManager.Instance.AttachParticle("producerReady", gameObject);
        }
    }

    protected virtual void StartRecharge()
    {
        if (!isRecharging)
        {
            isRecharging = true;
            rechargeProgress = 0f;
            NotifyStateChanged();
        }
    }

    protected virtual void UpdateRecharge()
    {
        if (isRecharging)
        {
            rechargeProgress += Time.deltaTime;
            
            NotifyStateChanged();
            
            if (rechargeProgress >= CurrentLevelData.rechargeTime)
            {
                CompleteRecharge();
            }
        }
    }

    protected virtual void CompleteRecharge()
    {
        isRecharging = false;
        rechargeProgress = 0f;
        spawnsSinceLastRecharge = 0;
        FillInventory();
        ParticleManager.Instance.AttachParticle("producerReady", gameObject);
        NotifyStateChanged();
    }

    protected virtual void Update()
    {
        UpdateRecharge();
    }

    protected virtual bool SpawnSingleItem(ItemLevelCount capacity)
    {
        if (inventoryItemCounts[capacity.level][capacity.itemDefinition] > 0)
        {
            GridItem newItem = null;

            if (capacity.itemDefinition is ResourceItemProperties)
            {
                var emptyPos = GridManager.Instance.FindNearestEmptyCell(gridPosition);
                if (!emptyPos.HasValue) return false;

                GameObject itemObj = ItemManager.Instance.CreateGridItemBase($"{capacity.itemDefinition.itemType} Item");
                if (itemObj == null) return false;

                ResourceItem item = itemObj.AddComponent<ResourceItem>();
                ItemAnimator animator = itemObj.AddComponent<ItemAnimator>();
                item.Initialize(capacity.itemDefinition, capacity.level);
                
                if (GridManager.Instance.TryPlaceItemInCell(emptyPos.Value, item))
                {
                    var cell = GridManager.Instance.Cells[emptyPos.Value];
                    cell.SetItem(item);
                    item.SetGridPosition(emptyPos.Value, cell);
                    animator.AnimateProduction(transform.position, cell.transform.position);
                    ItemManager.Instance.NotifyItemCreated(item);
                    newItem = item;
                }
                else
                {
                    Destroy(itemObj);
                }
            }
            else if (capacity.itemDefinition is ProducedItemProperties producedItemProperties)
            {
                newItem = ItemManager.Instance.CreateProducedItemWithAnimation(gridPosition, capacity.level, transform.position, producedItemProperties);
            }

            if (newItem != null)
            {
                inventoryItemCounts[capacity.level][capacity.itemDefinition]--;
                OnItemSpawned();
                NotifyStateChanged();
                return true;
            }
        }
        return false;
    }

    protected virtual ItemLevelCount? SelectItemToSpawn()
    {
        List<ItemLevelCount> availableSlots = new List<ItemLevelCount>();
        
        foreach (var capacity in CurrentLevelData.itemCapacities)
        {
            if (inventoryItemCounts[capacity.level][capacity.itemDefinition] > 0)
            {
                availableSlots.Add(capacity);
            }
        }

        if (availableSlots.Count > 0)
        {
            return availableSlots[UnityEngine.Random.Range(0, availableSlots.Count)];
        }

        return null;
    }

    protected virtual void OnItemSpawned()
    {
        ShowParticleEffect("itemSpawned");
        SoundManager.Instance.PlaySound("itemSpawned", false, 1 + spawnsSinceLastRecharge * 0.1f);
        spawnsSinceLastRecharge++;
        if (!isRecharging && HasEmptyInventorySlots())
        {
            ParticleManager.Instance.StopAttachedParticles(gameObject, true);
            StartRecharge();
        }
    }

    protected virtual void NotifyStateChanged()
    {
        OnContainerStateChanged?.Invoke();
    }

    protected override void OnDestroy()
    {
        OnContainerStateChanged = null;
        base.OnDestroy();
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
