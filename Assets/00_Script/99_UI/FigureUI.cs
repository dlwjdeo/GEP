using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FigureUI : MonoBehaviour
{

    [Header("Bars")]
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image fireBar;
    [SerializeField] private Image rocketBar;
    private GameManager gameManager;

    private void Awake()
    {
        UIManager uiManager = GetComponentInParent<UIManager>();
        gameManager = uiManager.GameManager;
    }

    private void Start()
    {   
        RefreshAll();
    }

    public void Update()
    {
        RefreshAll();
    }

    public void RefreshAll()
    {
        RefreshHunger(gameManager.PlayerCurrentHealth, gameManager.PlayerMaxHealth);
        RefreshFire(gameManager.FireCurrentHealth, gameManager.FireMaxHealth);
        RefreshRocket(gameManager.RocketCurrentHealth, gameManager.RocketMaxHealth);
    }

    public void RefreshHunger(float current, float max)
    {
        SetBarFill(hungerBar, current, max);
    }

    public void RefreshFire(float current, float max)
    {
        SetBarFill(fireBar, current, max);
    }

    public void RefreshRocket(float current, float max)
    {
        SetBarFill(rocketBar, current, max);
    }

    private void SetBarFill(Image targetBar, float current, float max)
    {
        float ratio = max <= 0f ? 0f : current / max;
        targetBar.fillAmount = Mathf.Clamp01(ratio);
    }
}