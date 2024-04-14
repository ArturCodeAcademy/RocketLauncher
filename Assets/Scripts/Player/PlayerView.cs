using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float _sensitivity = 10.0f;
    [SerializeField] private bool _invertX = false;
    [SerializeField] private bool _invertY = false;

    [Space(3)]
    [SerializeField] private float _minX = -90.0f;
    [SerializeField] private float _maxX = 20.0f;

	[Space(3)]
	[SerializeField] private Transform _muzzle;

    private float _rotationX = 0.0f;

	private void Update()
    {
		float mouseX = Input.GetAxis("Mouse X") * _sensitivity;
		float mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

		if (_invertX)
			mouseX *= -1;

		if (_invertY)
			mouseY *= -1;

		_rotationX -= mouseY;
		_rotationX = Mathf.Clamp(_rotationX, _minX, _maxX);

		_muzzle.localRotation = Quaternion.Euler(_rotationX, 0, 0);
		transform.Rotate(Vector3.up * mouseX);
	}

}
