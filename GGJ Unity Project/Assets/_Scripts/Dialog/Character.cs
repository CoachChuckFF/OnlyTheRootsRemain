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

    public ConversationNode.CharacterType ConversationCharacter => m_ConversationCharacter;

    private void Awake()
    {
        _image = this.GetComponent<Image>();
        SetTalking(false);
    }


    public void SetTalking(bool value)
    {
        if (value)
        {
            _image.color = Color.HSVToRGB(0, 0, 1);
            this.transform.localScale = Vector3.one * 1.2f;
        }
        else
        {
            _image.color = Color.HSVToRGB(0, 0, 0.7f);
            this.transform.localScale = Vector3.one;
        }
    }
}
