using CardboardCore.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Injectable]
public class PauseMenuManager : MonoBehaviour
{
    public Action ContinueBtnEvent;
    public Action ExitBtnEvent;
    public GameObject menuUI;

    public Button ContinueBtn;
    public Button ExitBtn;

    public void SetPauseMenuVisible (bool flag)
    {
        menuUI.SetActive(flag);

        if (flag) {
            ContinueBtn.onClick.AddListener(ContinueBtnPressed);
            ExitBtn.onClick.AddListener(ExitBtnPressed);
        } else {
            ContinueBtn.onClick.RemoveAllListeners();
            ExitBtn.onClick.RemoveAllListeners();
        }
    }

    public void ContinueBtnPressed()
    {
        ContinueBtnEvent?.Invoke();
    }

    public void ExitBtnPressed()
    {
        ExitBtnEvent?.Invoke();
    }
}
