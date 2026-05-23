using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FigureUI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header("Bars")]
    [SerializeField] private RectTransform hungerBar;
    [SerializeField] private RectTransform fireBar;
    [SerializeField] private RectTransform rocketBar;

    [SerializeField] private GameObject resultPanel;
    private Button restartButton; 

    private float hungerMaxWidth;
    private float fireMaxWidth;
    private float rocketMaxWidth;

    private void Awake()
    {
        hungerMaxWidth = hungerBar.sizeDelta.x;
        fireMaxWidth = fireBar.sizeDelta.x;
        rocketMaxWidth = rocketBar.sizeDelta.x;
    }

    private void Start()
    {
        RefreshAll();
        restartButton=GetComponentInChildren<Button>();
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
        SetBarWidth(hungerBar, hungerMaxWidth, current, max);
    }

    public void RefreshFire(float current, float max)
    {
        SetBarWidth(fireBar, fireMaxWidth, current, max);
    }

    public void RefreshRocket(float current, float max)
    {
        SetBarWidth(rocketBar, rocketMaxWidth, current, max);
    }

    //초기값에 의해 수치Bar들이 비례적으로 줄어야하기 때문에 세팅함수가 필요함
    private void SetBarWidth(RectTransform targetBar, float maxWidth, float current, float max)
    {
        float ratio = max <= 0f ? 0f : current / max;
        ratio = Mathf.Clamp01(ratio);

        Vector2 size = targetBar.sizeDelta;
        size.x = maxWidth * ratio;
        targetBar.sizeDelta = size;
    }

    public void PopupResult(string  message)
    {
        var tmp = resultPanel.GetComponentInChildren<TextMeshProUGUI>();
        if(tmp!=null)
        {
            tmp.text = message;
            resultPanel.SetActive(true);
        }
    }
 
}
