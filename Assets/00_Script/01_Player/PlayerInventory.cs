using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject currentItemObject;
    public bool isUnlockFishing;
    public Action OnInventoryChanged;

    [Header("Drop Ground")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float dropDistance = 1f;
    [SerializeField] private float rayStartHeight = 50f;
    [SerializeField] private float rayDistance = 100f;
    [SerializeField] private float dropGroundOffset = 0.55f;

    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
        isUnlockFishing = false;
    }

    public ItemType GetCurrentItemType()
    {
        if (currentItemObject != null)
        {
            Item item = currentItemObject.GetComponent<Item>();

            if (item != null)
            {
                return item.ItemType;
            }
        }

        return ItemType.None;
    }

    public Item GetCurrentItem()
    {
        if (currentItemObject == null)
            return null;

        var item = currentItemObject.GetComponent<Item>();

        if (item == null)
            return null;

        return item;
    }

    public void SetCurrentItem(GameObject itemObject)
    {
        currentItemObject = itemObject;
        OnInventoryChanged?.Invoke();

        if (_player != null)
            _player.RefreshForwardTarget();
    }

    public bool HasItem()
    {
        return currentItemObject != null;
    }

    public void DropItem()
    {
        if (currentItemObject == null)
            return;

        currentItemObject.transform.SetParent(null);

        Vector3 dropPosition = transform.position + transform.forward * dropDistance;
        dropPosition = SnapDropPositionToGround(dropPosition);

        currentItemObject.transform.position = dropPosition;

        SetCurrentItem(null);

        if (_player != null)
            _player.RefreshForwardTarget();
    }

    private Vector3 SnapDropPositionToGround(Vector3 position)
    {
        Vector3 rayStart = new Vector3(position.x, position.y + rayStartHeight, position.z);

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, rayDistance, groundLayerMask, QueryTriggerInteraction.Ignore))
        {
            position.y = hit.point.y + dropGroundOffset;
        }

        return position;
    }

    public void RemoveItem()
    {
        if (currentItemObject != null)
        {
            Destroy(currentItemObject);
            SetCurrentItem(null);

            if (_player != null)
                _player.RefreshForwardTarget();
        }
    }
}