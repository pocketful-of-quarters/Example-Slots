using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UISpinner : MonoBehaviour {



	void Awake () {
        this.transform.DORotate(new Vector3(0, 0, -90f), 0.5f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

        this.gameObject.SetActive(false);
	}



	

}
