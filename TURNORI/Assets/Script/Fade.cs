using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public void FadeIn(float fadeOutTime)
    {
        StartCoroutine(CoFadeIn(fadeOutTime));
    }

    public void FadeOut(float fadeOutTime)
    {
        StartCoroutine(CoFadeOut(fadeOutTime));
    }
    public void FadeIn2(float fadeOutTime)
    {
        StartCoroutine(SrFadeIn(fadeOutTime));
    }

    public void FadeOut2(float fadeOutTime)
    {
        StartCoroutine(SrFadeOut(fadeOutTime));
    }
    //투명->불투명
    IEnumerator CoFadeIn(float fadeOutTime)
    {
        Image im = GetComponent<Image>();
        Color tempColor = im.color;

        while(tempColor.a<1f)
        {
            tempColor.a += Time.deltaTime / fadeOutTime;
            im.color = tempColor;

            if (tempColor.a >= 1f) tempColor.a = 1f;
            yield return null;
        }
        im.color = tempColor;
    }

    //불투명 ->투명  
    IEnumerator CoFadeOut(float fadeOutTime)
    {
        Image im = GetComponent<Image>();
        Color tempColor = im.color;

        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeOutTime;
            im.color = tempColor;

            if (tempColor.a <= 0f) tempColor.a = 0f;
            yield return null;
        }
        im.color = tempColor;
    }


    //투명->불투명
    IEnumerator SrFadeIn(float fadeOutTime)
    {
        SpriteRenderer im = GetComponent<SpriteRenderer>();
        Color tempColor = im.color;

        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeOutTime;
            im.color = tempColor;

            if (tempColor.a >= 1f) tempColor.a = 1f;
            yield return null;
        }
        im.color = tempColor;
    }

    //불투명 ->투명  
    IEnumerator SrFadeOut(float fadeOutTime)
    {
        SpriteRenderer im = GetComponent<SpriteRenderer>();
        Color tempColor = im.color;

        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeOutTime;
            im.color = tempColor;

            if (tempColor.a <= 0f) tempColor.a = 0f;
            yield return null;
        }
        im.color = tempColor;
    }


}
