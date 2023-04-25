using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;

    //Zach: Bullet collision for if bullet doesn't collid with anything
    public void Update()
    {
        if (bulletPrefab.name == "shotgunBullet(Clone)") //simulates shotgun's short range
        {
            Destroy(this.gameObject, .25f);
        } 
        else //Zach: tried to have this in OnTriggerEnter2D, but it never executes
        {
            Destroy(this.gameObject, 1);
        }
        
    }

    //Zach: Bullet collision logic
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Squrtal")
        {
            Debug.Log("playerBullet: hit an enemy");
            Destroy(this.gameObject);
        }
        else if (hitInfo.tag == "Player")
        {
            Debug.Log("playerBullet: hit player");
        }
        else if (hitInfo.tag == "EnvironmentDecorations" ||
            hitInfo.tag == "EnvironmentObjects")
        {
            Debug.Log("playerBullet: hit an environment");
            Destroy(this.gameObject);
        }
    }
}
