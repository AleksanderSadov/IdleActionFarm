using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    public TextMeshProUGUI moneyCountText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        moneyCountText.text = gameManager.money.ToString();
    }
}
