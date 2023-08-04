using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
	public WheelCollider leftWheelCollider;
	public WheelCollider rightWheelCollider;
	public GameObject leftWheelMesh;
	public GameObject rightWheelMesh;
	public bool motor;
	public bool steering;
}
     
public class CarControllerrSimple : MonoBehaviour 
{
	public List<AxleInfo> axleInfos;
	public float maxMotorTorque;
	public float maxSteeringAngle;
	public float brakeTorque;
	public float decelerationForce;
    public float divisionOfSteering = 2.0f;
    Rigidbody playerRb;

    float wheelRadius = 0.31f; // put here your wheel radius
	float wheelRpm = 0.0f; // put here you rpm
	float circumFerence; //here we will store the circumFerence
	float speedOnKmh; // here the speed in kilometers in hour

	[Header ("AudioSettings")]
	[SerializeField] AudioClip clipHandbreak;
	AudioSource audioScPlayer;

	

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        playerRb.centerOfMass = new Vector3(0.0f, -0.1f, 0.0f);
        circumFerence = 2.0f * 3.14f * wheelRadius; // Finding circumFerence 2 Pi R
		audioScPlayer = gameObject.GetComponent<AudioSource>();
    }

	public void ApplyLocalPositionToVisuals (AxleInfo axleInfo)
	{
		Vector3 posit;
		Quaternion rotat;
		axleInfo.leftWheelCollider.GetWorldPose (out posit, out rotat);
		axleInfo.leftWheelMesh.transform.position = posit;
		axleInfo.leftWheelMesh.transform.rotation = rotat;
		axleInfo.rightWheelCollider.GetWorldPose (out posit, out rotat);
		axleInfo.rightWheelMesh.transform.position = posit;
		axleInfo.rightWheelMesh.transform.rotation = rotat;
	}

	void Update()
	{
		audioScPlayer.pitch = 1 + (speedOnKmh / 15);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			audioScPlayer.pitch = 1;
			audioScPlayer.PlayOneShot(clipHandbreak);
		}
	}
	void FixedUpdate ()
	{
        maxSteeringAngle = 25 - (speedOnKmh / 4);
		float motor = maxMotorTorque * Input.GetAxis ("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis ("Horizontal");
        Debug.Log(speedOnKmh);
		for (int i = 0; i < axleInfos.Count; i++)
		{
			if (axleInfos[i].motor)
			{
				Acceleration (axleInfos[i], motor);
			}
		}
		if (Input.GetKey(KeyCode.Space))
		{
			Brake (axleInfos[1]); // handbrake
		} 
        if (axleInfos[0].steering)
		{
		    Steering (axleInfos[0], steering);
		}
		ApplyLocalPositionToVisuals (axleInfos[0]); //visual move of front wheels
		ApplyLocalPositionToVisuals (axleInfos[1]); //visual move of back wheels

        wheelRpm = axleInfos[0].leftWheelCollider.rpm;
        speedOnKmh = circumFerence * wheelRpm / 60 ; // finding kmh
	}

	private void Acceleration (AxleInfo axleInfo, float motor)
	{
		if (motor != 0f)
		{
			axleInfo.leftWheelCollider.brakeTorque = 0;
			axleInfo.rightWheelCollider.brakeTorque = 0;
			axleInfo.leftWheelCollider.motorTorque = motor;
			axleInfo.rightWheelCollider.motorTorque = motor;
		} else
		{
			Deceleration (axleInfo);
		}
	}

	private void Deceleration (AxleInfo axleInfo)
	{
		axleInfo.leftWheelCollider.brakeTorque = decelerationForce;
		axleInfo.rightWheelCollider.brakeTorque = decelerationForce;
	}

	private void Steering (AxleInfo axleInfo, float steering)
	{
        axleInfo.leftWheelCollider.steerAngle = steering;
        axleInfo.rightWheelCollider.steerAngle = steering;
	}

	private void Brake (AxleInfo axleInfo)
	{
		axleInfo.leftWheelCollider.brakeTorque = brakeTorque;
		axleInfo.rightWheelCollider.brakeTorque = brakeTorque;
	}
}