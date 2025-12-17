using UnityEngine;
using UnityEngine.UI;

public class VolumOption : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = GameManager.Instance.KeepBGMVolumSize;//예전에 업데이트햇던 볼륨값 넣어놓기
        slider.onValueChanged.AddListener(SetVolume);//매개변수 안넣어도 자동으로 value값 들어감
    }

    void SetVolume(float value){
 // AudioManager 싱글톤에서 볼륨 조절
        AudioManager.Instance.SetBackgroundVolume(value);
        GameManager.Instance.KeepBGMVolumSize = value;//업데이트된 볼륨값 게임매니저에 일단 저장

    }
    // void SetVolume(float value)
    // {
    //     // AudioManager 싱글톤에서 볼륨 조절
    //     AudioManager.Instance.SetBackgroundVolume(value);
    // }
}
