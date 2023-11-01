using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeAi_BossMovement : MonoBehaviour
{ 

    private float jumpDelay;
    public float jumpingPower = 24f;
    public float horizontalSpeedValue = 10f;
    

    private float horizontalSpeed;
    private int rand;

    [DoNotSerialize] public bool canJump = true;
    [DoNotSerialize] public bool isNotTeleporting = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;

    public Transform playerPos;
    public BossAi BossAi;

    //public float speed;


    // Update is called once per frame
    void Update()
    {
        
        //horizontalSpeed = BossAi.horizontalSpeed * horizontalSpeedValue;
        //horizontalSpeed = (transform.position.x - playerPos.position.x >= 0) ? -horizontalSpeedValue : horizontalSpeedValue;
        jumpDelay -=  Time.deltaTime;
        if(jumpDelay <= 0)
        {
            BossAi.RequestDecision();
        }
        horizontalSpeed = (BossAi.horizontalSpeed == 1) ? horizontalSpeedValue : -horizontalSpeedValue;
       /* if(IsGrounded() && canJump && isNotTeleporting && BossAi.teleport == 1 && jumpDelay <= 0)
        {
            anim.SetTrigger("BossTeleport");
            isNotTeleporting = false;
        }    */
        if (IsGrounded() && canJump && isNotTeleporting && BossAi.jump > 0 & jumpDelay <= 0)
        {
            //Debug.Log("jump: " + BossAi.jump);
            canJump = false;
            jump();
            
            /*if (Mathf.Abs(transform.position.x - playerPos.position.x) > 6 && teleportCount <= 0)
            {
                teleportCount = 6;
                isNotTeleporting = false;
                anim.SetTrigger("BossTeleport");
               // Invoke("teleport", jumpDelay);
            }
            else
                Invoke("jump", jumpDelay);
            print("jump");*/
        }
            
        /*else if (transform.position.x - playerPos.position.x == 0)
            rb.velocity = new Vector2(0, 0);*/

    }
    private void jump()
    {
        float adjJumpingPower = jumpingPower;
        if (BossAi.jump == 2)
        {
            adjJumpingPower *= 2;
            BossAi.teleportReset();
        }
        //canJump = false;
        anim.SetTrigger("BossJump");
        //print(transform.position.x - playerPos.position.x);

        rb.velocity = new Vector2(horizontalSpeed, adjJumpingPower);
        canJump = true;
        jumpDelay = 1.4f;

    }
    /*public void teleport()
    {
        //float y;

       // rand = Random.Range(0, 1);
        //y = (rand == 0) ? playerPos.position.y : playerPos.position.y + 4;

        transform.localPosition = new Vector2(playerPos.localPosition.x, playerPos.localPosition.y);
        BossAi.teleportReset();
        Debug.Log("tp");
    }*/
    
    private bool IsGrounded()
    {
        //Ground check
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
