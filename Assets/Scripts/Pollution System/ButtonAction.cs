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

    private Coroutine moveCoroutine;
    private Coroutine getPeopleIntoLineCoroutine;
    private Coroutine cooldownCoroutine;

    public void Interact()
    {
        if (createdVehicle == null && isCooldownActive == false)
        {
            // Spawn solver unit
            GameObject newVehicle = Instantiate(vehiclePrefab, vehicleSpawnPoint.position + (Vector3.up * 10), Quaternion.identity);
            createdVehicle = newVehicle;
            moveCoroutine = StartCoroutine(Move(createdVehicle, vehicleSpawnPoint.position));
            getPeopleIntoLineCoroutine = StartCoroutine(GetPeopleIntoLine());
            cooldownCoroutine = StartCoroutine(StartCooldown());
        }
    }

    public void ClearAction()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        if (getPeopleIntoLineCoroutine != null)
            StopCoroutine(getPeopleIntoLineCoroutine);

        if (cooldownCoroutine != null)
            StopCoroutine(cooldownCoroutine);

        Destroy(createdVehicle);
    }

    IEnumerator Move(GameObject vehicle, Vector3 position)
    {
        while (Vector2.Distance(vehicle.transform.position, position) > 0.25f)
        {
            vehicle.transform.Translate(Vector3.down * 30 * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }

    IEnumerator GetPeopleIntoLine()
    {
        while (currentScore < scoreRequired)
        {
            yield return new WaitForSeconds(timeToGetIntoLine);

            while (lineSpawner.GetSpawnedPeople.Count > 0 && lineSpawner.GetSpawnedPeople[0].IsStartWaiting)
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
                    PollutionManager.Instance.ReducePollutionScore(pollutionReduceScore, createdVehicle.transform.position);
                    break;
                }

                yield return null;
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
