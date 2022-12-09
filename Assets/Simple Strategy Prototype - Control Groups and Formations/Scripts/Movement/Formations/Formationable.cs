using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IMoveable))]
public class Formationable : MonoBehaviour
{
    [SerializeField]
    private BaseFormationPreset myFormationPreset;
    public BaseFormationPreset MyFormationPreset { get => myFormationPreset; set => myFormationPreset = value; }
}