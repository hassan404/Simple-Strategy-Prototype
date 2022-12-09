using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "ControlGroupPresets", menuName = "Simple RTS/Control Groups/Control Group Preset", order = 1)]
public class ControlGroupInputPreset : ScriptableObject
{
    public List<Key> controlGroupKeys; //configure in editor

}
