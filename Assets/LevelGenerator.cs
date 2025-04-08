using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject goalPrefab;
    public int numberOfSteps = 20; // Strecke statt Fläche!
    public float baseSpacing = 6f;

    public int currentDifficulty = 1;

  public void GenerateLevel()
{
    // Standardwerte
    float spacing;
    float scale;
    float heightVariance;
    float sideVariance;

    if (currentDifficulty == 1)
    {
        // 📌 Feste Werte für einfaches Startlevel
        spacing = 5f;
        scale = 1.4f;             // große Plattformen
        heightVariance = 0.1f;    // fast keine Höhenunterschiede
        sideVariance = 0f;        // keine Versetzung
    }
    else
    {
        // 🔁 Ab Level 2: Schwierigkeit dynamisch
        spacing = 5f + Mathf.Clamp((currentDifficulty - 1) * 0.3f, 0f, 4f);
        scale = Mathf.Clamp(1.4f - (currentDifficulty - 1) * 0.07f, 0.6f, 1.4f);
        heightVariance = Mathf.Clamp((currentDifficulty - 1) * 0.4f, 0f, 3f);
        sideVariance = Mathf.Clamp((currentDifficulty - 1) * 0.3f, 0f, 2f);
    }

    Debug.Log($"🧠 Level {currentDifficulty} | Scale: {scale} | Spacing: {spacing}");

    Vector3 currentPos = Vector3.zero;

    // Startplattform (extra groß)
    SpawnPlatform(currentPos, 1.5f);

    for (int i = 0; i < numberOfSteps; i++)
    {
        currentPos.z += spacing;
        currentPos.x += Random.Range(-sideVariance, sideVariance);
        currentPos.y = Random.Range(0f, heightVariance);

        SpawnPlatform(currentPos, scale);
    }

    // Ziel etwas über der letzten Plattform
    Vector3 goalPos = currentPos + new Vector3(0, 1f, 0);
    GameObject goal = Instantiate(goalPrefab, goalPos, Quaternion.identity);
    goal.tag = "Goal";
}

    private void SpawnPlatform(Vector3 position, float scale = 1f)
    {
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity);
        platform.tag = "Platform";
        platform.transform.localScale = new Vector3(scale, 0.5f, scale);
    }
}
