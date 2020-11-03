using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tells us what faction (team) this object belongs to.
/// This is just an example. Your game could require a more complicated way of deciding
/// on whether an entity is friendly or enemy. This, however, should cover most basic use cases.
/// </summary>
public class BelongsToFaction : MonoBehaviour {
    public enum Faction {
        PLAYER,
        ENEMY
    }

    public Faction faction;
}
