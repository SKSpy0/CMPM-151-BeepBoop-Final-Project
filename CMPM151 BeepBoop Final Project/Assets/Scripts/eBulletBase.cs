using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eBulletBase : MonoBehaviour
{
    public GameObject player;

    [SerializeField] public float moveSpeed = 5;
    [SerializeField] public int attackDamage = 5;

    private Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        // grab player object
        player = GameObject.Find("Player");

        // create random bullet typing
        // 0 - 2 = normal speed, straight, big
        // 3 - 4 = normal speed, targetted, big
        // 5 = faster speed, straight
        // 6 = faster speed, targetted
        int bulletType = Random.Range(0, 6);

        // generate moveVector depending on the bullet typing
        if(bulletType == 0 || bulletType == 1 || bulletType == 2){
            moveVector = -this.gameObject.transform.up;
        } else if(bulletType == 3 || bulletType == 4){
            moveVector = player.GetComponent<Transform>().position - this.gameObject.transform.position;
        } else if(bulletType == 5){
            moveVector = -this.gameObject.transform.up;
        } else {
            moveVector = player.GetComponent<Transform>().position - this.gameObject.transform.position;
        }

        // assign bullet speed if needed
        if(bulletType >= 5){
            moveSpeed *= 1.5f;
        }
        
        // make bullet bigger if needed
        if(bulletType <= 2){
            this.gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collision Detected");
        if(other.gameObject.CompareTag("Border"))
        {
            Destroy(this.gameObject);
        }

        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().takeDamage(attackDamage);
            Destroy(this.gameObject);
        }
    }

    void move()
    {
        // normalize
        moveVector.Normalize();
        // multiply by movespeed
        moveVector *= moveSpeed * Time.deltaTime;
        // apply to character
        this.transform.position += moveVector;
    }
}
