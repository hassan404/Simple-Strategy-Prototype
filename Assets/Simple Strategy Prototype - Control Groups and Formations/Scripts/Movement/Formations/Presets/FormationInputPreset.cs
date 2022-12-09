using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public struct FormationKeyStruct {
    public BaseFormationPreset formationPreset;
    public Key key;
}

[CreateAssetMenu(fileName = "FormationInputPreset", menuName = "Simple RTS/Formation Input/Formation Input Preset", order = 1)]

public class FormationInputPreset : ScriptableObject
{
    public List<FormationKeyStruct> FormationPresetAndKeyList;
}