using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public Camera sceneCam;

    //Zach: variables for raycasting shooting
    private float speed;
    public Transform gunPoint;
    public GameObject bulletTrail;
    private float weaponRange = 10f;
    private Animator muzzleFlashAnimator;

    public void Start()
    {
        sceneCam = Camera.main;
        gunPoint = GetComponent<Transform>();
        //bulletTrail = GetComponent<BulletTrail2>();
    }

    /*public float lookAtMouse(Camera sceneCam, Vector2 playerPos)
    {
        Vector2 mousePos = sceneCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePos - playerPos;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        return aimAngle;
    }*/

    public void shoot()
    {
        //muzzleFlashAnimator.SetTrigger("Shoot");
        var hit = Physics2D.Raycast(
            gunPoint.position,
            transform.up,
            weaponRange
            );

        var trail = Instantiate(
            bulletTrail, 
            gunPoint.position,
            transform.rotation
            );

        var trailScript = trail.GetComponent<BulletTrailScript>();

        if (hit.collider != null) // if hit something
        {
            /*trailScript.SetTargetPosition(hit.point);
            var hittable = hit.collider.GetComponent<IHittable>(); // can't seem to get IHittable working
            hittable?.Hit();*/
            Debug.Log("Hit something");
        }
        else // if hit nothing
        {
            /*var endPosition = gunPoint.position + transform.up * weaponRange;
            trailScript.SetTargetPosition(endPosition);*/
            Debug.Log("Hit nothing");
        }
    }

    public float lookAtMouse(Vector2 playerPos)
    {
        Vector2 mousePos = sceneCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = mousePos - playerPos;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        return aimAngle;
    }
}
