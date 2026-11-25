using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private enum MoveState
    {
        Idle,
        MoveLeft,
        MoveRight
    }

    [SerializeField, Min(0f)] private float lateralSpeed = 6f;
    [SerializeField] private GameObject giftPrefab;
    [SerializeField] private Transform dropPoint;

    private Rigidbody body;
    private MoveState currentState = MoveState.Idle;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    /// A/D 또는 좌우 방향키 입력을 받아 이동 처리
    public void OnMove(InputValue value)
    {
        float horizontalInput = value.Get<Vector2>().x;

        if (horizontalInput < 0f)
        {
            currentState = MoveState.MoveLeft;
        }
        else if (horizontalInput > 0f)
        {
            currentState = MoveState.MoveRight;
        }
        else
        {
            currentState = MoveState.Idle;
        }
    }

    // 선물 떨어트리는 키
    public void OnDrop()
    {
        if (giftPrefab == null || dropPoint == null)
        {
            return;
        }
        Instantiate(giftPrefab, dropPoint.position, dropPoint.rotation);
    }

    // 플레이어 실제 이동 처리 
    private void FixedUpdate()
    {
        Vector3 moveDirection;

        switch (currentState)
        {
            case MoveState.MoveLeft:
                moveDirection = Vector3.left;
                break;

            case MoveState.MoveRight:
                moveDirection = Vector3.right;
                break;

            case MoveState.Idle:
            default:
                return;
        }

        Vector3 movement = moveDirection * lateralSpeed * Time.fixedDeltaTime;
        body.MovePosition(body.position + movement);
    }
}