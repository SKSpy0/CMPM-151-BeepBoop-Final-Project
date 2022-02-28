using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private int attackDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
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

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collision Detected");
        if(other.gameObject.CompareTag("Border"))
        {
            Destroy(this.gameObject);
        }

        if(other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Boss>().takeDamage(attackDamage);
            Destroy(this.gameObject);
        }
    }
}
