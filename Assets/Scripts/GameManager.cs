using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] UnityEvent onStartEvent;
    [SerializeField] string nextSceneName;


    public static GameManager Instance;

    void Awake()
    {
        if (Instance)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        onStartEvent.Invoke();
    }

    public void LoadNextScene()
    {
        if (nextSceneName == "")
        {
            Debug.LogError("Next scene name is empty");
            return;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
