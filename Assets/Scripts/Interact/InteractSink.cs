using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSink : InteractObject
{
    [Header("Sink")]
    [SerializeField]
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    protected override void InteractLogic()
    {
        base.InteractLogic();
        gameManager?.LoadNextScene();
    }
}
