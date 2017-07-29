using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject FakeBattery;

    private int _batteryLayer;
    private Transform _grabbedBattery;

    void Start()
    {
        _batteryLayer = LayerMask.NameToLayer("Battery");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (FakeBattery.activeInHierarchy)
            {
                _grabbedBattery.transform.position = transform.position + transform.forward.WithY(0);
                _grabbedBattery.gameObject.SetActive(true);
                FakeBattery.SetActive(false);
            }
            else
            {
                var midScreen = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
                RaycastHit hit;
                if (Physics.SphereCast(Camera.main.ScreenPointToRay(midScreen), 0.3f, out hit, 1f, 1 << _batteryLayer))
                {
                    _grabbedBattery = hit.transform;
                    _grabbedBattery.gameObject.SetActive(false);
                    FakeBattery.SetActive(true);
                }
            }

        }


    }
}
