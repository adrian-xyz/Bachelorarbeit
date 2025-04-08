using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 300f;
    public float distance = 3.5f;         // Kamera nicht zu weit weg
    public float minY = -10f;
    public float maxY = 45f;

    float rotX = 15f;
    float rotY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player == null) return;

        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotX -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, minY, maxY);

        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0);
        Vector3 offset = rotation * new Vector3(0, 1.7f, -distance);  // leicht über Spieler
        transform.position = player.position + offset;

        transform.LookAt(player.position + Vector3.up * 1.5f);        // Blick etwas über Kopf
    }
}
