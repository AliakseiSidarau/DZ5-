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

    private float _vertical;
    private float _horizontal;
    private float _mouseX;
    private float _mouseY;

    private float _x;
    private float _z;

    [SerializeField] private Button _jump;

    public CharacterController Controller
    { get { return controller = controller ?? GetComponent<CharacterController>(); } }


    void Start()
    {
        _isMobile = Application.isMobilePlatform;
        _jump.onClick.AddListener(Jump);

        if (!_isMobile)
        {
            _moveJoystick.gameObject.SetActive(false);
            _cameraJoystick.gameObject.SetActive(false);
            _jump.gameObject.SetActive(false);
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
            _x = _moveJoystick.Vertical;
            _z = _moveJoystick.Horizontal;
          
        }

        else
        {
            _x = Input.GetAxis("Horizontal");
            _z = Input.GetAxis("Vertical");
        }

        Vector3 move = transform.right * _x + transform.forward * _z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += _gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        }
               
    }

    void Jump()
    {
        if (_isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * _gravity);
        }
    }
}
