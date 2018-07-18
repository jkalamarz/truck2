using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public float maxMotorTorque;
    public float maxSteeringAngle;
}
     
public class SimpleCarController : MonoBehaviour {
    public List<AxleInfo> axleInfos; 
     
    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider, float steering)
    {
        if (collider.transform.childCount == 0) {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion r;
        collider.GetWorldPose(out position, out r);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = r;
            //Quaternion.AngleAxis(steering * Input.GetAxis("Horizontal"), Vector3.forward);
    }
     
    public void FixedUpdate()
    {
        foreach (AxleInfo axleInfo in axleInfos) {
            float motor = axleInfo.maxMotorTorque * Input.GetAxis("Vertical");
            float steering = axleInfo.maxSteeringAngle * Input.GetAxis("Horizontal");
            
            axleInfo.leftWheel.steerAngle = steering;
            axleInfo.rightWheel.steerAngle = steering;
            
            axleInfo.leftWheel.motorTorque = motor;
            axleInfo.rightWheel.motorTorque = motor;

            ApplyLocalPositionToVisuals(axleInfo.leftWheel, steering);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, steering);
        }
    }
}