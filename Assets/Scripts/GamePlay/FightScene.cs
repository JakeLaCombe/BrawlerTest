using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScene : MonoBehaviour
{
    public int EnemyCount;
    private int RemainingEnemies = 0;
    private WolfHowl wolfHowl;
    private List<WolfEnemy> enemies;
    private FightSceneState currentState;   // Start is called before the first frame update
    private Vector3 originalPosition;
    private int ENEMY_SCREEN_COUNT = 5;

    void Start()
    {
        enemies = new List<WolfEnemy>();
        originalPosition = this.transform.position;
        currentState = FightSceneState.INACTIVE;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case FightSceneState.WOLF_HOWL_INITIAL:
                InstantiateWolfHowl();
                break;
            case FightSceneState.WOLF_HOWL:
                CheckWolf();
                break;
            case FightSceneState.ENEMIES:
                ProcessEnemies();
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentState == FightSceneState.INACTIVE) {
            currentState = FightSceneState.WOLF_HOWL_INITIAL;
        }
    }

    private void InstantiateWolfHowl()
    {
        Vector3 position = GameObject.FindGameObjectWithTag("Player").transform.position;
        wolfHowl = GameObject.Instantiate(PrefabsManager.instance.wolfHowl, new Vector3(position.x + 2.0f, 10.0f, position.z), Quaternion.identity);
        currentState = FightSceneState.WOLF_HOWL;
    }

    private void CheckWolf()
    {
        if (wolfHowl == null) {
            currentState = FightSceneState.ENEMIES;        
        }
    }

    private void ProcessEnemies()
    {
        if (RemainingEnemies >= EnemyCount && enemies.Count <= 0) {
            currentState = FightSceneState.FINISHED;
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
}

public enum FightSceneState {
    INITIAL,
    INACTIVE,
    WOLF_HOWL_INITIAL,
    WOLF_HOWL,    
    ENEMIES,
    FINISHED
}