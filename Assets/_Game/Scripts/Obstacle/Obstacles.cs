using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] Obstacle[] allObstacles;
    [SerializeField] GameObject[] barries;
    [SerializeField] Vector2 positionRange;
    [SerializeField] GameObject obstaclesGroup;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        SetObstacles();
    }
    void SetObstacles()
    {
        for (int i = 0; i < allObstacles.Length; i++)
        {
            allObstacles[i].SetAmount();
        }

        for (int i = 0; i < barries.Length; i++)
        {
            bool randomBool = Random.value > 0.5f;
            barries[i].SetActive(randomBool);
        }

        DecreaseDifficulty();
    }

    void Reposition()
    {
        int obstaclesAmout = FindObjectsOfType<Obstacles>().Length;

        transform.position = new Vector2(0, player.position.y + (LevelController.instance.obstaclesDistance * (obstaclesAmout - 1)));
        obstaclesGroup.transform.localPosition = new Vector2(0, Random.Range(positionRange.x, positionRange.y));

    }

    void DecreaseDifficulty()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Reposition();
            SetObstacles();
        }
    }
}
