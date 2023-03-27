using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
public class Austin_Tests : MonoBehaviour
{
  
    // acceptance test
    [Test]
    public void testEnemyMeleeDamageSqurtal()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "Squrtal";
        float testDamage = AIAttaack.meleeD(testcase, 1);
        Assert.AreEqual(25.5f, testDamage);
    }
    // acceptance test
    [Test]
    public void testEnemyMeleeDamageBoss()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "Boss";
        float testDamage = AIAttaack.meleeD(testcase, 1);
        Assert.AreEqual(80f, testDamage);
    }
    // acceptance test
    [Test]
    public void testEnemyMeleeDamageCharmander()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "Charmander";
        float testDamage = AIAttaack.meleeD(testcase, 1);
        Assert.AreEqual(36.2f, testDamage);
    }
    // acceptance test
    [Test]
    public void testEnemyMeleeDamagePikachu()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "Pikachu";
        float testDamage = AIAttaack.meleeD(testcase, 1);
        Assert.AreEqual(15.3f, testDamage);
    }
    // acceptance test
    [Test]
    public void testEnemyMeleeDamageGameHazard()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "GameHazard";
        float testDamage = AIAttaack.meleeD(testcase, 1);
        Assert.AreEqual(8.9f, testDamage);
    }
// acceptance test
[Test]
    public void testEnemySpeedSqurthal()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "Squrtal";
        float testSpeed = EnemyAI.MoveSpeed(testcase, 0);
        Assert.AreEqual(20f, testSpeed);
    }
// acceptance test
[Test]
    public void testEnemySpeedEliteSqurthal()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "Squrtal";
        float testSpeed = EnemyAI.MoveSpeed(testcase, 1);
        Assert.AreEqual(30f, testSpeed);
    }



// acceptance test
[Test]
    public void testEnemySpeedTestSquare()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "TestSquare";
        float testSpeed = EnemyAI.MoveSpeed(testcase, 0);
        Assert.AreEqual(200f, testSpeed);
    }
// acceptance test
[Test]
    public void testEnemySpeedEliteTestSquare()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "TestSquare";
        float testSpeed = EnemyAI.MoveSpeed(testcase, 1);
        Assert.AreEqual(300f, testSpeed);
    }
// acceptance test
[Test]
    public void testEnemySpeedBoss()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "Boss";
        float testSpeed = EnemyAI.MoveSpeed(testcase, 0);
        Assert.AreEqual(350f, testSpeed);
    }
// acceptance test
[Test]
    public void testEnemySpeedEliteBoss()
    {
        GameObject testcase = new GameObject();
        testcase.tag = "Boss";
        float testSpeed = EnemyAI.MoveSpeed(testcase, 1);
        Assert.AreEqual(400f, testSpeed);
    }
   // WhiteBoxTest
[Test]
    public void testWhiteBoxAttackType()
    {
        //Gives 100% coverage with all 3 tests
        /*
        public static int attackType(float distance, float mR, float sR){
        if (distance > mR && distance < sR )
            {
                return (1);
            }
        else if (distance <= mR) {
                return (2);
            }
        else{
            return (3);
        }

    }
        
        */
        
        int typetest = AIAttaack.attackType(20, 5,30);
        Assert.AreEqual(1, typetest);
        typetest = AIAttaack.attackType(35, 5,30);
        Assert.AreEqual(3, typetest);
        typetest = AIAttaack.attackType(4, 5,30);
        Assert.AreEqual(2, typetest);
    }







    

    
}
