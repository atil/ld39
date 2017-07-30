using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Transform MoveTarget;
    public Transform TextSlot;
    public Transform Text;

    private int _batteryLayer;
    private int _kamyonLayer;
    private int _playerLayer;

    void Start()
    {
        _batteryLayer = LayerMask.NameToLayer("Battery");
        _kamyonLayer = LayerMask.NameToLayer("Kamyon");
        _playerLayer = LayerMask.NameToLayer("Player");
    }

    void Update ()
    {
        var moveDir = (MoveTarget.position - transform.position).normalized;
        transform.position += moveDir * Kamyon.MaxSpeed * Time.deltaTime;

        Text.position = TextSlot.position;
    }

    void OnTriggerEnter(Collider coll)
    {
        var layer = coll.gameObject.layer;
        if (layer == _batteryLayer)
        {
            Destroy(coll.gameObject);
        }

        if (layer == _kamyonLayer)
        {
            FindObjectOfType<GameManager>().EndGame(false);
        }

        if (layer == _playerLayer)
        {
            FindObjectOfType<GameManager>().EndGame(false);
        }
    }
}
