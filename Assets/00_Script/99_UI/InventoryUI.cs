
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Image axeImage;
    [SerializeField] private Image fishingRodImage;
    [SerializeField] private Image inventoryImage;
    [SerializeField] private Player _player;
    [SerializeField] private CraftManager _craftManager;

    private void Start()
    {
        
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
            var item = _player.PlayerInventory.GetCurrentItem();
            inventoryImage.sprite = item.ItemSprite;
            Color color = inventoryImage.color;
            color.a = 1.0f;
            inventoryImage.color = color;
            
            
            Debug.Log($"인벤토리 UI 업데이트: {_player.PlayerInventory.GetCurrentItemType()}");
        }
        else
        {
            Color color = inventoryImage.color;
            color.a = 0.0f;
            inventoryImage.color = color;
        }
    }

    private void OnAxeCrafted()
    {
        Color color = axeImage.color;
        color.a = 1.0f;
        axeImage.color = color;
    }

    private void OnFishingRodCrafted()
    {
        Color color = fishingRodImage.color;
        color.a = 1.0f;
        fishingRodImage.color = color;
    }
}
