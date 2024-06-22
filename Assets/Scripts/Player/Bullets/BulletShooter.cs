using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bricks.Player
{
    public class BulletShooter : MonoBehaviour
    {
        [SerializeField] Transform pivot;
        [SerializeField] Bullet[] baseBullets;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Bullet newBullet = Instantiate(baseBullets[0], pivot.position, pivot.rotation);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Bullet newBullet = Instantiate(baseBullets[1], pivot.position, pivot.rotation);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Bullet newBullet = Instantiate(baseBullets[2], pivot.position, pivot.rotation);
            }
        }
    }
}
