using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float targetTimer = 0.5f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (timer > targetTimer)
        {
            Destroy(gameObject);
        }
    }
}
