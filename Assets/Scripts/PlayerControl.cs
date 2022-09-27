using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public Vector3 velocity;

    private float _gravity = -9.8f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    private bool _isMobile;
    private bool _isGrounded;

    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private Joystick _cameraJoystick;
    [SerializeField] private Rigidbody _rb;

    private float _xRotation = 0f;
    private float _vertical;
    private float _horizontal;
    private float _mouseX;
    private float _mouseY;

    private float x;
    private float z;

    [SerializeField] private Button jump;

    public CharacterController Controller
    { get { return controller = controller ?? GetComponent<CharacterController>(); } }


    void Start()
    {
        _isMobile = Application.isMobilePlatform;
        _rb = GetComponent<Rigidbody>();

        if (!_isMobile)
        {
            _moveJoystick.gameObject.SetActive(false);
            _cameraJoystick.gameObject.SetActive(false);
            jump.gameObject.SetActive(false);
        }

        Debug.Log(_isMobile);
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (_isMobile)
        {
            x = _moveJoystick.Vertical;
            z = _moveJoystick.Horizontal;
            jump.onClick.AddListener(Jump);
        }

        if (!_isMobile)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += _gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        }
               

        _rb.velocity = new Vector3(_moveJoystick.Horizontal * speed, _rb.velocity.y, _moveJoystick.Vertical * speed);
        Vector3 direction = Vector3.forward * _moveJoystick.Vertical + Vector3.right * _moveJoystick.Horizontal;
        _rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            
    }

    void Jump()
    {
        if (_isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        }
    }
}
