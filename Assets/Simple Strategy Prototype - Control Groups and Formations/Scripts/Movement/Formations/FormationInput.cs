using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FormationInput : MonoBehaviour
{
    [SerializeField]
    private FormationLists formationLists;
    [SerializeField]
    private SelectionLists selectionLists;

    private void Update()
    {
        HandleFormationInputs();
    }

    private void HandleFormationInputs()
    {
        Keyboard kboard = Keyboard.current;

        foreach (FormationKeyStruct formationKeyStruct in formationLists.GetAllFormationKeyPairs())
        {
            if (kboard[formationKeyStruct.key].wasPressedThisFrame)
            {

                foreach (Selectable selectable in selectionLists.GetAllSelected())
                {
                    Formationable formationable = selectable.GetComponent<Formationable>();
                    if (formationable is not null)
                    {
                        formationable.MyFormationPreset = formationKeyStruct.formationPreset;
                    }
                }
            }
        }
    }
}
