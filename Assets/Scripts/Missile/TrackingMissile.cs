using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using static UnityEngine.ParticleSystem;

public class TrackingMissile : MonoBehaviour
{
    Transform _target;
    float _ratio = 1;  //ばね係数
    float _velo = 1;
    float _explodeDur = 1;
    Rigidbody _rigidbody;
    GameObject _explodeObj;
    float _explodeTime = 3;
    float _explodeDis = 1;
    float _smokeLifeTime = 5;
    bool _isFiring = false;
    [SerializeField] List<GameObject> _onHitDestroyObjs;
    [SerializeField] ParticleSystem _smoke;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void InitMissileInfo(Transform target, float velocity, float ratio, float explodeDur, GameObject explode, float explodeDis)
    {
        _target = target; 
        _velo = velocity;
        _ratio = ratio; 
        _explodeObj = explode;
        _explodeDur = explodeDur;
        _explodeDis = explodeDis;
        StartCoroutine(CountExplodeTime());
    }

    IEnumerator CountExplodeTime()
    {
        yield return new WaitForSeconds(_explodeDur);
        Explode();
    }

    private void Explode()
    {
        if (_isFiring) return; 
        _isFiring = true;
        if (_explodeObj != null)
        {
            var explode = Instantiate(_explodeObj, transform.position, Quaternion.identity);
            Destroy(explode, _explodeTime);
        }

        foreach (var obj in _onHitDestroyObjs)
        {
            obj.SetActive(false);
        }
        _velo = 0;
        GetComponent<Collider>().enabled = false;
        _smoke.enableEmission = false;
        Destroy(this.gameObject, _smokeLifeTime);
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _target.position) <= _explodeDis)
        {
            Explode();
        }

        _rigidbody.velocity = transform.forward * _velo;

        var diff = _target.position - transform.position;
        var targetRot = Quaternion.LookRotation(diff);
        var q = targetRot * Quaternion.Inverse(transform.rotation); //目標を向くローテーションから現在のローテーションの差分を取得
        var torque = new Vector3(q.x, q.y, q.z) * _ratio;
        _rigidbody.AddTorque(torque);
    }
}
