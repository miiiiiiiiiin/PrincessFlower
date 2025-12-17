using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    private void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
           Destroy(gameObject); 
        }
    }    
    public static AudioManager Instance{
        get{
            if(instance == null) instance = null;
            return instance;         
        }
    }     
//////////////////////////////////////////////////////////////
/// 
    [Header("Epect Sources")]
    public AudioSource WrongAnswer;//개수틀렷을때
    public AudioSource CorrectAnswer;//개수맞앗을때
    public AudioSource FlowerOn;//방향키 맞앗을때
    public AudioSource Select;//시작 버튼 눌럿을때
    public AudioSource BTNMouseon;//버튼위에 마우스 올렷을때
    public AudioSource GoodEnding;//게임 깻을때 환호성
    public AudioSource NotOn;//방향키 잘못눌럿을때





    [Header("Background Sources")]
    public AudioSource MainBackground;
    public AudioSource GameBackground;
    public AudioSource FeverTime;    
    // public AudioSource WrongAnswer;
    // public AudioSource WrongAnswer;
    public Light2D SpotLight;
    // private void Update() {
    //     if(SceneManager.GetActiveScene().name == "GoodEndingScene" || SceneManager.GetActiveScene().name == "BadEndingScene"){
    //         FeverTime.Stop();
    //         SpotLight.enabled = fasle;
    //     }
    // }
    public void SetBackgroundVolume(float value)
    {
        MainBackground.volume = value;
        GameBackground.volume = value;
        FeverTime.volume = value;

    }
    public void SetEpectVolume(float value)
    {
        WrongAnswer.volume = value;
        CorrectAnswer.volume = value;
        Select.volume = value;
        BTNMouseon.volume = value;
        GoodEnding.volume = value;
        FlowerOn.volume = value;
        NotOn.volume = value;


    }
}
