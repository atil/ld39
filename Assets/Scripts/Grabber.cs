using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject FakeBattery;
    public Collider GrabVolume;

    public AudioSource AudioSource;
    public AudioClip PickupClip;
    public AudioClip ThrowClip;

    private int _batteryLayer;
    private Transform _grabbedBattery;
    private FpsController _controller;
    private BatterySpawner _batterySpawner;

    void Start()
    {
        _batteryLayer = LayerMask.NameToLayer("Battery");
        _controller = FindObjectOfType<FpsController>();
        _batterySpawner = FindObjectOfType<BatterySpawner>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (FakeBattery.activeInHierarchy)
            {
                _grabbedBattery.transform.position = transform.position + transform.forward.WithY(0);
                _grabbedBattery.gameObject.SetActive(true);
                _grabbedBattery.GetComponent<Rigidbody>().velocity = _controller.Velocity.magnitude * Camera.main.transform.forward;
                FakeBattery.SetActive(false);

                AudioSource.PlayOneShot(ThrowClip);
            }
            else
            {
                //var midScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
                //RaycastHit hit;
                //var reachDist = Mathf.Max(_controller.Velocity.magnitude / 4f, 1f);
                //if (Physics.SphereCast(Camera.main.ScreenPointToRay(midScreen), 0.3f, out hit, reachDist, 1 << _batteryLayer))
                //{
                //    _grabbedBattery = hit.transform;
                //    _grabbedBattery.gameObject.SetActive(false);
                //    FakeBattery.SetActive(true);

                //    AudioSource.PlayOneShot(PickupClip);
                //}
            }

        }

        foreach (var battery in _batterySpawner.Batteries)
        {
            if (Vector3.Distance(battery.transform.position, GrabVolume.transform.position) < 5
                && battery.transform.position.y < 1)
            {
                var coll = battery.GetComponent<Collider>();
                Vector3 normal;
                float depth;
                if (Physics.ComputePenetration(GrabVolume, GrabVolume.transform.position, GrabVolume.transform.rotation,
                    coll, coll.transform.position, coll.transform.rotation,
                    out normal, out depth))
                {
                    _grabbedBattery = battery.transform;
                    _grabbedBattery.gameObject.SetActive(false);
                    FakeBattery.SetActive(true);

                    AudioSource.PlayOneShot(PickupClip);
                }
            }
        }



    }
}
