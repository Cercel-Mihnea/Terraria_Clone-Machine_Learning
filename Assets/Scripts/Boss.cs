/*using Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public int maxHealth = 100;
    public int damage = 33;
    private float timeBtwDamage = 1.5f;
    [NonSerialized] public int health;


    public Slider healthBar;

    private PlayerMovement player;
    public bool isDead;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        health = maxHealth;
    }

    private void Update()
    {
        if(health <=0)
        {
            isDead = true;
            Destroy(gameObject);
        }
        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0) {
            timeBtwDamage -= Time.deltaTime;
        }

        healthBar.value = health;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("DamageCheck") && isDead == false)
        {
            if (timeBtwDamage <= 0)
            {
                timeBtwDamage = 1.5f;
                player.health -= damage;
                Debug.Log("damage");
            }
        }
    }
}
*/