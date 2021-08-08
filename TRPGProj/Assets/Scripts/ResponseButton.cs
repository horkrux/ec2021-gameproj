using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResponseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        startColor = gameObject.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnMouseEnter()
    //{
    //    System.Action<ITween<Color>> updateColor = (t) =>
    //    {
    //        gameObject.GetComponent<Image>().color = t.CurrentValue;
    //    };

    //    Color endColor = new Color(1.0f, 1.0f, 0);

    //    // completion defaults to null if not passed in
    //    gameObject.Tween("Color", startColor, endColor, 1.0f, TweenScaleFunctions.QuadraticEaseOut, updateColor);
    //}

    //TODO: this doesn't really work
    public void OnPointerEnter(PointerEventData eventData)
    {
        System.Action<ITween<Color>> updateColor = (t) =>
        {
            gameObject.GetComponent<Image>().color = t.CurrentValue;
        };

        Color endColor = new Color(1.0f, 1.0f, 0);

        // completion defaults to null if not passed in
        gameObject.Tween("ColorEnter", startColor, endColor, 1.0f, TweenScaleFunctions.QuadraticEaseOut, updateColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        System.Action<ITween<Color>> updateColor = (t) =>
        {
            gameObject.GetComponent<Image>().color = t.CurrentValue;
        };

        // completion defaults to null if not passed in
        gameObject.Tween("ColorExit", gameObject.GetComponent<Image>().color, startColor, 1.0f, TweenScaleFunctions.QuadraticEaseOut, updateColor);
    }
}
