using UnityEngine;

public class PlayerPlanting : MonoBehaviour
{
    [Header("Planting")]
    public GameObject plantContainerPrefab; // prefab con script Plant y los stagePrefabs ya asignados
    public string plantableTag = "Plantable"; // tag que debe tener Mud_01
    public LayerMask plantableLayer = ~0; // opcional: limitar raycast a ciertas layers
    public float maxPlantDistance = 4f;
    public float minDistanceBetweenPlants = 0.6f;
    public Transform cameraTransform; // si la cámara no es main, arrastrala

    void Start()
    {
        if (cameraTransform == null)
        {
            if (Camera.main != null) cameraTransform = Camera.main.transform;
            else Debug.LogWarning("PlayerPlanting: no hay Camera asignada ni Camera.main.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // right click
        {
            TryPlant();
        }
    }

    void TryPlant()
    {
        if (cameraTransform == null || plantContainerPrefab == null) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxPlantDistance, plantableLayer))
        {
            // check tag first (optional)
            if (!string.IsNullOrEmpty(plantableTag))
            {
                if (hit.collider.gameObject.tag != plantableTag)
                {
                    // not plantable
                    return;
                }
            }

            Vector3 plantPos = hit.point;

            // avoid planting too close to other plants
            Collider[] overlaps = Physics.OverlapSphere(plantPos, minDistanceBetweenPlants);
            foreach (var c in overlaps)
            {
                // if any nearby object has a Plant component, we block planting
                if (c.GetComponentInParent<Plant>() != null || c.GetComponentInChildren<Plant>() != null)
                {
                    // too close to another plant
                    return;
                }
            }

            // Instantiate plant container prefab at position
            GameObject newPlant = Instantiate(plantContainerPrefab, plantPos, Quaternion.identity);
            // optionally align rotation to ground normal
            newPlant.transform.up = hit.normal;

            // Start growth immediately if Plant script requires (it already starts in Start)
            Plant plantScript = newPlant.GetComponent<Plant>();
            if (plantScript != null)
            {
                plantScript.StartGrowth();
            }
        }
    }

    // debug: visualize overlap sphere in editor
    void OnDrawGizmosSelected()
    {
        if (cameraTransform == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * maxPlantDistance);
    }
}
