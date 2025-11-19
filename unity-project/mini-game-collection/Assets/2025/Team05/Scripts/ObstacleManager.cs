using MiniGameCollection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject lowWallPrefab;
    public GameObject midWallPrefab;
    public GameObject highWallPrefab;

    public Transform highSpawn;
    public Transform midSpawn;
    public Transform lowSpawn;

    public Transform obstacleParent;

    public List<GameObject> obstacles = new List<GameObject>();

    [field: SerializeField] public MiniGameManager GameManager { get; private set; }


    float timer = 0f;
    float interval = 1f;

    public float moveSpeed = 10f;
    bool isMoveSpeedNegative = false;


    void Start()
    {
        if (moveSpeed < 0)
            isMoveSpeedNegative = true;
    }

    void Update()
    {
        HandleTimer();
        HandleObstacles();
    }

    void HandleTimer()
    {
        timer += Time.deltaTime;

        //Debug.Log($"{timer}, {timer % interval <= 0.5}");

        if (timer >= interval)
        {
            SpawnObstacle();
            timer -= interval;
        }
    }

    void SpawnObstacle()
    {
        // 0 = low, 1 = mid, 2 = high

        int randomSpawn = Random.Range(0, 3);

        if (GameManager.State != MiniGameManagerState.TimerRunning)
            return;

        if (randomSpawn == 0)
        {
            GameObject obstacle = Instantiate(lowWallPrefab);

            if (isMoveSpeedNegative)
            {
                obstacle.transform.localScale = new Vector3(-1, -1, 0);
            }

            obstacle.transform.SetParent(obstacleParent, true);
            obstacle.transform.position = lowSpawn.position;
            obstacles.Add(obstacle);
        }
        else if (randomSpawn == 1)
        {
            GameObject obstacle = Instantiate(midWallPrefab);

            if (isMoveSpeedNegative)
            {
                obstacle.transform.localScale = new Vector3(-1, -1, 0);
            }

            obstacle.transform.SetParent(obstacleParent, true);
            obstacle.transform.position = midSpawn.position;
            obstacles.Add(obstacle);
        }
        else if (randomSpawn == 2)
        {
            GameObject obstacle = Instantiate(highWallPrefab);

            if (isMoveSpeedNegative)
            {
                obstacle.transform.localScale = new Vector3(-1, -1, 0);
            }

            obstacle.transform.SetParent(obstacleParent, true);
            obstacle.transform.position = highSpawn.position;
            obstacles.Add(obstacle);
        }
    }

    void HandleObstacles()
    {
        for (int i = 0;  i < obstacles.Count; i++)
        {
            obstacles[i].transform.position = new Vector3(obstacles[i].transform.position.x, obstacles[i].transform.position.y + moveSpeed * Time.deltaTime, obstacles[i].transform.position.z);
        }
    }
}
