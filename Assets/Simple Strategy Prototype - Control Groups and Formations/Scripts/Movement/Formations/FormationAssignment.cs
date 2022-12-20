using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormationAssignment : MonoBehaviour
{
    [SerializeField]
    private BaseFormationPreset defaultPreset;
    [SerializeField]
    private FormationLists formationLists;
    [SerializeField]
    private SelectionInput selectionInput;

    private void Start()
    {
        SetAllSelectablesToDefaultPreset();
    }

    private void OnDisable()
    {
        SelectionLists.instance.OnSelectionChanged.RemoveListener(SetSelectedsFormationsByMajorityVote);
    }

    private void OnEnable()
    {
        SelectionLists.instance.OnSelectionChanged.AddListener(SetSelectedsFormationsByMajorityVote);
    }



    private void SetAllSelectablesToDefaultPreset()
    {
        if (!formationLists.GetAllowedFormations().Contains(defaultPreset))
        {
            Debug.LogError("Default formation preset not found in the list of allowed formations.");
            return;
        }

        foreach (Selectable selectable in SelectionLists.instance.GetAllSelectables())
        {
            Formationable formationable = selectable.GetComponent<Formationable>();
            if (formationable)
            {
                formationable.MyFormationPreset = defaultPreset;
            }
        }
    }

    public void SetSelectedsFormationsByMajorityVote()
    {
        if (SelectionLists.instance.GetAllSelected().Count > 0)
        {
            List<Formationable> allFormationablesInSelectables = GetFormationablesFromSelectables();

            BaseFormationPreset majorityPreset = allFormationablesInSelectables.GroupBy(x => x.MyFormationPreset)
                               .OrderBy(g => g.Count())
                               .Last()
                               .Key;

            foreach (Formationable formationable in allFormationablesInSelectables)
            {
                formationable.MyFormationPreset = majorityPreset;
            }
        }
    }

    public List<Formationable> GetFormationablesFromSelectables()
    {
        return SelectionLists.instance.GetAllSelected()
                    .FindAll(IsFormationable)
                    .ConvertAll(x => x.GetComponent<Formationable>());
    }
    
    public List<IMoveable> GetMoveablesFromSelectables()
    {
        return SelectionLists.instance.GetAllSelected()
                    .FindAll(IsNotFormationableButMoveable)
                    .ConvertAll(x => x.GetComponent<IMoveable>());
    }
  
    private bool IsFormationable(Selectable selectable)
    {
        return selectable.GetComponent<Formationable>() is not null;
    }

    private bool IsNotFormationableButMoveable(Selectable selectable)
    {
        return selectable.GetComponent<Formationable>() is null &&
            selectable.GetComponent<IMoveable>() is not null;
    }

}