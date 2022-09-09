using UnityEngine;
public class CinemachineSwitcher : MonoBehaviour{
    Animator animator;
    private void Start(){
        animator = GetComponent<Animator>();
    }

    public void Main(){
        animator.Play("Main");
    }

    public void LadderRight(){
        animator.Play("LadderRight");
    }

    public void LadderLeft(){
        animator.Play("LadderLeft");
    }

    public void RunFast(){
        animator.Play("RunFast");
    }

    public void Dialogue(){
        animator.Play("Dialogue");
    }
}
