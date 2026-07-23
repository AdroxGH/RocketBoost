using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float ThrustStrength = 100f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        thrust.Enable();
    }

    void FixedUpdate()
    {
        if (thrust.IsPressed()){
        rb.AddRelativeForce(Vector3.up* ThrustStrength* Time.fixedDeltaTime);
        }
    }



}
