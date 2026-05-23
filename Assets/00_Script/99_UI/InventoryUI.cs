using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;

    private Player _player;


    private void OnEnable()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
            _player.PlayerInventory.OnInventoryChanged += UpdateInventoryUI;
        }
        UpdateInventoryUI();
    }
    private void OnDisable()
    {
        if (_player != null)
        {
            _player.PlayerInventory.OnInventoryChanged -= UpdateInventoryUI;
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
}
