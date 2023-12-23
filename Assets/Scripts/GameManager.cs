using CardboardCore.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

[Injectable]
public class GameManager : MonoBehaviour
{
    private ApplicationStateMachine appStateMachine;
    public GameObject level;
    public PlayerController player;

    public Action _pauseMenuEvent;
     
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
            _pauseMenuEvent?.Invoke();
        }
    }
    
    public void StartGame() {
        level.SetActive(true);
        player.gameObject.SetActive(true);  

        // init player stuff?
        player.transform.position = Vector3.zero;
    }

    public void EndGame() {
        level.SetActive(false);
        player.PauseCharacter();
        player.gameObject.SetActive(false);

        // reset everything here
    }
}
