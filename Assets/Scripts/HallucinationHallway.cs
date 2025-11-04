using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallucinationHallway : MonoBehaviour
{
    [SerializeField]
    bool hasStarted = false;
    [SerializeField]
    Transform originalPoint;
    [SerializeField]
    Transform endPoint;

    float pointDis = 0;
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    Transform toiletTP;
    [SerializeField]
    DialogueController radioDialogue;

    // Start is called before the first frame update
    void Start()
    {
        if (!originalPoint)
        {
            originalPoint = transform;
        }
        if (!endPoint)
        {
            originalPoint = transform;
        }
        pointDis = Vector3.Distance(originalPoint.position, endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            UpdateHallucination();
        }
    }
    public void StartHallway()
    {
        hasStarted = true;
    }

    void UpdateHallucination()
    {
        float dis =  Vector3.Distance(PlayerController.Instance.transform.position, endPoint.position);
        UIController.SetHallucination(curve.Evaluate(1-dis/pointDis));

    }
    public void TeleportToToilet()
    {
        hasStarted = false;
        PlayerController.TeleportPlayer_Static(toiletTP.position);
        StartCoroutine(DelayEndHallucination(0.5f));
    }

    IEnumerator DelayEndHallucination(float time)
    {
        yield return new WaitForSeconds(time);
        UIController.SetHallucination(0);

        radioDialogue?.LoadDialogue();
    }
}
