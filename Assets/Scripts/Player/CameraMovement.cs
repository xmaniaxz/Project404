using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float Sensitivity = 5f;
    private float xAxis = 0f;
    private float yAxis = 0f;
    public static bool inFocus;
    public static bool inMenu = false;

    public void Awake()
    {
        //hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LateUpdate()
    {
        LookAround();
    }

    public void Update()
    {
        if (!inFocus)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                ToggleFocus();
                ToggleCursor();
            }
        }
        //Toggle lockstates of the mouse
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleFocus();
            ToggleCursor();
        }
    }

    /// <summary>
    /// Toggle for Infocus Bool
    /// </summary>
    public void ToggleFocus()
    {
        inFocus = !inFocus;
    }

    private void ToggleCursor()
    {
        if (inFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!inFocus)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }    
    /// <summary>
    /// Allows the player to look around
    /// </summary>
    public void LookAround()
    {
        //Locks the movement only when the application is in focus
        if (!inFocus) return;
        if (inMenu) return;

            xAxis -= Input.GetAxisRaw("Mouse Y") * Sensitivity;
            yAxis = Input.GetAxisRaw("Mouse X") * Sensitivity;
            xAxis = Mathf.Clamp(xAxis, -70f, 56f);
            transform.eulerAngles = new Vector3(xAxis, transform.eulerAngles.y, transform.eulerAngles.z);
            transform.parent.eulerAngles = new Vector3(0, transform.parent.eulerAngles.y + yAxis, 0f);

    }
}