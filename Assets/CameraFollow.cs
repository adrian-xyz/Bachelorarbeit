using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referenz zum Spieler
    public Vector3 offset = new Vector3(0, 3, -5);  // Position der Kamera hinter dem Spieler
    public float smoothSpeed = 5f;  // Geschwindigkeit der Kamera-Bewegung

void LateUpdate()
{
    if (player == null) return;

    // Kamera relativ zur Blickrichtung des Spielers setzen
    Vector3 desiredPosition = player.position + player.transform.forward * -5 + Vector3.up * 3;
    transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

    // Kamera schaut auf den Spieler
    transform.LookAt(player);
}
}
