using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offSet;
    // Start is called before the first frame update
    void Start()
    {
        offSet = transform.position - player.position;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(0, player.position.y + offSet.y, player.position.z + offSet.z);
    }
}
