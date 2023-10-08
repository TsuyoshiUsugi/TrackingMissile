using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGenerator : MonoBehaviour
{
    [SerializeField] TrackingLaser _laser;
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _originSpeed;
    [SerializeField] float _maxHitDur;
    [SerializeField] float _minHitDur;
    [SerializeField] float _maxHorizontalVeloDiff;
    [SerializeField] float _minHorizontalVeloDiff;
    [SerializeField] int _instatintiateNum = 1;
    [SerializeField] float _maxAccele = 100;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < _instatintiateNum; i++)
            {
                var laser = Instantiate(_laser, transform.position, Quaternion.identity);
                var horizontalVeloDiff = Random.Range(_minHorizontalVeloDiff, _maxHorizontalVeloDiff);
                var originSpeed = new Vector3(horizontalVeloDiff, _originSpeed.y, _originSpeed.z);
                var hitDur = Random.Range(_minHitDur, _maxHitDur);
                laser.GetComponent<TrackingLaser>().InitLaser(_target, transform.position, originSpeed, hitDur, _maxAccele);
            }
        }
    }
}
