using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//************** use UnityOSC namespace...
using UnityOSC;
//*************

public class Boss : MonoBehaviour
{
    [SerializeField] private int bossHealth = 100;

    [SerializeField] private GameObject Bullet1;
    //[SerializeField] private GameObject Bullet2;
    //[SerializeField] private GameObject Bullet3;
    //[SerializeField] private GameObject Bullet4;
    //[SerializeField] private GameObject Bullet5;
    //[SerializeField] private GameObject Bullet6;


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Shoot(Bullet1,new Vector3(this.transform.position.x+Random.Range(-10,10),this.transform.position.y,0), Quaternion.Euler(new Vector3(0, 0, Random.Range(170,190))));

        if(bossHealth <= 0)
        {
            Destroy(this.gameObject);
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/bossBoom", 1);
        }

    }

    public void takeDamage(int dmg)
    {
        bossHealth -= dmg;
    }

    void Shoot(GameObject Bullet, Vector3 Location, Quaternion Angle)
    {
        Instantiate(Bullet,Location,Angle);
    }
}
