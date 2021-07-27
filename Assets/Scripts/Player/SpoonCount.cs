using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpoonCount : MonoBehaviour
{
    // Start is called before the first frame update
    private Text spoonCountText;
    private Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spoonCountText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        spoonCountText.text = "x " + player.silverCount;
    }
}
