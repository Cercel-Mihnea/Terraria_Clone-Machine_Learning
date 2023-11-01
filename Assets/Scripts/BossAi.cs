using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using Controller;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;

public class BossAi : Agent
{
    public float horizontalSpeed;
    public int jump;
    public int teleport;
    [SerializeField] public Transform player;
    public PlayerMovement playerMov;

    public int maxHealth = 100;
    public int damage = 33;
    private float timeBtwDamage = 1.2f;
    private float timeBtwJump = 5f;
    public int health;
    private float rewards = 0;
    private float distance;
    private float minx = 1.2f;
    private float maxx = 27f;

    //private PlayerMovement playerMov;
    private float playerHealth ;
    public bool isDead = false;
    private Rigidbody2D rb;

    public Slider healthBar;
    [SerializeField] private Tilemap background;
    public override void Initialize()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public override void OnEpisodeBegin()
    {
        //playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.localPosition = new Vector2(Random.Range(minx , maxx), 3.3f);
        transform.localPosition = new Vector2(Random.Range(2.7f, 26f), 4.4f);
        health = maxHealth;
        // playerMov.health = 100;
        playerHealth = 100;
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
        sensor.AddObservation((Vector2)transform.localPosition);
        sensor.AddObservation((Vector2)player.localPosition);
        sensor.AddObservation(rb.velocity);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        
        jump = actions.DiscreteActions[0];
        horizontalSpeed = actions.DiscreteActions[1];
        
        // transform.localPosition += new Vector3(horizontalSpeed, 0) * Time.deltaTime * 5;
    }
    public override void WriteDiscreteActionMask(IDiscreteActionMask actionMask)
    {
        if(transform.localPosition.x < 2.7f && distance >1.5f) 
        {
            actionMask.SetActionEnabled(1, 0, false);
        }
        if (transform.localPosition.x > 26.15f && distance > 1.5f)
        {
            actionMask.SetActionEnabled(1, 1, false);
        }
        if (timeBtwJump <= 0)
        {
           actionMask.SetActionEnabled(0, 2, true);
        }
        else
        {
            actionMask.SetActionEnabled(0, 2, false);
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        /*Debug.Log("Jump " + Input.GetButtonDown("Jump"));
        ActionSegment<int> discreteAction = actionsOut.DiscreteActions;
        ActionSegment<float> continuousAction = actionsOut.ContinuousActions;

        continuousAction[0] = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            discreteAction[0] = 1;
            Debug.Log("yaay");
        }*/
       
    }
    private void Update()
    {
        //Debug.Log("Jump " + horizontalSpeed);
        if (health <= 0)
        {
            isDead = true;
            EndEpisode();
        }
        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }
        if (timeBtwJump > 0)
            timeBtwJump-= Time.deltaTime;

        healthBar.value = health;

        distance = Vector2.Distance(transform.localPosition, player.localPosition);
        
        if(distance >5)
        {
            AddReward(-0.005f);
            rewards += 0.005f;
        }
        if(playerHealth <= 0)
        {
            //background.color = Color.red;
            //AddReward(3f);
            //Debug.Log(rewards);
            //rewards = 0;
            EndEpisode();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("DamageCheck") && isDead == false)
        {
            //AddReward(0.2f);
            //background.color = Color.green;
            //playerHealth -= damage;
            //EndEpisode();
            
            if (timeBtwDamage <= 0)
            {

                /*Vector3 pos = player.localPosition;
                if (pos.x < (minx + 3f))
                    player.localPosition = new Vector2(pos.x + 2, 3.3f);
                else if(pos.x > (maxx - 3f))
                    player.localPosition = new Vector2(pos.x - 2, 3.3f);
                else
                {
                    float[] values = { -1.5f, 1.5f };
                    int random = Random.Range(0, 2);
                    player.localPosition = new Vector2(pos.x + Random.Range(-3.5f, 3.5f), 3.3f);
                }
                */

                AddReward(0.2f);
                //playerHealth -= damage;
                Vector2 dir = new Vector2((player.localPosition - transform.localPosition).normalized.x, 0.3f);
                playerMov.knock(dir);
                playerMov.health -= damage;
                timeBtwDamage = 1.2f;
                background.color = Color.green;

                //Debug.Log("damage");
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Wall")
        {
            AddReward(-0.5f);
            background.color = Color.red;
            //EndEpisode();
            //AddReward(-0.1f);
            // Debug.Log("wall");

        }
    }
    public void teleportReset()
    {
        timeBtwJump = 5f;
    }
}
