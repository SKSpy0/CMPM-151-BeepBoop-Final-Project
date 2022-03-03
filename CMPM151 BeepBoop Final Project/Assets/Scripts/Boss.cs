using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int bossHealth = 100;

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

    void Update()
    {
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

        //Debug.Log("Audio Spectrum Data:");
        //Debug.Log(aveMag[0]);

        if(aveMag[0] > 5){
            Shoot(Bullet1,new Vector3(this.transform.position.x+Random.Range(-10,10),this.transform.position.y,0), Quaternion.Euler(new Vector3(0, 0, Random.Range(170,190))));
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Shoot(Bullet1,new Vector3(this.transform.position.x+Random.Range(-10,10),this.transform.position.y,0), Quaternion.Euler(new Vector3(0, 0, Random.Range(170,190))));

        if(bossHealth <= 0)
        {
            Destroy(this.gameObject);
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
