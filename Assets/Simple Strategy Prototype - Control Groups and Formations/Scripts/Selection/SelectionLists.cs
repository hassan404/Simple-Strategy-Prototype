using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class SelectionLists : MonoBehaviour
{
    public static SelectionLists instance;

    //1. purposefully didnt use = new() to keep compatibility with older C# versions
    //2. allSelectables will be used later when comnat is added
    [SerializeField]
    private List<Selectable> allSelectables = new List<Selectable>(); 
    [SerializeField]
    private List<Selectable> selectedSelectables = new List<Selectable>();

    public UnityEvent OnSelectionChanged;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddSelectable(Selectable selectable)
    {
        allSelectables.Add(selectable);
    }

    public void RemoveSelectable(Selectable selectable)
    {
        allSelectables.Remove(selectable);
    }

    public void AddSelected (Selectable selectable)
    {
        selectedSelectables.Add(selectable);
        selectable.Select();
        OnSelectionChanged?.Invoke();
    }
    public void AddSelecteds (List<Selectable> selectables)
    {
        selectedSelectables.AddRange(selectables);
        foreach (ISelectable selectable in selectables)
            selectable.Select();
        OnSelectionChanged?.Invoke();
    }
    public void RemoveSelected(Selectable selectable)
    {
        selectable.Deselect();
        selectedSelectables.Remove(selectable);
        OnSelectionChanged?.Invoke();
    }

    public void RemoveSelected(List<Selectable> selectables)
    {
        foreach (Selectable selectable in selectables)
        {
            selectable.Deselect();
            selectedSelectables.Remove(selectable);
        }
        OnSelectionChanged?.Invoke();
    }

    public void RemoveAllSelected()
    {
        foreach (Selectable selectable in selectedSelectables)
        {
            selectable.Deselect();
        }
        selectedSelectables.Clear();
        OnSelectionChanged?.Invoke();
    }

    public List<Selectable> GetAllSelected()
    {
        return selectedSelectables;
    }

    public List<Selectable> GetAllSelectables()
    {
        return allSelectables;
    }

}
