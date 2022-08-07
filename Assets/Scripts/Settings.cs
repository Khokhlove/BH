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
    private void Start()
    {
        //ChangeCursorState();
    }

    private void ChangeCursorState()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
