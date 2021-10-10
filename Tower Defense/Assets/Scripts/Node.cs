using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    [SerializeField] Color LowEnergyColor;
    private SpriteRenderer rend;
    private Color startColor;

    BuildManager buildManager;

    public GameObject turret;
    public TurretBlueprint turretBlueprint;
    public bool isUpgraded = false;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.material.color;
        turret = null;

        buildManager = BuildManager.instance;
    }

    // Position
    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }

    private void OnMouseDown()
    {
        // see if a UI element is in the way
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Check if there is a turret on the selected node, select that node to show sell or upgrade options
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        // check if selected turret is blank, if so then do not perform any actions
        if (!buildManager.CanBuild)
        {
            return;
        }

        // build turret
        BuildTurret( buildManager.getTurretToBuild() );
    }

    // Build a turret
    void BuildTurret(TurretBlueprint blueprint)
    {
        // set blueprint
        turretBlueprint = blueprint;

        // Verify player has sufficent energy
        if (PlayerStats.energy < blueprint.cost)
        {
            Debug.Log("Insufficient energy");
            return;
        }

        // decrease cost from energy and update UI
        PlayerStats.energy -= blueprint.cost;

        // Instantiate turret and set node property
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(),
            Quaternion.identity, buildManager.parentGameObject.transform);
        turret = _turret;
    }

    public void UpgradeTurret()
    {
        if(PlayerStats.energy < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough energy to upgrade");
            return;
        }


        // Subtract funds
        PlayerStats.energy -= turretBlueprint.upgradeCost;

        // Generate new turret from prefab
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, turret.transform.position,
            Quaternion.identity, buildManager.parentGameObject.transform);

        // Remove prior turret and set new one
        Destroy(turret);
        turret = _turret;

        // set flag for turret being upgrade
        isUpgraded = true;

        // Effect for upgrading turret
        GameObject effect = (GameObject)Instantiate(buildManager.upgradeEffect, _turret.transform.position,
            Quaternion.identity);
        Destroy(effect, 0.5f);
    }

    public void SellTurret()
    {
        PlayerStats.energy += turretBlueprint.GetSellAmount();

        // Effect for selling turret
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, turret.transform.position,
            Quaternion.identity);
        Destroy(effect, 1f);

        Destroy(turret);
        turretBlueprint = null;
    }

    private void OnMouseEnter()
    {
        // see if a UI element is in the way
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // check if selected turret is blank, if so then highlight node
        if (buildManager != null && !buildManager.CanBuild)
        {
            return;
        }

        // if there is a turret selected, then add color to node depending on available funds
        if(buildManager != null && buildManager.CanBuy)
        {
            if(rend != null)
                rend.material.color = hoverColor;
        }
        else
        {
            if(rend != null)
                rend.material.color = LowEnergyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
