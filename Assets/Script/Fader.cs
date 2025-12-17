using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Fader : MonoBehaviour
{
    public Image game;

    [SerializeField]
    private float alpha = 1.0f;

    [SerializeField]
    private float FadeSpeed = 0.5f;

    void Start()
    {
        StartFade();//페이드인
    }

    public void StartFade(){
        StartCoroutine(FadeIn());
        //메인씬이면 켜기
        if(SceneManager.GetActiveScene().name == "GoodEndingScene"){
            AudioManager.Instance.GoodEnding.Play();//타이틀음악켜기
        }
    }

    public void GameStartFade(){//겜시작
        StartCoroutine(FadeOut("GameScene"));
        StartCoroutine(PlayMusic(AudioManager.Instance.MainBackground, AudioManager.Instance.GameBackground));
    }

    public void GoHomeFade(){//집가기
        StartCoroutine(FadeOut("MainScene"));
        StartCoroutine(PlayMusic(AudioManager.Instance.GameBackground, AudioManager.Instance.MainBackground));        
    }
    
    public void GoRulesFade(){
        StartCoroutine(FadeOut("RulesScene"));
        
    }
    IEnumerator PlayMusic(AudioSource Stopaudio, AudioSource Startaudio){
        //1번매개변수: 멈출음악, 2번매개변수: 틀 음악
        if(Stopaudio.isPlaying){//멈추고 싶은 음악이 틀어져잇으면
            Stopaudio.Stop();
        }
        yield return new WaitForSeconds(0.7f);
        
        if(!Startaudio.isPlaying){//틀고싶은 음악 꺼져잇음녀
            Startaudio.Play();
        }
        
    }
    IEnumerator FadeIn(){//까만게 걷혀지는 함수
        game.raycastTarget = true;
        alpha = 1f;
        while(alpha>0){
            alpha -= FadeSpeed * Time.deltaTime;
            game.color = new Color(0,0,0,alpha);
            yield return null;   
        }
        game.raycastTarget = false;
    }
    IEnumerator FadeOut(string SceneName){//까만게 덮히는 함수
        game.raycastTarget = true;
        //게임매니저 모든 변수 초기화
        GameManager.Instance.setBottleTriggerFalse();
        GameManager.Instance.ResetAllCount();

        alpha = 0f;
        while(alpha<1){
            alpha += FadeSpeed * Time.deltaTime;
            game.color = new Color(0,0,0,alpha);
            yield return null;   
        }
        SceneManager.LoadScene(SceneName);
    }
   //
}
