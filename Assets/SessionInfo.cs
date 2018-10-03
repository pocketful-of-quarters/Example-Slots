using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuartersSDK;

public class SessionInfo : MonoBehaviour {

    public Text title;
    public Text userId;

    public void UpdateUI () {
        if (Quarters.Instance.IsAuthorized && !Quarters.Instance.session.IsGuestSession) {
            title.text = "Connected to Quarters";
        } else {
            title.text = "You're playing as guest.";
        }
    }

    public void Start() {
        UpdateUI();
    }

    public void Logout() {
        Quarters.Instance.Deauthorize();
        UpdateUI();
    }
}
