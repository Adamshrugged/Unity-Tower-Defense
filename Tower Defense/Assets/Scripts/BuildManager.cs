using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    [SerializeField] public GameObject parentGameObject;

    // Starting money
    [SerializeField] UIController uIController;
    [SerializeField] NodeUI nodeUI;

    [Header("Optional")]
    private TurretBlueprint turretToBuild;
    private Node selectedNode;

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
        DeselectNode();
    }
    public TurretBlueprint getTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectNode(Node node)
    {
        // hide if selecting again
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
}
