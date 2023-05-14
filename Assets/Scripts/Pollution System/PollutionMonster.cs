using System.Collections;
using UnityEngine;

public class PollutionMonster : MonoBehaviour, IInteractable
{
    [SerializeField] private float yAxisDestination;
    [SerializeField] private float speed;
    [SerializeField] private int pollutionReduceScore;

    [Header("User Interface")]
    [SerializeField] private SpriteRenderer monsterImage;
    [SerializeField] private SpriteRenderer deadImage;
    private bool isDead;
    private Coroutine flyCoroutine;

    void Start()
    {
        monsterImage.gameObject.SetActive(true);
        deadImage.gameObject.SetActive(false);

        flyCoroutine = StartCoroutine(Fly());

        IEnumerator Fly()
        {
            while (transform.position.y < yAxisDestination)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                yield return null;
            }

            // Reach the ceiling
            PollutionManager.Instance.AddPollutionScore(5, null);
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        if (isDead == false)
        {
            isDead = true;
            StopCoroutine(flyCoroutine);
            monsterImage.gameObject.SetActive(false);
            deadImage.gameObject.SetActive(true);
            PollutionManager.Instance.ReducePollutionScore(pollutionReduceScore, transform);
            Destroy(gameObject, 1f);
        }
    }
}
