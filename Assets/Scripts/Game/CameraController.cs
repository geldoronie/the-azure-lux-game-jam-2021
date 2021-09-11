using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 mapSize = new Vector2(20, 20);
    [SerializeField] private float _mouseSensitivity = 20f;
    [SerializeField] private float _movementSpeed = 10f;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _yMinLimit = 0f;
    [SerializeField] private float _yMaxLimit = 80f;
    [SerializeField] private float _distanceMin = 10f;
    [SerializeField] private float _distanceMax = 20f;

    private float _currentDistance = 15f;
    private Vector2 _currentRotation;
    private Vector3 _targetPosition;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        _currentRotation = new Vector2(angles.y, angles.x);
    }

    private void Update()
    {
        Vector3 position = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        position = transform.right * position.x + transform.forward * position.z;
        position.y = 0;
        position.Normalize();

        _targetPosition = Vector3.MoveTowards(_targetPosition, _targetPosition + position, _movementSpeed * Time.deltaTime);
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, -mapSize.x / 2, mapSize.x / 2);
        _targetPosition.z = Mathf.Clamp(_targetPosition.z, -mapSize.y / 2, mapSize.y / 2);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(2))
        {
            _currentRotation.x += Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            _currentRotation.y -= Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime * (Screen.width / Screen.height);
        }

        Quaternion rotation = Quaternion.Euler(ClampAngle(_currentRotation.y, _yMinLimit, _yMaxLimit), _currentRotation.x, 0);

        _currentDistance = Mathf.Clamp(_currentDistance - Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed, _distanceMin, _distanceMax);

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -_currentDistance);
        Vector3 position = rotation * negDistance + _targetPosition;

        transform.rotation = rotation;
        transform.position = position;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
