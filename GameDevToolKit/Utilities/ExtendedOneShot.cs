using UnityEngine;

namespace GameDevToolKit
{
    public class ExtendedOneShot : MonoBehaviour
    {
        public float Lifetime;

        private void Update()
        {
            Lifetime -= Time.deltaTime;
            if (Lifetime < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}