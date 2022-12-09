using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseFormationPreset: ScriptableObject
{
    public virtual List<Vector3> GetFormationOffsets(int count)
    {
        return Enumerable.Repeat(Vector3.zero, count).ToList();
    }
}
