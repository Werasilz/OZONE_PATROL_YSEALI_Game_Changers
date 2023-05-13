using System.Collections;
using UnityEngine;

public class PollutionMonster : MonoBehaviour, IInteractable
{
    [SerializeField] private float yAxisDestination;
    [SerializeField] private float speed;
    private Coroutine flyCoroutine;

    void Start()
    {
        flyCoroutine = StartCoroutine(Fly());

        IEnumerator Fly()
        {
            while (transform.position.y < yAxisDestination)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                yield return null;
            }

            // Reach the ceiling
            PollutionManager.Instance.AddPollutionScore(1);
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        StopCoroutine(flyCoroutine);
        Destroy(gameObject);
    }
}
