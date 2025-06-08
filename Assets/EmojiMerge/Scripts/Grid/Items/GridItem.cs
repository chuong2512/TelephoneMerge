using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class GridItem : MonoBehaviour
{
    public BaseItemProperties properties;
    protected Cell occupiedCell;
    protected Vector2Int gridPosition;
    
    protected int currentLevel = 1;
    protected bool isReadyToMerge = true;
    protected SpriteRenderer spriteRenderer;
    protected GameObject tickMark;
    protected GameObject mergeHighlight;
    private bool isBeingMerged = false;
    protected bool isPendingDestruction = false;
    private HashSet<Order> markingOrders = new HashSet<Order>();

    public event Action<GridItem> OnItemDestroyed;

    public int CurrentLevel => currentLevel;
    public bool IsReadyToMerge => isReadyToMerge;
    public Cell OccupiedCell => occupiedCell;
    public Vector2Int GridPosition => gridPosition;
    public bool CanMerge(GridItem other) => MergeManager.Instance.CanMergeItems(this, other);
    public bool IsMarkedForDelivery => markingOrders.Count > 0;
    public bool IsPendingDestruction => isPendingDestruction;
    public IReadOnlyCollection<Order> MarkingOrders => markingOrders;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError($"SpriteRenderer component missing on GridItem {gameObject.name}");
        }
        tickMark = transform.Find("Mark")?.gameObject;
        mergeHighlight = transform.Find("GlowMask")?.gameObject;
    }

    protected virtual void Start()
    {
        UpdateVisuals();
        
        if (GetComponent<ItemAnimator>() == null)
        {
            Vector3 targetScale = transform.localScale;
            transform.localScale = Vector3.zero;
            StartCoroutine(InitialScaleAnimation(targetScale));
        }
    }

    private IEnumerator InitialScaleAnimation(Vector3 targetScale)
    {
        float duration = 0.3f;
        float bounceScale = 1.2f;

        yield return StartCoroutine(ScaleWithEase(Vector3.zero, targetScale * bounceScale, duration * 0.4f));
        
        yield return StartCoroutine(ScaleWithEase(targetScale * bounceScale, targetScale, duration * 0.6f));
    }

    private IEnumerator ScaleWithEase(Vector3 start, Vector3 end, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            t = t * t * (3f - 2f * t);
            
            transform.localScale = Vector3.Lerp(start, end, t);
            yield return null;
        }
        transform.localScale = end;
    }

    public virtual void Initialize(BaseItemProperties props, int level = 1)
    {
        properties = props;
        currentLevel = Mathf.Clamp(level, 1, props.maxLevel);
        UpdateVisuals();
    }

    public virtual void SetGridPosition(Vector2Int position, Cell cell)
    {
        gridPosition = position;
        occupiedCell = cell;
        if (cell != null)
        {
            transform.position = cell.transform.position;
        }
    }

    public virtual void SetMergeState(bool isMerging)
    {
        if (isMerging && !isBeingMerged)
        {
            isBeingMerged = true;
            ClearAllMarks();
        }
        isBeingMerged = isMerging;
    }

    public bool AddOrderMark(Order order)
    {
        if (!isPendingDestruction && !isBeingMerged)
        {
            if (markingOrders.Contains(order))
                return true;

            if (order.CanBeCompleted)
                return false;

            markingOrders.Add(order);
            UpdateDeliveryVisual();
            return true;
        }
        return false;
    }

    public bool RemoveOrderMark(Order order)
    {
        if (!markingOrders.Contains(order))
            return false;

        bool removed = markingOrders.Remove(order);
        if (removed)
        {
            UpdateDeliveryVisual();
            
            var incompleteOrders = OrderManager.Instance.GetCurrentOrders()
                .Where(o => o != order && 
                       !o.CanBeCompleted && 
                       o.RequiresItem(properties, currentLevel));
                       
            foreach (var otherOrder in incompleteOrders)
            {
                otherOrder.TryMarkAvailableItems();
            }
        }
        return removed;
    }

    public void ClearAllMarks()
    {
        var orders = markingOrders.ToList();
        foreach (var order in orders)
        {
            order.OnMarkedItemDestroyed(this);
        }
        markingOrders.Clear();
        UpdateDeliveryVisual();
    }

    public void ShowMergeHighlight()
    {
        if (mergeHighlight != null)
        {
            mergeHighlight.SetActive(true);
        }
    }

    public void ClearMergeHighlight()
    {
        if (mergeHighlight != null)
        {
            mergeHighlight.SetActive(false);
        }
    }

    protected virtual void OnDestroy()
    {
        var affectedOrders = markingOrders.ToList();
        markingOrders.Clear();
        UpdateDeliveryVisual();
        
        foreach (var order in affectedOrders)
        {
            order.OnMarkedItemDestroyed(this);
        }
        
        if (occupiedCell != null && !isBeingMerged)
        {
            GridManager.Instance?.ClearCell(gridPosition);
        }
        
        OnItemDestroyed?.Invoke(this);
    }

    public virtual void OnTapped() 
    { 
    }

    public virtual void OnTouchStart() 
    { 
        UIManager.Instance.OpenItemDetailsPane(this);
        SoundManager.Instance.PlaySound("tapItem");
    }

    public virtual void OnDragStart() 
    { 
        SoundManager.Instance.PlaySound("dragStarted");
    }

    public virtual bool CanPerformAction()
    {
        return true;
    }

    protected virtual void UpdateVisuals()
    {
        if (spriteRenderer && properties != null && properties.levelSprites != null && 
            currentLevel <= properties.levelSprites.Length)
        {
            spriteRenderer.sprite = properties.levelSprites[currentLevel - 1];
        }
    }

    protected virtual void UpdateDeliveryVisual()
    {
        if (tickMark != null)
        {
            tickMark.SetActive(markingOrders.Any());
        }
    }

    public Sprite GetNextLevelSprite()
    {
        if (properties != null && properties.levelSprites != null &&
            currentLevel < properties.maxLevel)
        {
            return properties.levelSprites[currentLevel];
        }
        return null;
    }

    protected void ShowParticleEffect(string effectType)
    {
        ParticleManager.Instance?.SpawnParticle(effectType, transform.position);
    }

    public void MarkForDestruction()
    {
        if (!isPendingDestruction)
        {
            isPendingDestruction = true;
            ClearAllMarks();
        }
    }

    public void UnblockAdjacentCells()
    {
        if (occupiedCell != null)
        {
            var adjacentCells = GridManager.Instance.GetAdjacentCells(occupiedCell.GridPosition);
            foreach (var cell in adjacentCells)
            {
                cell.UnblockCell();
            }
        }
    }

    public void ItemUnblocked()
    {
        OrderManager.Instance.OnItemUnblocked(this);
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
