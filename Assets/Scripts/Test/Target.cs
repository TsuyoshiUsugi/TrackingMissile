using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Vector3 _dir = Vector3.right;
    [SerializeField] Vector3 _returnPos = new Vector3(5, 0, 0);
    [SerializeField] float _speed = 5;
    bool _isRightMoving = true;
    // Update is called once per frame
    void Update()
    {
        transform.position += _dir * Time.deltaTime * _speed;
        if (_isRightMoving && transform.position.x > _returnPos.x)
        {
            ReverseDir();
            _isRightMoving = false;
        }

        if (!_isRightMoving && transform.position.x < _returnPos.x)
        {
            ReverseDir();
            _isRightMoving = true;
        }
    }

    private void ReverseDir()
    {
        _dir = -_dir;
        _returnPos = -_returnPos;
    }
}
