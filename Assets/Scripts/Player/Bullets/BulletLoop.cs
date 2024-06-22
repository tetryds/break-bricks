using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bricks.Player
{
    public class BulletLoop : Bullet
    {
        [SerializeField] float rotSpeed;
        [SerializeField] float speed;

        private void Update()
        {
            transform.Rotate(transform.up, rotSpeed * Time.deltaTime);
            transform.localPosition += speed * Time.deltaTime * transform.forward;
        }

        public void SetSpeed(float speed) => this.speed = speed;
    }
}
