using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Input_IP : MonoBehaviour {

  //オブジェクトと結びつける
  public TMP_InputField inputField;
  public static string IP = "172.0.0.0";

  void Start () {
    //Componentを扱えるようにする
    inputField = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();

    }

    public void InputText(){
                //テキストにinputFieldの内容を反映
        IP = inputField.text;
        PlayerPrefs.SetString("IP", IP);
        PlayerPrefs.Save();
        SceneManager.LoadScene("pause");



     }

}