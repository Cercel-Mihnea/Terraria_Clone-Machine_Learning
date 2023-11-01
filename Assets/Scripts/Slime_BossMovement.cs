using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime_BossMovement : MonoBehaviour
{ 

    public float jumpDelay = 1;
    public float jumpingPower = 24f;
    public float horizontalSpeedValue = 10f;
    public int megaJumpCount = 2;

    private int teleportCount = 0;
    private int counter;
    private float horizontalSpeed;
    private int rand;

    [DoNotSerialize] public bool canJump = true;
    [DoNotSerialize] public bool isNotTeleporting = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;


    private Transform playerPos;

    //public float speed;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        counter = megaJumpCount + 1;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalSpeed = (transform.position.x - playerPos.position.x >= 0) ? -horizontalSpeedValue : horizontalSpeedValue;
        if (IsGrounded() && canJump && isNotTeleporting)
        {
            canJump = false;
            if (Mathf.Abs(transform.position.x - playerPos.position.x) > 6 && teleportCount <= 0)
            {
                teleportCount = 6;
                isNotTeleporting = false;
                anim.SetTrigger("BossTeleport");
               // Invoke("teleport", jumpDelay);
            }
            else
                Invoke("jump", jumpDelay);
            print("jump");
        }
            
        /*else if (transform.position.x - playerPos.position.x == 0)
            rb.velocity = new Vector2(0, 0);*/

    }
    private void jump()
    {
        float adjJumpingPower = jumpingPower;
        counter--;
        teleportCount--;
        if (counter == 0)
        {
            adjJumpingPower *= 2;
            counter = megaJumpCount;
        }
        //canJump = false;
        anim.SetTrigger("BossJump");
        //print(transform.position.x - playerPos.position.x);

        rb.velocity = new Vector2(horizontalSpeed, adjJumpingPower);
        canJump = true;
        
    }
    public void teleport()
    {
        //float y;

        /*rand = Random.Range(0, 1);
        y = (rand == 0 ) ? playerPos.position.y : playerPos.position.y + 4; */

        transform.position = new Vector2(playerPos.position.x, playerPos.position.y);
        

    }

    private bool IsGrounded()
    {
        //Ground check
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
