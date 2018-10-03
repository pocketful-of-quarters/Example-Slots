using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UIToolkit.UI;
using DG.Tweening;


public class ModalView : UIView {
 
	public static ModalView instance;
	public delegate void AlertButtonTappedDelegate(string button);
	public AlertButtonTappedDelegate alertButtonDelegate;

	public Text title;
	public Text message;
	public Button buttonPrototype;

	public RectTransform alertRect;

	private AudioSource audioSource;

	private List<Button> buttons = new List<Button>();

	private CanvasGroup alertViewCanvasGroup {
		get {
			return alertRect.GetComponent<CanvasGroup>();
		}
	}


	public override void Awake () {
		base.Awake ();
		buttonPrototype.gameObject.SetActive(false);
		instance = this;

		alertViewCanvasGroup.alpha = 0;
		alertViewCanvasGroup.interactable = false;
		alertViewCanvasGroup.blocksRaycasts = false;
		alertRect.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		ViewWillDissappear();
		SetVisible(false);
		ViewDisappeared();

		audioSource = this.GetComponent<AudioSource> ();
	}

    public bool IsActive()
    {
        return camera.enabled;
    }

	public void ShowAlert(string title, string message, string[] buttonNames, AlertButtonTappedDelegate alertButtonDelegate) {
		this.alertButtonDelegate = alertButtonDelegate;

		Debug.Log("Show alert: " + title);
       

		Clean();

		this.title.text = title;
		this.message.text = message;

		for (int i=0; i<buttonNames.Length; i++) {
			Button copy = Instantiate<Button>(buttonPrototype);
			copy.transform.SetParent(buttonPrototype.transform.parent, false);
			copy.GetComponentInChildren<Text>().text = buttonNames[i];
			copy.gameObject.SetActive(true);


			this.buttons.Add(copy);
		}
			
		alertViewCanvasGroup.alpha = 1f;
		alertViewCanvasGroup.interactable = true;
		alertViewCanvasGroup.blocksRaycasts = true;
		audioSource.Play ();

        alertRect.DOKill();
		alertRect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
			
		ViewWillAppear();
		SetVisible(true);
		ViewAppeared();


	}


	public void Hide(System.Action OnAnimationFinished = null) {

		alertRect.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.1f).OnComplete(delegate() {
			alertViewCanvasGroup.alpha = 0;
			alertViewCanvasGroup.interactable = false;
			alertViewCanvasGroup.blocksRaycasts = false;
			ViewWillDissappear();
			SetVisible(false);
			ViewDisappeared();
			if (OnAnimationFinished != null) OnAnimationFinished();
			alertButtonDelegate = null;
		});
	}


	void Clean() {
		foreach (Button button in buttons) Destroy(button.gameObject);
		this.buttons = new List<Button>();
	}




	public void ButtonTapped(Button button) {

		string buttonText = button.GetComponentInChildren<Text>().text;

		Hide(delegate() {
			alertButtonDelegate(buttonText);
		});

	}



	public override void ViewWillDissappear () {
		base.ViewWillDissappear ();

	}




	public void ShowActivity() {
		ViewWillAppear();
		SetVisible(true);
		ViewAppeared();
	}
		

	public void HideActivity() {

		ViewWillDissappear();
		SetVisible(false);
		ViewDisappeared();
	}

	/*test
	public void TestAlertView() {
		ModalView.instance.ShowAlert("Oops", "You need to register an account or login before you can invite friends to play", new string[] {"Cancel", "OK"}, delegate(string button) {
			if (button == "OK") {
				Debug.Log("OK pressed");
			} else if(button == "Cancel") {
				Debug.Log("Cancel pressed");
			}
		});
	}
	*/
}