using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class TeleportEnter : StateMachineBehaviour
{
    
    private SlimeAi_BossMovement boss;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            animator.SetTrigger("BossExitTeleport");
            //boss.teleport();
        }
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<SlimeAi_BossMovement>();
    }

    
}
