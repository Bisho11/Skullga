using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;




public class SoundButton_Script : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default,_pressed;

    [SerializeField] private AudioClip _compresseClip,_uncompressClip;
    [SerializeField] private AudioSource _source;

    private bool toggle = true;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (toggle)
        {
            _img.sprite = _pressed;
            _source.PlayOneShot(_compresseClip);
            toggle = false;
        }
        else
        { _img.sprite = _default; toggle = true; }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _source.PlayOneShot(_uncompressClip);
    }   

}
