using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public Renderer Renderer;

	void Update ()
	{
	    var s = Mathf.Abs(Mathf.Sin(Time.time * 8f));
        var c = new Color(s,s,s);

	    Renderer.sharedMaterial.SetColor("_EmissionColor", c);

    }
}
