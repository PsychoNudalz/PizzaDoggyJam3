using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientKit : MonoBehaviour
{
    [SerializeField]
    int PatientID;
    [Header("Dead")]
    [SerializeField]
    GameObject root_Dead;
    [SerializeField]
    List<GameObject> patients = new List<GameObject>();

    // [SerializeField]
    // GameObject Patient_Dead;
    [Header("Treated")]
    [SerializeField]
    GameObject root_Treated;
    [SerializeField]
    SpriteRenderer patientSprite;
    [SerializeField]
    List<Sprite> treatedSprites;

    public void IsDead(bool isDead)
    {
        root_Dead.SetActive(isDead);
        root_Treated.SetActive(!isDead);

        if (isDead)
        {
            for (int index = 0; index < patients.Count; index++)
            {
                GameObject patient = patients[index];
                if (index == PatientID-1)
                {
                    patient.SetActive(true);
                }
                else
                {
                    patient.SetActive(false);
                }
            }
        }
        else
        {
            if (patientSprite)
            {
                patientSprite.sprite = treatedSprites[PatientID - 1];
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
