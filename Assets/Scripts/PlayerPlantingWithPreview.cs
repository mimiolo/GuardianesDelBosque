using System.Collections;
using UnityEngine;

public class PlayerPlantingWithPreview : MonoBehaviour
{
    [Header("Camera Reference")]
    public Camera playerCamera;   // ? Arrastra aquí tu cámara en el inspector

    [Header("Planting prefabs / settings")]
    public GameObject plantContainerPrefab;
    public GameObject seedPreviewCorrectPrefab;
    public GameObject seedPreviewErrorPrefab;
    public string plantableTag = "Plantable";
    public LayerMask plantableLayer = ~0;
    public float maxPlantDistance = 4f;
    public float minDistanceBetweenPlants = 0.6f;

    [Header("Preview")]
    public float previewYOffset = 0.01f;
    public bool rotateToNormal = true;

    [Header("UI References")]
    public UIToast uiToast;

    GameObject previewCorrectInstance;
    GameObject previewErrorInstance;

    bool previewIsValid = false;
    bool previewIsError = false;
    Vector3 lastValidPreviewPos;
    Vector3 lastValidNormal;

    void Start()
    {
        // ? Validar que la cámara esté asignada
        if (playerCamera == null)
        {
            Debug.LogError("PlayerPlantingWithPreview: No se asignó la cámara del jugador. Arrástrala en el inspector.");
            enabled = false;
            return;
        }

        // Instanciar previews (desactivados)
        if (seedPreviewCorrectPrefab != null)
        {
            previewCorrectInstance = Instantiate(seedPreviewCorrectPrefab);
            previewCorrectInstance.SetActive(false);
        }
        if (seedPreviewErrorPrefab != null)
        {
            previewErrorInstance = Instantiate(seedPreviewErrorPrefab);
            previewErrorInstance.SetActive(false);
        }
    }

    void Update()
    {
        UpdatePreview();

        if (Input.GetMouseButtonDown(1)) // Click derecho
            TryPlant();
    }

    void UpdatePreview()
    {
        previewIsValid = false;
        previewIsError = false;

        if (previewCorrectInstance != null) previewCorrectInstance.SetActive(false);
        if (previewErrorInstance != null) previewErrorInstance.SetActive(false);

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxPlantDistance, plantableLayer))
        {
            if (!hit.collider.CompareTag(plantableTag))
                return;

            Vector3 plantPos = hit.point + hit.normal * previewYOffset;

            Collider[] overlaps = Physics.OverlapSphere(plantPos, minDistanceBetweenPlants);
            bool closeToPlant = false;

            foreach (var c in overlaps)
            {
                if (c.GetComponentInParent<Plant>() != null || c.GetComponentInChildren<Plant>() != null)
                {
                    closeToPlant = true;
                    break;
                }
            }

            if (closeToPlant)
            {
                if (previewErrorInstance != null)
                {
                    previewErrorInstance.SetActive(true);
                    previewErrorInstance.transform.position = plantPos;
                    if (rotateToNormal) previewErrorInstance.transform.up = hit.normal;
                }

                previewIsError = true;
                return;
            }
            else
            {
                if (previewCorrectInstance != null)
                {
                    previewCorrectInstance.SetActive(true);
                    previewCorrectInstance.transform.position = plantPos;
                    if (rotateToNormal) previewCorrectInstance.transform.up = hit.normal;
                }

                previewIsValid = true;
                lastValidPreviewPos = plantPos;
                lastValidNormal = hit.normal;
                return;
            }
        }
    }

    void TryPlant()
    {
        if (previewIsValid)
        {
            GameObject newPlant = Instantiate(plantContainerPrefab, lastValidPreviewPos, Quaternion.identity);
            newPlant.transform.up = lastValidNormal;

            Plant plantScript = newPlant.GetComponent<Plant>();
            plantScript?.StartGrowth();

            uiToast?.ShowMessage("Semilla plantada.");
            return;
        }

        if (previewIsError)
        {
            uiToast?.ShowMessage("Muy cerca de otra plantación.");
            return;
        }

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxPlantDistance, plantableLayer))
        {
            if (!hit.collider.CompareTag(plantableTag))
                uiToast?.ShowMessage("No se puede plantar aquí.");
            else
                uiToast?.ShowMessage("No se puede plantar aquí.");
        }
        else
        {
            uiToast?.ShowMessage("Fuera de rango de plantación.");
        }
    }
}
