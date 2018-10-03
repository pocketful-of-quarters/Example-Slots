using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class UICanvasScaler : MonoBehaviour {

	[HideInInspector]
	public Vector2 nominalSize = new Vector2();

	private CanvasScaler canvasScaler {
		get {
			return this.GetComponent<CanvasScaler>();
		}
	}




	void Awake() {

		nominalSize = canvasScaler.referenceResolution;
		float aspectRatio = (float)Screen.width / (float)Screen.height;

        Debug.Log(aspectRatio);

		if (aspectRatio > 1.9f) {
			//super wide 2:1 ratio support
			canvasScaler.referenceResolution = new Vector2(nominalSize.y * aspectRatio, nominalSize.y);
		}


	}



}
