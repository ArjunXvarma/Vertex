using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    Player player;

    void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            if (player != null)
            {
                player.powerup = true;  
            }
            Destroy(this.gameObject);
        }
    }
}
