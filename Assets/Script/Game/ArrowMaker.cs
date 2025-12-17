using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//화살표 생성, 공주 표정 관리
public class ArrowMaker : MonoBehaviour
{
    //private AudioSource Fail;
    //화살표 모음 배열0~3 총 4개 
    [SerializeField]
    private GameObject[] Arrows;

    //생성 좌표 배열(왼->오) 0~3 총4개
    [SerializeField]
    private RectTransform[] coordinate;
    [SerializeField]
    private TMP_Text TotalCount;

    //생성한 화살표 조작할 수 있게 이름 붙이기
    private GameObject[] ArrowNaming = new GameObject[4];
    [SerializeField]
    private RectTransform EmptyObj;
    private int num;

    /// <summary>
    /// 캐릭터 애니메이션
    /// </summary>
    [SerializeField]
    private Animator Princess;

//////////맨 처음 초기화/////////////////
    void Start()
    {
        FirstRange();
    }
    void Update()
    {
        if(!GameManager.Instance.isCorrect){
            if(!GameManager.Instance.isRandomBox){
                //랜덤박스 아니고 평상시
                if(!isFailed && !GameManager.Instance.Passing) isCorrect2();
            }else isCorrect3();//랜덤박스일때
        }
        
    }
    private void FirstRange(){  
        //맨처음: 모든 자리에 화살표 랜덤 생성
        for(int i = 0;i<4;i++){
            num = Random.Range(0,4);//0~3까지 랜덤 정수
            ArrowNaming[i] = Instantiate(Arrows[num],EmptyObj);
            ArrowNaming[i].transform.position = coordinate[i].position;

        }
    }

///// 인덱스 관리 변수와 함수 //////
    private int FirstNum = 0;
    private int SecondNum = 1;
    private int ThirdNum = 2;
    private int FourthNum = 3;

    private void IndexManager(){
        FirstNum++; SecondNum++; ThirdNum++; FourthNum++;
        if(FourthNum > 3) FourthNum = 0;
        else if(ThirdNum > 3) ThirdNum = 0;
        else if(SecondNum > 3) SecondNum = 0;
        else if(FirstNum > 3) FirstNum = 0;
    }

//////////*방향키 정답일 때 실행되는 코루틴함수*///////
/////나머지 화살표 좌표 이동, 맨 오른쪽 랜덤 생성
    IEnumerator Generator(){
        isFailed =true;
        //누른 방향키 없애기
        //FirstNum = SecondNum; SecondNum = THirdNum; ThirdNum = FourthNum; Destroy(FourthNum); 
        Destroy(ArrowNaming[FirstNum]);
        //다음 방향키 오브젝트 이동
        ArrowNaming[SecondNum].transform.position = coordinate[0].position;//1
        ArrowNaming[ThirdNum].transform.position = coordinate[1].position;//2
        ArrowNaming[FourthNum].transform.position = coordinate[2].position;//3
        //맨 끝에 새 화살표 생성
        num = Random.Range(0,4);//0~3까지 랜덤 정수
        ArrowNaming[FirstNum] = Instantiate(Arrows[num], EmptyObj);
        ArrowNaming[FirstNum].transform.position = coordinate[3].position;
        //Princess.SetTrigger("isCorrect");
        //인덱스 업데이트, 각각의 인덱스는 위치가 0이 될 때 다음 루틴에서 사라짐 
        IndexManager();
        //꽃 이동하는 시간 0.2초 쉬기
        yield return new WaitForSeconds(0.1f);
        isFailed =false;
    }
    private bool isFailed = false;
///////틀렸을 떄 실행되는 코루틴///////
    IEnumerator Fails(){
        //틀린횟수 업데이트
        //연속으로 틀려서 꽃 생성 렉걸리지 않게 isFailed로 지연
        isFailed = true;
        //방향키 틀리면 1점씩깎기
        GameManager.Instance.TotalScore--;
        Princess.SetTrigger("isFail");
        TotalCount.text = GameManager.Instance.TotalScore.ToString();
        Debug.Log("틀렷슴");
        //정답이 아닐 경우 점수나 게이지 깎는 코드 짜기
        AudioManager.Instance.NotOn.Play();
        //틀림 음악 넣기
        yield return new WaitForSeconds(0.25f);
        isFailed = false;
    }
    
