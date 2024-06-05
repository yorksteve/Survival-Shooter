using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _jumpHeight = 8f;
    [SerializeField] private float _speed = 6f;
    private Vector3 _velocity;
    private const float _gravity = 20f;
    
    void Start()
    {
        if (_characterController == null) _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_characterController.isGrounded)
        {
            _velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }
        }

        _velocity.y -= _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
}
