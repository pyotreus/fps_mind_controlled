using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision hitObject)
    {
        if (hitObject.gameObject.CompareTag("Target")) {
            print("hit " + hitObject.gameObject.name);
            CreateBulletImpactEffect(hitObject);
            Destroy(gameObject);
        }

        if (hitObject.gameObject.CompareTag("Wall"))
        {
            print("hit a wall" + hitObject.gameObject.name);
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
