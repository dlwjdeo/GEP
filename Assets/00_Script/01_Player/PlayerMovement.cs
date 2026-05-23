using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 30f;
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
        transform.position += movement;
        if(_player != null)
        {
            _player.RefreshForwardTarget();
        }
    }

    public void SetMoveSpeed(float newSpeed)
    {
        currentMoveSpeed = newSpeed;
    }
}
