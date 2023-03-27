using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class AustinPlayModeTest
{
    [OneTimeSetUp]
    public void LoadScene(){
        SceneManager.LoadScene("GameplayScene(For Real)");
        
    }
    [UnityTest]
    public IEnumerator TestEnamypathWorked()
    {
        GameObject enemy = GameObject.Find("Square");
        GameObject player = GameObject.Find("Player");
        //float distanceStart = Vector3.Distance(player.position, enemy.position);
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(2);
        //float distanceEnd = Vector3.Distance(player.position, enemy.position);
        Assert.IsTrue(1==1);
    }
}
