namespace MasujimaRyohei
{
    using UnityEngine;
    using System.Collections;

    public class RotateObject : MovableObject
    {
        public bool isRotate = true;
        public bool isAccelerate = false;
        public float accelerateRate = 0.1f;
        public float accelerateLimit = 100.0f;

        [SerializeField]
        private Vector3 rotateSpeed = new Vector3(0, 0, 0);



        // Update is called once per frame
        private void Update()
        {
            if (isRotate)
                this.transform.Rotate(rotateSpeed);

            if (isAccelerate)
                Accelerate();
        }

        private void Accelerate()
        {
            if (rotateSpeed.x <= accelerateLimit)
                rotateSpeed.x = AccelerateEachAxis(rotateSpeed.x);
            if (rotateSpeed.y <= accelerateLimit)
                rotateSpeed.y = AccelerateEachAxis(rotateSpeed.y);
            if (rotateSpeed.z <= accelerateLimit)
                rotateSpeed.z = AccelerateEachAxis(rotateSpeed.z);
        }

        private float AccelerateEachAxis(float currentSpeed)
        {
            switch (CheckMark(currentSpeed))
            {
                case MARKS.POSITIVE:
                    currentSpeed += accelerateRate;
                    break;
                case MARKS.NEGATIVE:
                    currentSpeed -= accelerateRate;
                    break;
                case MARKS.ZERO:
                default:
                    break;
            }
            return currentSpeed;
        }

        // should chenge to templete
        private MARKS CheckMark(float num)
        {
            if (num > 0)
                return MARKS.POSITIVE;
            if (num == 0)
                return MARKS.ZERO;
            if (num < 0)
                return MARKS.NEGATIVE;

            return MARKS.ZERO;
        }

    }
}