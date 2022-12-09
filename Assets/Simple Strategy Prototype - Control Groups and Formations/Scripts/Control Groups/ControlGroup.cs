using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[System.Serializable]
public class ControlGroup
{
    public Key key;
    public List<Selectable> assignedSelectables;

    public ControlGroup(Key key)
    {
        this.key = key;
        assignedSelectables = new List<Selectable>();
    }
}
