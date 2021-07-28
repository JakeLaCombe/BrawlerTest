using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    void Start()
    {
        player = this.transform.parent.GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            player.AddHitTarget(other.gameObject);
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            player.RemoveHitTarget(other.gameObject);
        }    
    }
}
