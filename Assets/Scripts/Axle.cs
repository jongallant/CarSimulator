using UnityEngine;

namespace CarSimulator2D
{
    public class Axle : MonoBehaviour
    {

        public Tire TireLeft; // direct reference
        public Tire TireRight; // direct reference
        public Transform AxleTransform; // direct reference

        public float DistanceToCG { get; set; }
        public float WeightRatio { get; set; }
        public float SlipAngle { get; set; }
        public float FrictionForce => (TireLeft.FrictionForce + TireRight.FrictionForce) / 2f;
        public float AngularVelocity => Mathf.Min(TireLeft.AngularVelocity + TireRight.AngularVelocity);
        public float Torque => (TireLeft.Torque + TireRight.Torque) / 2f;

        void OnEnable()
        {
        }

        public void Init(Rigidbody2D rb, float wheelBase)
        {

            // Weight distribution on each axle and tire
            WeightRatio = DistanceToCG / wheelBase;

            // Calculate resting weight of each Tire
            float weight = rb.mass * (WeightRatio * -Physics2D.gravity.y);
            TireLeft.RestingWeight = weight;
            TireRight.RestingWeight = weight;
        }
    }
}