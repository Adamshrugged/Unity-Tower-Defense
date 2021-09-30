using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    [SerializeField] public GameObject standardTurretPrefab;
    [SerializeField] public GameObject missileTurretPrefab;
    private GameObject turretToBuild;

    // Only one build manager - called when awoken
    private void Awake()
    {
        instance = this;
    }


    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void setTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

}
