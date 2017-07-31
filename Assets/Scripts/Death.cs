using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Transform MoveTarget;
    public Transform TextSlot;
    public Transform Text;

    public Transform Particles;
    public Transform ParticlesSlot;
    public Transform GroundParticles;
    public Transform GroundParticlesSlot;

    public Transform SfxSlot;

    private int _batteryLayer;
    private int _kamyonLayer;
    private int _playerLayer;

    private Transform _cameraTransform;

    void Start()
    {
        _batteryLayer = LayerMask.NameToLayer("Battery");
        _kamyonLayer = LayerMask.NameToLayer("Kamyon");
        _playerLayer = LayerMask.NameToLayer("Player");

        _cameraTransform = Camera.main.transform;
    }

    void Update ()
    {
        var moveDir = (MoveTarget.position - transform.position).normalized;
        transform.position += moveDir * Kamyon.MaxSpeed * Time.deltaTime;

        Text.position = TextSlot.position;
        Particles.position = ParticlesSlot.position;
        GroundParticles.position = GroundParticlesSlot.position;

        SfxSlot.position = SfxSlot.position.WithX(_cameraTransform.position.x);
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
            FindObjectOfType<GameManager>().EndGame(EndGameReason.KamyonWall);
        }

        if (layer == _playerLayer)
        {
            FindObjectOfType<GameManager>().EndGame(EndGameReason.PlayerWall);
        }
    }
}
