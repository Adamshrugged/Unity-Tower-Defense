using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserTurret;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        buildManager.selectTurretToBuild(standardTurret);
    }
    public void SelectMissileTurret()
    {
        buildManager.selectTurretToBuild(missileTurret);
    }
    public void SelectLaserTurret()
    {
        buildManager.selectTurretToBuild(laserTurret);
    }
}