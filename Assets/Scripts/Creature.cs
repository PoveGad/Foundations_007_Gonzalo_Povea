using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public enum Team
    {
        player=0,
        Enemy=1,
        Mine=2,
    }
    public Transform head;
    public Team team;

}