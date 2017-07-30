using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public GameObject MinionPrefab;
    public BoxCollider[] SpawnVolumes;

    private float _spawnTimer;
    private float _nextSpawnTime;
    private readonly List<Minion> _minions = new List<Minion>();

	void Update ()
	{
	    _spawnTimer += Time.deltaTime;

	    if (_spawnTimer > _nextSpawnTime)
	    {
	        var pos = BatterySpawner.RandomPointInVolume(SpawnVolumes[Random.Range(0, SpawnVolumes.Length)].bounds).WithY(0f);
            var go = Instantiate(MinionPrefab, pos, Quaternion.identity);
            _minions.Add(go.GetComponent<Minion>());

	        _spawnTimer = 0;
	        _nextSpawnTime = Random.Range(5f, 7.5f);
	    }
	}

    public void DeactivateMinions()
    {
        foreach (var minion in _minions)
        {
            minion.enabled = false;
        }
    }

    public void KillMinion(Minion minion)
    {
        _minions.Remove(minion);
        Destroy(minion.gameObject);
    }
}
