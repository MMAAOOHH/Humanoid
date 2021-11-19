using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _target;
    
    [SerializeField] private Vector3 _offset;
    
    private float _smoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;
    
    private void FixedUpdate()
    {
        FollowTarget();
    }

    public void SetCameraTarget(GameObject target)
    {
        _target = target;
    }

    private void FollowTarget()
    {
            Vector3 targetPosition = _target.transform.position + _offset;
            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
            transform.position = smoothPosition;
    }
}
