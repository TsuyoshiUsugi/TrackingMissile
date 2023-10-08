using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauchPad : MonoBehaviour
{
    [SerializeField] TrackingMissile _missile;
    [SerializeField] List<Transform> _launches; //発射地点のオブジェクトのTransform
    [SerializeField] List<Transform> _targets;
    [SerializeField] float _missileVelocity;
    [SerializeField] float _ratio;
    [SerializeField] GameObject _explodeObj;
    [SerializeField] float _explodeDur = 5;
    [SerializeField] float _explodeDis = 1;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var launchPos = Random.Range(0, _launches.Count);
            var forword = _launches[launchPos].transform.forward;
            var missile = Instantiate(_missile, _launches[launchPos].position, _launches[launchPos].rotation);
            missile.InitMissileInfo(_targets[0], _missileVelocity, _ratio, _explodeDur, _explodeObj, _explodeDis);
        }
    }
}
