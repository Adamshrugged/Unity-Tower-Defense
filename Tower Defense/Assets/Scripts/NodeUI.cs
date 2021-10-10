using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject ui;

    public void SetTarget(Node node)
    {
        target = node;

        Vector3 newTransform = target.GetBuildPosition();
        newTransform.y += 1f;
        transform.position = newTransform;

        ui.SetActive(true);
    }

    // 
    public void Hide()
    {
        ui.SetActive(false);
    }

}
