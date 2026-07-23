using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float ThrustStrength = 100f;

    [SerializeField] float RotationStrength = 100f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void FixedUpdate()
    {
        ProcessThrust();        

        ProcessRotation();

    }

//For the boosting of the rocket
    void ProcessThrust()
    {
        if (thrust.IsPressed()){
        rb.AddRelativeForce(ThrustStrength * Time.fixedDeltaTime * Vector3.up);
        }
    }
//For the Rotation of the rocket
    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            ApplyRotation(RotationStrength);
        }

        else if(rotationInput > 0)
        {
            ApplyRotation(-RotationStrength);
        }


    }

    private void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(rotationThisFrame * Time.fixedDeltaTime * Vector3.forward);
    }




}
