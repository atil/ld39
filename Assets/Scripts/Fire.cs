using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public const float Cooldown = 1.4f;

    public LineRenderer Line;
    public Transform BarrelSlot;

    public AnimationCurve LineFade;
    public Color LineColor;
    
    private MinionSpawner _minionSpawner;
    private bool _isInCooldown;
    private int _fireLayerMask;

    void Start()
    {
        _minionSpawner = FindObjectOfType<MinionSpawner>();
        var playerLayer = LayerMask.NameToLayer("Player");
        var volumeLayer = LayerMask.NameToLayer("SpawnVolume");

        _fireLayerMask = ~((1 << playerLayer) | (1 << volumeLayer));
    }

	void Update ()
    {
	    if (Input.GetMouseButtonDown(0) && !_isInCooldown)
	    {
	        var midScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
	        var endPos = transform.forward * 1000f;

            var hits = Physics.SphereCastAll(Camera.main.ScreenPointToRay(midScreen), 0.2f, 1000f, _fireLayerMask);
	        foreach (var hit in hits)
	        {
	            if (hit.transform.GetComponent<Minion>() != null)
	            {
	                _minionSpawner.KillMinion(hit.transform.GetComponent<Minion>());
	            }
	            else
	            {
	                endPos = hit.point;
                }
            }


            Line.SetPositions(new[]
            {
                BarrelSlot.position, endPos
            });

	        StartCoroutine(AfterFire());
	    }
	}

    private IEnumerator AfterFire()
    {
        _isInCooldown = true;
        Line.startColor = LineColor;
        Line.endColor = LineColor;

        var timer = 0f;

        while (timer< Cooldown)
        {
            var c = LineColor;
            c.a = LineFade.Evaluate(timer / Cooldown);

            Line.startColor = c;
            Line.endColor = c;

            timer += Time.deltaTime;

            yield return null;
        }
        _isInCooldown = false;

    }
}
