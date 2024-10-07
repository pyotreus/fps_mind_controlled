using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool enemyDamage, playerDamage;
    private void OnCollisionEnter(Collision hitObject)
    {
        Transform hitTransform = hitObject.transform;
        if (hitObject.gameObject.CompareTag("Target") 
            && hitObject.gameObject.GetComponentInParent<EnemyHealth>()
            && enemyDamage) {
            //print("hit " + hitObject.gameObject.name);
            CreateBulletImpactEffect(hitObject);
            EnemyHealth enemyHealth = hitObject.gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeDamage(10);
            Destroy(gameObject);
        }
        
        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("hit player");
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(10);
            Destroy(gameObject);
        }

        if (hitObject.gameObject.CompareTag("Wall"))
        {
            //print("hit a wall" + hitObject.gameObject.name);
            CreateBulletImpactEffect(hitObject);
            Destroy(gameObject);
        }
        
    }

    void CreateBulletImpactEffect(Collision hitObject)
    {
        ContactPoint contactPoint = hitObject.contacts[0];
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactPrefabEffect,
            contactPoint.point,
            Quaternion.LookRotation(contactPoint.normal)
            );
    }
}
