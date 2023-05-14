using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float targetTimer = 0.5f;
    [SerializeField] private TextMeshPro popupText;
    private float timer;
    private float fadingTimeElapsed;

    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        fadingTimeElapsed += Time.deltaTime;

        if (timer > targetTimer / 2)
        {
            float fadeAmount = Mathf.Clamp01(fadingTimeElapsed / 1);

            // Set color with faded alpha
            Color textColor = popupText.color;
            textColor.a = 1.0f - fadeAmount; // alpha is the inverse of fade amount
            popupText.color = textColor;
        }

        if (timer > targetTimer)
        {
            Destroy(gameObject);
        }
    }
}
