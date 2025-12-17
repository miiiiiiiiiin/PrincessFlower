using UnityEngine;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    [SerializeField]
    private RectTransform ClockHands;
    private Vector3 rot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rot = ClockHands.eulerAngles;
        //rot.z -= Time.deltaTime * 50f;//360도가 됨

    }

    // Update is called once per frame
    void Update()
    {
        //z가 -1인데 euler로 계산하면 360도임 
        //360에서 180까지 가는거
        if (rot.z > 180f)
        {
            rot.z -= Time.deltaTime *2f;// 180도/2 = 90초(1분30초)
            ClockHands.eulerAngles = rot; // 반드시 다시 할당해야 적용됨
        }
        if(rot.z <= 180f){
            AudioManager.Instance.FeverTime.Stop();
            AudioManager.Instance.SpotLight.enabled = false;
            if(GameManager.Instance.TotalScore > 1999){
                SceneManager.LoadScene("GoodEndingScene");
            }else{
                SceneManager.LoadScene("BadEndingScene");
                
            }
        }
    }
}
