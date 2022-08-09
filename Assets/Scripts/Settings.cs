using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Settings : Singleton<Settings>
{
    [SyncVar]
    [SerializeField]
    private float timeDelay = 3;
    public float TimeDelay { get { return timeDelay; } }
    [SyncVar]
    [SerializeField]
    private int scoreToWin = 3;
    public int ScoreToWin { get { return scoreToWin; } }
    [SyncVar]
    [SerializeField]
    private float timeRestart = 3;
    public float TimeRestart { get { return timeRestart; } }

    public void ChangeCursorState(bool visible, CursorLockMode mode)
    {
            Cursor.visible = visible;
            Cursor.lockState = mode;
    }

    public void CheckPressESC()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            ChangeCursorState(true, CursorLockMode.None);
    }
}
