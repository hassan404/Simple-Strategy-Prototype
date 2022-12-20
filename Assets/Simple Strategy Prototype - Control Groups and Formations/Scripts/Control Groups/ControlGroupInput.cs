using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ControlGroupInput : MonoBehaviour
{
    [SerializeField]
    private ControlGroupLists controlGroupLists;
    [SerializeField]
    private SelectionLists selectionLists;

    private void Update()
    {
        HandleControlGroupInputs();
    }

    private void HandleControlGroupInputs()
    {
        Keyboard kboard = Keyboard.current;

        if (kboard.ctrlKey.isPressed)
        {
            foreach (ControlGroup controlGroup in controlGroupLists.ConfiguredControlGroups)
            {
                if (kboard[controlGroup.key].wasPressedThisFrame)
                {
                    print($"Pressed:{controlGroup.key}");
                    foreach(Selectable selectable in controlGroup.assignedSelectables)
                    {
                        selectable.RemoveControlGroupName(controlGroup.key.ToString());
                    }
                    //assign control group to key, remove old assignment
                    controlGroup.assignedSelectables.Clear();
                    controlGroup.assignedSelectables.AddRange(selectionLists.GetAllSelected());

                    foreach (Selectable selectable in controlGroup.assignedSelectables)
                    {
                        selectable.AddControlGroupName(controlGroup.key.ToString());
                    }
                }
            }
        }
        else
        {
            foreach (ControlGroup controlGroup in controlGroupLists.ConfiguredControlGroups)
            {
                if (kboard[controlGroup.key].wasPressedThisFrame)
                {
                    //If some selectables are assigned to the control group, select them and deselect the rest.
                    if (controlGroup.assignedSelectables.Count > 0)
                    {
                        selectionLists.RemoveAllSelected();
                        selectionLists.AddSelecteds(controlGroup.assignedSelectables);
                    }
                }
            }
        }
    }
}
