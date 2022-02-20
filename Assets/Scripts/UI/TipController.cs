using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class TipController : MonoBehaviour
{
    [SerializeField] string text;
    [SerializeField] GameObject popUp;
    [SerializeField] Text textLine;
    [SerializeField] Text timer;
    [SerializeField] float popUpTime = 10f;

    private float _time;
    private bool _alreadyShown=false;
    private int _tipOption;

    private void Start()
    {
        _tipOption = PlayerPrefs.GetInt("TipShow");
        Debug.Log("Tips = " + _tipOption);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_alreadyShown && _tipOption==1)
        {
            StartCoroutine(showPopup());
            _alreadyShown=true;
        }
    }
    IEnumerator showPopup()
    {
        textLine.text = text;
        popUp.SetActive(true);
        for(_time = popUpTime; _time>0; _time--)
        {
            timer.text = _time.ToString();
            yield return new WaitForSeconds(1);
        }

        popUp.SetActive(false);
    }
}
