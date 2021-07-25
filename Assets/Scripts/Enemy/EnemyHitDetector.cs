using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitDetector : MonoBehaviour
{
    // Start is called before the first frame update
   
    private WolfEnemy enemy;
    void Start()
    {
        enemy = this.transform.parent.GetComponent<WolfEnemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.SetHitTarget(other.gameObject);
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemy.SetHitTarget(null);
        }    
    }
}
