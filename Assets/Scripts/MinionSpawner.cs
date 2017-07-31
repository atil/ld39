using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    public const float MaxSpawnDuration = 5f;
    public const float MinSpawnDuration = 2.5f;

    public GameObject MinionPrefab;
    public BoxCollider[] SpawnVolumes;
    public Kamyon Kamyon;
    public Goal Goal;
    public AnimationCurve ProgressSpawnTimeRelation;

    private float _spawnTimer;
    private float _nextSpawnTime = 3f;
    private readonly List<Minion> _minions = new List<Minion>();
    private float _initialDistance;

    void Start()
    {
        _initialDistance = GetRemainingDistance();
    }

	void Update ()
	{
	    _spawnTimer += Time.deltaTime;

	    if (_spawnTimer > _nextSpawnTime)
	    {
            var pos = BatterySpawner.RandomPointInVolume(SpawnVolumes[Random.Range(0, SpawnVolumes.Length)].bounds).WithY(0f);
            var go = Instantiate(MinionPrefab, pos, Quaternion.identity);
            _minions.Add(go.GetComponent<Minion>());

	        var t = ProgressSpawnTimeRelation.Evaluate(GetRemainingDistance() / _initialDistance);
	        var t1 = Mathf.Lerp(MaxSpawnDuration, MinSpawnDuration, t);
            _nextSpawnTime = Random.Range(t1, t1 * 1.5f);
	        _spawnTimer = 0;
	    }

	    foreach (var minion in _minions)
	    {
	        minion.SetSpeed(GetRemainingDistance() / _initialDistance);
	    }
	}

    public void DeactivateMinions()
    {
        enabled = false;
        foreach (var minion in _minions)
        {
            minion.enabled = false;
        }
    }

    public void KillMinion(Minion minion)
    {
        minion.PlayDeathClip();
        _minions.Remove(minion);
        Destroy(minion.gameObject);
    }

    private float GetRemainingDistance()
    {
        return Vector3.Distance(Kamyon.transform.position, Goal.transform.position);
    }

    
}
