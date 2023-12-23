using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ApplicationStateMachine appStateMachine;
    public GameObject level;
    public GameObject playerPrefab;

    public Action _exitGameplayEvent;

    // Start is called before the first frame update
    void Start()
    {
        appStateMachine = new ApplicationStateMachine(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {

        }
    }
    
    public void StartGame() {
        level.SetActive(true);
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}
