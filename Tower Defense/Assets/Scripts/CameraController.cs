using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public float panSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameIsOver)
        {
            this.enabled = false;
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
