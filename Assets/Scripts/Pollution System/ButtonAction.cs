using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    [Header("Solve Unit")]
    [SerializeField] private GameObject solverPrefab;
    [SerializeField] private int scoreRequired;
    [SerializeField] private Transform solverSpawnPoint;
    [SerializeField] private GameObject createdSolver;

    [Header("Line")]
    [SerializeField] private LineSpawner lineSpawner;
    [SerializeField] private float timeToGetIntoLine;

    [Header("Score")]
    public int currentScore;

    [Header("Cooldown")]
    [SerializeField] private float cooldownTime;
    [SerializeField] private float remainingCooldownTime;
    [SerializeField] private bool isCooldownActive;

    [Header("User Interface")]
    [SerializeField] private Image cooldownIndicator;

    public void Interact()
    {
        if (createdSolver == null && isCooldownActive == false)
        {
            // Spawn solver unit
            GameObject newSolver = Instantiate(solverPrefab, solverSpawnPoint.position, Quaternion.identity);
            createdSolver = newSolver;
            StartCoroutine(GetPeopleIntoLine());
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator GetPeopleIntoLine()
    {
        while (currentScore < scoreRequired)
        {
            yield return new WaitForSeconds(timeToGetIntoLine);

            currentScore += 1;
            createdSolver.GetComponent<SolverUnit>().SetAmountText(currentScore);
            Destroy(lineSpawner.GetSpawnedPeople[0].gameObject);
            lineSpawner.RemoveFirstPeople();

            // Reach the score
            if (currentScore == scoreRequired)
            {
                yield return new WaitForSeconds(1);
                Destroy(createdSolver);
                break;
            }

            yield return null;
        }
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
        remainingCooldownTime = cooldownTime;

        // Cooling down
        while (remainingCooldownTime > 0f)
        {
            remainingCooldownTime -= Time.deltaTime;
            cooldownIndicator.fillAmount = remainingCooldownTime / cooldownTime;
            yield return null;
        }

        // Reset cooldown
        isCooldownActive = false;
        remainingCooldownTime = 0;

        // Reset requester
        currentScore = 0;
    }
}
