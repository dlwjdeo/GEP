using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Ray Settings")]
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private LayerMask ItemLayer;

    public PlayerInventory PlayerInventory => playerInventory;
    public PlayerMovement PlayerMovement => playerMovement;
    public GameObject FowardGameObject { get; private set; }

    public PlayerStateMachine stateMachine;

    public Vector3 movementInput { get; private set; }
    public float fishingDuration { get; private set; }
    public float loggingDuration { get; private set; }
    public float eatingDuration { get; private set; }

    public void Awake()
    {
        stateMachine = new PlayerStateMachine(this);
    }

    public void Update()
    {
        DetectMovementInput();
        
        stateMachine.Update();
        FowardGameObject = CheckForward();
        
        // 입력 처리 후 각종 행동 처리
        if(FowardGameObject != null)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                if(FowardGameObject.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Interact(this);
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
                eatable.Eat(this);
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
            pickupable.Pickup(this);
        }
    }

    public void DetectMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(horizontal, 0f, vertical).normalized;
    }

    public void SetFishingDuration(float duration) => fishingDuration = duration;
    public void SetLoggingDuration(float duration) => loggingDuration = duration;   
    public void SetEatingDuration(float duration) => eatingDuration = duration;
}
