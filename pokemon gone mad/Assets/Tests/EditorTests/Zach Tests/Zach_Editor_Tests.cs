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
}
