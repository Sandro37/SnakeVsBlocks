using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Text livesText;
    [SerializeField] private float speed;
    private float mouseDistance;
    private Rigidbody2D rig;
    private float lastYPos;

    private bool sliding;
    private int dir;


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        lastYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 wordPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float xPos = wordPoint.x;

        mouseDistance = Mathf.Clamp(xPos - transform.position.x, -1, 1);


        if(transform.position.y > lastYPos + 5)
        {
            LevelController.instance.Score(10);
            lastYPos = transform.position.y;
        }
    }

    private void FixedUpdate()
    {
        if (LevelController.instance.gameOver)
            return;
        if (!sliding)
            rig.velocity = new Vector2(mouseDistance * speed, LevelController.instance.GameSpeed * LevelController.instance.mutiplier);
        else
            rig.velocity = new Vector2(dir * 2.5f, LevelController.instance.GameSpeed * LevelController.instance.mutiplier);
    }

    public void SetSlide(int direction)
    {
        sliding = true;
        dir = direction;
        Invoke(nameof(SetSlideToFalse), 0.25f);
    }

    void SetSlideToFalse()
    {
        sliding = false;
    }
    public void TakeDamage()
    {

        if (LevelController.instance.gameOver)
            return;

        int chindrens = transform.childCount;

        if(chindrens <= 1)
        {
            LevelController.instance.GameOver();
        }
        else
        {
            Destroy(transform.GetChild(chindrens - 1).gameObject);
        }

        SetText(chindrens -1);
    }

    public void SetText(int amount)
    {
        livesText.text = amount.ToString();
    }
}
