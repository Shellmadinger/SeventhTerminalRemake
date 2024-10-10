using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverResize : MonoBehaviour
{
    public float buttonSize;
    //These functions exists for the buttons in the menu, and they're meant to change the size of the button based on a few inputs by the player
    public void OnEnable()
    {
        //I'll highlight this though, as this fixes a bug where repeatedly clicking a button caused it to shrink.
        //I couldn't find a way to reset the button size after clicking the button, so I did this instead
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
