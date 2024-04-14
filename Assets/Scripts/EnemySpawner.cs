using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private float _spawnRadius = 50f;

    private IEnumerator Start()
    {
		WaitForSeconds wait = new WaitForSeconds(_spawnInterval);

		while (true)
        {
			Vector3 spawnPosition = Random.insideUnitSphere * _spawnRadius;
			spawnPosition.y = Mathf.Abs(spawnPosition.y);
			Vector3 direction = -spawnPosition.normalized;
			
			Instantiate(_enemyPrefab, spawnPosition, Quaternion.LookRotation(direction));
			yield return wait;
		}
	}
}
