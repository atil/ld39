using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public const float BaseSpeed = 5f;
    public AnimationCurve SpeedProgressRelation;

    public AudioClip[] DeathClips;

    private Kamyon _kamyon;
    private float _difficulty = 1f;

    void Start()
    {
        _kamyon = FindObjectOfType<Kamyon>();
    }
	
	void Update ()
	{
	    var dir = (_kamyon.transform.position - transform.position).normalized.WithY(0f);
	    transform.position += dir * BaseSpeed * _difficulty * Time.deltaTime;

        transform.LookAt(_kamyon.transform.position.WithY(0f));
	}

    public void SetSpeed(float normalizedKamyonProgress)
    {
        _difficulty = 1 + SpeedProgressRelation.Evaluate(normalizedKamyonProgress) * 0.7f;
    }

    public void PlayDeathClip()
    {
        AudioSource.PlayClipAtPoint(DeathClips[Random.Range(0, DeathClips.Length)], transform.position);
    }
}
