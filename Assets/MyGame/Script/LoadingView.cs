using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UIToolkit.UI;
using System;

public class LoadingView : UIView {


    public static LoadingView instance;


    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public void Show() {

        ViewWillAppear();
        SetVisible(true);
        ViewAppeared();
    }


    public void Hide() {

        ViewWillDissappear();
        SetVisible(false);
        ViewDisappeared();

    }

}