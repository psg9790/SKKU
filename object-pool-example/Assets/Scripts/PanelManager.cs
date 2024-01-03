using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelManager : MonoBehaviour
{
    [SerializeField] Panel panelFactory;    // 패널 프리팹
    [SerializeField] Transform canvas;  // 캔버스 transform
    Stack<Panel> panelsStack = new Stack<Panel>();  // 생성한 패널 스택

    MyStack<int> stack = new MyStack<int>();
    private void Start()
    {
        //stack.Pop();

        stack.Push(1);  // 0
        stack.Push(2);  // 1
        stack.Push(3);  // 2
        stack.Push(4);  // 3

        Debug.Log(stack.Pop());
        Debug.Log(stack.Pop());
        Debug.Log(stack.Pop());
        Debug.Log(stack.Pop());

    }

    /// <summary>
    /// Open 버튼 클릭할 시 새로운 패널 생성
    /// </summary>
    public void OnOpenButtonClick()
    {
        int nextNum = panelsStack.Count;    // 스택 개수만큼 번호 설정

        Panel panel = Instantiate(panelFactory, canvas);    // canvas를 부모로 패널 생성
        panel.panelManager = this;
        panel.SetNum(nextNum);  // UI 갱신

        // DoTween 팝업 효과
        panel.gameObject.transform.localScale = Vector3.zero;
        panel.gameObject.transform.DOScale(1, .2f);

        panelsStack.Push(panel);    // 스택에 패널 추가
    }

    /// <summary>
    /// Prev 버튼 클릭할 시 스택에서 가장 위에 있는 패널 삭제
    /// </summary>
    public void OnPrevButtonClick()
    {
        if (panelsStack.Count <= 0)     // 스택에 패널 없으면 스킵
            return;

        Panel panel = panelsStack.Pop();    // 스택에서 패널 뽑고

        // DoTween 종료 효과 (DoScale 완료 후 Destroy 하기 위해 OnComplete 사용)
        panel.gameObject.transform.DOScale(0, 0.15f).OnComplete(() =>
        {
            Destroy(panel.gameObject);  // 삭제
        });

    }
}
