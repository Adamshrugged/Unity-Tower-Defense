using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        buildManager.setTurretToBuild(buildManager.standardTurretPrefab);
    }
    public void PurchaseAnotherTurret()
    {
        buildManager.setTurretToBuild(buildManager.missileTurretPrefab);
    }
}