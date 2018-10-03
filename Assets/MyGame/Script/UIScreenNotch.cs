using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenNotch : MonoBehaviour {

    public RectTransform rectTransform;


	void Awake () {


        float aspectRatio = (float)Screen.width / (float)Screen.height;

        //Debug.Log(aspectRatio);

        if (aspectRatio > 1.9f) {

            rectTransform.offsetMax = new Vector2(-100f, rectTransform.offsetMax.y);
        }


    }
	

}
