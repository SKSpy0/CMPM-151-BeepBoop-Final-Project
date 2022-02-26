using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get inital vector
        Vector3 moveVector = new Vector3(0,1,0);
        // normalize
        moveVector.Normalize();
        // multiply by movespeed
        moveVector *= moveSpeed * Time.deltaTime;
        // apply to character
        this.transform.position += moveVector;
    }

    
}
