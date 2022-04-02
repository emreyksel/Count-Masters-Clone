using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject> enemys = new List<GameObject>();
    [HideInInspector] public bool isEnemyAlive = true;

    void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        if (enemys.Count == 0)
        {
            isEnemyAlive = false;
        }
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject newEnemy = Instantiate(enemy, EnemyPosition(), Quaternion.identity, transform);
            enemys.Add(newEnemy);
        }
    }

    public Vector3 EnemyPosition()
    {
        Vector3 pos = Random.insideUnitSphere * 0.1f;
        Vector3 newPos = transform.position + pos;
        newPos.y = 0.5f;
        return newPos;
    }
}
