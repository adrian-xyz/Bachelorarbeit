using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public float fallThreshold = -5f;
    private LevelManager levelManager;
    private bool isFalling = false;

    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    void Update()
    {
        if (!isFalling && transform.position.y < fallThreshold)
        {
            isFalling = true;
            StartCoroutine(RestartLevelAfterFall());
        }
    }

    private System.Collections.IEnumerator RestartLevelAfterFall()
    {
        Debug.Log("💀 Spieler gefallen!");

        // Bewegung stoppen
        var move = GetComponent<PlayerMovement>();
        if (move != null)
            move.canMove = false;

        // Nachricht anzeigen
        if (levelManager.levelUI != null)
            levelManager.levelUI.ShowMessage("Du bist gefallen!");

        yield return new WaitForSeconds(2f);

        levelManager.SetupLevel();

        // Wieder bereit fürs nächste Runterfallen:
        isFalling = false;
    }
}
