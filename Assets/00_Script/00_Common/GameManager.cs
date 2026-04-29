using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Stats")]
    [SerializeField] private float playerMaxHealth = 100f;
    [SerializeField] private float playerCurrentHealth = 100f;
    [SerializeField] private float healthDecreaseRate = 1f; // 체력 감소 속도
    [Header("Fire Stats")]
    [SerializeField] private float fireMaxHealth = 100f;
    [SerializeField] private float fireCurrentHealth = 100f;
    [SerializeField] private float fireDecreaseRate = 1f; // 불의 체력 감소 속도
    [Header("Rocket Stats")]
    [SerializeField] private float rocketMaxHealth = 2000f;
    [SerializeField] private float rocketCurrentHealth = 0f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
        DecreasePlayerHealth();
        DecreaseFireHealth();
    }

    private void DecreasePlayerHealth()
    {
        playerCurrentHealth -= healthDecreaseRate * Time.deltaTime;
        playerCurrentHealth = Mathf.Clamp(playerCurrentHealth, 0f, playerMaxHealth);
        if (playerCurrentHealth <= 0f)
        {
            GameOver();
        }
    }

    private void DecreaseFireHealth()
    {
        fireCurrentHealth -= fireDecreaseRate * Time.deltaTime;
        fireCurrentHealth = Mathf.Clamp(fireCurrentHealth, 0f, fireMaxHealth);
        if (fireCurrentHealth <= 0f)
        {
            GameOver();
        }
    }

    public void IncreasePlayerHealth(float amount)
    {
        playerCurrentHealth += amount;
        playerCurrentHealth = Mathf.Clamp(playerCurrentHealth, 0f, playerMaxHealth);
    }

    public void IncreaseFireHealth(float amount)
    {
        fireCurrentHealth += amount;
        fireCurrentHealth = Mathf.Clamp(fireCurrentHealth, 0f, fireMaxHealth);
    }

    public void IncreaseRocketHealth(float amount)
    {
        rocketCurrentHealth += amount;
        rocketCurrentHealth = Mathf.Clamp(rocketCurrentHealth, 0f, rocketMaxHealth);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        // 게임 오버 처리 로직 추가 (예: 씬 리로드, UI 표시 등)
    }
}

public enum GameState
{
    InGame,
    GameOver
}