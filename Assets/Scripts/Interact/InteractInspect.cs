using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInspect : InteractObject
{


    [Header("Item")]
    [SerializeField]
    ItemEnum itemEnum;

    public override void OnInteract()
    {
        UIController.InspectItem_Static(itemEnum);
        base.OnInteract();
    }
}
