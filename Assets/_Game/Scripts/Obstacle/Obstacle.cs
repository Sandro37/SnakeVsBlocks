using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Text amountText;

    private int amount;
    private SpriteRenderer sprite;
    private Player player;
    private float nextTime;
    private Color initialColor;

    [Header("Audio clip")]
    [SerializeField] private AudioClip impactSound;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
    }

    private void Update()
    {
        if(player != null && nextTime < Time.time)
        {
            PlayerDamage();
        }
    }
    public void SetAmount()
    {
        gameObject.SetActive(true);
        amount = Random.Range(0, LevelController.instance.ObstaclesAmount);
        if(amount <= 0)
            gameObject.SetActive(false);
        
        SetAmountText();
        SetColor();
    }

    public void SetAmountText()
    {
        amountText.text = amount.ToString();
    }

    public void SetColor()
    {
        int playerLives = FindObjectOfType<Player>().transform.childCount;
        Color newColor;

        if (amount > playerLives)
            newColor = LevelController.instance.hardColor;
        else if (amount > playerLives / 2)
            newColor = LevelController.instance.mediumColor;
        else
            newColor = LevelController.instance.easyColor;

        sprite.color = newColor;
        initialColor = newColor;
    }

    void PlayerDamage()
    {
        if (LevelController.instance.gameOver)
            return;

        AudioSource.PlayClipAtPoint(impactSound, Camera.main.transform.position);
        nextTime = Time.time + LevelController.instance.damageTime;
        player.TakeDamage();
        amount--;
        SetAmountText();
        if (amount <= 0)
        {
            gameObject.SetActive(false);
            player = null;
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(DamageColor());
        }
    }

    IEnumerator DamageColor()
    {
        float timer = 0f;
        float t = 0f;
        sprite.color = initialColor;
        while(timer < LevelController.instance.damageTime)
        {
            sprite.color = Color.Lerp(initialColor, Color.white, t);
            timer += Time.deltaTime;
            t += Time.deltaTime / LevelController.instance.damageTime;
            yield return null;
        }
        sprite.color = initialColor;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player otherPlayer = collision.gameObject.GetComponent<Player>();

        if(otherPlayer != null)
        {
            player = otherPlayer;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Player otherPlayer = collision.gameObject.GetComponent<Player>();

        if (otherPlayer != null)
        {
            player = null;
        }
    }
}
