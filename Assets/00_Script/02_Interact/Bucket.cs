using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Item,IPickupable
{   
    public bool isFilled = false; //false는 비었음 true는 채워짐

    [SerializeField] private Sprite filledSprite;
    [SerializeField] private Sprite defaultSprite;


    [SerializeField] private Material emptyMaterial;
    [SerializeField] private Material filledMaterial;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Pickup(Player player)
    {
        if (player.PlayerInventory.HasItem()) return; // 이미 아이템이 있는 경우 픽업 불가
        player.PlayerInventory.SetCurrentItem(gameObject);
        gameObject.transform.SetParent(player.transform); // 플레이어의 자식으로 설정하여 함께 이동하도록 함
        gameObject.transform.localPosition = Vector3.up; // 플레이어의 위치에 아이템
    }

    private void Start()
    {
        defaultSprite = itemSprite;
        ApplyEmptyVisual();

    }
    public void Fill()
    {   
        
        isFilled = true;
        itemSprite = filledSprite;
        ApplyFilledVisual();
    }

    public void Empty()
    {
        isFilled = false;
        itemSprite = defaultSprite;
        ApplyEmptyVisual();
    }

    private void ApplyFilledVisual()
    {
        Material[] mats = meshRenderer.materials;
        mats[0] = filledMaterial;
        meshRenderer.materials = mats;
    }

    private void ApplyEmptyVisual()
    {
        Material[] mats = meshRenderer.materials;
        mats[0] = emptyMaterial;
        meshRenderer.materials = mats;
    }
}

