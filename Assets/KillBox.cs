using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KillBox : MonoBehaviour
{
    [SerializeField]
    UnityEvent onKill;
    [SerializeField]
    float blinkTime=5;
    [SerializeField]
    float restartDelay=3;
    [SerializeField]
    bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")|| isTriggered)
        {
            return;
        }
        StartCoroutine(KillRoutine());

    }


    IEnumerator KillRoutine()
    {
        onKill.Invoke();
        isTriggered = true;
        PlayerController.Blink_Static(blinkTime);
        yield return new WaitForSeconds(restartDelay);
        GameManager.ResetScene();
    }
}
