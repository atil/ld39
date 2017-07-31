using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject FakeBattery;

    public AudioSource AudioSource;
    public AudioClip PickupClip;
    public AudioClip ThrowClip;

    private int _batteryLayer;
    private Transform _grabbedBattery;
    private FpsController _controller;

    void Start()
    {
        _batteryLayer = LayerMask.NameToLayer("Battery");
        _controller = FindObjectOfType<FpsController>();
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
                var midScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
                RaycastHit hit;
                var reachDist = Mathf.Max(_controller.Velocity.magnitude / 4f, 1f);
                if (Physics.SphereCast(Camera.main.ScreenPointToRay(midScreen), 0.3f, out hit, reachDist, 1 << _batteryLayer))
                {
                    _grabbedBattery = hit.transform;
                    _grabbedBattery.gameObject.SetActive(false);
                    FakeBattery.SetActive(true);

                    AudioSource.PlayOneShot(PickupClip);
                }
            }

        }
    }
}
