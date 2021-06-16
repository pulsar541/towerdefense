using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    SceneController _sceneController;
    TimeRewindController _timeRewindController; 
    GameObject _sliderGO;
    Slider _slider;

    GameObject buttonPause;
    GameObject buttonResume;
    GameObject buttonRestart;
    void Awake()
    {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        _timeRewindController = _sceneController.GetComponent<TimeRewindController>(); 
        _sliderGO = FindObject(transform.parent.gameObject, "SliderRewindTime");

        _slider = _sliderGO.GetComponent<Slider>();

        buttonPause = FindObject(transform.parent.gameObject, "ButtonPause");
        buttonResume = FindObject(transform.parent.gameObject, "ButtonResume");
        buttonRestart = FindObject(transform.parent.gameObject, "ButtonRestart");  
    }
  
    public static GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs= parent.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs){
            if(t.name == name){
                return t.gameObject;
            }
        }
        return null;
    }

    public void OnButtonPause() { 
        _sceneController.SetPause();  

        _sliderGO.SetActive(true);
        buttonResume.SetActive(true);

        _slider.minValue = 0; 
        _slider.maxValue = _timeRewindController.GameTime - 1; 
        _slider.value = _slider.maxValue; 

        buttonPause.SetActive(false);    
        buttonRestart.SetActive(false);   
    } 

    public void OnButtonResume() { 
        _sceneController.Resume(); 
      
        buttonPause.SetActive(true);
        buttonRestart.SetActive(true);

        _slider.minValue = 0;  
        _timeRewindController.ClearHistory((int)_slider.value, (int)_slider.maxValue);

        buttonResume.SetActive(false); 
         _sliderGO.SetActive(false);

    } 

    public void OnButtonRestart() {
        _sceneController.Resume();
         _timeRewindController.RestoreScene(0);
         _timeRewindController.ClearHistory(0, (int)_slider.maxValue);
    } 

    public void OnSliderRewindTime() {
        _timeRewindController.RestoreScene((int)_slider.value);
    }

}
