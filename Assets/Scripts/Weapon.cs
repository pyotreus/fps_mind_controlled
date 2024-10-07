using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    //Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    //Spread
    public float spreadIntensity;

    //Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletLifeTime = 3f;

    public GameObject muzzleEffect;


    //Shooting mode - remove later
    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    public GameObject currentTarget;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }

    void Update()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        } else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        //print("shooting mode " + currentShootingMode);
        if (readyToShoot && isShooting)
        {
            
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }
        
    }


    private void FireWeapon()
    {
        //if (currentTarget == null)
        //{
        //    print("currentTarget not set");
        //}

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.Instance.shootingHeavySound.Play();
        readyToShoot = false;

        // Calculate the initial shooting direction
        Vector3 shootingDirection = CalculateShootingDirectionAndSpread().normalized;

        // Instantiate bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // Point the bullet in the initial shooting direction
        bullet.transform.forward = shootingDirection;

        // Shoot the bullet
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        // If there's a target, give the bullet the homing behavior
        if (currentTarget != null)
        {
            HomingBullet homingBullet = bullet.GetComponent<HomingBullet>();
            if (homingBullet != null)
            {
                homingBullet.target = currentTarget.transform;  // Assign the target to the homing bullet
                homingBullet.bulletSpeed = bulletVelocity;      // Ensure the bullet keeps moving at the same speed
            }
        }

        // Destroy the bullet after its lifetime expires
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));

        // Check if we are done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        if (currentShootingMode == ShootingMode.Burst || burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateShootingDirectionAndSpread()
    {
        //Shooting from the middle of the screen to check where are we pointing at
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            //Hitting something
            targetPoint = hit.point;
        } else
        {
            //Shooting in the air
            targetPoint= ray.GetPoint(100);
        }
        Vector3 direction = targetPoint - bulletSpawn.position;

        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float bulletLifeTime)
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(bullet); 
    }

    public void SetCurrentTarget(GameObject enemy)
    {
        currentTarget = enemy;
    }
}
