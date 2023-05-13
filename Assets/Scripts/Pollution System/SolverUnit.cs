using UnityEngine;
using TMPro;

public class SolverUnit : MonoBehaviour
{
    [Header("User Interface")]
    [SerializeField] private TextMeshPro amountText;

    private void Start()
    {
        SetAmountText(0);
    }

    public void SetAmountText(int amount)
    {
        amountText.text = amount.ToString();
    }
}
