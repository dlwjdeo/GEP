using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FigureUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header("Bars")]
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image fireBar;
    [SerializeField] private Image rocketBar;

    [SerializeField] private GameObject resultPanel;
    private Button restartButton;

    private void Start()
    {
        RefreshAll();
        restartButton = GetComponentInChildren<Button>();
        restartButton.onClick.AddListener(gameManager.Restart);
        resultPanel.SetActive(false);
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

    public void PopupResult(string message)
    {
        var tmp = resultPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = message;
            resultPanel.SetActive(true);
        }
    }
}