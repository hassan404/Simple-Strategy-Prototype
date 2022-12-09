using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlGroupLists : MonoBehaviour
{
    public ControlGroupInputPreset controlGroupConfigs;
    [SerializeField]
    private List<ControlGroup> configuredControlGroups = new List<ControlGroup>();

    public List<ControlGroup> ConfiguredControlGroups 
    {
        get => configuredControlGroups;
        private set => configuredControlGroups = value;
    }

    private void Awake()
    {
        PopulateConfiguredControlGroups();
    }

    public void PopulateConfiguredControlGroups()
    {
        foreach (Key key in controlGroupConfigs.controlGroupKeys)
        {
            ConfiguredControlGroups.Add(new ControlGroup(key));
        }
    }

}