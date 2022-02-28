using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eBulletBase : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5;
    [SerializeField] public int attackDamage = 5;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public virtual void move()
    {
        // does nothing
    }
}
