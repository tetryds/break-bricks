using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bricks.Player
{
    public class BulletSimple : Bullet
    {
        [SerializeField] float speed;

        private void Update()
        {
            transform.localPosition += speed * Time.deltaTime * transform.forward;
        }

        public void SetSpeed(float speed) => this.speed = speed;
    }
}
