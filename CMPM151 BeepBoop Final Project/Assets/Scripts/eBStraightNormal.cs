using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eBStraightNormal : eBulletBase
{
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
    }
}
