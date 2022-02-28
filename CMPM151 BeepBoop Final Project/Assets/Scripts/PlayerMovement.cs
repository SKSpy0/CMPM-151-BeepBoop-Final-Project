using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;

    [SerializeField] private int playerHealth = 100;
    [SerializeField] private GameObject Pbullet;
    [SerializeField] private Transform bulletSpawn;

    [SerializeField] private float cooldownDuration = 0.1f;
    private float cooldown;

    

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get inital vector
        Vector3 moveVector = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0);
        // normalize
        moveVector.Normalize();
        // multiply by movespeed
        moveVector *= moveSpeed * Time.deltaTime;
        // apply to character
        this.transform.position += moveVector;
    
        if(Input.GetButton("Jump"))
        {
            Shoot();
        }
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if(playerHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void Shoot()
    {
        if(cooldown<=0)
        {
            Instantiate(Pbullet,bulletSpawn.transform.position,Quaternion.identity);
            cooldown = cooldownDuration;
        }
    }

    public void takeDamage(int dmg)
    {
        playerHealth -= dmg;
    }
}
