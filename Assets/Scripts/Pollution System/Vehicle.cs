using UnityEngine;
using TMPro;

public class Vehicle : MonoBehaviour
{
    [Header("User Interface")]
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshPro amountText;

    private void Start()
    {
        SetAmountText(0, 0);
        popup.SetActive(false);
    }

    public void SetAmountText(int amount, int full)
    {
        if (popup.activeInHierarchy == false)
        {
            popup.SetActive(true);
        }

        amountText.text = amount.ToString() + " / " + full.ToString();
    }
}
