using System;
using System.Collections.Generic;
using Elona.Slot;
using CSFramework;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Purchasing;
using QuartersSDK;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class Signup : MonoBehaviour {
	public Elos elos;
    public GM gameManager;
    public Button signupButton;

    public Transform window;
	public Image background;
    public AuthFailed authFailed;

    public delegate void OnDismissed();
    public OnDismissed onDismissed;


    private Elos.Assets assets { get { return elos.assets; } }

    public void Activate(OnDismissed onDismissed) {
        Debug.Log("Activating signup modal...");

        this.onDismissed = onDismissed;

        gameObject.SetActive(true);
		assets.audioClick.Play();
		background.color = new Color(0, 0, 0, 0);
		background.DOFade(0.5f, 1f);
		window.DOLocalMoveY(0, 1f).SetEase(Ease.OutBounce);
		Util.Tween(0.35f, null, () => {
			assets.audioImpact.Play();
			Camera.main.DOShakePosition(1.2f, 6, 12);
		});
	}

    public void Register () {
        Quarters.Instance.SignUp(AuthorizationSuccess, AuthorizationFailed);
    }

    private void AuthorizationSuccess()
    {
        Debug.Log("OnAuthorizationSuccess");
        Deactivate();
        gameManager.AuthComplete();
    }

    private void AuthorizationFailed(string error)
    {
        Debug.LogError("OnAuthorizationFailed: " + error);
        authFailed.Activate();
    }

    public void Deactivate() {
        Debug.Log("Deactivating signup modal...");

        assets.audioClick.Play();
		background.DOFade(0f, 0.8f).OnComplete(_Deactivate);
		window.DOLocalMoveY(-1200, 0.8f).SetEase(Ease.InBack);

        if (onDismissed != null) onDismissed();

    }

	public void _Deactivate() { gameObject.SetActive(false); }

}