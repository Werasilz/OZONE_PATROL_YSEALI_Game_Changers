using UnityEngine;

[CreateAssetMenu(fileName = "Solver", menuName = "Solver", order = 0)]
public class Solver : ScriptableObject
{
    public string solverName;

    [Header("Solver Stats")]
    public int level;
    public int requesterRequired;

    [Header("Cooldown")]
    public float cooldownTime;
}