using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SelectionInput : MonoBehaviour
{
    [SerializeField]
    private SelectionLists selectionLists;
    [SerializeField]
    private RectTransform selectionBoxUI;
    private Vector2 startPos;
    
    //Serialize and assign in editor if you don't want it to be the main camera.
    private Camera selectionCam;

    private void Awake()
    {
        if (!selectionCam)
            selectionCam = Camera.main;
    }

    private void Update()
    {
        HandleClickSelectionInputs();
        HandleDragClickSelectionInputs();
    }

    private void HandleClickSelectionInputs()
    {

        if (Keyboard.current.shiftKey.isPressed)
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Selectable selectable = CheckForSelectableClickedAt();
                if (selectable)
                {
                    selectionLists.AddSelected(selectable);
                }
            }
        }
        else if (Keyboard.current.ctrlKey.isPressed)
        {
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Selectable selectable = CheckForSelectableClickedAt();
                if (selectable)
                {
                    selectionLists.RemoveSelected(selectable);
                }
            }
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            selectionLists.RemoveAllSelected();

            Selectable selectable = CheckForSelectableClickedAt();
            if (selectable)
            {
                selectionLists.AddSelected(selectable);
            }
        }
    }

    private void HandleDragClickSelectionInputs()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startPos = Input.mousePosition;
  
        }
        if (Mouse.current.leftButton.isPressed)
        {
            UpdateSelectionBox(Input.mousePosition);
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ReleaseSelectionBox();
        }
    }

    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBoxUI.gameObject.activeInHierarchy)
            selectionBoxUI.gameObject.SetActive(true);
        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;
        selectionBoxUI.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBoxUI.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    void ReleaseSelectionBox()
    {
        selectionBoxUI.gameObject.SetActive(false);
        Vector2 min = selectionBoxUI.anchoredPosition - (selectionBoxUI.sizeDelta / 2);
        Vector2 max = selectionBoxUI.anchoredPosition + (selectionBoxUI.sizeDelta / 2);

        foreach (Selectable selectable in selectionLists.GetAllSelectables())
        {
            Vector3 screenPos = selectionCam.WorldToScreenPoint(selectable.transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectionLists.AddSelected(selectable);
            }
        }
    }

    public Selectable CheckForSelectableClickedAt()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<Selectable>())
            {
                return hit.collider.GetComponent<Selectable>();
            }
            //Debug.Log($"Hit:{hit.transform.name}" );
        }

        return null;
    }

}
