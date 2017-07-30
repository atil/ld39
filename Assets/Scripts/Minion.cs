using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public const float Speed = 5f;
    private Kamyon _kamyon;

    void Start()
    {
        _kamyon = FindObjectOfType<Kamyon>();
    }
	
	void Update ()
	{
	    var dir = (_kamyon.transform.position - transform.position).normalized.WithY(0f);
	    transform.position += dir * Speed * Time.deltaTime;

        transform.LookAt(_kamyon.transform.position.WithY(0f));
	}
}
