using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Collider> _detachObjects;
    [SerializeField] private GameObject _explosionPrefab;

    [Space(3)]
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionUpward = 3f;
    [SerializeField] private float _detachLifetime = 5f;

    [Space(3)]
    [SerializeField] private float _initImpulse = 5f;

    private Rigidbody _rb;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * _initImpulse, ForceMode.Impulse);
	}

	public void Expload()
    {
        Instantiate(_explosionPrefab, transform.position, transform.rotation);

        foreach (var detachObject in _detachObjects)
        {
			if (detachObject is null)
            {
                Destroy(gameObject);
				continue;
            }

			detachObject.transform.parent = null;
			detachObject.gameObject.SetActive(true);

			if (!detachObject.TryGetComponent(out Rigidbody rb))
                rb = detachObject.gameObject.AddComponent<Rigidbody>();

			rb.isKinematic = false;
			rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _explosionUpward);

			Destroy(detachObject.gameObject, _detachLifetime);
		}

        Destroy(gameObject);
    }

#if UNITY_EDITOR

    [ContextMenu("Find Detach Objects")]
    private void FindDetachObjects()
    {
		_detachObjects = new List<Collider>();
        FindInChild(transform);
	}

    private void FindInChild(Transform t)
    {
		if (t.TryGetComponent(out Collider c))
			_detachObjects.Add(c);

		foreach (Transform child in t)
		    FindInChild(child);
	}

#endif
}
