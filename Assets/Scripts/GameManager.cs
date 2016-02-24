﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static bool lost;
	public static bool win;
	private bool slowMotion;

	void Start(){
		lost = false;
		win = false;
		slowMotion = true;
	}

	void Update(){
		CheckLost();
		CheckWin();
	}

	public void CheckLost(){
		if (lost) {
			UIManager.displayLost = true;
		}
	}

	public void CheckWin(){
		//Checks if the boss health is 0 -- for alpha
		if(win){
			UIManager.displayWin = true;
		}
	}
}
