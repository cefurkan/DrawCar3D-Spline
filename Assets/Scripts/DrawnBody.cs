using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawnBody : MonoBehaviour
{
    [SerializeField] private List<Transform> carParts;
    public List<Transform> CarParts { get { return carParts; } }

    Vector3 firstPart, lastPart;

    GameObject wheelPrefab;
    [SerializeField] float motorPower = 30000f;

    GameObject backRightWheel, backLeftWheel, frontRightWheel, frontLeftWheel;
    WheelCollider backRightWC, backLeftWC, frontRightWC, frontLeftWC;

    Rigidbody rigidBody;

    DrawLine drawLine;

    void Awake()
    {
        wheelPrefab = Resources.Load<GameObject>("Wheel");
        carParts = new List<Transform>();
        drawLine = FindObjectOfType<DrawLine>();
    }

    public void ActivateBodyParts()
    {
        rigidBody = GetComponent<Rigidbody>();

        firstPart = drawLine.firstCarMeshPartPos;
        lastPart = drawLine.lastCarMeshPartPos;


        if (firstPart != null)
        {
            // TODO: edit magic numbers
            Vector3 frontLeftWheelPosition = new Vector3(lastPart.x, lastPart.y, lastPart.z - wheelPrefab.transform.localScale.z * .5f);
            Vector3 frontRightWheelPosition = new Vector3(lastPart.x, lastPart.y, lastPart.z + wheelPrefab.transform.localScale.z * .5f);
            Vector3 backLeftWheelPosition = new Vector3(firstPart.x, firstPart.y, lastPart.z - wheelPrefab.transform.localScale.z * .5f);
            Vector3 backRightWheelPosition = new Vector3(firstPart.x, firstPart.y, lastPart.z + wheelPrefab.transform.localScale.z * .5f);

            frontLeftWheel = Instantiate(wheelPrefab, frontLeftWheelPosition, Quaternion.identity, transform);
            frontRightWheel = Instantiate(wheelPrefab, frontRightWheelPosition, Quaternion.identity, transform);
            backRightWheel = Instantiate(wheelPrefab, backRightWheelPosition, Quaternion.identity, transform);
            backLeftWheel = Instantiate(wheelPrefab, backLeftWheelPosition, Quaternion.identity, transform);

            frontLeftWC = frontLeftWheel.GetComponent<WheelCollider>();
            frontRightWC = frontRightWheel.GetComponent<WheelCollider>();
            backRightWC = backRightWheel.GetComponent<WheelCollider>();
            backLeftWC = backLeftWheel.GetComponent<WheelCollider>();

            backLeftWC.steerAngle = 90f;
            backRightWC.steerAngle = 90f;
            frontLeftWC.steerAngle = 90f;
            frontRightWC.steerAngle = 90f;
        }

    }

    private void FixedUpdate()
    {
        if (backRightWC)
        {
            MoveForward();
        }
    }

    private void MoveForward()
    {
        backRightWC.motorTorque = motorPower * Time.fixedDeltaTime;
        backLeftWC.motorTorque = motorPower * Time.fixedDeltaTime;
        frontRightWC.motorTorque = motorPower * Time.fixedDeltaTime;
        frontLeftWC.motorTorque = motorPower * Time.fixedDeltaTime;
    }

    public Vector3 GetDrawnBodyVelocity()
    {
        if (rigidBody != null)
        {
            return new Vector3(rigidBody.velocity.x, 0, 0);
        }
        return Vector3.zero;
    }

    public void ReturnCheckPoint(Transform checkPointPosition)
    {
        float offsetY = 2.5f;
        transform.position = new Vector3(checkPointPosition.position.x, checkPointPosition.position.y + offsetY, transform.position.z);
        rigidBody.velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
