using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingLaser : MonoBehaviour
{
    Vector3 _velo;     //���x�A�n�߂���l������ƂԂ��^���邱�Ƃ��o����
    Vector3 _pos;
    Transform _target;
    float _period;  //���e�܂ł̎c�莞��
    float _acceleLimit = 0;
    Vector3 _dir;
    Vector3 _prePos;

    /// <summary>
    /// Laser�̏��������s��
    /// </summary>
    /// <param name="target">�ڕW��Transform</param>
    /// <param name="startPos">�ړ��J�n�n�_</param>
    /// <param name="hitDuration">���[�U�[��������܂ł̎���</param>
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
        acceleration += (diff - _velo * _period) * 2f / (_period * _period);    //�^����������ό`���ĉ����x�����߂�
        _period -= Time.deltaTime;
        if (_period < 0)
        {
            Destroy(this.gameObject, 1);
        }
        else
        {
            acceleration = TrunctAccele(acceleration);
            _velo += acceleration * Time.deltaTime; //���x = �����x�~����
        }
        _pos += _velo * Time.deltaTime;         //�ʒu = ���x�~����
        transform.position = _pos;
        _prePos = _target.position;
    }

    /// <summary>
    /// �����x�����߂�ꂽ����Ŋۂ߂�
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
