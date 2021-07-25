using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScene : MonoBehaviour
{
    public int EnemyCount;

    private bool isActive;
    private int RemainingEnemies = 0;
    private List<Enemy> enemies;    // Start is called before the first frame update

    private Vector3 originalPosition;
    private int ENEMY_SCREEN_COUNT = 5;

    void Start()
    {
        enemies = new List<Enemy>();
        isActive = false;
        originalPosition = this.transform.position;
        Debug.Log("Position");
        Debug.Log(originalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) {
            return;
        }

        if (RemainingEnemies >= EnemyCount && enemies.Count <= 0) {
            return;
        }

        if (RemainingEnemies < EnemyCount) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (enemies.Count > 0) {
                return;
            }


            for(int i = 0; i < ENEMY_SCREEN_COUNT; i++) {
                LayerMask floorMask = LayerMask.GetMask("Platforms");
                RaycastHit hit;
                Vector3 startingPoint = this.GetComponent<BoxCollider>().center;

                if (Physics.Raycast(this.transform.position, new Vector3(0.0f, -100.0f, 0.0f), out hit, 100, floorMask))
                {
                    BoxCollider collider = hit.collider.gameObject.GetComponent<BoxCollider>();

                    if (collider != null)
                    {
                        Vector3 size = collider.size;
                        float x = i % 2 == 0 ? Camera.main.orthographicSize : -Camera.main.orthographicSize;
                        Vector3 transformPosition = hit.point - new Vector3(x, 0.5f, -size.z / 4);
                        enemies.Add(GameObject.Instantiate(PrefabsManager.instance.enemy, transformPosition, Quaternion.identity));
                        RemainingEnemies += 1;
                    }
                } 
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        isActive = true;    
    }

    public void Activate()
    {
        isActive = true;
    }
}
