using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject lifeBar;
    private Player player;

    void Start()
    {
        lifeBar = this.transform.Find("HealthContainer").gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeBar.transform.localScale = new Vector3(player.health / player.maxHealth, 1.0f, 1.0f);
    }
}
