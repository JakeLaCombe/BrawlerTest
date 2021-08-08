using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelTrigger : MonoBehaviour, IFightEndEvent
{
    private Player player;
    private Vector3 endTarget;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        endTarget = this.transform.Find("EndTarget").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateEvent()
    {
        this.player.moveToEnd(endTarget);
    }

}
