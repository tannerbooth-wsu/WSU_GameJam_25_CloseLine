using System;
using System.Collections;
using System.Diagnostics;
using TMPro;
//using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyMovement : MonoBehaviour
{
    [Header("Enemy References")]
    public GameObject redEnemyPrefab;
    public GameObject blueEnemyPrefab;
    public GameObject yellowEnemyPrefab;
    public GameObject pinkEnemyPrefab;

    static System.Random rand = new System.Random(System.DateTime.Now.Millisecond);

    private float redWaitTime = 4f;
    private float blueWaitTime = 10f;
    private float yellowWaitTime = 16f;

    public Canvas ui;
    public TextMeshProUGUI Scoreui;

    private float score = 0f;

    public void IncrementScore()
    {
        score += 1;
        Scoreui.text = "Score: " + score.ToString();
        if (score == 8)
        {
            redWaitTime -= 1;
            blueWaitTime -= 2;
            yellowWaitTime -= 2;
            StartCoroutine(SpawnPinkEnemy());
        }
        else if (score == 17)
        {
            redWaitTime -= 1;
            blueWaitTime -= 2;
            yellowWaitTime -= 2;
        }
        else if (score == 27)
        {
            blueWaitTime -= 2;
            yellowWaitTime -= 2;
        }
        else if (score == 38)
        {
            redWaitTime -= 1;
            blueWaitTime -= 1;
            yellowWaitTime -= 2;
        }
        else if (score == 50)
        {
            blueWaitTime -= 1;
            yellowWaitTime -= 1;
        }
        else if (score == 100)
        {
            blueWaitTime -= 1;
            yellowWaitTime -= 1;
        }
    }

    public void EndGame()
    {
        Camera camera = Camera.main;
        if (camera != null)
        {
            camera.backgroundColor = Color.black;
            ui.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    IEnumerator SpawnRedEnemy()
    {
        yield return new WaitForSeconds(redWaitTime);
        GameObject enemy = Instantiate(redEnemyPrefab, transform);

        float edgeSpawn = rand.Next(0, 4);
        switch(edgeSpawn)
        {
            case 0:
                enemy.transform.position = new Vector2(-11, rand.Next(-5, 5));
                enemy.GetComponent<EnemyColision>().angle = rand.Next(90 - 30, 90 + 30);
                break;
            case 1:
                enemy.transform.position = new Vector2(rand.Next(-10, 10), 6);
                enemy.GetComponent<EnemyColision>().angle = rand.Next(180 - 30, 180 + 30); 
                break;
            case 2:
                enemy.transform.position = new Vector2(11, rand.Next(-5, 5));
                enemy.GetComponent<EnemyColision>().angle = rand.Next(270 - 30, 270 + 30); 
                break;
            case 3:
                enemy.transform.position = new Vector2(rand.Next(-10, 10), -6);
                enemy.GetComponent<EnemyColision>().angle = rand.Next(-30, 30); 
                break;

        }
        StartCoroutine(SpawnRedEnemy());

    }

    IEnumerator SpawnBlueEnemy()
    {
        yield return new WaitForSeconds(blueWaitTime);
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

    IEnumerator SpawnYellowEnemy()
    {
        yield return new WaitForSeconds(yellowWaitTime);
        GameObject enemy = Instantiate(yellowEnemyPrefab, transform);

        float edgeSpawn = rand.Next(0, 4);
        switch (edgeSpawn)
        {
            case 0:
                enemy.transform.position = new Vector2(-10, rand.Next(-5, 5));
                enemy.GetComponent<EnemyColisionYellow>().angle = rand.Next(90 - 30, 90 + 30);
                break;
            case 1:
                enemy.transform.position = new Vector2(rand.Next(-10, 10), 5);
                enemy.GetComponent<EnemyColisionYellow>().angle = rand.Next(180 - 30, 180 + 30);
                break;
            case 2:
                enemy.transform.position = new Vector2(10, rand.Next(-5, 5));
                enemy.GetComponent<EnemyColisionYellow>().angle = rand.Next(270 - 30, 270 + 30);
                break;
            case 3:
                enemy.transform.position = new Vector2(rand.Next(-10, 10), -5);
                enemy.GetComponent<EnemyColisionYellow>().angle = rand.Next(-30, 30);
                break;

        }
        StartCoroutine(SpawnYellowEnemy());

    }

    IEnumerator SpawnPinkEnemy()
    {
        yield return new WaitForSeconds(25);
        GameObject enemy = Instantiate(pinkEnemyPrefab, transform);

        float edgeSpawn = rand.Next(0, 4);
        switch (edgeSpawn)
        {
            case 0:
                enemy.transform.position = new Vector2(-10, rand.Next(-5, 5));
                enemy.GetComponent<EnemyColisionPink>().angle = rand.Next(90 - 30, 90 + 30);
                break;
            case 1:
                enemy.transform.position = new Vector2(rand.Next(-10, 10), 5);
                enemy.GetComponent<EnemyColisionPink>().angle = rand.Next(180 - 30, 180 + 30);
                break;
            case 2:
                enemy.transform.position = new Vector2(10, rand.Next(-5, 5));
                enemy.GetComponent<EnemyColisionPink>().angle = rand.Next(270 - 30, 270 + 30);
                break;
            case 3:
                enemy.transform.position = new Vector2(rand.Next(-10, 10), -5);
                enemy.GetComponent<EnemyColisionPink>().angle = rand.Next(-30, 30);
                break;

        }
        StartCoroutine(SpawnPinkEnemy());

    }

    private void Start()
    {
        SpawnRedEnemy();
        StartCoroutine(SpawnRedEnemy());
        StartCoroutine(SpawnBlueEnemy());
        StartCoroutine(SpawnYellowEnemy());
    }

}
