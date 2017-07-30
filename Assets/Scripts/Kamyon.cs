using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Kamyon : MonoBehaviour
{
    public const float HpDegenRate = 2f;
    public const float MaxSpeed = 0.5f;
    public const float MaxHp = 100f;
    public const float MaxDeathBonusDistance = 20f;

    public AnimationCurve HpSpeedRelation;
    public AnimationCurve DeathDistanceBonus;
    public AnimationCurve EmptyTankBonus;
    public Transform SliderSlot;
    public Transform MoveTarget;

    private int _batteryLayer;
    private int _minionLayer;
    private int _goalLayer;
    private float _hp;
    private Slider _hpSlider;
    private Death _death;

    void Start()
    {
        _hp = MaxHp;
        _hpSlider = FindObjectOfType<Ui>().KamyonHpSlider;
        _batteryLayer = LayerMask.NameToLayer("Battery");
        _minionLayer = LayerMask.NameToLayer("Minion");
        _goalLayer = LayerMask.NameToLayer("Goal");
        _death = FindObjectOfType<Death>();
    }

    void Update()
    {
        _hp -= Time.deltaTime * HpDegenRate;

        _hp = Mathf.Clamp(_hp, 0f, MaxHp);

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

        _hpSlider.normalizedValue = _hp / MaxHp;

        if (_hp > 0)
        {
            var moveDir = (MoveTarget.position - transform.position).normalized;
            transform.position += moveDir * HpSpeedRelation.Evaluate(_hp / MaxHp) * MaxSpeed * Time.deltaTime;
        }

    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == _batteryLayer)
        {
            var deathDist = Vector3.Distance(transform.position.WithY(0), _death.transform.position.WithY(0));
            var distanceCoeff = DeathDistanceBonus.Evaluate(Mathf.Clamp01(deathDist / MaxDeathBonusDistance));
            var emptyTankCoeff = EmptyTankBonus.Evaluate(_hp / MaxHp);
            _hp += MaxHp * 0.2f // Base
                   + distanceCoeff * 0.1f // The closer the death is the more this fills up the tank
                   + emptyTankCoeff * 0.15f;

            Destroy(coll.gameObject);
        }

        if (coll.gameObject.layer == _minionLayer)
        {
            Destroy(coll.gameObject);
            FindObjectOfType<GameManager>().EndGame(false);
        }

        if (coll.gameObject.layer == _goalLayer)
        {
            FindObjectOfType<GameManager>().EndGame(true);
        }
    }
}
