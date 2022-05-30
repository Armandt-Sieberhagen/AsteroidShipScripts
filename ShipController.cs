using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float speed;
    [SerializeField] float CameraTurnSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] public Camera cam;
    [SerializeField] LayerMask GroundMask;
    [SerializeField] GameObject debugOnj;
    [SerializeField] GameObject ShipVisual;
    Vector3 mousepositon;
    Vector3 movement;
    Vector3 target;
    Vector3 RigidbodyMovement;

  
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        movement.y = 0;
        mousepositon = getMousePosition();
    }

    private void velocityRotate()
    {
        target = Vector3.Lerp(target, rb.velocity+transform.forward, Time.deltaTime * 5f);
      
        target.y = transform.rotation.eulerAngles.y;
        target.z = target.x;
        target.x = transform.rotation.eulerAngles.x;
        transform.localEulerAngles = target;
    }

   

    private void FixedUpdate()
    {
        float GotoSpeed = 0;
        if (movement!=Vector3.zero)
        {
            GotoSpeed = MoveSpeed;
            RigidbodyMovement = movement.normalized;
        }
        else RigidbodyMovement = rb.velocity.normalized;
  
        speed = Mathf.Lerp(speed, GotoSpeed, Time.deltaTime * 2f);

        handlePosition();
        GenerateBodyMovements();
        GenerateShipVisuals();


    }

    void handlePosition()
    {
        rb.position = new Vector3(rb.position.x, 0, rb.position.z);
        rb.velocity = (RigidbodyMovement * speed);
        mousepositon.y = 0;
    }

    void GenerateBodyMovements()
    {
        Vector3 direction = mousepositon - rb.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 finalDir = new Vector3(transform.rotation.eulerAngles.x, angle, transform.rotation.eulerAngles.x);
        Quaternion rot = Quaternion.Euler(finalDir);
        transform.rotation = rot;
    }

    void GenerateShipVisuals()
    {
        Vector3 direction = mousepositon - rb.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 finalDir = new Vector3(0, 0, TurnShip(direction));
        Quaternion rot = Quaternion.Euler(finalDir);
        ShipVisual.transform.Rotate(finalDir);
    }

    float TurnShip(Vector3 direction)
    {
        float shipTurnRot = 0;
        if (Vector3.Angle(transform.forward, direction) > 0.5f )
        {
            float DotResult = Vector3.Dot(transform.right, direction);
            if (DotResult > 0) shipTurnRot = 1f;
            else shipTurnRot = -1f;
        }
        else shipTurnRot = 0f;
        return shipTurnRot;
    }

    

    Vector3 getMousePosition()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitinfo, Mathf.Infinity,GroundMask))
        {
            return hitinfo.point;
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }
}
