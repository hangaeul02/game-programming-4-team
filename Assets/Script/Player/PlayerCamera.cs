using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 3f, -6f);
    [SerializeField] private Vector3 lookOffset = new Vector3(0f, 0.8f, 2f);
    [SerializeField, Min(0.01f)] private float followTime = 0.15f;

    private Vector3 followVelocity;

    private void Start()
    {
        if (target != null)
        {
            transform.position = target.position + target.rotation * cameraOffset;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // 썰매 뒤쪽으로 부드럽게 따라가는 코드
        Vector3 cameraPosition = target.position + target.rotation * cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref followVelocity, followTime);

        // 썰매를 중앙에 고정시켜서 보게 하는 코드
        Vector3 lookPosition = target.position + target.rotation * lookOffset;
        transform.LookAt(lookPosition, Vector3.up);
    }
}
