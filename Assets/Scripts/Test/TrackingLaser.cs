using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingLaser : MonoBehaviour
{
    Vector3 _velo;     //速度、始めから値を入れるとぶれを与えることが出来る
    Vector3 _pos;
    Transform _target;
    float _period;  //着弾までの残り時間
    float _acceleLimit = 0;
    Vector3 _dir;
    Vector3 _prePos;

    /// <summary>
    /// Laserの初期化を行う
    /// </summary>
    /// <param name="target">目標のTransform</param>
    /// <param name="startPos">移動開始地点</param>
    /// <param name="hitDuration">レーザーが当たるまでの時間</param>
    public void InitLaser(Transform target, Vector3 startPos, Vector3 originSpeed, float hitDuration, float acceleLimit)
    {
        _target = target;
        _pos = startPos;
        _velo = originSpeed;
        _period = hitDuration;
        _acceleLimit = acceleLimit;
    }

    void Update()
    {
        CalcuPos();
    }

    private void CalcuPos()
    {
        var acceleration = Vector3.zero;
        var diff = _target.position - _pos;
        acceleration += (diff - _velo * _period) * 2f / (_period * _period);    //運動方程式を変形して加速度を求める
        _period -= Time.deltaTime;
        if (_period < 0)
        {
            Destroy(this.gameObject, 1);
        }
        else
        {
            acceleration = TrunctAccele(acceleration);
            _velo += acceleration * Time.deltaTime; //速度 = 加速度×時間
        }
        _pos += _velo * Time.deltaTime;         //位置 = 速度×時間
        transform.position = _pos;
        _prePos = _target.position;
    }

    /// <summary>
    /// 加速度を決められた上限で丸める
    /// </summary>
    /// <param name="acceleration"></param>
    /// <returns></returns>
    private Vector3 TrunctAccele(Vector3 acceleration)
    {
        if (acceleration.magnitude > _acceleLimit)
        {
            acceleration = acceleration.normalized * _acceleLimit;
        }

        return acceleration;
    }
}
