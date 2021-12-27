using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    [SerializeField] private Text amountText;
    [SerializeField] private GameObject bodyPrefab;
    private int amount;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip pickupSound;


    private void OnEnable()
    {
        amount = Random.Range(1, 6);
        amountText.text = amount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);
            for (int i = 0; i < amount; i++)
            {
                int index = collision.transform.childCount;
                GameObject newBody = Instantiate(bodyPrefab, collision.transform);
                newBody.transform.localPosition = new Vector3(0, -index, 0);

                FollowTarget follow = newBody.GetComponent<FollowTarget>();
                if(follow != null)
                {
                    follow.Target = collision.transform.GetChild(index - 1);
                }
            }

            Player player = collision.GetComponent<Player>();
            if(player != null)
                player.SetText(player.transform.childCount);
        }

        gameObject.SetActive(false);
    }
}
