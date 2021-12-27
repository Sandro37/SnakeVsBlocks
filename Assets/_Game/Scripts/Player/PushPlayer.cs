using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if(player != null)
        {
            float direction = player.transform.position.x - transform.position.x;
            player.SetSlide((int) Mathf.Sign(direction));
        }
    }
}
