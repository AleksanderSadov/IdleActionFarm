using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public UIConfig config;
    public Camera cameraUI;
    public GameObject floatingCoinPrefab;
    public RectTransform floatingCoinTarget;
    public TextMeshProUGUI moneyCountText;

    private int currentMoneyDisplayed = 0;
    private int targetMoneyToDisplay = 0;
    private float moneyCountTextOriginalFontSize;

    private void Start()
    {
        moneyCountTextOriginalFontSize = moneyCountText.fontSize;
        EventManager.AddListener<UpdateMoneyUIEvent>(StartFloatingCoinAnimation);
    }

    private void Update()
    {
        if (currentMoneyDisplayed != targetMoneyToDisplay)
        {
            moneyCountText.fontSize = moneyCountTextOriginalFontSize * config.moneyDisplayCountIncrementScale;
        }
        else
        {
            moneyCountText.fontSize = moneyCountTextOriginalFontSize;
        }

        moneyCountText.text = currentMoneyDisplayed.ToString();
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

        uiFloatingCoin.GetComponent<AudioSource>().Play();

        uiFloatingCoin.anchorMin = floatingCoinTarget.anchorMin;
        uiFloatingCoin.anchorMax = floatingCoinTarget.anchorMax;
        uiFloatingCoin.pivot = floatingCoinTarget.pivot;
        uiFloatingCoin.DOSizeDelta(floatingCoinTarget.sizeDelta, config.floatingCoinAnimationDuration);
        uiFloatingCoin
            .DOAnchorPos(floatingCoinTarget.anchoredPosition, config.floatingCoinAnimationDuration)
            .OnComplete(() =>
                {
                    SmoothCurrentMoneyToNewValue(evt.newMoneyValue);
                    uiFloatingCoin.GetComponent<Image>().enabled = false;
                    Destroy(uiFloatingCoin.gameObject, uiFloatingCoin.GetComponent<AudioSource>().clip.length);
                }
            );
    }

    private void SmoothCurrentMoneyToNewValue(int newMoneyValue)
    {
        targetMoneyToDisplay = newMoneyValue;

        if (currentMoneyDisplayed < targetMoneyToDisplay)
        {
            DOTween.To(
                () => currentMoneyDisplayed,
                x => currentMoneyDisplayed = x,
                targetMoneyToDisplay,
                config.moneyDisplayCountAnimationDuration
            );
        }
    }
}
