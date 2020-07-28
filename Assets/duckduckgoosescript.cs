using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class duckduckgoosescript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMNeedyModule Needy;

    public Material[] photos;
    public GameObject screen;
    public KMSelectable[] buttons;


    int selectedphoto = -1;


    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;
        Needy = GetComponent<KMNeedyModule>();
        Needy.OnNeedyActivation += OnNeedyActivation;
        Needy.OnNeedyDeactivation += OnNeedyDeactivation;
        Needy.OnTimerExpired += OnTimerExpired;


        foreach (KMSelectable button in buttons) {
            button.OnInteract += delegate () { buttonPress(button); return false; };
        }


        //button.OnInteract += delegate () { PressButton(); return false; };

    }

    // Use this for initialization
    void Start () {
        moduleSolved = true;

	}

	void OnNeedyActivation(){
    moduleSolved = false;
    selectedphoto = UnityEngine.Random.Range(0,30);
    screen.GetComponent<MeshRenderer>().material = photos[selectedphoto];

  }

  void OnNeedyDeactivation(){
    moduleSolved = true;

  }

  void OnTimerExpired(){
    Needy.HandleStrike();
    moduleSolved = true;
  }

void buttonPress(KMSelectable button){
  if (moduleSolved==false){
  if ((button == buttons[0])&&(selectedphoto<10)){

    Needy.HandlePass();
  }
  else if((button == buttons[1])&&(selectedphoto>9 && selectedphoto<20)){
    Needy.HandlePass();
  }
  else if((button == buttons[2])&&(selectedphoto>19)){
    Needy.HandlePass();
  }
  else{
    Needy.HandleStrike();
    Needy.HandlePass();

  }
  }
}}
