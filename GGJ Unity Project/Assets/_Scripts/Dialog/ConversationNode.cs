using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the Class that interfaces with the ConversationNode JSONs
[System.Serializable]
public struct ConversationNode
{

    private static readonly IReadOnlyDictionary<string, ModifierType> _stringToModifierMap =
        new Dictionary<string, ModifierType>()
    {
            { "sh", ModifierType.shakey },
            { "s", ModifierType.small },
            { "l", ModifierType.large },
    };

    private static readonly IReadOnlyDictionary<string, CharacterType> _stringToCharacterMap =
        new Dictionary<string, CharacterType>()
    {
            { "s", CharacterType.Sapling },
            { "o", CharacterType.Oak },
            { "n", CharacterType.Narrator },
    };

    public enum ModifierType
    {
        none = 0,
        shakey = 1,
        small = 2,
        large = 3
    }

    public enum CharacterType
    {
        None = 0,
        Sapling = 1,
        Oak = 2,
        Narrator = 3
    }

    [System.NonSerialized]
    private CharacterType _character;

    [System.NonSerialized]
    private ModifierType _modifier;

    [System.NonSerialized]
    private string _text;

    public CharacterType Character => _character;
    public ModifierType Modifier => _modifier;
    public string Text => _text;

    /// <summary>
    /// returns a ConverationNode from the string inside the StoryNode
    /// </summary>
    /// <param name="path">Relative to Resources</param>
    /// <returns></returns>
    public static ConversationNode FromStoryNodeString(string storyNodeString)
    {
        ConversationNode ret = default;

        #region Character
        if (_stringToCharacterMap.TryGetValue(storyNodeString[0].ToString().ToLower(), out CharacterType character))
        {
            ret._character = character;
        }
        #endregion

        #region Modifier
        if (storyNodeString[1] == '-')
        {
            string modifierString = System.String.Empty;

            for(int c = 2; c < storyNodeString.Length && storyNodeString[c] != ':'; c++)
            {
                modifierString += storyNodeString[c];
            }

            if(_stringToModifierMap.TryGetValue(modifierString.ToLower(), out ModifierType modifier))
            {
                ret._modifier = modifier;
            }
            else
            {
                Debug.LogWarning("Unable to find Modifier");
            }

        }
        #endregion

        #region Text
        int characterIndex = 0;
        while(characterIndex < storyNodeString.Length && storyNodeString[characterIndex] != ':')
        {
            characterIndex++;
        }
        // skip the collin and following space
        characterIndex += 2;

        ret._text = storyNodeString.Substring(characterIndex);
        #endregion

        return ret;
    }
}
