using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    private float currentSpeed;

    [Header("Normal Speed")]
    public float normalSpeed = 4f;
    private float normalSpeedFOV;

    [Header("Sprint Speed")]
    public float sprintSpeed = 6f;
    public float sprintSpeedFOV = 75f;


    void Awake()
    {
        currentSpeed = normalSpeed;
        normalSpeedFOV = Camera.main.fieldOfView;
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked) 
            return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;

        characterController.Move(move * currentSpeed * Time.deltaTime);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
        }
        else
        {
            Walk();
        }
    }

    void Sprint()
    {
        currentSpeed = sprintSpeed;

        if (Camera.main.fieldOfView < sprintSpeedFOV)
        {
            Camera.main.fieldOfView += 100f * Time.deltaTime;
        }
        else if (Camera.main.fieldOfView > sprintSpeedFOV)
        {
            Camera.main.fieldOfView = sprintSpeedFOV;
        }
    }

    void Walk()
    {
        currentSpeed = normalSpeed;

        if (Camera.main.fieldOfView > normalSpeedFOV)
        {
            Camera.main.fieldOfView -= 100f * Time.deltaTime;
        }
        else if (Camera.main.fieldOfView < normalSpeedFOV)
        {
            Camera.main.fieldOfView = normalSpeedFOV;
        }
    }
}
