using System.Collections;
using UnityEngine;

public class PollutionMonster : MonoBehaviour, IInteractable
{
    [SerializeField] private float yAxisDestination;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer monsterImage;
    [SerializeField] private SpriteRenderer deadImage;
    private bool isDead;
    private Coroutine flyCoroutine;

    void Start()
    {
        monsterImage.enabled = true;
        deadImage.enabled = false;

        flyCoroutine = StartCoroutine(Fly());

        IEnumerator Fly()
        {
            while (transform.position.y < yAxisDestination)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                yield return null;
            }

            // Reach the ceiling
            PollutionManager.Instance.AddPollutionScore(5);
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        if (isDead == false)
        {
            isDead = true;
            StopCoroutine(flyCoroutine);
            monsterImage.enabled = false;
            deadImage.enabled = true;
            Destroy(gameObject, 0.1f);
        }
    }
}
