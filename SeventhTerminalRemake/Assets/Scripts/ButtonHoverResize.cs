using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverResize : MonoBehaviour
{
    public float buttonSize;

    public void OnEnable()
    {
        transform.localScale = new Vector2(buttonSize, buttonSize);
    }

    public void OnHoverEnter()
    {
        transform.localScale = new Vector2(transform.localScale.x * 1.1f, transform.localScale.y*1.1f);
    }

    public void OnHoverExit()
    {
        transform.localScale = new Vector2(buttonSize, buttonSize);
    }

    public void OnClick()
    {
        transform.localScale = new Vector2(transform.localScale.x / 1.05f, transform.localScale.y / 1.05f);
    }

    public void PlaySound()
    {
        this.GetComponent<AudioSource>().Play();
    }
}
