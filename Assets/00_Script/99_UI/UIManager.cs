using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private FigureUI figureUI;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private ResultUI resultUI;
    [SerializeField] private GameManager gameManager;

    public GameManager GameManager => gameManager;

    public void PopupResult(string message, float timer = 0f)
    {
        resultUI.PopupResult(message, timer);
    }

    public void RefreshFigureUI()
    {
        figureUI.RefreshAll();
    }
}
