using System;
using UnityEngine;

namespace Darkmatter.Presentation
{
    public class Gun : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public float LifeTime = 0.05f;

        public void Init(Vector3 start, Vector3 end)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
            Invoke("DisableBullet", LifeTime);
        }
        void DisableBullet()
        {
            lineRenderer.enabled = false;
        }

    

        public void Shoot()
        {
            Debug.Log("Shooting");
        }
    }
}
