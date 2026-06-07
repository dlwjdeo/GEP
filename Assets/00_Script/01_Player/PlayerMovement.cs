using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 30f;

    [Header("Ground Limit")]
    [SerializeField] private BoxCollider moveBoundsCollider;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float rayStartHeight = 50f;
    [SerializeField] private float rayDistance = 100f;
    [SerializeField] private float groundOffset = 0f;

    private float currentMoveSpeed;
    private Player _player;

    public float MoveSpeed => moveSpeed;

    private void Awake()
    {
        _player = GetComponent<Player>();
        currentMoveSpeed = moveSpeed;
    }

    public void Move(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 movement = direction.normalized * currentMoveSpeed * Time.deltaTime;
        Vector3 nextPosition = transform.position + movement;

        nextPosition = ClampToInnerCircle(nextPosition);
        nextPosition = SnapToGround(nextPosition);

        transform.position = nextPosition;

        if (_player != null)
        {
            _player.RefreshForwardTarget();
        }
    }

    private Vector3 ClampToInnerCircle(Vector3 position)
    {
        if (moveBoundsCollider == null)
            return position;

        Bounds bounds = moveBoundsCollider.bounds;
        Vector3 center = bounds.center;
        float radius = Mathf.Min(bounds.extents.x, bounds.extents.z);

        Vector2 offset = new Vector2(position.x - center.x, position.z - center.z);

        if (offset.magnitude > radius)
        {
            offset = offset.normalized * radius;
            position.x = center.x + offset.x;
            position.z = center.z + offset.y;
        }

        return position;
    }

    private Vector3 SnapToGround(Vector3 position)
    {
        Vector3 rayStart = new Vector3(position.x, position.y + rayStartHeight, position.z);

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, rayDistance, groundLayerMask, QueryTriggerInteraction.Ignore))
        {
            position.y = hit.point.y + groundOffset;
        }

        return position;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        currentMoveSpeed = newSpeed;
    }
}