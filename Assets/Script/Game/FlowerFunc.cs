using UnityEngine;

public class FlowerFunc : MonoBehaviour
{
    private Vector3 offset;

    void OnEnable()
    {
        // 랜덤 목표 offset 미리 정해두기
        offset = new Vector2(Random.Range(-1.6f, -0.9f), 0.72f);
    }
    void Start()
    {
         if(GameManager.Instance.isRose){
            offset = new Vector2(Random.Range(-1.6f, -0.9f), 0.72f);
            Debug.Log("장미 끝! 랜덤 위치 이동");
        }else if(GameManager.Instance.isGreen){
            offset = new Vector3(Random.Range(-1.75f, -0.95f), 0.2f);
            Debug.Log("청록 끝! 랜덤 위치 이동");
        }else if(GameManager.Instance.isGlass){
            offset = new Vector3(Random.Range(-1.7f, -0.8f), 0.2f);
            Debug.Log("유리 끝! 랜덤 위치 이동");
        }else if(GameManager.Instance.isCeramic){
            offset = new Vector3(Random.Range(-2f, -0.7f), 0.3f);
            Debug.Log("세라믹 끝! 랜덤 위치 이동");
        }else if(GameManager.Instance.isRandomBox){
            offset = new Vector3(Random.Range(-3.2f, 0.75f), 0.3f);
            Debug.Log("랜박 끝! 랜덤 위치 이동");
        }  
    }

    void LateUpdate()
    {
        // 애니메이션이 끝나갈수록 랜덤 offset 적용
        Animator anim = GetComponent<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        if (state.normalizedTime > 0.7f)
        {
            transform.localPosition = offset;
            
        }
    }
    public void FlowerDestination(){
        Debug.Log("현재 변수상태" + GameManager.Instance.isRose + GameManager.Instance.isGreen+ transform.position);
        if(GameManager.Instance.isRose){
            // Animator anim = GetComponent<Animator>();
            // anim.enabled = false; // Animator 끄기
            transform.position = new Vector2(Random.Range(-1.6f, -0.9f), 0.72f);
            //transform.position = randPos;
            Debug.Log("장미 끝! 랜덤 위치 이동");
        }else if(GameManager.Instance.isGreen){
            Vector3 randPos = new Vector3(Random.Range(-1.75f, -0.95f), 0.5f);
            transform.position = randPos;
            Debug.Log("청록 끝! 랜덤 위치 이동");
        }else if(GameManager.Instance.isGlass){
            Vector3 randPos = new Vector3(Random.Range(-1.7f, -0.8f), 0.5f);
            transform.position = randPos;
            Debug.Log("유리 끝! 랜덤 위치 이동");
        }else if(GameManager.Instance.isCeramic){
            Vector3 randPos = new Vector3(Random.Range(-2f, -0.7f), 0.3f);
            transform.position = randPos;
            Debug.Log("세라믹 끝! 랜덤 위치 이동");
        }else if(GameManager.Instance.isRandomBox){
            Vector3 randPos = new Vector3(Random.Range(-3.2f, 0.75f), 0.3f);
            transform.position = randPos;
            Debug.Log("랜박 끝! 랜덤 위치 이동");
        }
    }
}
