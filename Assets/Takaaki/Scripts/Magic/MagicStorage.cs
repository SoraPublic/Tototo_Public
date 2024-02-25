using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicStorage", menuName = "Create MagicStorage")]
public class MagicStorage : ScriptableObject
{
    public List<MagicEntity> MagicList;
}
