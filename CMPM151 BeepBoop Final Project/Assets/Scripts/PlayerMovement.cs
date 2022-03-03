using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//************** use UnityOSC namespace...
using UnityOSC;
//*************

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;

    [SerializeField] private int playerHealth = 100;
    [SerializeField] private GameObject Pbullet;
    [SerializeField] private Transform bulletSpawn;

    [SerializeField] private float cooldownDuration = 0.1f;
    private float cooldown;

    public Text countText;

    //************* Need to setup this server dictionary...
  	Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog> ();
  	//*************

    // Start is called before the first frame update
    void Start()
    {
      //************* Instantiate the OSC Handler...
  	  OSCHandler.Instance.Init ();
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

        //************* Routine for receiving the OSC...
    		OSCHandler.Instance.UpdateLogs();
    		Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
    		servers = OSCHandler.Instance.Servers;

    		foreach (KeyValuePair<string, ServerLog> item in servers) {
    			// If we have received at least one packet,
    			// show the last received from the log in the Debug console
    			if (item.Value.log.Count > 0) {
    				int lastPacketIndex = item.Value.packets.Count - 1;

    				//get address and data packet
    				countText.text = item.Value.packets [lastPacketIndex].Address.ToString ();
    				countText.text += item.Value.packets [lastPacketIndex].Data [0].ToString ();

    			}
    		}
    }

    void Shoot()
    {
        if(cooldown<=0)
        {
            Instantiate(Pbullet,bulletSpawn.transform.position,Quaternion.identity);
            cooldown = cooldownDuration;
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/gunSound", 1);
        }
    }

    public void takeDamage(int dmg)
    {
        playerHealth -= dmg;
    }
}
