using System.Collections;
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
            var go = Instantiate(BatteryPrefab,
                RandomPointInVolume(SpawnVolumes[Random.Range(0, SpawnVolumes.Length)].bounds), Quaternion.identity);
            _spawnTimer = 0;
            _nextSpawnTime = Random.Range(5f, 7.5f);

            StartCoroutine(BatteryLifeTime(go));
        }
    }

    private IEnumerator BatteryLifeTime(GameObject go)
    {
        var lifeTimer = 25f;
        var timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;

            if (go == null) // Kamyona koduk
            {
                break;
            }

            if (timer > lifeTimer)
            {
                if (go.activeInHierarchy)
                {
                    Destroy(go);
                    break;
                }

                lifeTimer *= 2f;
            }

            yield return null;
        }
    }

    public static Vector3 RandomPointInVolume(Bounds bnds)
    {
        var center = bnds.center;
        var x = Random.Range(-bnds.size.x, bnds.size.x);
        var y = Random.Range(-bnds.size.y, bnds.size.y);
        var z = Random.Range(-bnds.size.z, bnds.size.z);
        return center + new Vector3(x, y, z);
    }
}
