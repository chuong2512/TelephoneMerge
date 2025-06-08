using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    private Vector2Int gridPosition;
    private SpriteRenderer spriteRenderer;
    private GridItem currentItem;
    private bool isCellBlocked = false;
    private GameObject cellBlocker;

    public bool IsCellBlocked => isCellBlocked;
    public Vector2Int GridPosition => gridPosition;
    public GridItem CurrentItem => currentItem;
    public bool IsOccupied => currentItem != null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cellBlocker = transform.Find("Obstacle").gameObject;
    }

    public void Initialize(Vector2Int position)
    {
        gridPosition = position;
        ApplyStyling();
    }

    private void ApplyStyling()
    {
        if (GridManager.Instance != null && GridManager.Instance.Styling != null)
        {
            spriteRenderer.color = GridManager.Instance.Styling.GetCellColor(gridPosition);
        }
    }

    public void SetItem(GridItem item)
    {
        if (currentItem == item)
            return;
            
        if (currentItem != null)
        {
            currentItem = null;
        }
        
        currentItem = item;
    }

    public void ClearItem()
    {
        if (currentItem != null)
        {
            if (currentItem.OccupiedCell == this)
            {
                currentItem = null;
            }
        }
    }

    public void BlockCell()
    {   
        cellBlocker.SetActive(true);
        isCellBlocked = true;
    }

    public void UnblockCell()
    {
        if (!isCellBlocked) return;
        cellBlocker.SetActive(false);
        isCellBlocked = false;
        ParticleManager.Instance.SpawnParticle("cellUnblocked", transform.position);
        SoundManager.Instance.PlaySound("cellUnblocked");
        if (currentItem != null)
        {
            currentItem.ItemUnblocked();
        }
        GridManager.Instance.CellUnblocked(gridPosition);
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
