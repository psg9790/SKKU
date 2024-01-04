using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TAGO_Cell : MonoBehaviour
{
    [SerializeField] Text arrPlaceText;
    [SerializeField] Text arrTimeText;
    [SerializeField] Text charText;
    [SerializeField] Text depPlaceText;
    [SerializeField] Text depTimeText;
    [SerializeField] Text vehicleText;

    public void SetItem(Item item)
    {
        arrPlaceText.text = item.arrPlaceNm;
        arrTimeText.text = item.arrPlandTime.ToString();
        charText.text = item.charge.ToString();
        depPlaceText.text = item.depPlaceNm;
        depTimeText.text = item.depPlandTime.ToString();
        vehicleText.text = item.vihicleNm;
    }
}
