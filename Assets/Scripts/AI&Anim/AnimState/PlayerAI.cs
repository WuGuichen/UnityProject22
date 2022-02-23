using UnityEngine;
using System.Collections.Generic;

public class PlayerAI:ICharacterAI
{
    public PlayerAI(ICharacter character):base(character)
    {
        ChangeAIState(new IdleAIState());
    }
} 