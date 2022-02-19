using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anima : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anima;
    private string currentState;

    private void Awake()
    {
        anima = GetComponent<Animator>();
        if (anima == null) Debug.LogError("Animator not attached ");
    }
    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        //play the animation
        anima.Play(newState);

        //reassign the current state
        currentState = newState;
    }
}
