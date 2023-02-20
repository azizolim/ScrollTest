using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScrollRectController : MonoBehaviour
{
    [SerializeField] private RectTransform scrollPanel;
    [SerializeField] private Button[] buttons;
    [SerializeField] private RectTransform centerToCompare;
    [SerializeField] private float[] distance;
    [SerializeField] private float[] distanceReposition;
    
    
    private bool _dragging;
    private int _buttonDistance;
    private int _minButtonNumber;


    private void Start()
    {
        int buttonLenght = buttons.Length;
        distanceReposition = new float[buttonLenght];
        distance = new float[buttonLenght];

        _buttonDistance = (int)Mathf.Abs(buttons[1].GetComponent<RectTransform>().anchoredPosition.x -
                                         buttons[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    private void Update()
    {
        
        for (int i = 0; i < buttons.Length; i++)
        {
            distance[i] = Mathf.Abs(centerToCompare.transform.position.x - buttons[i].transform.position.x);
        }

        float minDistance = Mathf.Min(distance);

        for (int a = 0; a < buttons.Length; a++)
        {
            if (minDistance == distance[a] )
            {
                _minButtonNumber = a;
            }
        }

        if (!_dragging)
        {
            LerpToButton(_minButtonNumber * -_buttonDistance);
        }
    }

    private void LerpToButton(int position)
    {
        float newX = Mathf.Lerp(scrollPanel.anchoredPosition.x, position, Time.deltaTime * 5f);
        Vector2 newPosition = new Vector2(newX, scrollPanel.anchoredPosition.y);

        scrollPanel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        _dragging = true;
    }

    public void EndDrag()
    {
        _dragging = false;
    }
}
