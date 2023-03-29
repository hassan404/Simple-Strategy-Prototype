using System.Collections;
using UnityEngine;

/// <summary>
/// Temporary container for the FormationPreset until Unit script takes it over
/// </summary>

[RequireComponent(typeof(IMoveable))]
public class Formationable : MonoBehaviour
{
    [SerializeField]
    private float collisionRadius = 0.5f;
    [SerializeField]
    private BaseFormationPreset myFormationPreset;
    public float CollisionRadius { get => collisionRadius; set => collisionRadius = value; }
    public BaseFormationPreset MyFormationPreset { get => myFormationPreset; set => myFormationPreset = value; }
}