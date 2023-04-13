using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRaycast : Gun
{

    public override void shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, transform.TransformDirection(Vector2.up), 10f);
        if (hitInfo)
        {
            Debug.Log("Hit something w/ raycast");
            //Instantiate(impactEffect, hitInfo.point, Quaternion.identity); //not sure what the Quaternion does
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            Debug.Log("Missed w/ raycast");
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, Input.mousePosition * 10);
        }
    }
}
