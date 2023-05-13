using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour, IInteractable
{
    [Header("Solve Unit")]
    [SerializeField] private Solver solver;
    [SerializeField] private Transform solverSpawnPoint;
    [SerializeField] private GameObject createdSolver;
    [SerializeField] private float timeToGetInLine;
    public int currentScore;

    [Header("Cooldown")]
    [SerializeField] private bool isCooldownActive;
    [SerializeField] private float remainingCooldownTime;

    [Header("User Interface")]
    [SerializeField] private Image cooldownIndicator;

    public void Interact()
    {
        if (createdSolver == null)
        {
            // Spawn solver unit
            GameObject newSolver = Instantiate(solver.solverPrefab, solverSpawnPoint.position, Quaternion.identity);
            createdSolver = newSolver;
            StartCoroutine(GetInLine(createdSolver.GetComponent<SolverUnit>()));
            StartCoroutine(StartCooldown());
        }
    }

    public IEnumerator GetInLine(SolverUnit solverUnit)
    {
        while (currentScore < solver.scoreRequired)
        {
            yield return new WaitForSeconds(timeToGetInLine);
            currentScore += 1;
            solverUnit.SetAmountText(currentScore);

            if (currentScore == solver.scoreRequired)
            {
                yield return new WaitForSeconds(1);
                Destroy(createdSolver);
            }

            yield return null;
        }

        currentScore = 0;
        createdSolver = null;
    }

    IEnumerator StartCooldown()
    {
        if (isCooldownActive)
        {
            // Cooldown is already active, so do nothing
            yield break;
        }

        // Start cooldown
        isCooldownActive = true;
        remainingCooldownTime = solver.cooldownTime;

        // Cooling down
        while (remainingCooldownTime > 0f)
        {
            remainingCooldownTime -= Time.deltaTime;
            cooldownIndicator.fillAmount = remainingCooldownTime / solver.cooldownTime;
            yield return null;
        }

        // Reset cooldown
        isCooldownActive = false;
        remainingCooldownTime = 0;

        // Reset requester
        currentScore = 0;
    }
}
