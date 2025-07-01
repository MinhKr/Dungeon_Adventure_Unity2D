using UnityEngine;

public class RandomRotater : MonoBehaviour
{
    private HingeJoint2D hinge;

    [SerializeField] private float minSpeed = 100f;
    [SerializeField] private float maxSpeed = 300f;

    [SerializeField] private float minForce = 15000f;
    [SerializeField] private float maxForce = 25000f;

    private void Start()
    {
        hinge = GetComponent<HingeJoint2D>();

        if (hinge != null)
        {
            JointMotor2D motor = hinge.motor;
            motor.motorSpeed = Random.Range(minSpeed, maxSpeed) * (Random.value > 0.5f ? 1 : -1);
            motor.maxMotorTorque = Random.Range(minForce, maxForce);
            hinge.motor = motor;
        }
    }
}
