using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.EventSystems;


public class NoahPlayerTest {
    
    private GameObject player;
    private AnimatorClipInfo[] animatorinfo;
    private string current_animation;
    private GameObject canvas;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    [OneTimeSetUp]
    public void LoadScene(){
        SceneManager.LoadScene("Noah_Tests");
        
    }
    [SetUp]
    public void Setup(){
        canvas = GameObject.Find("Canvas");
    }

    public Vector2 getInput() {
        // Return normalized input vector
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }


    //////////Four Primary Directions ^^^^^


    [UnityTest]
    //Blackbox Test - ACCEPTANCE TEST Idle player with no previous directional changes
    //checking to see if the player animation while idle is correct
    public IEnumerator PlayerIdleAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd_IdleWest",current_animation);
        yield return null;
    }

    [UnityTest]
    //Blackbox Test - ACCEPTANCE TEST Moving the player North, checking if
    //the animation played matches what should be played.
    public IEnumerator PlayerNorthAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(0,1);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd 1_MoveNorth",current_animation);
        yield return null;
    }
    [UnityTest]
    //Blackbox Test - ACCEPTANCE TEST Moving the player South, checking if
    //the animation played matches what should be played.
    public IEnumerator PlayerSouthAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(0,-1);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd 1_MoveSouth",current_animation);
        yield return null;
    }
    [UnityTest]
    //Blackbox Test - ACCEPTANCE TEST  Moving the player East, checking if
    //the animation played matches what should be played.
    public IEnumerator PlayerEastAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(1,0);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd_MoveEast",current_animation);
        yield return null;
    }
    [UnityTest]
    //Blackbox Test - ACCEPTANCE TEST Moving the player West, checking if
    //the animation played matches what should be played.
    public IEnumerator PlayerWestAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(-1,0);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd_moveWest",current_animation);
        yield return null;
    }
    [UnityTest]
    //Blackbox Test - ACCEPTANCE TEST  Moving the player West, checking if
    //the animation played matches what should be played.
    public IEnumerator PlayerNorthWestAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(-0.71f,0.71f);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd_moveNorthWest",current_animation);
        yield return null;
    }
    [UnityTest]
    //Blackbox Test - ACCEPTANCE TEST  Moving the player West, checking if
    //the animation played matches what should be played.
    public IEnumerator PlayerNorthEastAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(0.71f,0.71f);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd_moveNorthEast",current_animation);
        yield return null;
    }




    //////////Moving in 6/8 Directions ^^^^^

    //////////Additional Idles Based on movement \/\/\/\/

    [UnityTest]
    //Blackbox Test -  ACCEPTANCE TEST Moving the player West, checking if
    //the alternate idle animation following player movement is played.
    public IEnumerator PlayerWestIdleAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(-1,0);
        yield return new WaitForSecondsRealtime(1);
        player.GetComponent<Player>().testDirection = new Vector2(0,0);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd_IdleWest",current_animation);
        yield return null;
    }
    [UnityTest]
    //Blackbox Test -  ACCEPTANCE TEST Moving the player East, checking if
    //the alternate idle animation following player movement is played.
    public IEnumerator PlayerEastIdleAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(1,0);
        yield return new WaitForSecondsRealtime(1);
        player.GetComponent<Player>().testDirection = new Vector2(0,0);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd 1_IdleEast",current_animation);
        yield return null;
    }
    [UnityTest]
    //Blackbox Test -  ACCEPTANCE TEST Moving the player North, checking if
    //the alternate idle animation following player movement is played.
    public IEnumerator PlayerNorthIdleAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(0,1);
        yield return new WaitForSecondsRealtime(1);
        player.GetComponent<Player>().testDirection = new Vector2(0,0);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd 1_IdleNorth",current_animation);
        yield return null;
    }
    [UnityTest]
    //Blackbox Test -  ACCEPTANCE TEST Moving the player South, checking if
    //the alternate idle animation following player movement is played.
    public IEnumerator PlayerSouthIdleAnimation() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(0,-1);
        yield return new WaitForSecondsRealtime(1);
        player.GetComponent<Player>().testDirection = new Vector2(0,0);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("farfetch'd 1_IdleSouth",current_animation);
        yield return null;
    }

    
    //Integration Test -BIG BANG
    //Tests if when the player navigates to an item using functionality from Player.cs "Player" class
    //the player's form will impact an object, triggering a response from ItemManager.cs "ItemManager" class
    //which will alter the form of the player as well as trigger a flag that the player has hit an object in both
    //ItemManager and in TopDownController which is attached to the "Player" class
    [UnityTest]
    public IEnumerator PlayerItemManagerInteraction() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();
        player.GetComponent<Player>().Test = true;
        player.GetComponent<Player>().testDirection = new Vector2(1,0);
        yield return new WaitForSecondsRealtime(0.7f);
        player.GetComponent<Player>().testDirection = new Vector2(0,-1);
        yield return new WaitForSecondsRealtime(2);
        player.GetComponent<Player>().testDirection = new Vector2(1,0);
        yield return new WaitForSecondsRealtime(1);
        animatorinfo = player.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        Assert.AreEqual("Squirtle_moveWest",current_animation);
        Assert.AreEqual(true,player.GetComponent<ItemManager>().ifCollision);
        Assert.AreEqual(true,player.GetComponent<TopDownController>().ifCollision);
        yield return null;
    }
    [UnityTest]
    //White Box: Statement Coverage
    // Checks edge cases for animator assignment in animate() function
    //This test checks the output of each function within TopDownController, determining if the outputs
    //Are equal to the expected information that this function completes as well as any variables
    //these functions alter
    public IEnumerator PlayerToptoDownTopDownController() {
        player = GameObject.Find("Player");
        yield return new WaitForFixedUpdate();

        //run manages player calculation for distance travelled given the directional inputs
        //  currVelocity is the current speed and direction
        // inputDir is the incoming directional change
        Vector2 vectorTemp = new Vector2 (0,1);
        Vector2 directionTemp = new Vector2 (1,0);
        Vector2 outputVector = new Vector2 (1.90f,0);
        Assert.AreEqual(outputVector,player.GetComponent<TopDownController>().run(vectorTemp,directionTemp));

    //     public Vector2 run(Vector2 currVelocity, Vector2 inputDir)
    // {
    //     maxSpeedChange = maxAccel * Time.deltaTime;
    //     desiredVelocity = new Vector2(inputDir.x, inputDir.y).normalized * (maxSpeed - friction);
    //     currVelocity.x = Mathf.MoveTowards(currVelocity.x, desiredVelocity.x, maxSpeedChange);
    //     currVelocity.y = Mathf.MoveTowards(currVelocity.y, desiredVelocity.y, maxSpeedChange);
    //     return currVelocity;
    // }

        //animate checks and modifies the animation system's variables to allow for multiple directions of
        //player animation, all eight cardinal directions as well as additional functionality for 
        //unique effects depending on what objects the player interacts with
        yield return new WaitForFixedUpdate();
        player.GetComponent<TopDownController>().animate(new Vector2 (0,0));
        Assert.AreEqual(0,player.GetComponent<Animator>().GetFloat("Speed"));
        Assert.AreEqual(0,player.GetComponent<Animator>().GetFloat("Horizontal"));
        Assert.AreEqual(0,player.GetComponent<Animator>().GetFloat("Vertical"));
        yield return new WaitForFixedUpdate();

        player.GetComponent<TopDownController>().animate(new Vector2 (0,1));
        Assert.AreEqual(1,player.GetComponent<Animator>().GetFloat("Speed"));
        Assert.AreEqual(1,player.GetComponent<Animator>().GetFloat("Horizontal"));
        Assert.AreEqual(0,player.GetComponent<Animator>().GetFloat("Vertical"));
        yield return new WaitForFixedUpdate();

        player.GetComponent<TopDownController>().animate(new Vector2 (1,1));
        Assert.AreEqual(1,player.GetComponent<Animator>().GetFloat("Speed"));
        Assert.AreEqual(1,player.GetComponent<Animator>().GetFloat("Horizontal"));
        Assert.AreEqual(1,player.GetComponent<Animator>().GetFloat("Vertical"));
        yield return new WaitForFixedUpdate();

        player.GetComponent<TopDownController>().animate(new Vector2 (-1,-1));
        Assert.AreEqual(1,player.GetComponent<Animator>().GetFloat("Speed"));
        Assert.AreEqual(-1,player.GetComponent<Animator>().GetFloat("Horizontal"));
        Assert.AreEqual(-1,player.GetComponent<Animator>().GetFloat("Vertical"));
        yield return new WaitForFixedUpdate();

    //     public void animate(Vector2 inputDir)
    // {
        
    //     //Debug.Log("x");
    //     //Debug.Log(inputDir.x);
        
        
    //     if (inputDir.x != 0 ){
    //         animator.SetFloat("speed", 1);
    //         if (inputDir.x > 0) {
    //             animator.SetFloat("horizontal", 1);
    //         } else { animator.SetFloat("horizontal", -1);}
    //     } else {animator.SetFloat("horizontal", 0);}
    //     if (inputDir.y != 0 ){
    //         //Debug.Log("y");
    //         //Debug.Log(inputDir.y);
    //         animator.SetFloat("speed", 1);
    //         if (inputDir.y > 0) {
    //             animator.SetFloat("vertical", 1);
    //         } else { animator.SetFloat("vertical", -1);}
            
    //     } else {animator.SetFloat("vertical", 0);}
    //     if ((inputDir.x == 0) && (inputDir.y == 0)) {
            
    //         animator.SetFloat("speed", 0);
    //     }
    // }

    }



}
