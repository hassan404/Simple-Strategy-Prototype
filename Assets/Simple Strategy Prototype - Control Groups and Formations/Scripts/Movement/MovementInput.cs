using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInput : MonoBehaviour
{
    [SerializeField]
    private SelectionLists selectionLists;
    [SerializeField]
    private FormationAssignment formationAssignment;

    // Update is called once per frame
    void Update()
    {
        HandleMovementInputs();
    }

    void HandleMovementInputs()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3 clickPos = CheckForMoveClickedAt();
            if (clickPos != Vector3.zero)
            {
                List<Formationable> selectedFormationables = formationAssignment.GetFormationablesFromSelectables();
                List<Vector3> offsets = selectedFormationables[0].
                    MyFormationPreset.GetFormationOffsets(selectedFormationables.Count);
                for (int i = 0; i < selectedFormationables.Count; i++)
                {
                    //print(offsets[i]);
                    selectedFormationables[i].GetComponent<IMoveable>().Move(clickPos + offsets[i]);
                }
            }
        }
    }

    public Vector3 CheckForMoveClickedAt()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //Debug.Log($"Move to:{hit.point}" );
            return hit.point;
        }

        return Vector3.zero;
    }
}
