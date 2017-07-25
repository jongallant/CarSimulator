using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {
	
	[SerializeField]
	int[] TorqueCurve = new int[8] { 100, 280, 325, 420, 460, 340, 300, 100 };

	[SerializeField]
	float[] GearRatios = new float[] { 5.8f, 4.5f, 3.74f, 2.8f, 1.6f, 0.79f, 4.2f };

	public int CurrentGear { get; private set; }

	public float GearRatio {
		get { return GearRatios[CurrentGear]; }
	}

	public float EffectiveGearRatio {
		get { return GearRatios[GearRatios.GetLength(0) - 1]; }
	}

	public void ShiftUp() {
		CurrentGear++;
	}

	public void ShiftDown() {
		CurrentGear--;
	}

	public float GetTorque(Rigidbody2D rb) {
		return GetTorque(GetRPM (rb));
	}

	public float GetRPM(Rigidbody2D rb) {
		return rb.velocity.magnitude / (Mathf.PI * 2 / 60f) * (GearRatio * EffectiveGearRatio);
	}

	public float GetTorque(float rpm)
	{		
		if (rpm < 1000) {			
			return Mathf.Lerp (TorqueCurve [0], TorqueCurve [1], rpm / 1000f);
		} else if (rpm < 2000) {
			return Mathf.Lerp (TorqueCurve [1], TorqueCurve [2], (rpm - 1000) / 1000f);
		} else if (rpm < 3000) {
			return Mathf.Lerp (TorqueCurve [2], TorqueCurve [3], (rpm - 2000) / 1000f);
		} else if (rpm < 4000) {
			return Mathf.Lerp (TorqueCurve [3], TorqueCurve [4], (rpm - 3000) / 1000f);
		} else if (rpm < 5000) {
			return Mathf.Lerp (TorqueCurve [4], TorqueCurve [5], (rpm - 4000) / 1000f);
		} else if (rpm < 6000) {
			return Mathf.Lerp (TorqueCurve [5], TorqueCurve [6], (rpm - 5000) / 1000f);
		} else if (rpm < 7000) {
			return Mathf.Lerp (TorqueCurve [6], TorqueCurve [7], (rpm - 6000) / 1000f);
		} else {			
			return TorqueCurve [6];
		}

	}

	public void UpdateAutomaticTransmission(Rigidbody2D rb) {
		float rpm = GetRPM (rb);

		if (rpm > 6200) {
			if (CurrentGear < 5)
				CurrentGear++;
		} else if (rpm < 2000) {
			if (CurrentGear > 0)
				CurrentGear--;
		}
	}


}
