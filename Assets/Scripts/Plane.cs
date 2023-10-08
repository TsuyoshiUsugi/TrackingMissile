using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] float _speed;

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
