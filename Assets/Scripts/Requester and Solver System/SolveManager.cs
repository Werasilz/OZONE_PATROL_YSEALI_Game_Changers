using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SolveManager : Singleton<SolveManager>
{
    [Header("Solver")]
    [SerializeField] private Solver[] solvers;
    public Solver[] GetSolvers => solvers;
    [SerializeField] private Transform[] solversTransform;
    public Transform[] GetSolversTransform => solversTransform;

    [Header("Solver Unit")]
    [SerializeField] private SolverUnit[] solverUnits;
    public SolverUnit[] GetSolverUnits => solverUnits;

    public void AddRequester(int ID)
    {
        // Add requester
        solverUnits[ID].currentRequesters += 1;
        solverUnits[ID].requestersText.text = solverUnits[ID].currentRequesters.ToString();

        // Reach the required
        if (solverUnits[ID].currentRequesters == solvers[ID].requesterRequired)
        {
            StartCoroutine(StartCooldown(ID));
        }
    }

    IEnumerator StartCooldown(int ID)
    {
        if (solverUnits[ID].isCooldownActive)
        {
            // Cooldown is already active, so do nothing
            yield break;
        }

        // Start cooldown
        solverUnits[ID].isCooldownActive = true;
        solverUnits[ID].remainingCooldownTime = solvers[ID].cooldownTime;

        // Cooling down
        while (solverUnits[ID].remainingCooldownTime > 0f)
        {
            solverUnits[ID].remainingCooldownTime -= Time.deltaTime;
            solverUnits[ID].cooldownIndicator.fillAmount = solverUnits[ID].remainingCooldownTime / solvers[ID].cooldownTime;
            yield return null;
        }

        // Reset cooldown
        solverUnits[ID].isCooldownActive = false;
        solverUnits[ID].remainingCooldownTime = 0;

        // Reset requester
        solverUnits[ID].currentRequesters = 0;
        solverUnits[ID].requestersText.text = solverUnits[ID].currentRequesters.ToString();

        // Todo : spawn air pollution here
    }

    [System.Serializable]
    public struct SolverUnit
    {
        public int currentRequesters;
        public bool isCooldownActive;
        public float remainingCooldownTime;
        public TextMeshPro requestersText;
        public Image cooldownIndicator;
    }
}