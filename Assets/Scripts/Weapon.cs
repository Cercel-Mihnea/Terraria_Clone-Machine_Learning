using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform shotPoint;

    private int rotationOffset = 0;


    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0)
        {
            rotationOffset = 0;
        }
        else if (horizontal < 0)
        {
            rotationOffset = 180;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

    }
}
