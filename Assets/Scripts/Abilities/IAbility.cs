using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public void GetDirectionToGo(Vector3 clickPosition);
    public void GetNpcFireDirection(Vector3 playerPosition);

}
