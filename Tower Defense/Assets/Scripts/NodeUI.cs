using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject ui;

    [SerializeField] private Text upgradeCost;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text sellAmount;

    public void SetTarget(Node node)
    {
        target = node;

        Vector3 newTransform = target.GetBuildPosition();
        newTransform.y += 1f;
        transform.position = newTransform;

        // change UI
        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
            upgradeCost.text = "MAX";
        }
        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();
        ui.SetActive(true);
    }

    // Deactivate (hide) the UI
    public void Hide()
    {
        ui.SetActive(false);
    }

    // Upgrade call for UI button. Then deselects node
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    // Selling turret
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

}
