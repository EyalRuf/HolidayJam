using CardboardCore.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Injectable]
public class MenuManager : MonoBehaviour
{
    public Action StartBtnEvent;
    public GameObject menuUI;

    public Button StartBtn;
    public Button ExitBtn;

    public void SetMenuVisible (bool flag)
    {
        menuUI.SetActive(flag);

        if (flag ) {
            StartBtn.onClick.AddListener(StartBtnPressed);
            ExitBtn.onClick.AddListener(ExitBtnPressed);
        }
        else {
            StartBtn.onClick.RemoveAllListeners();
            ExitBtn.onClick.RemoveAllListeners();
        }
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
