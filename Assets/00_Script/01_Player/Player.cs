using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Ray Settings")]
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private LayerMask ItemLayer;

    public PlayerInventory PlayerInventory => playerInventory;
    public GameObject FowardGameObject { get; private set; }

    public void Update()
    {
        FowardGameObject = CheckForward();
        
        // 입력 처리 후 각종 행동 처리
        if(FowardGameObject != null)
        {
            if(FowardGameObject.TryGetComponent<Item>(out var item))
            {
                item.BindPlayer(this); // 아이템이 있을 때 플레이어 참조 전달
            }

            if(Input.GetKeyDown(KeyCode.X))
            {
                if(FowardGameObject.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Interact();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            HandlePickupOrDrop();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(playerInventory.HasItem() && playerInventory.currentItemObject.TryGetComponent<IEatable>(out var eatable))
            {
                eatable.Eat();
            }
        }
    }

    private GameObject CheckForward()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        bool isHit = Physics.Raycast(ray, out RaycastHit hit, interactRange, ItemLayer);

        Debug.DrawRay(ray.origin, ray.direction * interactRange, isHit ? Color.green : Color.red);

        if (isHit)
        {
            GameObject go = hit.collider.gameObject;
            if (go != null)
            {
                return go;
            }
        }
        return null;
    }

    private void HandlePickupOrDrop()
    {
        if(playerInventory.HasItem())
        {
            playerInventory.DropItem();
            return;
        }

        if(FowardGameObject != null && FowardGameObject.TryGetComponent<IPickupable>(out var pickupable))
        {
            pickupable.Pickup();
        }
    }

    public void Interact()
    {
        if(FowardGameObject != null && FowardGameObject.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.Interact();
        }
    }
}
