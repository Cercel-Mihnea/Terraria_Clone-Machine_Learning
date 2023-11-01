using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public int damage;
    public float destroyTime;
    public float delay;
    public float volume;

    public AudioClip WallHit;
    public AudioClip Spawn;

    public void Start()
    {
       //projectile sound
        AudioSource.PlayClipAtPoint(Spawn, transform.position,volume/2);
    }

    private void Update()
    {
       //Destroy(gameObject, destroyTime);
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            AudioSource.PlayClipAtPoint(WallHit,transform.position, volume);
            Object.Destroy(gameObject, delay);
        }
        if(collision.gameObject.name == "Boss")
        {
            collision.GetComponent<Boss>().health -= damage;
            print("damaged");
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        //play sound when hitting wall
        if (other.CompareTag("Tilemap"))
        {
            AudioSource.PlayClipAtPoint(WallHit, transform.position, volume);
            Object.Destroy(gameObject, delay);
        }
        //deal damage to boss
        if (other.CompareTag("Boss"))
        {
            other.GetComponent<BossAi>().health -= damage;

        }
    }
}
