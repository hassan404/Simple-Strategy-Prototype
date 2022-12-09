using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Selectable : MonoBehaviour, ISelectable
{
    [SerializeField]
    private GameObject selectionVisuals;
    [SerializeField]
    private TMP_Text controlGroupText;
    private List<string> controlGroupTexts;

    private void OnEnable()
    {
        controlGroupTexts = new List<string>();
        SelectionLists.instance.AddSelectable(this);
    }

    private void OnDisable()
    {
        SelectionLists.instance.RemoveSelectable(this);
    }

    public void Deselect()
    {
        selectionVisuals.SetActive(false);
    }

    public void Select()
    {
        selectionVisuals.SetActive(true);
    }

    public void AddControlGroupName(string str)
    {
        controlGroupTexts.Add(str);
        UpdateControlGroupText();
    }

    public void RemoveControlGroupName(string str)
    {
        controlGroupTexts.Remove(str);
        UpdateControlGroupText();
    }

    private void UpdateControlGroupText()
    {
        string temp = "";
        foreach (string str in controlGroupTexts)
        {
            temp += str + ",";
        }
        temp = temp.TrimEnd(',').Replace("Digit", "");
        controlGroupText.text = temp;
    }

}
