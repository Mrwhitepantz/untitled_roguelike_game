using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    //firepoint1 is inherited from Gun
    //[SerializeField] protected playerBullet bulletScript = GetComponent<shotgunBullet>();
    [SerializeField] protected Transform firePoint2;
    [SerializeField] protected Transform firePoint3;
    [SerializeField] protected float fireRate = .65f;
    [SerializeField] protected float bulletSpeed = 9f;

    public override void shoot()
    {
        StartCoroutine("shotDelay", fireRate);
    }

    private IEnumerator shotDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject projectile1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject projectile2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        GameObject projectile3 = Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
        Rigidbody2D bullet1 = projectile1.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet2 = projectile2.GetComponent<Rigidbody2D>();
        Rigidbody2D bullet3 = projectile3.GetComponent<Rigidbody2D>();
        /*bullet1.AddForce(firePoint.up * bulletScript.speed, ForceMode2D.Impulse);
        bullet2.AddForce(firePoint2.up * bulletScript.speed, ForceMode2D.Impulse);
        bullet3.AddForce(firePoint2.up * bulletScript.speed, ForceMode2D.Impulse);*/
        bullet1.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        bullet2.AddForce(firePoint2.up * bulletSpeed, ForceMode2D.Impulse);
        bullet3.AddForce(firePoint2.up * bulletSpeed, ForceMode2D.Impulse);
        sfx.PlayOneShot(gunshotSFX, .3f);
        StopCoroutine("shotDelay");
    }
}
