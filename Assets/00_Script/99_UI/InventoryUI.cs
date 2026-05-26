using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Image axeImage;
    [SerializeField] private Image fishingRodImage;

    [SerializeField] private Player _player;
    [SerializeField] private CraftManager _craftManager;

    private void Start()
    {
        axeImage.color = new Color(1f, 0f, 0f, 1f); // 초기에는 빨갛게 설정
        fishingRodImage.color = new Color(1f, 0f, 0f, 1f); // 초기에는 빨갛게 설정
    }

    private void OnEnable()
    {
        if (_player != null)
        {
            _player.PlayerInventory.OnInventoryChanged += UpdateInventoryUI;
        }
        if(_craftManager != null)
        {
            _craftManager.OnAxeCrafted += OnAxeCrafted;
            _craftManager.OnFishingRodCrafted += OnFishingRodCrafted;
        }
        UpdateInventoryUI();
    }
    private void OnDisable()
    {
        if (_player != null)
        {
            _player.PlayerInventory.OnInventoryChanged -= UpdateInventoryUI;
        }
        if(_craftManager != null)
        {
            _craftManager.OnAxeCrafted -= OnAxeCrafted;
            _craftManager.OnFishingRodCrafted -= OnFishingRodCrafted;
        }
    }

    private void UpdateInventoryUI()
    {
        if (_player.PlayerInventory.HasItem())
        {
            itemNameText.text = _player.PlayerInventory.GetCurrentItemType().ToString();
            Debug.Log($"인벤토리 UI 업데이트: {_player.PlayerInventory.GetCurrentItemType()}");
        }
        else
        {
            itemNameText.text = "No Item";
        }
    }

    private void OnAxeCrafted()
    {
        axeImage.color = new Color(0f, 1f, 0f, 1f); // 도끼 제작 시 초록색으로 변경
    }

    private void OnFishingRodCrafted()
    {
        fishingRodImage.color = new Color(0f, 1f, 0f, 1f); // 낚시대 제작 시 초록색으로 변경
    }
}
