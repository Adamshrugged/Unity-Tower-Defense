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

        // check if selected turret is blank, if so then do not perform any actions
        if (!buildManager.CanBuild)
        {
            return;
        }

        if (turret != null)
        {
            Debug.Log("Cannot build here");
            return;
        }

        // can build
        buildManager.BuildTurretOn(this);
    }

    private void OnMouseEnter()
    {
        // see if a UI element is in the way
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // check if selected turret is blank, if so then do not perform any actions
        if (!buildManager.CanBuild)
        {
            return;
        }

        // if there is a turret selected, then add color to node depending on available funds
        if(buildManager.CanBuy)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = LowEnergyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
