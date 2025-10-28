using System;
using System.Collections;
using System.Diagnostics;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy References")]
    public GameObject redEnemyPrefab;
    public GameObject blueEnemyPrefab;

    static System.Random rand = new System.Random(System.DateTime.Now.Millisecond);

    private void FixedUpdate()
    {
        
    }

    IEnumerator SpawnRedEnemy()
    {
        yield return new WaitForSeconds(4);
        GameObject enemy = Instantiate(redEnemyPrefab, transform);

        float edgeSpawn = rand.Next(0, 4);
        switch(edgeSpawn)
        {
            case 0:
                enemy.transform.position = new Vector2(-10, rand.Next(-5, 5));
                enemy.GetComponent<EnemyColision>().angle = rand.Next(90 - 30, 90 + 30);
                break;
            case 1:
                enemy.transform.position = new Vector2(rand.Next(-10, 10), 5);
                enemy.GetComponent<EnemyColision>().angle = rand.Next(180 - 30, 180 + 30); 
                break;
            case 2:
                enemy.transform.position = new Vector2(10, rand.Next(-5, 5));
                enemy.GetComponent<EnemyColision>().angle = rand.Next(270 - 30, 270 + 30); 
                break;
            case 3:
                enemy.transform.position = new Vector2(rand.Next(-10, 10), -5);
                enemy.GetComponent<EnemyColision>().angle = rand.Next(-30, 30); 
                break;

        }
        StartCoroutine(SpawnRedEnemy());

    }

    IEnumerator SpawnBlueEnemy()
    {
        yield return new WaitForSeconds(10);
        GameObject enemy = Instantiate(blueEnemyPrefab, transform);

        float edgeSpawn = rand.Next(0, 4);
        switch (edgeSpawn)
        {
            case 0:
                enemy.transform.position = new Vector2(-10, rand.Next(-5, 5));
                enemy.GetComponent<EnemyColisionBlue>().angle = rand.Next(90 - 30, 90 + 30);
                break;
            case 1:
                enemy.transform.position = new Vector2(rand.Next(-10, 10), 5);
                enemy.GetComponent<EnemyColisionBlue>().angle = rand.Next(180 - 30, 180 + 30);
                break;
            case 2:
                enemy.transform.position = new Vector2(10, rand.Next(-5, 5));
                enemy.GetComponent<EnemyColisionBlue>().angle = rand.Next(270 - 30, 270 + 30);
                break;
            case 3:
                enemy.transform.position = new Vector2(rand.Next(-10, 10), -5);
                enemy.GetComponent<EnemyColisionBlue>().angle = rand.Next(-30, 30);
                break;

        }
        StartCoroutine(SpawnBlueEnemy());

    }

    private void Start()
    {
        SpawnRedEnemy();
        StartCoroutine(SpawnRedEnemy());
        StartCoroutine(SpawnBlueEnemy());
    }

}
