using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Bricks.Player
{
    public class BulletSmart : Bullet
    {
        [SerializeField] float rotSpeed;
        [SerializeField] float speed;
        [SerializeField] float destroyDistance;
        [SerializeField] GameObject baseExplosion;

        GameObject target;

        private void Update()
        {
            if (!target)
                target = FindTarget();

            if (target)
            {
                AimToTarget(target);
            }

            GoStraight();

            CheckCollision();
        }


        private GameObject FindTarget()
        {
            Target[] targets = FindObjectsOfType<Target>();

            Target target = null;
            float closest = float.PositiveInfinity;

            for (int i = 0; i < targets.Length; i++)
            {
                float distance = Vector3.Distance(targets[i].transform.position, transform.position);
                if (distance < closest)
                {
                    target = targets[i];
                    closest = distance;
                }
            }
            return target?.gameObject;
        }

        private void AimToTarget(GameObject target)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;

            Debug.DrawLine(target.transform.position, transform.position, Color.red);
            Debug.DrawRay(transform.position, dir, Color.blue);
            //Quaternion rot = transform.rotation;
            Quaternion targetRot = Quaternion.FromToRotation(Vector3.forward, dir);
            //Quaternion interpolated = Quaternion.Slerp(Quaternion.identity, targetRot, rotSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        }

        private void GoStraight()
        {
            transform.localPosition += speed * Time.deltaTime * transform.forward;
        }

        private void CheckCollision()
        {
            if (!target) return;

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < destroyDistance)
            {
                Vector3 explosionPos = transform.position;
                explosionPos.y = 0;
                var explosion = Instantiate(baseExplosion, explosionPos, Quaternion.identity);
                Destroy(explosion, 5f);
                Destroy(gameObject);
                Destroy(target);
            }
        }
    }
}
