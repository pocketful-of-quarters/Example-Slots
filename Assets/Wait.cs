using System;
using System.Collections.Generic;
using Elona.Slot;
using CSFramework;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Purchasing;
using QuartersSDK;
using UnityEngine.UI;
using QuartersSDK;

//[ExecuteInEditMode]
public class Wait : MonoBehaviour
{
    public Elos elos;

    public Transform window;
    public Image background;
    public UISpinner spinner;

    private Elos.Assets assets { get { return elos.assets; } }



    private void CheckBalance () {
        Debug.Log("Checking balance...");

        Quarters.Instance.GetAccountBalance(
            delegate (User.Account.Balance balance) {

                Quarters.Instance.GetAccountReward(delegate (User.Account.Reward reward) {

                    int quartersBalance = ToInt(balance.quarters + reward.rewardAmount);
                    int currentBalance = PlayerPrefs.GetInt("quartersBalance");

                    if (quartersBalance != currentBalance) {
                        Debug.Log("Balance updated. New balance: " + quartersBalance);

                        PlayerPrefs.SetInt("quartersBalance", quartersBalance);
                        spinner.gameObject.SetActive(false);

                        //elos.slot.gameInfo.balance = quartersBalance;
                        //elos.slot.gameInfo.roundBalance = quartersBalance;
                        elos.slot.gameInfo.AddBalance(Math.Abs(quartersBalance - currentBalance));

                        CancelInvoke("CheckBalance");
                    }
                    else {
                        Debug.Log("Balance didn't change: retrying in 10 seconds...");
                        Invoke("CheckBalance", 10f);
                    }


                }, delegate (string rewardError) {
                    Debug.LogError(rewardError);
                });




           
        },
            delegate (string error) {
            Debug.LogError("Cannot load the user balance: " + error);
        });
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        assets.audioClick.Play();
        background.color = new Color(0, 0, 0, 0);
        background.DOFade(0.5f, 1f);
        window.DOLocalMoveY(0, 1f).SetEase(Ease.OutBounce);
        Util.Tween(0.35f, null, () =>
        {
            assets.audioImpact.Play();
            Camera.main.DOShakePosition(1.2f, 6, 12);
        });

        spinner.gameObject.SetActive(true);

        CheckBalance();
    }

    public void Dismiss() {
        Deactivate();
    }

    public void Deactivate() {
		assets.audioClick.Play();
		background.DOFade(0f, 0.8f).OnComplete(_Deactivate);
		window.DOLocalMoveY(-1200, 0.8f).SetEase(Ease.InBack);
	}

	public void _Deactivate() { gameObject.SetActive(false); }

    private int ToInt(long? value)
    {
        if (value.HasValue)
            return int.Parse(string.Format("{0}", value));

        return 0; //the input value was null
    }
}