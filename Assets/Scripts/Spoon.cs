using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Spoon : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidBody;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!spriteRenderer.isVisible)
        {
            // Destroy(this.gameObject);
        }
    }

    public void StartThrow(float direction)
    {
        rigidBody.velocity = new Vector3(direction * 6.0f, 0.0f, 0.0f);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            WolfEnemy enemy = other.GetComponent<WolfEnemy>();
            enemy.InstantKill(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
