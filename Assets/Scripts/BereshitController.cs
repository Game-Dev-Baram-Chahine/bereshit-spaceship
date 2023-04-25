using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component simulates the controls of a spaceship that should land on the moon without exploding.
 */
[RequireComponent(typeof(Rigidbody2D))]
public class BereshitController : MonoBehaviour
{
    [SerializeField]
    float distanceToStartSlowdown = 0;

    float dragForceForSlowdown = 0;

    [SerializeField]
    float thrustForce = 10f;

    [SerializeField]
    float torqueForce = 10f;

    [SerializeField]
    InputAction noseForce = new InputAction(type: InputActionType.Button);

    [SerializeField]
    InputAction tailForce = new InputAction(type: InputActionType.Button);

    [SerializeField]
    InputAction rightAngleTorque = new InputAction(type: InputActionType.Button);

    [SerializeField]
    InputAction leftAngleTorque = new InputAction(type: InputActionType.Button);

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        noseForce.Enable();
        tailForce.Enable();
        rightAngleTorque.Enable();
        leftAngleTorque.Enable();
    }

    void OnDisable()
    {
        noseForce.Disable();
        tailForce.Disable();
        rightAngleTorque.Disable();
        leftAngleTorque.Disable();
    }

    /* To prevent explosion - send a ray to the moon and "drag" the spaceship when the moon is nearby: */
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            origin: transform.position,
            direction: Vector2.down,
            distance: Mathf.Infinity
        );
        float rightAngle = rightAngleTorque.ReadValue<float>();
        float leftAngle = leftAngleTorque.ReadValue<float>();
        if (rightAngle == 1 || leftAngle == 1)
        {
            var impulse =
                (leftAngle * torqueForce * Mathf.Deg2Rad - rightAngle * torqueForce * Mathf.Deg2Rad)
                * rb.inertia;
            rb.AddTorque(impulse, ForceMode2D.Impulse);
        }
        float nose = noseForce.ReadValue<float>();
        float tail = tailForce.ReadValue<float>();
        if (nose == 1)
        {
            rb.AddForce(transform.up * thrustForce, ForceMode2D.Force);
        }
        if (tail == 1)
        {
            rb.AddForce(Vector2.down * thrustForce, ForceMode2D.Force);
        }
        Debug.DrawRay(transform.position, Vector2.down * distanceToStartSlowdown, Color.red);
    }
}
