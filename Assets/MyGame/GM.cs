using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using QuartersSDK;
using PlayFab;
using PlayFab.ClientModels;
using Elona.Slot;

public class GM : MonoBehaviour
{

    //public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public SessionInfo sessionInfo;
    public Elos elos;

    ////Awake is always called before any Start functions
    //void Awake()
    //{
    //    //Check if instance already exists
    //    if (instance == null)

    //        //if not, set instance to this
    //        instance = this;

    //    //If instance already exists and it's not this:
    //    else if (instance != this)

    //        //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
    //        Destroy(gameObject);

    //    //Sets this to not be destroyed when reloading scene
    //    DontDestroyOnLoad(gameObject);
    //}

    void Start()
    {
        //PlayerPrefs.DeleteKey("QuartersRefreshToken");

    }

    public void AuthComplete () {
        GetUserDetails();
        sessionInfo.UpdateUI();
    }

    public void DeauthComplete () {
        sessionInfo.UpdateUI();
    }

    private void GetUserDetails()
    {
        Quarters.Instance.GetUserDetails(delegate (User user) {
            Debug.Log("User loaded");

            UpdateBalance();
        }, delegate (string error) {
            Debug.LogError("Cannot load the user details: " + error);

        });
    }

    private void UpdateBalance()
    {
        Quarters.Instance.GetAccountBalance(
            delegate (User.Account.Balance balance) {


                Quarters.Instance.GetAccountReward(delegate (User.Account.Reward reward) {

                    int quartersBalance = ToInt(Quarters.Instance.CurrentUser.accounts[0].AvailableQuarters);

                    PlayerPrefs.SetInt("quartersBalance", quartersBalance);
                    Debug.Log("Quarters balance: " + quartersBalance);

                    //elos.slot.gameInfo.balance = quartersBalance;
                    //elos.slot.gameInfo.roundBalance = quartersBalance;
                    elos.slot.gameInfo.SetBalance(quartersBalance);



                }, delegate (string rewardError) {
                    Debug.LogError(rewardError);
                });



               




            },
            delegate (string error) {
                Debug.LogError("Cannot load the user balance: " + error);
            }
        );
    }

    private int ToInt(long? value)
    {
        if (value.HasValue)
            return int.Parse(string.Format("{0}", value));

        return 0; //the input value was null
    }
}