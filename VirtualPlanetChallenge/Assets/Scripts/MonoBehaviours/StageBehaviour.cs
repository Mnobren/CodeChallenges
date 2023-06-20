using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBehaviour : MonoBehaviour
{
    public static StageBehaviour instance = null;

    [SerializeField] private GameObject slot;
    public GameObject timeDisplay;

    private bool play;
    private DateTime start;
    private DateTime finish;
    
    void Awake()
    {
        //Singleton Pattern (kind of)
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        StartGame();
    }

    public void FixedUpdate()
    {
        if(play)
        {
            timeDisplay.GetComponent<DisplayBehaviour>().ShowText(finish.Subtract(DateTime.Now).Minutes+":"+finish.Subtract(DateTime.Now).Seconds);
        }
    }

    public void StartGame()
    {
        play = true;
        start = DateTime.Now;
        finish = start + new TimeSpan(0,2,0);
    }

    public void FillSlot(GameObject obj)
    {
        obj.transform.position = slot.transform.position;
        NextSlot();
    }

    public void NextSlot()
    {
        slot.transform.position += new Vector3(0, 0.5f, 0);
    }

    public Transform GetSlot()
    {
        return slot.transform;
    }
}
