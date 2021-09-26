using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    [SerializeField] public GameObject standardTurretPrefab;
    private GameObject turretToBuild;

    // Only one build manager - called when awoken
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }


    public GameObject GetTurretToBuild()
    {
        if (turretToBuild == null)
        {
            Debug.Log("turret is null");
        }
        return turretToBuild;
    }

}
