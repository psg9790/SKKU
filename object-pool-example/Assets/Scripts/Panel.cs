using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    [SerializeField] TMP_Text text;     // 번호 표시용
    [HideInInspector] public PanelManager panelManager;

    public void SetNum(int v_num)   // 번호 표시용 함수
    {
        text.text = v_num.ToString();

        // 랜덤 색
        GetComponent<Image>().color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f), 1);
    }

    public void OnPrevButtonClick()
    {
        panelManager?.OnPrevButtonClick();
    }

    public void OnOpenButtonClick()
    {
        panelManager?.OnOpenButtonClick();
    }
}
