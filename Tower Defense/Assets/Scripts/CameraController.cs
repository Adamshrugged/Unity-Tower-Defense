using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public float panSpeed = 1f;
    private bool allowMovement = true;

    // Update is called once per frame
    void Update()
    {
        // Allow toggling of movement
        if (Input.GetKey(KeyCode.Escape))
        {
            allowMovement = !allowMovement;
        }

        // check if movement is allowed
        if (!allowMovement)
        {
            return;
        }

        // Check for input from keys
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

    }
}
