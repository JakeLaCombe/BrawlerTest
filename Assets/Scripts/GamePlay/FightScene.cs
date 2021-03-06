using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FightScene : MonoBehaviour
{
    public int EnemyCount;
    public CinemachineVirtualCamera swapCamera;
    private int RemainingEnemies = 0;
    private WolfHowl wolfHowl;
    private List<WolfEnemy> enemies;
    private FightSceneState currentState;   // Start is called before the first frame update
    private Vector3 originalPosition;

    public GameObject[] spawnPoints;
    private int ENEMY_SCREEN_COUNT = 5;

    void Start()
    {
        enemies = new List<WolfEnemy>();
        originalPosition = this.transform.position;
        currentState = FightSceneState.INACTIVE;

        spawnPoints = getSpawnPoints();
    }

    public GameObject[] getSpawnPoints()
    {
        List<GameObject> spawnPoints = new List<GameObject>();
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.tag == "FightSpawnPoint") {
                spawnPoints.Add(t.gameObject);
            }
        }   
        
        return spawnPoints.ToArray();
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
            case FightSceneState.FINISHED:
                currentState = FightSceneState.TRANSITIONING;
                StartCoroutine(changeCamera());
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") {
            return;
        }

        if (currentState == FightSceneState.INACTIVE) {
            currentState = FightSceneState.WOLF_HOWL_INITIAL;
        }
    }

    private void InstantiateWolfHowl()
    {
        Vector3 position = this.transform.position;
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
        List<WolfEnemy> enemiesToRemove = new List<WolfEnemy>();

        for(int i = 0; i < enemies.Count; i++) {
            if (enemies[i] == null) {
                enemiesToRemove.Add(enemies[i]);
            }
        }

        foreach(WolfEnemy enemy in enemiesToRemove)
        {
            enemies.Remove(enemy);
        }

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
                        Vector3 transformPosition = spawnPoints[i].transform.position - new Vector3(0.0f, -5.0f, 0);
                        enemies.Add(GameObject.Instantiate(PrefabsManager.instance.enemy, transformPosition, Quaternion.identity));
                        RemainingEnemies += 1;
                    }
                } 
            }
        }
    }

    private IEnumerator changeCamera()
    {
        yield return new WaitForSeconds(2.0f);
        CameraSwap.SwapCamera(swapCamera);

        IFightEndEvent lastEvent = GetComponent<IFightEndEvent>();

        if (lastEvent != null) {
            lastEvent.ActivateEvent();
        }

        Destroy(this.gameObject);
    }
}

public enum FightSceneState {
    INITIAL,
    INACTIVE,
    WOLF_HOWL_INITIAL,
    WOLF_HOWL,    
    ENEMIES,
    FINISHED,
    TRANSITIONING
}