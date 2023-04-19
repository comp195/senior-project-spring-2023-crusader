using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        float py = player.transform.position.y;
        transform.position = player.transform.position + new Vector3(10, -py, -5);
    }
}
