using System;
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
    private Comparer comparer;
    public Comparer Comparer => comparer;
    public Vector3 movementInput { get; private set; }
    public float fishingDuration { get; private set; }
    public float loggingDuration { get; private set; }
    public float eatingDuration { get; private set; }
    public float cookingDuration { get; private set; }
    public float repairDuration { get; private set; }

    public GameObject[] prefabToSpawn { get; private set; }

    public Action<string, GameObject> OnChangeForwardGameObject;
    public Action OnPressedInteract;

    public void Awake()
    {
        if (playerInventory == null)
            playerInventory = GetComponent<PlayerInventory>();

        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

        comparer = new Comparer(this);

        stateMachine = new PlayerStateMachine(this);

        loggingDuration = 3f;
    }

    public void Update()
    {
        DetectMovementInput();
        
        stateMachine.Update();
        
        // 입력 처리 후 각종 행동 처리
        if(FowardGameObject != null)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
             

                if(FowardGameObject.TryGetComponent<IInteractable>(out var interactable))
                {
                    OnPressedInteract?.Invoke(); //
                    interactable.Interact(this);
                    
                }
            }
        }
        else
        {
            if(PlayerInventory.GetCurrentItemType() == ItemType.Apple)
            {
                if(Input.GetKeyDown(KeyCode.X) && PlayerInventory.currentItemObject.TryGetComponent<Apple>(out var apple))
                {
                    apple.Plant(this);
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
        float fanHalfAngle = 45f;
        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;

        GameObject bestTarget = null;
        float bestDistance = 5f;
        Transform[] allTransforms = FindObjectsOfType<Transform>();

        foreach (var target in allTransforms)
        {
            if (target == null) continue;
            if (target == transform) continue;

            GameObject targetObject = target.gameObject;
            if (((1 << targetObject.layer) & ItemLayer.value) == 0) continue;

            Vector3 toTarget = target.position - origin;
            toTarget.y = 0f;

            if (toTarget.sqrMagnitude <= Mathf.Epsilon) continue;

            float angle = Vector3.Angle(forward, toTarget);
            if (angle > fanHalfAngle) continue;

            float distance = toTarget.sqrMagnitude;
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestTarget = targetObject;
            }
        }

        Color debugColor = bestTarget != null ? Color.green : Color.red;
        Debug.DrawRay(origin, Quaternion.Euler(0f, -fanHalfAngle, 0f) * forward * interactRange, debugColor);
        Debug.DrawRay(origin, forward * interactRange, debugColor);
        Debug.DrawRay(origin, Quaternion.Euler(0f, fanHalfAngle, 0f) * forward * interactRange, debugColor);

        return bestTarget;
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
        if(stateMachine.currentState == stateMachine.fishingState || 
        stateMachine.currentState == stateMachine.loggingState|| 
        stateMachine.currentState == stateMachine.eatingState||
        stateMachine.currentState == stateMachine.cookingState) return;
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(horizontal, 0f, vertical).normalized;
    }

    public void RefreshForwardTarget()
    {   
        
        FowardGameObject = CheckForward();
        OnChangeForwardGameObject?.Invoke(
            comparer.CheckAllInteractable(playerInventory.GetCurrentItem(), FowardGameObject),
            FowardGameObject
        );
      
    }

    public void SetFishingDuration(float duration) => fishingDuration = duration;
    public void SetLoggingDuration(float duration) => loggingDuration = duration;   
    public void SetEatingDuration(float duration) => eatingDuration = duration;
    public void SetCookingDuration(float duration) => cookingDuration = duration;
    public void SetRepairDuration(float duration) => repairDuration = duration;

    public void SetPrefabToSpawn(GameObject[] prefabs) => prefabToSpawn = prefabs;
}
