using UnityEngine;

public class Axle : MonoBehaviour {

	public float DistanceToCG { get; set; }
	public float WeightRatio { get; set; } 
	public float SlipAngle { get; set; }
	public float FrictionForce {
		get {
			return (TireLeft.FrictionForce + TireRight.FrictionForce) / 2f;
		}
	}
	public float AngularVelocity {
		get {
			return Mathf.Min (TireLeft.AngularVelocity + TireRight.AngularVelocity);
		}
	}
	public float Torque {
		get {
			return (TireLeft.Torque + TireRight.Torque) / 2f;
		}
	}

	public Tire TireLeft { get; private set; }
	public Tire TireRight { get; private set; }

	void Awake() {
		TireLeft = transform.Find ("TireLeft").GetComponent<Tire> ();
		TireRight = transform.Find ("TireRight").GetComponent<Tire> ();
	}

	public void Init(Rigidbody2D rb, float wheelBase) {

		// Weight distribution on each axle and tire
		WeightRatio = DistanceToCG / wheelBase;

		// Calculate resting weight of each Tire
		float weight = rb.mass * (WeightRatio * -Physics2D.gravity.y);
		TireLeft.RestingWeight = weight;
		TireRight.RestingWeight = weight;
	}

}
