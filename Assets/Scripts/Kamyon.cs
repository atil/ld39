using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Kamyon : MonoBehaviour
{
    public const float HpDegenRate = 2f;
    public Transform SliderSlot;

    private float _hp;
    private Slider _hpSlider;

    void Start()
    {
        _hp = 100;
        _hpSlider = FindObjectOfType<Ui>().KamyonHpSlider;
    }

    void Update()
    {
        _hp -= Time.deltaTime * HpDegenRate;

        var camToKamyon = transform.position - Camera.main.transform.position;
        var camForward = Camera.main.transform.forward;
        if (Vector3.Dot(camToKamyon, camForward) > 0)
        {
            _hpSlider.gameObject.SetActive(true);
            _hpSlider.transform.position =
                Camera.main.WorldToScreenPoint(SliderSlot.position);
        }
        else
        {
            _hpSlider.gameObject.SetActive(false);
        }

        _hpSlider.normalizedValue = _hp / 100f;
    }

}
