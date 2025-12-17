using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BotttleScript : MonoBehaviour
{
//꽃병 랜덤으로 나오고 스페이스바 누르면 패스, 옳고그름 변수 만들어서  게임매니저 카운트 조작
//꽃병과 꽃은 빈 게임오브젝트 아래 자식으로 생성, 패스되면 애니메이션 실행되고 자식만 destroy
//랜덤박스 플래그도 이 스크립트에서 실행

    //부모/자식 계층은 gameobject가 아닌 transform 이 관리함!
    [SerializeField]
    private Transform EmptyParent;

    //꽃병 배열 변수
    [SerializeField]
    private GameObject[] bottleOBJ;

    //패스 애니메이션
    [SerializeField]
    private Animator ParentAni;

    //일회용 꽃병
    private GameObject ExBottle;

    private bool isPass = false;

    //눌러야될 갯수 텍스트
    [SerializeField]
    private TMP_Text StaticInt;

    //총점텍스트
    [SerializeField]
    private TMP_Text TotalCount;

    [SerializeField]
    private Animator Sunglass;    

    //병에 담은 꽃 개수
    void Start()
    {
        int rand = Random.Range(0,4);//0~3랜덤정수
        ExBottle = Instantiate(bottleOBJ[rand], EmptyParent);
        RandomNum();
        //ParentAni = GetComponent<Animator>();
        SetOnAniDestination(rand);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isPass && !GameManager.Instance.isRandomBox){
            StartCoroutine(PushBar());
            Debug.Log("몇번되냐.");

        }
        if(GameManager.Instance.SliderCount == 0 && GameManager.Instance.isRandomBox){
            //랜덤박스타임 끝낫을 때//
            GameManager.Instance.isRandomBox = false;
            Sunglass.SetBool("isFever", false);
            AudioManager.Instance.FeverTime.Stop();//피버타임 음악 끝
            AudioManager.Instance.GameBackground.Play();//다시 메인음악 재생
            AudioManager.Instance.SpotLight.enabled = false;//조명 끄기


            StartCoroutine(PushBar());
            Debug.Log("피버끝^^");
        }
        if(GameManager.Instance.ReadyBouncing && !isPass){
            GameManager.Instance.ReadyBouncing = false;
            ExBottle.GetComponent<Animator>().SetTrigger("isCorrect");

        }
    }
//////////담아야 할 꽃 개수 랜덤으로 정하기///////////
    private int num = 0;

    private void RandomNum(){
        if(GameManager.Instance.isRandomBox){
            num = 100;
            StaticInt.text = "HIT!!";
            StaticInt.fontSize = 55f;

        }else{
            num = Random.Range(1, 8);
            StaticInt.text = num.ToString();
            StaticInt.fontSize = 90f;
        }
        

    }
//////////병 종류 맞춰서 꽃 위치 랜덤 정할 수 잇게 트리거 여는 함수///////
    private void SetOnAniDestination(int num){
        
        switch(num){
            case 0:
                GameManager.Instance.isCeramic = true;
                break;
            case 1:
                GameManager.Instance.isGlass = true;
                break;
            case 2:
                GameManager.Instance.isRose = true;
                break;
            case 3:
                GameManager.Instance.isGreen = true;
                break;
            case 4:
                GameManager.Instance.isRandomBox = true;
                break;
            default:
                break;
        }

    }
/////////////스페이스바 누르면 다음으로 넘어가는 스크립트//////////////
////////
    IEnumerator PushBar(){
        //isPass = true일 때는 스페이스바 못누름
        isPass = true;
        GameManager.Instance.Passing = true;
        ParentAni.SetTrigger("Pushbar");
    /////랜덤으로 나온 숫자와 내가 병에 넣은 꽃 개수가 일치할 때(랜덤num100제외)//
        if(num != 100){
            if(num == GameManager.Instance.FlowerCount){
                AudioManager.Instance.CorrectAnswer.Play();
                GameManager.Instance.TotalScore += 10;
                GameManager.Instance.SliderCount += 10;
                TotalCount.text = GameManager.Instance.TotalScore.ToString();
            }
            else{//아닐시 점수 깎기
                AudioManager.Instance.WrongAnswer.Play();
                GameManager.Instance.SliderCount = 0;
                GameManager.Instance.TotalScore -= 5;
                TotalCount.text = GameManager.Instance.TotalScore.ToString();
                //틀렷다는 소리 추가
            }
        }
        
     /////패스 애니메이션 0.3초 실행////
        yield return new WaitForSeconds(0.3f);
    ///////transform이 자식 정보를 알고잇으므로 transform으로 자식을 순회(본체는 못지움)
     ////////병.꽃 삭제(꽃은 localposition으로 지정하니까 지워졋음)///////
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
    ////////꽃병 isbool false로 바꿔서 꽃 위치 초기화///////
        GameManager.Instance.setBottleTriggerFalse();
    //////너무 빨리 생성되지 않게 0.2초 기다리기///////
        yield return new WaitForSeconds(0.2f);
    ////랜덤 꽃꽂이 개수 변경///////
        
////////슬라이더카운터 꽉차면 랜덤박스 레즈게이릿///////
        if(GameManager.Instance.SliderCount > 99){
            ExBottle = Instantiate(bottleOBJ[4], EmptyParent);
            //병 종류에 맞춰서 isbool 열기
            SetOnAniDestination(4);
            Sunglass.SetBool("isFever", true);

            AudioManager.Instance.GameBackground.Stop();
            AudioManager.Instance.FeverTime.Play();
            AudioManager.Instance.SpotLight.enabled = true;
            
        }else{
    /////// //디폴트: 랜덤으로 새로운 보틀 생성///////
            int rand = Random.Range(0,4);//0~3랜덤정수
            ExBottle = Instantiate(bottleOBJ[rand], EmptyParent);
            //병 종류에 맞춰서 isbool 열기
            SetOnAniDestination(rand);
        }
        RandomNum();

        //병 종류에 맞춰서 isbool 열기
        isPass = false;
        GameManager.Instance.Passing = false;
    }
}
