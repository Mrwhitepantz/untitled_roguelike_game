using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class Zach_Play_Tests
{
    private GameObject player;

    [OneTimeSetUp]
    public void LoadScene()
    {
        SceneManager.LoadScene("Zach_Automated_Testroom");
    }

    [UnityTest]
    //Black box Acceptance: Testing if run() outputs a Vector2() in
    //corresponding to moving up
    public IEnumerator TestRun_North()
    {
        player = GameObject.Find("Player");
        TopDownController movement = player.GetComponent<TopDownController>();
        Vector2 start_vel = new Vector2(0f, 0f);
        Vector2 inputDir = new Vector2(0f, 1f);
        Vector2 end_vel = movement.run(start_vel, inputDir);
        Assert.AreEqual(end_vel.x, start_vel.x, .01f);
        Assert.Greater(end_vel.y, start_vel.y);
        yield return null;
    }

    [UnityTest]
    //Black box Acceptance: Testing if run() outputs a Vector2() corresponding
    //to moving down
    public IEnumerator TestRun_South()
    {
        player = GameObject.Find("Player");
        TopDownController movement = player.GetComponent<TopDownController>();
        Vector2 start_vel = new Vector2(0f, 0f);
        Vector2 inputDir = new Vector2(0f, -1f);
        Vector2 end_vel = movement.run(start_vel, inputDir);
        Assert.AreEqual(end_vel.x, start_vel.x, .01f);
        Assert.Less(end_vel.y, start_vel.y);
        yield return null;
    }

    [UnityTest]
    //Black box Acceptance: Testing if run() outputs a Vector2() corresponding
    //to moving left
    public IEnumerator TestRun_West()
    {
        player = GameObject.Find("Player");
        TopDownController movement = player.GetComponent<TopDownController>();
        Vector2 start_vel = new Vector2(0f, 0f);
        Vector2 inputDir = new Vector2(-1f, 0f);
        Vector2 end_vel = movement.run(start_vel, inputDir);
        Assert.Less(end_vel.x, start_vel.x);
        Assert.AreEqual(end_vel.y, start_vel.y);
        yield return null;
    }

    [UnityTest]
    //Black box Acceptance: Testing if run() outputs a Vector2() corresponding
    //to moving right
    public IEnumerator TestRun_East()
    {
        player = GameObject.Find("Player");
        TopDownController movement = player.GetComponent<TopDownController>();
        Vector2 start_vel = new Vector2(0f, 0f);
        Vector2 inputDir = new Vector2(1f, 0f);
        Vector2 end_vel = movement.run(start_vel, inputDir);
        Assert.Greater(end_vel.x, start_vel.x);
        Assert.AreEqual(end_vel.y, start_vel.y);
        yield return null;
    }

    [UnityTest]
    //Black box Acceptance: Testing if run() outputs a Vector2() corresponding
    //to moving up-right
    public IEnumerator TestRun_NE()
    {
        player = GameObject.Find("Player");
        TopDownController movement = player.GetComponent<TopDownController>();
        Vector2 start_vel = new Vector2(0f, 0f);
        Vector2 inputDir = new Vector2(1f, 1f);
        Vector2 end_vel = movement.run(start_vel, inputDir);
        Assert.Greater(end_vel.x, start_vel.x);
        Assert.Greater(end_vel.y, start_vel.y);
        yield return null;
    }

    [UnityTest]
    //Black box Acceptance: Testing if run() outputs a Vector2() corresponding
    //to moving up-left
    public IEnumerator TestRun_NW()
    {
        player = GameObject.Find("Player");
        TopDownController movement = player.GetComponent<TopDownController>();
        Vector2 start_vel = new Vector2(0f, 0f);
        Vector2 inputDir = new Vector2(-1f, 1f);
        Vector2 end_vel = movement.run(start_vel, inputDir);
        Assert.Less(end_vel.x, start_vel.x);
        Assert.Greater(end_vel.y, start_vel.y);
        yield return null;
    }

    [UnityTest]
    ///Black box Acceptance: Testing if run() outputs a Vector2() corresponding
    //to moving down-right
    public IEnumerator TestRun_SE()
    {
        player = GameObject.Find("Player");
        TopDownController movement = player.GetComponent<TopDownController>();
        Vector2 start_vel = new Vector2(0f, 0f);
        Vector2 inputDir = new Vector2(1f, -1f);
        Vector2 end_vel = movement.run(start_vel, inputDir);
        Assert.Greater(end_vel.x, start_vel.x);
        Assert.Less(end_vel.y, start_vel.y);
        yield return null;
    }

    [UnityTest]
    //Black box Acceptance: Testing if run() outputs a Vector2() corresponding
    //to moving down-left
    public IEnumerator TestRun_SW()
    {
        player = GameObject.Find("Player");
        TopDownController movement = player.GetComponent<TopDownController>();
        Vector2 start_vel = new Vector2(0f, 0f);
        Vector2 inputDir = new Vector2(-1f, -1f);
        Vector2 end_vel = movement.run(start_vel, inputDir);
        Assert.Less(end_vel.x, start_vel.x);
        Assert.Less(end_vel.y, start_vel.y);
        yield return null;
    }

    [UnityTest]
    //Black box Acceptance: Testing if diagonal and linear movement produce
    //velocity of same magnitude (noticed that diagonal movement seemed
    //to move faster than horizontal and vertical movement)
    public IEnumerator TestRun_Diagonal_Linear_Same_Velocity()
    {
        player = GameObject.Find("Player");
        TopDownController movement = player.GetComponent<TopDownController>();
        //Moving NE
        Vector2 start_vel = new Vector2(0f, 0f);
        Vector2 inputDir = new Vector2(1f, 1f);
        Vector2 diagonal_vel = movement.run(start_vel, inputDir);

        //Moving East
        inputDir = new Vector2(1f, 0f);
        Vector2 horizontal_vel = movement.run(start_vel, inputDir);

        Assert.AreEqual(diagonal_vel.x, horizontal_vel.x);
        Assert.AreEqual(diagonal_vel.y, horizontal_vel.y);
        yield return null;
    }

    [UnityTest]
    //Black box Acceptance: Testing if we can no longer dash after 3 uses
    public IEnumerator Test_Dash_Counter()
    {
        player = GameObject.Find("Player");
        TopDownController movement = player.GetComponent<TopDownController>();
        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        Vector2 inputDir = new Vector2(1f, 1f);
        Assert.AreEqual(movement.canDash(), true);
        movement.dash(body, inputDir);
        movement.dash(body, inputDir);
        movement.dash(body, inputDir);
        Assert.AreEqual(movement.canDash(), false);
        yield return null;
    }

    [UnityTest]
    //Black Box Acceptance: Testing if lookAtMouse function returns expected values
    public IEnumerator Test_lookAtMouse()
    {
        player = GameObject.Find("Player");
        ShootingController shooter = player.GetComponent<ShootingController>();
        //Assert.AreEqual(0, shooter.lookAtMouse(new Vector2(0, 0), new Vector2(0, 0)));
        //Assert.Greater(0, shooter.lookAtMouse(new Vector2(0, 0), new Vector2(1, 1)));
        yield return null;
    }
}
