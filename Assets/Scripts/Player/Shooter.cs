using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bricks.Player
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] Rigidbody ammo;
        [SerializeField] float speed;

        void Update()
        {
            HandleSpace();
        }

        private void HandleSpace()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            Transform camTransform = cam.transform;

            Ray fireRay = cam.ScreenPointToRay(Input.mousePosition);

            Rigidbody spanwed = Instantiate(ammo, fireRay.origin, Quaternion.LookRotation(fireRay.direction));

            spanwed.velocity = fireRay.direction * speed;
        }
    }
}
