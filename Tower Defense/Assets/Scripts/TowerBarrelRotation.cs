using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBarrelRotation : MonoBehaviour
{
    /*
    [SerializeField] public Transform pivot;
    [SerializeField] public Transform barrel;

    public Tower tower;
    bool shotAimed = false;

    private void Update()
    {
        if(tower != null && tower.currentTarget != null)
        {
            Vector2 relative = tower.currentTarget.transform.position - pivot.position;
            float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;

            Vector3 newRotation = new Vector3(0, 0, angle);
            pivot.localRotation = Quaternion.Euler(newRotation);

            // Toggle aimed to true
            shotAimed = true;
        }
        shotAimed = false;
    }*/
}
