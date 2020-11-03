using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An AI script which handles:
///  - vision and vision ranges
///  - wandering around aimlessly
///  - following the player
///
/// Note that AI is a pretty complicated topic, and there are many, many ways to do the same
/// thing differently with various benefits and drawbacks to each. This is just a straightforward
/// way that i immediately have in mind, but if i were making a proper game, i would probably look
/// for some better approaches - a state machine, perhaps
/// </summary>
public class EnemyAI : MonoBehaviour {

    [SerializeField]
    float visionRange;
    [SerializeField]
    LayerMask visionLayerMask;
    
    IEnumerator currentCoroutine;

    GameObject target;
    Vector3 moveTo;

    [SerializeField]
    EntityMovement controlledEntity;

    void Start() {
        StartCoroutine(SeekTarget());
        StartCoroutine(DropTargetIfOutOfRange());
    }

    void Update() {
        if (target != null) {
            // we want to move to half the vision range away from the target
            // of course this is very simplistic; only an example
            // this could make some sense for an AI that can shoot bullets at the players from a distance
            var targetPosition = target.transform.position;
            var ourPosition = gameObject.transform.position;
            var fromTargetToUsVector = (ourPosition - targetPosition);
            moveTo = targetPosition + fromTargetToUsVector.normalized * (visionRange / 2f);
        } else {
            // if we've reached our wandering waypoint
            if( (moveTo - transform.position).magnitude < 1f ) {
                // then pick a random point in the 100 meters radius from map center as our waypoint
                moveTo = Random.insideUnitSphere * 100f;
                moveTo.y = 0f;
            }
        }

        // and then, it's surprisingly simple
        controlledEntity.input = (moveTo - transform.position).normalized;
    }

    // these are coroutines. coroutines are very, very awesome. 
    // coroutines can suspend their execution. not that many programming languages support them,
    // which is unfortunate. JS, Kotlin, C# and perhaps a few more.
    // doing the following via coroutines could in theory help performance 
    // (checking vision range every tick could get a bit expensive with many entities)
    // but i'm writing coroutines here mainly to just provide an example of their usage
    IEnumerator DropTargetIfOutOfRange() {
        while(true) {
            if(
                target != null && // if we have a target and
                // it's now out of our vision range
                (target.transform.position - gameObject.transform.position).magnitude > visionRange 
            ) {
                target = null; // then forget about it
            }

            // don't remove the following line, or your Unity3D will freeze :)
            // this line suspends the execution of this function for several seconds
            // everything else in the game proceeds as usual, but this piece of code will wait.
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SeekTarget() {
        while(true) {
            if(target == null) {
                // grab all objects in an area
                var nearbyObjects = Physics.OverlapSphere(transform.position, visionRange, visionLayerMask.value);

                // check every object to see if it has a BelongsToFaction component
                var potentialTargets = new HashSet<GameObject>();
                foreach(var obj in nearbyObjects) {
                    var belongsToFaction = obj.GetComponentInParent<BelongsToFaction>();
                    // check the faction. we want to find a player we could follow
                    if(belongsToFaction?.faction == BelongsToFaction.Faction.PLAYER) {
                        potentialTargets.Add(belongsToFaction.gameObject);
                    }
                }

                
                if(potentialTargets.Count > 0) {
                    // we found one or more players. pick one at random
                    var randomIndex = Random.Range(0, potentialTargets.Count);
                    target = new List<GameObject>(potentialTargets)[randomIndex];
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
