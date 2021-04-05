using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;

    public UpdatedMovement movementScript;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, movementScript.wallRunCameraTilt);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Math.Abs(movementScript.wallRunCameraTilt) < movementScript.maxWallRunCameraTilt && movementScript.isWallRunning && movementScript.isWallRight)
            movementScript.wallRunCameraTilt += Time.deltaTime * movementScript.maxWallRunCameraTilt * 2;
        if (Math.Abs(movementScript.wallRunCameraTilt) < movementScript.maxWallRunCameraTilt && movementScript.isWallRunning && movementScript.isWallLeft)
            movementScript.wallRunCameraTilt -= Time.deltaTime * movementScript.maxWallRunCameraTilt * 2;

        //Tilts camera back again
        if (movementScript.wallRunCameraTilt > 0 && !movementScript.isWallRight && !movementScript.isWallLeft)
            movementScript.wallRunCameraTilt -= Time.deltaTime * movementScript.maxWallRunCameraTilt * 2;
        if (movementScript.wallRunCameraTilt < 0 && !movementScript.isWallRight && !movementScript.isWallLeft)
            movementScript.wallRunCameraTilt += Time.deltaTime * movementScript.maxWallRunCameraTilt * 2;

    }
}
