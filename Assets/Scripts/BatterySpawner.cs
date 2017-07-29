using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BatterySpawner : MonoBehaviour
{
    public GameObject BatteryPrefab;
    public BoxCollider[] SpawnVolumes;

    private float _nextSpawnTime;
    private float _spawnTimer;

    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer > _nextSpawnTime)
        {
            Instantiate(BatteryPrefab,
                RandomPointInVolume(SpawnVolumes[Random.Range(0, SpawnVolumes.Length)].bounds), Quaternion.identity);
            _spawnTimer = 0;
            _nextSpawnTime = Random.Range(1f, 1f);
        }
    }

    private Vector3 RandomPointInVolume(Bounds bnds)
    {
        var center = bnds.center;
        var x = Random.Range(-bnds.size.x, bnds.size.x);
        var y = Random.Range(-bnds.size.y, bnds.size.y);
        var z = Random.Range(-bnds.size.z, bnds.size.z);
        return center + new Vector3(x, y, z);
    }
}
