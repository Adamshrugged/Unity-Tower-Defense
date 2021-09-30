using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    private SpriteRenderer rend;
    private Color startColor;

    BuildManager buildManager;

    private GameObject turret;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.material.color;
        turret = null;

        buildManager = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        // see if a UI element is in the way
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // check if selected turret is blank, if so then do not perform any actions
        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }

        Debug.Log(turret);
        if (turret != null)
        {
            Debug.Log("Cannot build here");
            return;
        }

        // Display build UI
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
    }

    private void OnMouseEnter()
    {
        // see if a UI element is in the way
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        // check if selected turret is blank, if so then do not perform any actions
        if (buildManager.GetTurretToBuild() == null)
        {
            return;
        }

        // if there is a turret selected, then add color to node
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
