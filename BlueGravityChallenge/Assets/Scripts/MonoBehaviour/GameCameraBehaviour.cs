using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraBehaviour : MonoBehaviour
{
    private GameObject player;
    
    void Start()
    {
        //Find player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Update camera position to follow player
        Vector3 pos = player.transform.position;
        gameObject.transform.position = pos + new Vector3(0, 0, -10);
    }
}
