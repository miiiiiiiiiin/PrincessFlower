using UnityEngine;

public class OptionFunc : MonoBehaviour
{
    /////////////////일시정지 시간 멈추기 함수///////////////////////
    public void StopTime(){// 게임 일시중지하려면 time.timeScale을 1에서 0으로 변경하면 됨.
        Time.timeScale = 0.0f;
    }
    public void StartTime(){// 게임 중지 풀려면 time.timeScale을 0에서 1으로 변경하면 됨.
        Time.timeScale = 1.0f;
    }
}
