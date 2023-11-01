using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportExit : StateMachineBehaviour
{
    private SlimeAi_BossMovement boss;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<SlimeAi_BossMovement>();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("yay");
        boss.isNotTeleporting = true;
        boss.canJump = true;
    }
}
