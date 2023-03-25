using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Zach_Editor_Tests
{
    [Test]
    //Acceptance: testing that we have the correct fields for movement to
    //ensure that movement is exactly how we want it to feel
    public void TestMovementParameters()
    {
        TopDownController movement = new TopDownController();
        Assert.AreEqual(8f, movement.maxSpeed);
        Assert.AreEqual(95f, movement.maxAccel);
        Assert.AreEqual(1f, movement.friction);

        //This is just to prove that the test does work, even though
        //Unity complains that movement is a MonoBehavior
        Assert.AreEqual(9f, movement.maxSpeed);
    }

    [UnityTest]
    //Acceptance: testing that given an input direction, we get a new 
    //vector2D in correct corresponding direction
    public IEnumerator TestRun()
    {
        GameObject player = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Assets/Resources/Prefabs/Player.prefab"));
        TopDownController movement = player.GetComponent<TopDownController>();
        Vector2 currVelocity = new Vector2(0f, 0f);
        Vector2 inputDir = new Vector2(0f, 1f); //moving up
        Vector2 newVelocity = movement.run(currVelocity, inputDir);
        Assert.AreEqual(newVelocity.x, currVelocity.x, 1f);
        Assert.Greater(newVelocity.y, currVelocity.x);
        yield return null;
    }

    [UnityTest]
    //Acceptance: checking if diagonal movement doesn't lead to a greater
    //Vector 2 than linear movement
    public IEnumerator TestIfDiagonalAndLinearMovementSame()
    {
        yield return null;
    }

    [UnityTest] //White box
    public IEnumerator TestDirection()
    {
        var player = new GameObject();
        TopDownController movement = player.GetComponent<TopDownController>();

        movement.getInput();

        yield return null;
    }
}
