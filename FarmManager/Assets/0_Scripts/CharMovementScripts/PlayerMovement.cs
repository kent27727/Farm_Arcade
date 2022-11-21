using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour
{
    private CharacterController _charController;
    Animator _anim;
    private ManagerJoystick _mngrJoyStick;

    private CollectObject collectObject;
    public GameObject lArm,rArm;

    public Canvas Cnvs;

    public float speedMove;
    private float inputX, inputY;
    private Vector3 posMove, posRotation;
    private Transform meshChar;

    
    
    //void Awake() => _animator = GetComponent<Animator>();

    private void Start()
    {
        _charController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        meshChar = _charController.transform;
        _anim = meshChar.GetComponent<Animator>();
        _mngrJoyStick = GameObject.Find("imgJoystickBg").GetComponent<ManagerJoystick>();
        collectObject = this.gameObject.GetComponent<CollectObject>();
    }
    void Update()
    {
        inputX = _mngrJoyStick.inputHorizontal();
        inputY = _mngrJoyStick.inputVertical();

        if(inputX != 0 || inputY !=0)
        {
            _anim.SetBool("isRunning", true);
        }
        else
        {
            _anim.SetBool("isRunning", false);
        }

        //char move
        posMove = new Vector3((inputX * speedMove) / Cnvs.scaleFactor, 0, (inputY * speedMove) / Cnvs.scaleFactor);
        _charController.Move(posMove);

        //char rotate
        if(_mngrJoyStick.inputHorizontal() !=0 || _mngrJoyStick.inputVertical() !=0)
        {
            posRotation = new Vector3(_mngrJoyStick.inputHorizontal(), 0, _mngrJoyStick.inputVertical());
            meshChar.rotation = Quaternion.LookRotation(posRotation);
        }
        // if (collectObject.stackList.Count == 0)
        // {
        //     animateArms(false);
        // }
        // else if (collectObject.stackList.Count >0)
        // {
        //     animateArms(true);
        // }
        }
        void animateArms(bool stackFull){
            if (!stackFull)
            {
                lArm.transform.DORotate(new Vector3(12,-94,70),1);
                rArm.transform.DORotate(new Vector3(12,94,-72),1);
            }
            else if(stackFull){
                lArm.transform.DORotate(new Vector3(12,-50,94),1);
                rArm.transform.DORotate(new Vector3(2,55,-105),1);

            }

        }
    }
