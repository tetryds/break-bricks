using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Bricks.Player
{
    public class DragHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] Canvas canvas;
        //[SerializeField] GameObject trail;
        [SerializeField] bool invertY;
        [SerializeField] Vector2 sensitivity;
        [SerializeField] float gain;
        [SerializeField] int smoothBufferSize;

        public event Action<Vector2> DraggingStart;
        public event Action<Vector2> Dragging;
        public event Action DraggingEnd;

        bool dragging = false;

        Vector2 pos;
        Vector2 previousPos;

        Vector2[] positions;

        private void Start()
        {
            positions = new Vector2[smoothBufferSize];
        }

        private void Update()
        {
            Vector2 targetPos = Input.touchCount > 0 ? Input.GetTouch(0).position : Vector2.zero;
            pos += (targetPos - pos) * gain * Time.deltaTime;

            for (int i = positions.Length - 1; i >= 1; i--)
            {
                positions[i] = positions[i - 1];
            }

            positions[0] = pos;

            if (dragging)
            {
                Vector2 smoothPos = GetSmoothPos();

                Vector2 posDelta = smoothPos - previousPos;
                previousPos = smoothPos;

                Vector2 move = posDelta / Time.deltaTime;
                float screenWidth = Screen.height;
                Vector2 scaled = Vector2.Scale(move, sensitivity) / screenWidth;
                Vector2 clamped = Vector2.ClampMagnitude(scaled, 1f);
                if (invertY)
                    clamped.y = -clamped.y;
                if (dragging)
                    Dragging?.Invoke(clamped);
            }
        }

        private Vector2 GetSmoothPos()
        {
            Vector2 smoothPos = Vector2.zero;

            for (int i = 0; i < positions.Length; i++)
            {
                smoothPos += positions[i];
            }
            smoothPos /= positions.Length;
            return smoothPos;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = Vector2.zero;
            }
            dragging = false;
            Dragging?.Invoke(Vector2.zero);
            DraggingEnd?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            pos = eventData.position;
            previousPos = pos;
            Array.Fill(positions, pos);
            dragging = true;
            DraggingStart?.Invoke(pos);
        }
    }
}
