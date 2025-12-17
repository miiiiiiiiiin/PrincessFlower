using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//이 스크립트는 꽃에 삽입되는 스크립트임
public class FlowerGame : MonoBehaviour
{
    //방향키 눌렷다는 트리거 열리면 꽃 애니 생성(꽃병도 든 빈 오브젝트에 생성 )
    //private AudioSource FlowerAudio;
    //프리팹 꽃 담는 배열변수
    [SerializeField]
    private GameObject[] FlowersObject;
    //빈부모오브젝트
    [SerializeField]
    private Transform EmptyBox;
    //일회용 꽃(보틀 스크립트에서 사라짐)
    private List<GameObject> OnehandFlowers = new List<GameObject>();
    void Start(){
        //꽃 오브젝트에서 오디오 소스 가져오기
        //FlowerAudio = GetComponent<AudioSource>();
        //머시깽이<>는 벡터나 대신 쓰는 템플릿 함수라고 알고잇는데

    }
    void Update()
    {
        if(GameManager.Instance.isCorrect){
            //꽃 랜덤 생성(하나만 생성돼야함)
            StartCoroutine(Move());
        }
        
        
    }
    IEnumerator Move(){
        //isCorrect 바로 꺼서 업데이트 방지
        GameManager.Instance.isCorrect = false;
        //꽃 움직일 때 코루틴의 지연을 사용해 개빠른 void 업데이트 방지(0.2초)
        //isMoving이 false상태일때만 움직이게 하기
        //isMoving = true;
        AudioManager.Instance.FlowerOn.Play();
        //랜덤 꽃 생성
        int rand = Random.Range(0,4);
        OnehandFlowers.Add(Instantiate(FlowersObject[rand], EmptyBox));
        GameManager.Instance.FlowerCount++;

        yield return new WaitForSeconds(0.1f);
        

    }

    
}
