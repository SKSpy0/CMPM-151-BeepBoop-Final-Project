using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//************** use UnityOSC namespace...
using UnityOSC;
//*************

public class Boss : MonoBehaviour
{
    public GameObject player;
    public HealthBar healthBar;
    public int bossHealth = 100;
    private int bossMaxHealth;
    public float bulletCooldown;
    private float bulletTimer;

    [SerializeField] private AudioSource BGM;

    [SerializeField] private GameObject Bullet1;
    [SerializeField] private GameObject Bullet2;
    //[SerializeField] private GameObject Bullet3;
    //[SerializeField] private GameObject Bullet4;
    //[SerializeField] private GameObject Bullet5;
    //[SerializeField] private GameObject Bullet6;

    [SerializeField] private GameObject Square0;
    [SerializeField] private GameObject Square1;
    // [SerializeField] private GameObject Square2;
    // [SerializeField] private GameObject Square3;
    // [SerializeField] private GameObject Square4;
    // [SerializeField] private GameObject Square5;
    // [SerializeField] private GameObject Square6;
    // [SerializeField] private GameObject Square7;

    // movement vars
    bool postiveTravel = true;

    // base beat bullet variables
    private bool hasFiredBase = false;


    // Start is called before the first frame update
    void Start()
    {
        bulletTimer = bulletCooldown;
        bossMaxHealth = bossHealth;
        healthBar.SetMaxHealth(bossMaxHealth);
    }

    void Update()
    {
        BGM.pitch = ((1f-((float)bossHealth/(float)bossMaxHealth))*0.75f)+0.75f;
        //create audio sample data
        int numPartitions = 8;
        float[] aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = 512 / 2;

        //assign raw spectrum data into values within aveMag float array
        for(int i = 0; i < numDisplayedBins; i++){
            if(i < numDisplayedBins * (partitionIndx + 1) / numPartitions){
                aveMag[(int)partitionIndx] += AudioReact.spectrumData[i] / (512/numPartitions);
            }
            else{
                partitionIndx++;
                i--;
            }
        }

        //scale aveMag array higher
        for(int i = 0; i < numPartitions; i++){
            aveMag[i] *= 10000;
        }

        /*Debug.Log("Audio Spectrum Data:");
        Debug.Log("aveMag 0: " + aveMag[0]);
        Debug.Log("aveMag 1: " + aveMag[1]);*/

        // some data visualization tests here - David
        // Square0.gameObject.transform.localScale = new Vector3(0.5f,aveMag[0],1);
        // Square1.gameObject.transform.localScale = new Vector3(0.5f,aveMag[1],1);
        // Square2.gameObject.transform.localScale = new Vector3(0.5f,aveMag[2],1);
        // Square3.gameObject.transform.localScale = new Vector3(0.5f,aveMag[3],1);
        // Square4.gameObject.transform.localScale = new Vector3(0.5f,aveMag[4],1);
        // Square5.gameObject.transform.localScale = new Vector3(0.5f,aveMag[5],1);
        // Square6.gameObject.transform.localScale = new Vector3(0.5f,aveMag[6],1);
        // Square7.gameObject.transform.localScale = new Vector3(0.5f,aveMag[7],1);


        // bulletSpawning
        if(bulletTimer <= 0){
            if(aveMag[0] > 10){
                // goal is to focus this on main underlying beat
                // if need to shoot
                if(!hasFiredBase){
                    // then shoot
                    int shotangle = 0;
                    for(int i = 0; i < 36; i++)
                    {
                        Shoot(Bullet1,new Vector3(this.transform.position.x,this.transform.position.y,0), Quaternion.Euler(new Vector3(0, 0, shotangle)));
                        shotangle += 10;
                    }
                    hasFiredBase = true;
                }
                // else carry one
            }
            else
            {
                // if base drops kill the current wave and ready another
                hasFiredBase = false;
            }
            if(aveMag[1] > 10)
            {
                // fire a shotgun blast outward
                int shotangle = 180 - 45;
                for(int i = 0; i < 10; i++)
                {
                    Shoot(Bullet2,new Vector3(this.transform.position.x,this.transform.position.y,0), Quaternion.Euler(new Vector3(0, 0, shotangle)));
                    shotangle += 10;
                }

            }
            bulletTimer = bulletCooldown;
        }
        bulletTimer -= Time.deltaTime;
        //Debug.Log("Bullet Timer : " + bulletTimer);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Shoot(Bullet1,new Vector3(this.transform.position.x+Random.Range(-10,10),this.transform.position.y,0), Quaternion.Euler(new Vector3(0, 0, Random.Range(170,190))));
        // basic movement
        float moveSpeed = BGM.pitch;
        if(this.transform.position.x<=-5)
        {
            postiveTravel = true;
        }
        if(this.transform.position.x>=5)
        {
            postiveTravel = false;
        }
        if(!postiveTravel)
        {
            moveSpeed*=-1;
        }
        this.transform.position += new Vector3(moveSpeed*Time.deltaTime,0,0);

        if(bossHealth <= 0)
        {
            BGM.pitch = 0;
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/bossBoom", 1);
            Destroy(this.gameObject);
        }

    }

    public void takeDamage(int dmg)
    {
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/bossDamaged", 1);
        bossHealth -= dmg;
        healthBar.SetHealth(bossHealth);
    }

    void Shoot(GameObject Bullet, Vector3 Location, Quaternion Angle)
    {
        Instantiate(Bullet,Location,Angle);
    }
}
