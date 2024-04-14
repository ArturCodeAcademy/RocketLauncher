using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;

	private void OnTriggerEnter(Collider other)
	{
		Transform parent = other.transform;
		while (parent != null)
		{
			if (parent.TryGetComponent(out Enemy enemy))
			{
				enemy.Expload();
				break;
			}

			parent = parent.parent;
		}

		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		Instantiate(_explosionPrefab, transform.position, transform.rotation);
	}
}
