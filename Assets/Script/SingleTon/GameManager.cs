using UnityEngine;
//싱글톤
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
           Destroy(gameObject); 
        }
    }    
    public static GameManager Instance{
        get{
            if(instance == null) instance = null;
            return instance;         
        }
    }     

///////////////////////////////////////////////////////////////////////
/// 
/// 맞앗나 틀렷나 확인 트리거
    public bool isCorrect = false;

///////방향키 맞았을 때 꽃병 튕기는 애니메이션 실행만을 위한 변수///////
    public bool ReadyBouncing = false;
/// <summary>
/// //패스중일때 방향키 누르지 못하게 하는 트리거 변수
/// </summary>
    public bool Passing = false;

    //꽃병 종류 트리거
    public bool isRose = false;
    public bool isGreen = false;
    public bool isGlass = false;
    public bool isCeramic = false;
    public bool isRandomBox = false;

    //패스할때마다 초기화되는 꽃 카운트
    public int FlowerCount = 0;

    //총점수
    public int TotalScore = 0;

    //피버타임 슬라이더 value
    public int SliderCount = 0;
    public float KeepBGMVolumSize = 1;
    public float KeepEPVolumSize = 1;


//꽃병 종류 초기화
    public void setBottleTriggerFalse(){
        isRose = false;
        isGreen = false;
        isGlass = false;
        isCeramic = false;
        isRandomBox = false;
        FlowerCount = 0;
    }
//점수 초기화
    public void ResetAllCount(){
        FlowerCount = 0;
        TotalScore = 0;
        SliderCount = 0;
        Passing = false;
        isCorrect = false;
        ReadyBouncing = false;

    }
    
}
