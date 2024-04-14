using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Rigidbody _rocketPrefab;

    [Space(3)]
    [SerializeField, Min(0.1f)] private float _fireRate = 0.5f;
    [SerializeField, Min(0.1f)] private float _rocketImpulse = 10.0f;
    [SerializeField, Min(1f)] private float _rocketLifeTime = 5.0f;

    private bool _canShoot = true;

    private void Update()
    {
        if (Input.GetMouseButton(0) && _canShoot)
        {
			var rocket = Instantiate(_rocketPrefab, _muzzle.position, _muzzle.rotation);
			rocket.AddForce(_muzzle.forward * _rocketImpulse, ForceMode.Impulse);
            Destroy(rocket.gameObject, _rocketLifeTime);

			_canShoot = false;
			Invoke(nameof(Cooldown), _fireRate);
		}
    }

    private void Cooldown()
    {
        _canShoot = true;
    }
}
