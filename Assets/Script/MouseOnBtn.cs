using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//시작하기 게임방법 버튼에 마우스 올리면 소리나게하는 스크립트
public class MouseOnBtn : MonoBehaviour
{
    public void OnMouseEnter() {
        //유니티 내에서 직접 끌어당겨서 사용
        AudioManager.Instance.BTNMouseon.Play();
    }
    public void SelectBTN() {
        //유니티 내에서 직접 끌어당겨서 사용
        AudioManager.Instance.Select.Play();

    }
}
