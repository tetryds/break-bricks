using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Bricks.Player
{
    public class DoubleTapDetector : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] float tapInterval;
        [SerializeField] float maxDiffDistance;

        public event Action Fired;

        float lastPointerUp = 0f;
        Vector2 previousPointerDownPos;

        public void OnPointerUp(PointerEventData eventData)
        {
            float time = Time.unscaledTime;

            lastPointerUp = time;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            float time = Time.unscaledTime;
            Vector2 position = eventData.position;

            if (time - lastPointerUp <= tapInterval)
            {
                if ((position - previousPointerDownPos).magnitude <= maxDiffDistance)
                {
                    Fired?.Invoke();
                }
            }

            previousPointerDownPos = position;
        }
    }
}
