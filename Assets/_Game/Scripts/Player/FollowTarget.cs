using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float averageSpeed = 15f;
    [SerializeField] private float maxSpeed = 30f;

    private float initialHeight;

    public Transform Target
    {
        set => this.target = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        initialHeight = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(target.position.x - transform.position.x);
        float newSpeed;

        float percent = (distance / 2);

        newSpeed = (averageSpeed * percent) + (minSpeed * percent);

        if(distance > 2)
        {
            newSpeed = maxSpeed;
        }

        Vector2 newPos = new Vector2(target.position.x, transform.position.y + percent);
        transform.position = Vector2.MoveTowards(transform.position, newPos, newSpeed * Time.deltaTime);

        transform.localPosition = new Vector2(transform.localPosition.x,
            Mathf.Clamp(transform.localPosition.y, initialHeight, initialHeight + percent));
    }
}
