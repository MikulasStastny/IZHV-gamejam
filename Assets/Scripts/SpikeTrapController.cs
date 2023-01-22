using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapController : MonoBehaviour
{
    public Animator animator;
    public bool isTriggered = false;

    private IEnumerator WaitAndChangeTriggeredState()
    {
        yield return new WaitForSeconds(1.0f);
        isTriggered = false;
        animator.SetBool("isSpikeTriggered", isTriggered);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // We don't have to check for object for now If we are going to add
        // monsters, then we only have to check if it is not a monster.
        Debug.Log("Something entered a trap");
        isTriggered = true;
        // Other state is handled by animator script
        animator.SetBool("isSpikeTriggered", isTriggered);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            StartCoroutine(WaitAndChangeTriggeredState());
        }
    }
}
