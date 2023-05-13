using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    [Header("Vehicle")]
    [SerializeField] private GameObject vehiclePrefab;
    [SerializeField] private int scoreRequired;
    [SerializeField] private int pollutionReduceScore;
    [SerializeField] private Transform vehicleSpawnPoint;
    private GameObject createdVehicle;

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
        if (createdVehicle == null && isCooldownActive == false)
        {
            // Spawn solver unit
            GameObject newSolver = Instantiate(vehiclePrefab, vehicleSpawnPoint.position, Quaternion.identity);
            createdVehicle = newSolver;
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
            createdVehicle.GetComponent<Vehicle>().SetAmountText(currentScore, scoreRequired);
            Destroy(lineSpawner.GetSpawnedPeople[0].gameObject);
            lineSpawner.RemoveFirstPeople();

            // Reach the score
            if (currentScore == scoreRequired)
            {
                yield return new WaitForSeconds(1);
                Destroy(createdVehicle);
                PollutionManager.Instance.ReducePollutionScore(pollutionReduceScore, createdVehicle.transform);
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
