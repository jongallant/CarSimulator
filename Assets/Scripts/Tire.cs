using UnityEngine;

namespace CarSimulator2D
{
    public class Tire : MonoBehaviour
    {
        public float RestingWeight { get; set; }
        public float ActiveWeight { get; set; }
        public float Grip { get; set; }
        public float FrictionForce { get; set; }
        public float AngularVelocity { get; set; }
        public float Torque { get; set; }

        public float Radius = 0.5f;

        float TrailDuration = 5;
        bool TrailActive;

        public GameObject SkidmarkPrefab; // direct reference
        GameObject Skidmark;

        public void SetTrailActive(bool active)
        {
            if (active && !TrailActive)
            {
                // These should be pooled and re-used
                Skidmark = GameObject.Instantiate(original: SkidmarkPrefab, position: this.transform.position, rotation: Quaternion.identity, parent: this.transform);
                Skidmark.GetComponent<TrailRenderer>().time = TrailDuration;

                //Fix issue where skidmarks draw at 0,0,0 at slow speeds
                Skidmark.GetComponent<TrailRenderer>().Clear();

                Skidmark.GetComponent<TrailRenderer>().enabled = true;
                

                
                //Skidmark.GetComponent<TrailRenderer>().sortingOrder = 0;
                //Skidmark.transform.parent = this.transform;
                //Skidmark.transform.localPosition = Vector2.zero;
            }
            else if (!active && TrailActive)
            {
                Skidmark.transform.parent = null;
                GameObject.Destroy(Skidmark.gameObject, TrailDuration);
            }
            TrailActive = active;
        }

    }
}