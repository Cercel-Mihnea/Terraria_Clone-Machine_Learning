using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    public GameObject projectile;
    public Transform Trajectory;
    public float speed = 30;
    private float wallCheck = 1;
    public float timesBetweenShots;

    private bool shooting, readytoShoot ;
    public bool allowInvoke = true;

    public int bulletsShot;
    public int bulletsPerTap = 1;

    public AudioClip WallHit;


    // Update is called once per frame
    private void Awake()
    {
        readytoShoot = true;
    }
    void Update()
    {
        shooting = Input.GetKey(KeyCode.Mouse0);
        if (shooting && wallCheck ==1 && readytoShoot)
        {
            bulletsShot = 0;
            Shoot();
        }
        else if (shooting && wallCheck != 1 && readytoShoot)
        {
            bulletsShot = 0;
            PlaySound();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        wallCheck = -1;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        wallCheck = 1;
    }
    private void Shoot()
    {
        readytoShoot = false;

        Quaternion rotation = transform.rotation;
        rotation.eulerAngles += new Vector3(0, 0, -90);
        GameObject currentBullet = Instantiate(projectile, transform.position, rotation);  
        currentBullet.GetComponent<Rigidbody2D>().velocity = (transform.position - Trajectory.position) * speed;

        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timesBetweenShots);
            allowInvoke = false;
        }
        if(bulletsShot < bulletsPerTap)
        Invoke("Shoot", timesBetweenShots);
    }
    private void PlaySound()
    {
        readytoShoot = false;

        AudioSource.PlayClipAtPoint(WallHit, transform.position, 1);

        bulletsShot++;
        if (allowInvoke)
        {
            Invoke("ResetShot", timesBetweenShots);
            allowInvoke = false;
        }
        if (bulletsShot < bulletsPerTap)
            Invoke("PlaySound", timesBetweenShots);
    }
    private void ResetShot()
    {
        readytoShoot = true;
        allowInvoke = true;
    }
}
