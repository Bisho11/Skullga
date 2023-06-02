using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class Clicked : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default,_pressed;
    [SerializeField] private AudioClip _compresseClip,_uncompressClip;
    [SerializeField] private AudioSource _source;




    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
        _source.PlayOneShot(_compresseClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
        _source.PlayOneShot(_uncompressClip);
    }   

    public void Chilling() {
        Debug.Log("chilling");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Exit() {
        Application.Quit();
    }
}
