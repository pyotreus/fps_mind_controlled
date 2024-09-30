using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletLifeTime = 3f;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
        
    }

    private void FireWeapon()
    {
        //Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        //shoot the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        //Destroy the bullet
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletLifeTime)
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(bullet); 
    }
}
