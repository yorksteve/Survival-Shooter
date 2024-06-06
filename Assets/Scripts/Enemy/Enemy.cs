using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 2.5f;

    private CharacterController _controller;
    private Transform _target;
    private Vector3 _velocity;
    private float _gravity = 20f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null) throw new ArgumentNullException(nameof(_controller));
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.isGrounded)
        {
            var direction = _target.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _speed;
        }

        _velocity.y -= _gravity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _target = null;
        }
    }
}
