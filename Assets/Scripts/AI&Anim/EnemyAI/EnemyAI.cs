using UnityEngine;
using System.Collections.Generic;

public class EnemyAI:ICharacterAI
{
    public EnemyAI(ICharacter character):base(character)
    {
        ChangeAIState(new IdleAIState());
    }
    // ========== 写死的 ============
    public override bool IsFalling()
    {
        return false;
    }
    public override bool IsOnGround()
    {
        return true;
    }
    // ========== end ============

} 