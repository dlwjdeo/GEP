using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private CraftManager craftManager;
    public CraftManager CraftManager => craftManager;

    [SerializeField] private FishingManager fishingManager;
    public FishingManager FishingManager => fishingManager;

    [SerializeField] private MissionManager missionManager;
    public MissionManager MissionManager => missionManager;

    [SerializeField] private SpawnManager spawnManager;
    public SpawnManager SpawnManager => spawnManager;

    [SerializeField] private UIManager uiManager;
    public UIManager UIManager => uiManager;

    [Header("Player Stats")]
    [SerializeField] private float playerMaxHealth = 100f;
    [SerializeField] private float playerCurrentHealth = 100f;
    [SerializeField] private float healthDecreaseRate = 1f;

    [Header("Fire Stats")]
    [SerializeField] private float fireMaxHealth = 100f;
    [SerializeField] private float fireCurrentHealth = 100f;
    [SerializeField] private float fireDecreaseRate = 1f;

    [Header("Rocket Stats")]
    [SerializeField] private float rocketMaxHealth = 200f;
    [SerializeField] private float rocketCurrentHealth = 0f;

    public float PlayerMaxHealth => playerMaxHealth;
    public float PlayerCurrentHealth => playerCurrentHealth;

    public float FireMaxHealth => fireMaxHealth;
    public float FireCurrentHealth => fireCurrentHealth;

    public float RocketMaxHealth => rocketMaxHealth;
    public float RocketCurrentHealth => rocketCurrentHealth;

   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    private void Update()
    {
        DecreasePlayerHealth();
        DecreaseFireHealth();
    }


    //Rect범위를 초과하지 않아야 하기 때문에 Clamp를 사용해서 범위 제한
    private void DecreasePlayerHealth()
    {
        playerCurrentHealth = Mathf.Clamp(
            playerCurrentHealth - healthDecreaseRate * Time.deltaTime,
            0f,
            playerMaxHealth
        );

        if (playerCurrentHealth <= 0f)
        {
            GameOver();
        }
    }

    private void DecreaseFireHealth()
    {
        fireCurrentHealth = Mathf.Clamp(
            fireCurrentHealth - fireDecreaseRate * Time.deltaTime,
            0f,
            fireMaxHealth
        );

        if (fireCurrentHealth <= 0f)
        {
            GameOver();
        }
    }

    public void IncreasePlayerHealth(float amount)
    {
        playerCurrentHealth = Mathf.Clamp(playerCurrentHealth + amount, 0f, playerMaxHealth);
    }

    public void IncreaseFireHealth(float amount)
    {
        fireCurrentHealth = Mathf.Clamp(fireCurrentHealth + amount, 0f, fireMaxHealth);
    }

    public void IncreaseRocketHealth(float amount)
    {
        rocketCurrentHealth = Mathf.Clamp(rocketCurrentHealth + amount, 0f, rocketMaxHealth);

        if(rocketCurrentHealth>=rocketMaxHealth)
        {
            GameClear();
        }
    }


    public void GameClear()
    {
        uiManager.PopupResult("GameClear");
    }
    public void GameOver()
    {
        uiManager.PopupResult("Game Over");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
