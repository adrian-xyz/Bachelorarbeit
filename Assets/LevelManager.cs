using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelGenerator generator;
    public GameObject player;
    public Transform startPoint;
    public LevelUI levelUI;

    private int currentLevel = 1;

    void Start()
    {
        // Zeige erstes Level korrekt
        levelUI.ShowLevel(currentLevel);
        SetupLevel(); // keine Erhöhung hier!
    }

    public void LevelCompleted()
    {
        Debug.Log("🎉 Level geschafft!");
        levelUI.ShowMessage("Level geschafft!");
        player.GetComponent<PlayerMovement>().canMove = false;

        // ⏳ Warte, dann neues Level
        Invoke(nameof(NextLevel), 2f);
    }

    private void NextLevel()
    {
        currentLevel++;                 // ⬅️ Jetzt erst hochzählen
        levelUI.ShowLevel(currentLevel);
        SetupLevel();
    }

    public void SetupLevel()
{
    // UI updaten (auch bei Neustart!)
    levelUI.ShowLevel(currentLevel);   // 👈 DAS IST NEU

    // Alte Plattformen entfernen
    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Platform"))
        Destroy(go);

    foreach (GameObject go in GameObject.FindGameObjectsWithTag("Goal"))
        Destroy(go);

    // Neues Level bauen
    generator.currentDifficulty = currentLevel;
    generator.GenerateLevel();

    // Spieler reset
    var cc = player.GetComponent<CharacterController>();
    var movement = player.GetComponent<PlayerMovement>();
    movement.canMove = true;
    cc.enabled = false;
    player.transform.position = startPoint.position;
    cc.enabled = true;
}
}
