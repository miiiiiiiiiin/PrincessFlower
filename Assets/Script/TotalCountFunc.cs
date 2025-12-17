using UnityEngine;
using TMPro;

public class TotalCountFunc : MonoBehaviour
{
    private TMP_Text TotalText;
    void Start() {
        TotalText = GetComponent<TMP_Text>();
        TotalText.text = "총점: "+ GameManager.Instance.TotalScore.ToString();
    }
}
