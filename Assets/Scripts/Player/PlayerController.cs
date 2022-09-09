using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour {
    
    public InputManager inputManager;
    [Header("Стандартное передвижение")]
    public CinemachineSwitcher cameraState;
    public float walkSpeed = 8.0F;
    public float runSpeed = 13.0F;
    float defaultSpeed = 0F;
    private float speed = 0.0F;
    public float jumpValue = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public GameObject player;
    public CharacterController controller;
    public bool stay;
    public bool mustStop;
    public ObstacleChecker obstacleChecker;
    public Animator _anim;
    GameObject body;
    public GameObject highChecker;
    bool ToHighFall = false;
    public AudioSource audioS;
    bool fallingImpact = false;
    float ClimbUpState1Time;
    float ClimbUpState2Time;
    public UIManager uiManager;
    public int rotateDirection = 0;

    [Header("Передвижение вприсядь")]
    public float crouchSpeed = 5.0F;
    public bool crouch;

    [Header("Передвижение по лестнице")]
    public bool nearClimb;
    public bool ClimbMode = false;
    public float climbSpeed = 2.0F;
    public FadeInOut fade;
    bool OnTheRoof = false;
    bool CanClimb = false;

    [Header("Двигание препятствий")]
    GameObject movableObstacle = null;
    Vector3 playerPosition;
    bool isDragging = false;

    void Start () {
        controller = GetComponent<CharacterController>();
        body = transform.Find("Body").gameObject;
        _anim = body.GetComponent<Animator>();
        defaultSpeed = walkSpeed;
        playerPosition.z = transform.localPosition.z;
	}

    public void CrouchToIdle(){
        crouch = false;
        if(!obstacleChecker.UpChecker){
            _anim.SetBool("Crouch", false);
            controller.height = 2.0f;
            controller.center = new Vector3(0.01f, 2, 0);
            defaultSpeed = 10.0f;
        }
    }

    void CheckHigh(){
        RaycastHit hit;
        Physics.Raycast(highChecker.transform.position, -highChecker.transform.up, out hit);
        float distance = Vector3.Distance(highChecker.transform.position, hit.point); 
        if(distance > 5){
            _anim.SetBool("TooHigh", true);
            ToHighFall = true;
            if(!fallingImpact)
                StartCoroutine(BeforeFall());
        }
        else{
            _anim.SetBool("TooHigh", false);
            fallingImpact = false;
        }
    }
 
    void CheckTriggers(){
        if(nearClimb && controller.isGrounded && !ClimbMode)
            uiManager.Trigger(true, "Залезть", inputManager.climbUpHelp);
        else if(obstacleChecker.CanClimbToLadder && controller.isGrounded)
            uiManager.Trigger(true, "Спуститься", inputManager.climbDownHelp);
        else
            uiManager.Trigger(false, "", "");
    }

    void StandartMoving(){
        float horizontal = Input.GetAxis("Horizontal");
        if (controller.isGrounded){
            _anim.SetBool("OnGrounded", true);
            moveDirection = new Vector3(-horizontal, 0, 0);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= defaultSpeed;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            controller.SimpleMove(forward * speed);
            if(Input.GetButtonDown("Jump") && !isDragging){
                if(crouch && stay){
                    CrouchToIdle();
                }
                else if(nearClimb && stay){
                    StartCoroutine(ClimbUpTimeout());
                }
                else{
                    Jump();
                }
            }

            Crouching();
            
            if(ToHighFall){
                StartCoroutine(AfterFall());
            }
        }
        else{
            _anim.SetBool("OnGrounded", false);
            CheckHigh();
        }
        if(Input.GetButtonDown("FastRun")){
            if (controller.isGrounded && !crouch && !stay && !isDragging){
                defaultSpeed = runSpeed;
                _anim.SetBool("RunFast", true);
                cameraState.RunFast();
            }
        }

        if(Input.GetButtonUp("FastRun")){
            if(!isDragging){
                defaultSpeed = walkSpeed;
            }
            _anim.SetBool("RunFast", false);
            cameraState.Main();
            if(controller.velocity == Vector3.zero){
               _anim.SetBool("stay", true);
                stay = true; 
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        if(controller.velocity == Vector3.zero && Input.GetAxisRaw("Horizontal") == 0){
			_anim.SetBool("stay", true);
            stay = true;
		}
        if(controller.velocity != Vector3.zero && Input.GetAxisRaw("Horizontal") != 0 && controller.enabled){
            if(isDragging){
                if(obstacleChecker.movableEntered && movableObstacle.GetComponent<MovableObject>().canMoving){
                    _anim.SetBool("Push", true);
                }
                else{
                    _anim.SetBool("Push", false);
                    isDragging = false;
                }
            }

            if(!mustStop){
                _anim.SetBool("stay", false);
                stay = false;
            }
            else{
                _anim.SetBool("stay", true);
                stay = true;
            }
            if(moveDirection.x > 0){
                PlayerRotateRight();
            }
            else{
                PlayerRotateLeft();
            }
        }

        // Check climbing
        if(obstacleChecker.OnClimbUp){
            nearClimb = true;
        }
        else{
            nearClimb = false;
        }

        if(movableObstacle != null && !obstacleChecker.movableEntered || movableObstacle != null && stay){
            MovableObjectOut();
        }
    }

    public void MovableObjectOut(){
        movableObstacle.transform.parent = transform.parent;
        _anim.SetBool("Push", false);
        isDragging = false;
        movableObstacle.GetComponent<MovableObject>().EndMoving();
    }

    public void ResetSpeed(){
        defaultSpeed = walkSpeed;
    }

    void ClimbMoving(){
        if(rotateDirection > 0)
            cameraState.LadderRight();
        else
            cameraState.LadderLeft();

        float vertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0, vertical, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= climbSpeed;
        controller.Move(moveDirection * Time.deltaTime);

        if(Input.GetAxis("Vertical") < 0){
            if (controller.isGrounded && !OnTheRoof){
                StartCoroutine(ClimbDownTimeout());
            }
        }
        if(CanClimb){
            if(controller.velocity != Vector3.zero && Input.GetAxis("Vertical") > 0){
                _anim.SetBool("ClimbingUp", true);
                _anim.SetBool("ClimbingDown", false);
            }
            if(controller.velocity != Vector3.zero && Input.GetAxis("Vertical") < 0){
                _anim.SetBool("ClimbingDown", true);
                _anim.SetBool("ClimbingUp", false); 
            }
            if(controller.velocity == Vector3.zero && Input.GetAxisRaw("Vertical") == 0){
                _anim.SetBool("ClimbingDown", false);
                _anim.SetBool("ClimbingUp", false);
            }
        }

        if(!obstacleChecker.OnClimbUp && !OnTheRoof){
            controller.enabled = false;
            _anim.SetBool("ClimbingUp", false);
            _anim.SetBool("ClimbingDown", false);
            StartCoroutine(ClimbToRoof());
        }
    }

    void IdleToCrouch(){
        _anim.SetBool("Crouch", true);
        controller.height = 1f;
        controller.center = new Vector3(0.01f, 1.5f, 0);
        crouch = true;
        defaultSpeed = crouchSpeed;
    }
    
    void Crouching(){
        if (Input.GetButtonDown("Crouch") && stay){
            if(obstacleChecker.CanClimbToLadder){
                ClimbDownToLadder();
            }
            if(!obstacleChecker.CanClimbToLadder){
                IdleToCrouch();
            }
        }

        if (Input.GetButtonUp("Crouch")){
            CrouchToIdle();
        }
    }

    void Jump(){
        if(!crouch && !obstacleChecker.UpChecker){
            moveDirection.y = jumpValue;
            _anim.SetTrigger("Jump");
        }
    }

    void OnClimbDown(){
        _anim.SetBool("OnClimb", false);
        ClimbMode = false;
    }

    public void Stop(){
        _anim.SetBool("stay", true);
        mustStop = true;
    }

    void Update()
    {
        if(!ClimbMode){
            StandartMoving();
        }
        else{
            ClimbMoving();
        }
        CheckTriggers();
        OnControlTrigger();
    }

    void ClimbDownToLadder(){
        if(obstacleChecker.ClimbDownDirection == 0){
            PlayerRotateRight();
            StartCoroutine(ClimbDownToLadderTimeout());
        }
        if(obstacleChecker.ClimbDownDirection != 0){
            PlayerRotateLeft();
            StartCoroutine(ClimbDownToLadderTimeout());
        }
    }

    void PlayerRotateLeft(){
        rotateDirection = 1;
        body.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    void PlayerRotateRight(){
        rotateDirection = 0;
        body.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    void OnGUI(){
        Event e = Event.current;
        if (e.isKey){
            inputManager.MainController = "keyboard";
        }
    }

    void OnControlTrigger(){
        // if(Input.GetAxisRaw("HorizontalHidden") != 0)
        //     inputManager.MainController = "gamepad";
    }
    void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit.collider.tag == "MovableObstacle" && obstacleChecker.movableEntered && !crouch && controller.isGrounded){
            defaultSpeed = 3f;
            movableObstacle = hit.gameObject;
            MovableObject movableLogic = movableObstacle.GetComponent<MovableObject>();
            if(movableLogic.canMoving){
                if(!movableLogic.moving){
                    movableLogic.StartMoving();
                }
                movableObstacle.transform.parent = transform;
                isDragging = true;
            }
        }
    }

    IEnumerator ClimbUpTimeout() {
        controller.enabled = false;
        ClimbMode = true;
        _anim.SetBool("OnClimb", true);
		yield return new WaitForSeconds(1);
        CanClimb = true;
        controller.enabled = true;
	}

    IEnumerator ClimbDownTimeout() {
        _anim.SetTrigger("ClimbOnGround");
        yield return new WaitForSeconds(.6f);
        _anim.ResetTrigger("ClimbOnGround");
        _anim.SetBool("OnClimb", false);
        ClimbMode = false;
        nearClimb = true;
        CanClimb = false;
        cameraState.Main();
	}

    IEnumerator ClimbToRoof() {
        CanClimb = false;
        OnTheRoof = true;
        _anim.SetBool("OnClimb", false);
        _anim.SetTrigger("ClimbingToRoof");
        yield return new WaitForSeconds(.7f);
        fade.Toggle();
        yield return new WaitForSeconds(.3f);
        _anim.ResetTrigger("ClimbingToRoof");
        player.transform.position = obstacleChecker.climbRoofTarget;
        controller.enabled = true;
        ClimbMode = false;
        cameraState.Main();
        nearClimb = false;
	}

    IEnumerator AfterFall() {
        ToHighFall = false;
        controller.enabled = false;
        yield return new WaitForSeconds(1.5f);
        controller.enabled = true;
	}

    IEnumerator BeforeFall() {
        OnTheRoof = false;
        yield return new WaitForSeconds(.3f);
        audioS.Pause();
        fallingImpact = true;
	}

    IEnumerator ClimbDownToLadderTimeout(){
        _anim.SetBool("stay", true);
        _anim.SetTrigger("ClimbingToLadder");
        controller.enabled = false;
        ClimbMode = true;
        yield return new WaitForSeconds(.5f);
        _anim.ResetTrigger("ClimbingToLadder");
        fade.Toggle();
        _anim.SetBool("OnClimb", true);
        yield return new WaitForSeconds(.3f);
        player.transform.position = obstacleChecker.ClimbDownTarget;
        if(obstacleChecker.ClimbDownDirection == 0){
            PlayerRotateLeft();
        }
        if(obstacleChecker.ClimbDownDirection != 0){
            PlayerRotateRight();
        }
        OnTheRoof = false;
        nearClimb = false;
        ClimbMode = false;
        cameraState.Main();
        StartCoroutine(ClimbDownTimeout());
        yield return new WaitForSeconds(.3f);
        controller.enabled = true;
    }
}