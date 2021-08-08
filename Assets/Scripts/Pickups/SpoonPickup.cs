using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonPickup : MonoBehaviour
{
    // Start is called before the first frame update
   private void OnTriggerEnter(Collider other)
   {
       if (other.tag != "Player") {
           return;
       }

       SoundManager.instance.Pickup.Play();
       Player player = other.GetComponent<Player>();
       player.AddSilver();
       Destroy(this.gameObject);
   }
}
