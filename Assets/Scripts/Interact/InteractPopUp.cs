using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPopUp : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text tmpText;
    Transform mainCamera;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
        if (mainCamera == null) return;

        Vector3 direction = transform.position - mainCamera.position;
        direction.y = 0; // optional: keep upright
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void ShowPopUp(Vector3 position,string text="")
    {
        animator?.SetTrigger("ShowPopUp");
        gameObject.SetActive(true);
        transform.position = position;
        tmpText.text = text;

    }

    public void HidePopUp()
    {
        gameObject.SetActive(false);

    }
}