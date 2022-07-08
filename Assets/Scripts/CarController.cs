using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private bool isBrake;
    private float currentBrakeForce;
    private float currentTurningAngle;

    [SerializeField] private GameObject pressR;
    [SerializeField] private float maxTurningAngle;
    [SerializeField] private float motorTorqueForce;
    [SerializeField] private float brakeForce;
        
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;
    
    private void FixedUpdate()
    {
        GetUserInput();
        CarMovement();
        RotateTheCar();
        RotateTheWheels();
    }
    
    private void GetUserInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBrake = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyUp(KeyCode.R))
        {
            ResetCarRotation();
        }
    }

    private void CarMovement()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorTorqueForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorTorqueForce;
        currentBrakeForce = isBrake ? brakeForce : 0f;
        // Eğer kullanıcı fren yapma tuşuna basmışsa brakeForce değişkenini currentBrakeForce değişkenine atanır.
        if (isBrake)
        {
            frontLeftWheelCollider.brakeTorque = currentBrakeForce;
            frontRightWheelCollider.brakeTorque = currentBrakeForce;
            backLeftWheelCollider.brakeTorque = currentBrakeForce;
            backRightWheelCollider.brakeTorque = currentBrakeForce;
        }
        else
        {
            frontLeftWheelCollider.brakeTorque = 0f;
            frontRightWheelCollider.brakeTorque = 0f;
            backLeftWheelCollider.brakeTorque = 0f;
            backRightWheelCollider.brakeTorque = 0f;
        }
    }

    private void RotateTheCar()
    {
        currentTurningAngle = maxTurningAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentTurningAngle;
        frontRightWheelCollider.steerAngle = currentTurningAngle;
    }
    
    private void RotateTheWheels()
    {
        RotateTheWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        RotateTheWheel(frontRightWheelCollider, frontRightWheelTransform);
        RotateTheWheel(backLeftWheelCollider, backLeftWheelTransform);
        RotateTheWheel(backRightWheelCollider, backRightWheelTransform);
    }
    
    private void RotateTheWheel(WheelCollider WheelCollider, Transform WheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        WheelCollider.GetWorldPose(out position, out rotation);
        WheelTransform.position = position;
        WheelTransform.rotation = rotation;
    }

    private void ResetCarRotation()
    {
        Quaternion rotation = transform.rotation;
        rotation.z = 0f;
        rotation.x = 0f;
        transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("World"))
        {
            Time.timeScale = 0f;
            pressR.SetActive(true);
        }
    }
}
