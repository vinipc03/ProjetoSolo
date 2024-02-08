using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSkin : MonoBehaviour
{
    public Transform wizard;

    public void Fireball()
    {
        wizard.GetComponent<WizardController>().Fireball();
    }
}
