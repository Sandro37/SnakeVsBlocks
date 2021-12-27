using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    [SerializeField] private float gameSpeed = 2f;
    public int ObstaclesAmount = 6;
    public Color easyColor, mediumColor, hardColor;
    public float damageTime = 0.1f;
    public float obstaclesDistance = 13;
    public ObjectPool pickupPool;
    [SerializeField] Vector2 xLimit;
    private Transform player;
    public float mutiplier = 1f;
    public float cicleTime = 10f;
    private int points;

    [Header("Points UI")]
    [SerializeField] private Text pointText;
    [Header("UI")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Clips")]
    [SerializeField] private AudioClip clickSound;

    public bool gameOver = true;
    public float GameSpeed
    {
        get => gameSpeed;
        private set => gameSpeed = value;
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine("SpawnPickup");
    }
    private IEnumerator SpawnPickup()
    {
        player = FindObjectOfType<Player>().transform;
        while (gameOver)
        {
            yield return null;
        }
        SpawnPickups();
        InvokeRepeating(nameof(IncreaseDifficulty), cicleTime, cicleTime);
    }

    void SpawnPickups()
    {
        pickupPool.GetObject().transform.position = new Vector2(
            Random.Range(xLimit.x, xLimit.y),
            player.position.y + 15f);

        Invoke(nameof(SpawnPickups), Random.Range(1, 3));
    }

    void IncreaseDifficulty()
    {
        ObstaclesAmount += 2;

        mutiplier *= 1.1f;
        
    }

    public void Score(int score)
    {
        points += score;

        pointText.text = points.ToString();
    }

    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        gamePanel.SetActive(true);
        startPanel.SetActive(false);
        gameOver = false;
    }

    public void GameOver()
    {
        gameOver = true;
        GameSpeed = 0;
        gameOverPanel.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
