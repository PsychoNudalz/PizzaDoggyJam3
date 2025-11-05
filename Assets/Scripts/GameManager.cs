using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] UnityEvent onStartEvent;
    [SerializeField] string nextSceneName;
    [SerializeField] UnityEvent onEndEvent;
    [SerializeField] float delay;
    bool loadingNextScene = false;
    [SerializeField] protected GameObject player;
    protected string TreatedPPrefName = "Treated_P";

    [SerializeField] bool lockMouse = true;


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
        if (lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

        PlayerController findObjectOfType = FindObjectOfType<PlayerController>();
        if (findObjectOfType)
        {
            player = findObjectOfType.gameObject;
        }
        onStartEvent.Invoke();
        if (nextSceneName == "")
        {
            nextSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
    }

    public virtual void LoadNextScene()
    {
        if (loadingNextScene) return;


        loadingNextScene = true;
        onEndEvent.Invoke();


        if (nextSceneName == "")
        {
            Debug.LogError("Next scene name is empty");
            return;
        }


        StartCoroutine(DelayLoadScene());
    }
    IEnumerator DelayLoadScene()
    {

        yield return new WaitForSeconds(delay);
        PlayerController.Blink_Static(1f);
        yield return new WaitForSeconds(1f);
        if (nextSceneName.Equals("EndGame"))
        {
            EndGame();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }
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

    public static void EndGame()
    {
        Application.Quit();
    }
}
