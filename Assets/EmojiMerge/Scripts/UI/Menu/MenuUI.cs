using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        SoundManager.Instance.PlayBGM("MenuBGM");
    }

    public void PlaySound(string soundName)
    {
        SoundManager.Instance.PlaySound(soundName);
    }

    public void IdleAnimation()
    {
        animator.Play("MenuIdle");
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
