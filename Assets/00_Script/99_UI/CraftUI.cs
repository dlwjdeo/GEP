using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftUI : MonoBehaviour
{
    [SerializeField] private GameObject craftPanel;


    public void Awake()
    {
        craftPanel.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (craftPanel.activeSelf)
            {
                HideCraftPanel();
            }
            else
            {
                ShowCraftPanel();
            }
        }
    }

    public void ShowCraftPanel()
    {
        craftPanel.SetActive(true);
    }

    public void HideCraftPanel()
    {
        craftPanel.SetActive(false);
    }
}
