using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource MainTheme;
    public AudioSource Punch;
    public AudioSource FallBack;
    public AudioSource HitGround;
    public AudioSource Throw;
    public AudioSource Pickup;

    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        FallBack = this.transform.Find("Falling_Back").GetComponent<AudioSource>();
        Punch = this.transform.Find("Punch").GetComponent<AudioSource>();
        HitGround = this.transform.Find("Hitting_Ground").GetComponent<AudioSource>();
        Throw = this.transform.Find("Throwing_Projectile").GetComponent<AudioSource>();
        Pickup = this.transform.Find("sfx_sounds_button3").GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }
}