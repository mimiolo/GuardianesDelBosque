using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Visual prefabs for each stage (0 = seed, 1 = small, 2 = medium, 3 = large)")]
    public GameObject[] stagePrefabs; // assign: Seed, Small, Medium, Large

    [Header("Growth settings")]
    public float timeBetweenStages = 60f; // default 60 seconds
    public bool startGrowingOnAwake = true;

    [Header("Optional")]
    public float visualYOffset = 0.0f; // if visuals need slight offset above ground

    int currentStage = 0;
    GameObject currentVisual;

    void Start()
    {
        if (startGrowingOnAwake)
            StartGrowth();
    }

    public void StartGrowth()
    {
        StopAllCoroutines();
        StartCoroutine(GrowRoutine());
    }

    IEnumerator GrowRoutine()
    {
        if (stagePrefabs == null || stagePrefabs.Length == 0)
        {
            Debug.LogWarning("Plant: stagePrefabs no asignado.");
            yield break;
        }

        currentStage = 0;
        SpawnStageVisual(currentStage);

        while (currentStage < stagePrefabs.Length - 1)
        {
            float timer = 0f;
            while (timer < timeBetweenStages)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            currentStage++;
            SpawnStageVisual(currentStage);
        }

        OnFullyGrown();
    }

    void SpawnStageVisual(int stage)
    {
        if (currentVisual != null)
            Destroy(currentVisual);

        if (stage < 0 || stage >= stagePrefabs.Length || stagePrefabs[stage] == null) return;

        currentVisual = Instantiate(stagePrefabs[stage], transform.position + Vector3.up * visualYOffset, Quaternion.identity, transform);
        currentVisual.transform.localPosition = Vector3.up * visualYOffset;
    }

    void OnFullyGrown()
    {
        // hook para cosecha/efectos si quieres
    }

    public int GetCurrentStage() => currentStage;
}
