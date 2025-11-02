using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] UnityEvent onStartEvent;
    [SerializeField] string nextSceneName;
    [SerializeField] protected GameObject player;


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
        PlayerController findObjectOfType = FindObjectOfType<PlayerController>();
        if (findObjectOfType)
        {
            player =  findObjectOfType.gameObject;
        }
        onStartEvent.Invoke();
        if (nextSceneName == "")
        {
            nextSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
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

    public static void ResetScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnCompleteMission(MissionManager missionManager, MissionObject mission)
    {

    }

}
