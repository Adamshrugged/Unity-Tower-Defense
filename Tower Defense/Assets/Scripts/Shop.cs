using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserTurret;
    public TurretBlueprint fireTurret;
    public TurretBlueprint slowTurret;

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
    public void SelectFireTurret()
    {
        buildManager.selectTurretToBuild(fireTurret);
    }
    public void SelectSlowTurret()
    {
        buildManager.selectTurretToBuild(slowTurret);
    }
}