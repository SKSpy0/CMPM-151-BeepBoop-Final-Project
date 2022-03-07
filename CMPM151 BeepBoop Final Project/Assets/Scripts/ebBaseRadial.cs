using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ebBaseRadial : eBulletBase
{
    public GameObject Boss;
    // Start is called before the first frame update

    public override void move()
    {
        // get inital vector
        Vector3 moveVector = this.gameObject.transform.up;
        // normalize
        moveVector.Normalize();
        // multiply by movespeed
        moveVector *= moveSpeed * Time.deltaTime;
        // apply to character
        this.transform.position += moveVector;

        Boss = GameObject.Find("BossEnemy");
    }
}
