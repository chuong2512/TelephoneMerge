using UnityEngine;
using System.Collections;

public class ItemAnimator : MonoBehaviour
{
    private const float MOVE_DURATION = 0.25f;
    private const float POP_DURATION = 0.08f;
    private const float BOUNCE_DURATION = 0.15f;
    private const float BOUNCE_SCALE_RATIO = 1.3f;
    private const int ANIMATION_SORTING_ORDER = 100;
    
    private Vector3 finalScale;
    private SpriteRenderer spriteRenderer;
    private int defaultSortingOrder;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            defaultSortingOrder = spriteRenderer.sortingOrder;
        }
    }

    public void AnimateProduction(Vector3 startPos, Vector3 targetPos)
    {
        finalScale = transform.localScale;
        transform.position = startPos;
        transform.localScale = Vector3.zero;
        
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = ANIMATION_SORTING_ORDER;
        }
        
        StartCoroutine(ProductionAnimationSequence(startPos, targetPos));
    }
    
    private IEnumerator ProductionAnimationSequence(Vector3 startPos, Vector3 targetPos)
    {
        yield return StartCoroutine(ScaleAnimation(Vector3.zero, finalScale, POP_DURATION));
        
        yield return StartCoroutine(MoveAnimation(startPos, targetPos, MOVE_DURATION));
        
        yield return StartCoroutine(BounceAnimation());

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = defaultSortingOrder;
        }
    }

    private IEnumerator MoveAnimation(Vector3 start, Vector3 end, float duration)
    {
        float elapsed = 0;
        float maxHeight = 0.5f;
        Vector3 midPoint = (start + end) * 0.5f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            float easeT = 1 - (1 - t) * (1 - t);
            
            float heightT = Mathf.Sin(t * Mathf.PI);
            Vector3 arcOffset = Vector3.up * (heightT * maxHeight);
            
            transform.position = Vector3.Lerp(start, end, easeT) + arcOffset;
            yield return null;
        }
        
        transform.position = end;
    }

    private IEnumerator ScaleAnimation(Vector3 start, Vector3 end, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            transform.localScale = Vector3.Lerp(start, end, t);
            yield return null;
        }
        transform.localScale = end;
    }

    private IEnumerator BounceAnimation()
    {
        Vector3 bounceScale = finalScale * BOUNCE_SCALE_RATIO;
        
        yield return StartCoroutine(ScaleAnimation(finalScale, bounceScale, BOUNCE_DURATION * 0.4f));
        yield return StartCoroutine(ScaleAnimation(bounceScale, finalScale, BOUNCE_DURATION * 0.6f));
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
