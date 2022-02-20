using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed = 20;

    [Tooltip("Чувствительность камеры")]
    public float lookSensitivity = 1f;

    [Tooltip("Скорость вращения камеры")]
    public float rotationSpeed = 200f;

    [Tooltip("Инвертировать Y")]
    public bool invertYAxis = false;
    [Tooltip("Инвертировать X")]
    public bool invertXAxis = false;

    [Tooltip("Начальная сила гравитации")]
    public float startingGravity = 0.3f;

    [Tooltip("Максимальная скорость падения")]
    public float maxDownVelosity = 20f;

    [Tooltip("Высота прыжка")]
    public float jumpForce = 10;

    [Tooltip("Ускорение при нажатом Shift")]
    public float sprintAcceleration = 1.5f;

    public UnityAction<float> GravityChanged;
    public UnityAction escButtonDown;



    private Camera _playerCamera;
    private Vector3 _movement;
    private float _CameraVerticalAngle;
    private CharacterController _characterController;
    private float _speedY = 0;
    private float _gravityCoeff = 3;
    private float _lastYpos = 0;

    void Awake()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _characterController = GetComponent<CharacterController>();
        _playerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) escButtonDown?.Invoke();

        if (CanProcessInput())
        {
            GravityChange();
            velosityYChange();
            CharacterMove();
            MouseMove();
        }
    }



    public bool CanProcessInput()
    {
        return Cursor.lockState == CursorLockMode.Locked;
    }

    private void CharacterMove()
    {
        _movement = (_characterController.transform.forward * Input.GetAxis("Vertical") + _characterController.transform.right * Input.GetAxis("Horizontal")) * Time.deltaTime * Speed;
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetAxis("Vertical")>0) _movement *= sprintAcceleration;

        //Вертикальная составляющая скорости
        _movement += _characterController.transform.up * _speedY * Time.deltaTime;
        _characterController.Move(_movement);
    }

    private void MouseMove()
    {
        int invertX=1, invertY=1;
        if (invertXAxis) invertX *= -1;
        if (invertYAxis) invertY *= -1;

        //transform.Rotate(new Vector3(0f, (Input.GetAxis("Mouse X") * rotationSpeed * lookSensitivity* invertX), 0f), Space.Self);
        _characterController.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed * lookSensitivity * invertX);

        _CameraVerticalAngle += Input.GetAxisRaw("Mouse Y") * rotationSpeed * lookSensitivity;
        // Ограничить углы обзора камеры
        _CameraVerticalAngle = Mathf.Clamp(_CameraVerticalAngle, -89f, 89f);
        _playerCamera.transform.localEulerAngles = new Vector3(_CameraVerticalAngle*invertY*-1, 0, 0);
    }

    private void velosityYChange()
    {

        if (_characterController.isGrounded && Input.GetButton("Jump"))
        {
            _speedY = jumpForce * 10;
            //Debug.Log("Прыжок");

        }

        if (!_characterController.isGrounded && _speedY>-maxDownVelosity*_gravityCoeff)
        {

            //Если кнопка прыжка нажата прыжок длится дольше
            if (Input.GetButton("Jump")) _speedY += jumpForce * 0.01f;

            //Проверка удара об потолок
            if (_lastYpos == _characterController.transform.position.y) _speedY = 0;

            _speedY -= startingGravity * _gravityCoeff;
        }
        _lastYpos = _characterController.transform.position.y;
    }

    private void GravityChange()
    {
        if (Input.GetButtonDown("GravityUP"))
        {
            if (_gravityCoeff < 9) _gravityCoeff *= 3;
            GravityChanged?.Invoke(_gravityCoeff);
        }
        else if (Input.GetButtonDown("GravityDOWN"))
        {
            if (_gravityCoeff > 1) _gravityCoeff /= 3;
            GravityChanged?.Invoke(_gravityCoeff);
        }
    }
    
    public float GetGravityCoeff()
    {
        return _gravityCoeff;
    }
}