    Dictionary<KeyCode, string> keyTagMap = new Dictionary<KeyCode, string>()
    {
        { KeyCode.UpArrow,    "up" },
        { KeyCode.DownArrow,  "down" },
        { KeyCode.LeftArrow,  "left" },
        { KeyCode.RightArrow, "right" }
    };
    private void isCorrect2(){
        foreach(var i in keyTagMap){
            //getKey는 눌러지고 잇는 경우에도 포함될 수 잇으므로
            //눌러진 순간만 감지하는 getkeyDown을 사용해야 플래그 안세우고 구현할 수 잇고 좋음
            if(Input.GetKeyDown(i.Key)){
                //태그 맞았을 경우
                if(ArrowNaming[FirstNum].tag == i.Value){
                    GameManager.Instance.ReadyBouncing = true;
                    GameManager.Instance.isCorrect = true;
                    Princess.SetTrigger("isCorrect");

                    //gameManager 플래그 세우기
                    //SetDirectionFlag(i.Key, true);
                    //화살표 업데이트
                    StartCoroutine(Generator());
                }
                //눌럿는데 틀렸을 경우
                else{
                    if(!isFailed){
                        StartCoroutine(Fails());
                    }
                    
                }
            }
        }
    }
    private void isCorrect3(){
        foreach(var i in keyTagMap){
            //getKey는 눌러지고 잇는 경우에도 포함될 수 잇으므로
            //눌러진 순간만 감지하는 getkeyDown을 사용해야 플래그 안세우고 구현할 수 잇고 좋음
            if(Input.GetKeyDown(i.Key)){
                GameManager.Instance.isCorrect = true;
                //Princess.SetTrigger("isCorrect");

                StartCoroutine(Generator());
                GameManager.Instance.TotalScore += 5;
                TotalCount.text = GameManager.Instance.TotalScore.ToString();
            }
        }
    }
}

    //스페이스바를 누르는게 양옆이 막혓을때만 가능하게 만들어야할지 항시 가능하게 만들어야할지 고민
    /* if else문으로 정답, 오답 체크 만들려고 햇는데 오답 코드를 짜는게 쉽지않앗음(비효율적이어서)
    그래서 딕셔너리문과 for each 문으로 효율적인 코드를 다시 작성함
    private void isCorrect(){
        //누른 방향키와 화살표 이미지의 태그가 맞았을 때 실행되는 코드+예외
        if(Input.GetKey(KeyCode.UpArrow) && ArrowNaming[FirstNum].tag == "up"){
            Debug.Log("업");
            GameManager.Instance.isUp = true;
            //isMoving은 방향키 눌렀을때 true로 바뀌고 0.2초 뒤에 false로 바뀜 그 사이를 노리기
            if(GameManager.Instance.isMoving == true && !isPoint){
                StartCoroutine(Generator());
            }
            //양옆이 닿을 상황이면 스페이스바 눌러서 바꿔야함(얘는 점수에 영향 없는 코드긔)
        }//다운
        else if(Input.GetKey(KeyCode.DownArrow) && ArrowNaming[FirstNum].tag == "down"){
            Debug.Log("다운");
            GameManager.Instance.isDown = true;
            if(GameManager.Instance.isMoving == true && !isPoint){
                StartCoroutine(Generator());
            }
        }//우측
        else if(Input.GetKey(KeyCode.RightArrow) && ArrowNaming[FirstNum].tag == "right"){
            Debug.Log("오른쪽");
            GameManager.Instance.isRight = true;
            if(GameManager.Instance.isMoving == true && !isPoint){
                StartCoroutine(Generator());
            }
        }//좌측
        else if(Input.GetKey(KeyCode.LeftArrow) && ArrowNaming[FirstNum].tag == "left"){
            Debug.Log("왼쪽");
            GameManager.Instance.isLeft = true;
            if(GameManager.Instance.isMoving == true && !isPoint){
                StartCoroutine(Generator());
            }
        }
        else if(Input.GetKey(KeyCode.LeftArrow) && ArrowNaming[FirstNum].tag != "up" || Input.GetKey(KeyCode.UpArrow) || 
                Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow)){
            Debug.Log("틀렷슴");
            //정답이 아닐 경우 점수나 게이지 깎는 코드 짜기
            Fail.Play();
            //틀림 음악 넣기
        }
    }
*/
    