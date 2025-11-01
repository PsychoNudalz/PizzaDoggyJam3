using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractVaccine : InteractObject
{

    public override void OnInteract()
    {
        base.OnInteract();
        Day2Manager day2Manager = FindObjectOfType<Day2Manager>();
        if (day2Manager != null)
        {
            day2Manager.LoadVaccine();
        }
    }
}
