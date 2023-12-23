using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Action StartBtnEvent;
    public GameObject menuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMenuVisible (bool flag)
    {
        menuUI.SetActive(true);
    }

    public void StartBtnPressed()
    {
        StartBtnEvent?.Invoke();
    }

    public void ExitBtnPressed()
    {
        Application.Quit();
    }
}
