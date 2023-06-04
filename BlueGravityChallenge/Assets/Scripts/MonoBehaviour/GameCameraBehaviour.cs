using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraBehaviour : MonoBehaviour
{
    private GameObject player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 pos = player.transform.position;
        gameObject.transform.position = pos + new Vector3(0, 0, -10);
    }
}
