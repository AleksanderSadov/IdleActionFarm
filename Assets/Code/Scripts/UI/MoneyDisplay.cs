using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    public UIConfig config;
    public Camera cameraUI;
    public GameObject floatingCoinPrefab;
    public RectTransform floatingCoinTarget;
    public TextMeshProUGUI moneyCountText;

    private void Start()
    {
        EventManager.AddListener<UpdateMoneyUIEvent>(StartFloatingCoinAnimation);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<UpdateMoneyUIEvent>(StartFloatingCoinAnimation);
    }
    
    private void StartFloatingCoinAnimation(UpdateMoneyUIEvent evt)
    {
        Vector2 uiCoinScreenPosition = RectTransformUtility.WorldToScreenPoint(
            cameraUI,
            evt.sellPoint.transform.position
        );

        RectTransform uiFloatingCoin = Instantiate(
            floatingCoinPrefab,
            uiCoinScreenPosition,
            floatingCoinPrefab.transform.rotation,
            transform
        ).GetComponent<RectTransform>();

        uiFloatingCoin.anchorMin = floatingCoinTarget.anchorMin;
        uiFloatingCoin.anchorMax = floatingCoinTarget.anchorMax;
        uiFloatingCoin.pivot = floatingCoinTarget.pivot;
        uiFloatingCoin
            .DOAnchorPos(floatingCoinTarget.anchoredPosition, config.floatingCoinAnimationDuration)
            .OnComplete(() => OnFloatingCoinAnimationComplete(uiFloatingCoin.gameObject, evt.newMoneyValue));
    }

    private void OnFloatingCoinAnimationComplete(GameObject uiFloatingCoin, float newMoneyValue)
    {
        float currentMoneyDisplayed = float.Parse(moneyCountText.text);

        if (currentMoneyDisplayed < newMoneyValue)
        {
            moneyCountText.text = newMoneyValue.ToString();
        }

        Destroy(uiFloatingCoin);
    }
}
