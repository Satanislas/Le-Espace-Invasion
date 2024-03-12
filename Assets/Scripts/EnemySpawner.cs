using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static List<GameObject> enemies;
    public List<GameObject> enemyPrefabs;
    public int rowCount;
    public int columnCount;
    public float distanceBetweenEachEnemy;
    public float cycleTime;
    public float maxCycletime;
    private float cycleTimer;
    public int rowMoveAmount;
    private int moveCount;
    private int direction; // 1 for right or -1 for left
    public float step;
    private float initialEnemyNumber;

    private Vector3 startPos;
    private bool isRespawning;

    private void Start()
    {
        if (startPos == new Vector3())
        {
            startPos = this.transform.position;
        }
        else
        {
            transform.position = startPos;
        }
        
        moveCount = rowMoveAmount / 2;
        direction = 1;
        enemies = new List<GameObject>();
        cycleTimer = cycleTime;
        float yOffset = 0;
        isRespawning = false;
        for (int i = 0; i < rowCount; i++)
        {
            float start = (float)(transform.position.x - 0.5 * (distanceBetweenEachEnemy) * (columnCount-1));
            for (int j = 0; j < columnCount; j++)
            {
                GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],
                    new Vector3(start + distanceBetweenEachEnemy * j,transform.position.y - yOffset,0),
                    Quaternion.identity,
                    transform);
                enemies.Add(enemy);
            }

            yOffset += distanceBetweenEachEnemy;
        }

        initialEnemyNumber = enemies.Count;
    }

    private void Update()
    {
        cycleTimer -= Time.deltaTime;
        if (cycleTimer <= 0)
        {
            Debug.Log("move");
            Move();
            if (enemies.Count == 0 && !isRespawning)
            {
                isRespawning = true;
                StartCoroutine(Respawn());
            }
        }
    }

    void Move()
    {
        //nothing to move ?
        if (enemies.Count == 0) return;
        
        //move down
        if (moveCount == rowMoveAmount)
        {
            transform.position += Vector3.down * step;
            direction *= -1;
            moveCount = 0;
        }
        else //move left or right
        {
            transform.position += Vector3.right * (direction*step);
            moveCount++;
        }

        int enemyNumber = enemies.Count;
        cycleTimer = enemyNumber / initialEnemyNumber * cycleTime + (1-enemyNumber/initialEnemyNumber) * maxCycletime;
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        Start();
    }
}
