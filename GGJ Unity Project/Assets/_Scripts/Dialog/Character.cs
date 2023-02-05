using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class Character : MonoBehaviour
{
    [SerializeField]
    private ConversationNode.CharacterType m_ConversationCharacter;

    private Image _image;

    private bool _isTalking;

    private const float BOUNCESPEED = 0.5f;

    private float _bounceTimer = 0;

    public ConversationNode.CharacterType ConversationCharacter => m_ConversationCharacter;

    private void Awake()
    {
        _image = this.GetComponent<Image>();
        SetTalking(false);
    }

    private void Update()
    {
        if (_isTalking)
        {
            (this.transform as RectTransform).anchoredPosition = new Vector3(0, Mathf.Sin(_bounceTimer * Mathf.PI)) * 15f;
            _bounceTimer += Time.deltaTime * BOUNCESPEED;
        }
    }

    public void SetTalking(bool value)
    {
        if (value)
        {
            _isTalking = true;
            _image.color = Color.HSVToRGB(0, 0, 1);
            this.transform.localScale = Vector3.one * 1.2f;
            _bounceTimer = 0;
        }
        else
        {
            _isTalking = false;
            _image.color = Color.HSVToRGB(0, 0, 0.7f);
            this.transform.localScale = Vector3.one;
        }
    }
}
