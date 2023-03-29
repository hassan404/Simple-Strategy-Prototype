using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationLists : MonoBehaviour
{
    [SerializeField]
    private FormationInputPreset formationInputPreset;
    [SerializeField]
    private List<BaseFormationPreset> allowedFormations = new List<BaseFormationPreset>();

    private void Awake()
    {
        foreach (FormationKeyStruct formationKeyStruct in formationInputPreset.FormationPresetAndKeyList)
        {
            allowedFormations.Add(formationKeyStruct.formationPreset);
        }
    }

    public List<BaseFormationPreset> GetAllowedFormations()
    {
        return allowedFormations;
    }

    public List<FormationKeyStruct> GetAllFormationKeyPairs()
    {
        return formationInputPreset.FormationPresetAndKeyList;
    }
}
