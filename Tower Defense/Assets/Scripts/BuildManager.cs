using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    [SerializeField] public GameObject parentGameObject;

    // Starting money
    [SerializeField] UIController uIController;

    [Header("Optional")]
    private TurretBlueprint turretToBuild;

    // Only one build manager - called when awoken
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uIController.updateEnergy(PlayerStats.energy);
    }

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool CanBuy { get { return PlayerStats.energy >= turretToBuild.cost; } }

    public void selectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public void BuildTurretOn( Node node )
    {
        // Verify player has sufficent energy
        if(PlayerStats.energy < turretToBuild.cost)
        {
            Debug.Log("Insufficient energy");
            return;
        }

        // decrease cost from energy and update UI
        PlayerStats.energy -= turretToBuild.cost;
        uIController.updateEnergy(PlayerStats.energy);

        // Instantiate turret and set node property
        GameObject turret = (GameObject)Instantiate( turretToBuild.prefab, node.GetBuildPosition(),
            Quaternion.identity, parentGameObject.transform);
        node.turret = turret;
    }

}
