using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Color hoverColor;
    private SpriteRenderer rend;
    private Color startColor;

    private GameObject turret;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.material.color;
        turret = null;
    }

    private void OnMouseDown()
    {
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
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
