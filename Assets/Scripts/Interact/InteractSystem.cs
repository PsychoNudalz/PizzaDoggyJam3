using System.Collections;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    [SerializeField] private Transform rootComponent;

    [Header("Raycast")]
    [SerializeField] private bool isRaycast = true;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string raycastTag;
    [SerializeField] private float raycastDistance = 4f;
    [SerializeField] private float raycastInterval = 0.5f;
    private Coroutine raycastCoroutine;

    [Header("Focus")]
    [SerializeField] private InteractAbstract interactTarget;

    private void Awake()
    {
        if (!rootComponent)
            rootComponent = transform;
    }

    private void Start()
    {
        if (isRaycast)
            raycastCoroutine = StartCoroutine(CastInteractRay());
    }

    private void SetRaycast(bool value)
    {
        if (value == isRaycast)
            return;

        isRaycast = value;

        if (isRaycast)
        {
            if (raycastCoroutine != null)
                StopCoroutine(raycastCoroutine);

            raycastCoroutine = StartCoroutine(CastInteractRay());
        }
        else
        {
            if (raycastCoroutine != null)
            {
                StopCoroutine(raycastCoroutine);
                raycastCoroutine = null;
            }
        }
    }

    private IEnumerator CastInteractRay()
    {
        while (isRaycast)
        {
            Ray ray = new Ray(rootComponent.position, rootComponent.forward);
            Debug.DrawLine(rootComponent.position, rootComponent.position + rootComponent.forward * raycastDistance, Color.red,raycastInterval);

            if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, layerMask))
            {
                if (string.IsNullOrEmpty(raycastTag) || hit.collider.CompareTag(raycastTag))
                {
                    InteractAbstract target = hit.collider.GetComponent<InteractAbstract>();
                    if (target && target != interactTarget)
                    {
                        SetInteractTarget(target);
                        print("New interact target: " + target.name);
                    }
                }
            }
            else
            {
                if (interactTarget)
                {
                    SetInteractTarget(null);
                    print("Lost interact target");
                }
            }

            yield return new WaitForSeconds(raycastInterval);
        }
    }

    private void SetInteractTarget(InteractAbstract target)
    {
        if (interactTarget)
        {
            interactTarget.OnFocus_Exit();
        }
        interactTarget = target;
        if (interactTarget)
        {
            interactTarget.OnFocus_Enter();
        }
    }

    public void OnInteract()
    {
        if (interactTarget)
        {
            interactTarget.OnInteract();
        }
    }
}
