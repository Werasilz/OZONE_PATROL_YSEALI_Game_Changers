using UnityEngine;

[CreateAssetMenu(fileName = "Solver", menuName = "Solver", order = 0)]
public class Solver : ScriptableObject
{
    public string solverName;
    public GameObject solverPrefab;

    [Header("Solver Stats")]
    public int level;
    public int scoreRequired;

    [Header("Cooldown")]
    public float cooldownTime;
}