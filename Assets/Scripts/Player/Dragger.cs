using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bricks.Player
{
    public class Dragger : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] float forceScale;
        [SerializeField] float dampenScale;
        [Min(0f)]
        [SerializeField] float dampenRotation;

        readonly TargetData target = new();

        Vector3 force;
        Vector3 forcePos;
        Vector3 previousForcePos;

        void Update()
        {
            HandleClick();
            HandleDrag();
            HandleRelease();

            HandleRightMouseClick();
        }

        private void HandleRightMouseClick()
        {
            if (!Input.GetMouseButtonDown(1)) return;

            Ray click = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(click, out RaycastHit hit))
            {
                if (hit.rigidbody)
                {
                    Debug.Log(hit.rigidbody.mass);
                    Debug.Log("Set density");
                    hit.rigidbody.SetDensity(1f);
                    hit.rigidbody.mass = hit.rigidbody.mass;
                }
            }
        }

        private void FixedUpdate()
        {
            ApplyForce();
        }

        private void HandleClick()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ray click = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(click, out RaycastHit hit))
            {
                target.AssignFromHit(click, hit);
                previousForcePos = target.point;
            }
        }

        private void HandleDrag()
        {
            if (target.rigidbody == null || !Input.GetMouseButton(0)) return;

            Ray drag = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 dragToPos = drag.GetPoint(target.distance);
            Vector3 delta = dragToPos - target.point;
            force = delta * forceScale;
            forcePos = target.point;
        }

        private void HandleRelease()
        {
            if (!Input.GetMouseButtonUp(0)) return;

            target.rigidbody = null;
        }

        private void ApplyForce()
        {
            Rigidbody rb = target.rigidbody;
            if (rb == null || !Input.GetMouseButton(0)) return;

            rb.AddForceAtPosition(force * force.magnitude - Physics.gravity, forcePos, ForceMode.Acceleration);

            //Vector3 forcePosDelta = forcePos - previousForcePos;
            //Debug.DrawRay(forcePos, forcePosDelta * 10f, Color.red);
            //rb.AddForceAtPosition(-forcePosDelta * dampenScale, forcePos, ForceMode.Acceleration);
            //Debug.DrawRay(forcePos, forcePosDelta * dampenScale * 10f, Color.blue);

            //rb.AddForce(-rb.velocity * dampingForce, ForceMode.VelocityChange);
            rb.angularVelocity *= 1f / (dampenRotation + 1f);
            rb.velocity *= 1f / (dampenScale + 1f);

            previousForcePos = forcePos;
        }

        class TargetData
        {
            public Ray ray;
            public float distance;
            public Rigidbody rigidbody;
            public Transform transform;
            public Vector3 localPoint;
            public Vector3 point => transform.TransformPoint(localPoint);

            public void AssignFromHit(Ray ray, RaycastHit hit)
            {
                this.ray = ray;
                distance = hit.distance;
                rigidbody = hit.rigidbody;
                transform = hit.transform;
                localPoint = hit.transform.InverseTransformPoint(hit.point);
            }
        }
    }
}
