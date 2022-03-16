using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gManager;
    public static GameManager Singleton
    {
        get
        {
            if (gManager == null)
                gManager = FindObjectOfType<GameManager>();
            return gManager;
        }
    }

    public Text scoreText;
    public Text highestScoreText;

    public int highestScore;
    public int score = 0;

    public float velocity = 20;
    private float maxVelocity = 75;
    public float difficulty;

    private bool canSpawn;
    private GameObject obstacleIndex;

    public GameObject[] obstacleType;
    public int[] obstacleTypePercent;
    Transform spawnPos;

    public static bool[] obstacleTypeIndexer;
    int randObstacleType;

    private void Start()
    {
        canSpawn = true;
        obstacleTypeIndexer = new bool[obstacleType.Length];
        AddHighestScore();
    }

    private void Update()
    {
        spawnPos = gameObject.transform;

        if (canSpawn)
        {
            canSpawn = false;

            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        int positionOffset = Random.Range(-30, 3);
        obstacleIndex = Instantiate(obstacleType[randObstacleType], spawnPos.position + new Vector3(0, 0, positionOffset), spawnPos.rotation); //cria um clone de um objeto no respectivo transform
        obstacleTypeIndexer[randObstacleType] = true;
    }

    public void DeleteObstacle()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        randObstacleType = Random.Range(0, 100);
        int obstacleTypeNumberIndex = 0;
        foreach (int percentage in obstacleTypePercent)
        {
            if (randObstacleType >= obstacleTypePercent[obstacleTypeNumberIndex])
            {
                randObstacleType = obstacleTypeNumberIndex;
            }
            obstacleTypeNumberIndex++;
        }
        if ((randObstacleType + 1) > obstacleType.Length)
        {
            randObstacleType = 0;
        }

        obstacleTypeIndexer[randObstacleType] = false;
        canSpawn = true;
        if(velocity < maxVelocity)
            velocity += difficulty;
        Destroy(obstacleIndex);
        AddScore();
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void AddHighestScore()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore");
        if (highestScore < score)
            highestScore = score;
        highestScoreText.text = highestScore.ToString();
        PlayerPrefs.SetInt("HighestScore", highestScore);
    }
}