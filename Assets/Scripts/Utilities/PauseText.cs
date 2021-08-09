using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseText : MonoBehaviour
{
    
    private Text pauseText;
    // Start is called before the first frame update
    void Start()
    {
        pauseText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        pauseText.enabled = Time.deltaTime == 0;
    }
}
