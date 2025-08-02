using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ThrowableObject : Skill_Base
{
    // Super hero throw large boulder , THrow Batarang type things,, throw weapons, throw inventory items?
    // should I still have Trajectory event though in a top donwn game I wouldnt have physics to have it drop. Should I have the dots show by defautl or a upgrade or nto at all.
    // Is there a way to fake the physics in a top down so  the throw works more realistically? since its supper heros does that matter since they should be strong and be able to throw it as far as they want?
    //THough a human with gadges wouldnt be as strong but could aurgure the gadget just goes that far...
    // if it jsut goes as far as we say then is there a point of a trajectory prediction?
    [Header("Regular Throwable Object Upgrade")]
    [SerializeField] private GameObject throwableObjectPrefab;
    [Range(0, 10)]
    [SerializeField] private float throwPower = 5;


    [Header("Trajectory Prediction")]
    [SerializeField] private GameObject predictionDot;
    [SerializeField] private int numberOfDots = 20;
    [SerializeField] private float spaceBetweenDots = .05f;
    private float gravity;
    private Transform[] dots;
    private Vector2 confirmDirection;

    protected override void Awake()
    {
        base.Awake();
        dots = GenerateDots();
        gravity = throwableObjectPrefab.GetComponent<Rigidbody2D>().gravityScale;
    }

    public void ThrowObject()
    {
        //Debug.Log("Create New Object!");

        GameObject newThrowableObject = Instantiate(throwableObjectPrefab, dots[1].position, Quaternion.identity);
        // so if using this as a generic thorwing can throw diffeernt items equipped or objects picked up then would need to figure out how to change the prefab here to that.
    //If going to use this as a specifc throwing Skilll then can do the determin prefab like on deployable bomb.

    }

    // public void ThrowItem()
    // {
    //         Debug.Log("Create New Weapon!");
    // }
    //public void ThrowWeapon(){  
    // }
    // do i need this all as a skill or jsut a throwing capability... can i leave it as a skill and just have it unlocked? what if  want a throwing upgrade for batarang and a regular throing thing.. should I make those seperate skills? maybe inherits from this one?
    // or can i just do throw object and then determin what type is being thrown?

    public void PredictTrajectory(Vector2 direction)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].position = GetTrajectoryPoint(direction, i * spaceBetweenDots);
        }
    }

    private Vector2 GetTrajectoryPoint(Vector2 direction, float t)
    {
        float scaledThrowPower = throwPower * 10;

        //gives us the inital velocity - the starting speed and direction of the throw.
        Vector2 initialVelocity = direction * scaledThrowPower;

        //gracity pulls it down over time. the longer in air the more it drops
        Vector2 gravityEffect = 0.5f * Physics2D.gravity * gravity * (t * t);

        Vector2 predictionPoint = (initialVelocity * t) + gravityEffect;

        Vector2 playerPosition = transform.root.position;
        return playerPosition + predictionPoint;

    }

    public void ConfirmTrajectory(Vector2 direction) => confirmDirection = direction;

    public void EnableDots(bool enable)
    {
        foreach (Transform t in dots)
            t.gameObject.SetActive(enable);
    }

    private Transform[] GenerateDots()
    {
        Transform[] newDots = new Transform[numberOfDots];

        for (int i = 0; i < numberOfDots; i++)
        {
            newDots[i] = Instantiate(predictionDot, transform.position, Quaternion.identity, transform).transform;
            newDots[i].gameObject.SetActive(false);
        }
        return newDots;
    }


}
