using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHealthBar : MonoBehaviour
{
    private GameObject healthBar;// Start is called before the first frame update
    void Start()
    {
        healthBar = this.transform.Find("HealthContainer").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthAmount(float amount)
    {
        healthBar.transform.localScale = new Vector3(amount, 1.0f, 1.0f);
    }
}
