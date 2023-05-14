using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float targetTimer = 0.5f;
    private float timer;
    private Coroutine createPollutionCoroutine;

    private void Start()
    {
        createPollutionCoroutine = StartCoroutine(CreatePollution(Random.Range(1, 4)));
    }

    IEnumerator CreatePollution(int iterate)
    {
        for (int i = 0; i < iterate; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));
            PollutionSpawner.Instance.Spawn(transform.position);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (timer > targetTimer)
        {
            StopCoroutine(createPollutionCoroutine);
            Destroy(gameObject);
        }
    }
}
