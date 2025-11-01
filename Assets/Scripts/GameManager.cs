using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] UnityEvent onStartEvent;
    // Start is called before the first frame update
    void Start()
    {
        onStartEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
