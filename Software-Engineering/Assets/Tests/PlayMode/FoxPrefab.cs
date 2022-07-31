using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


 public class GetFoxPrefab
{
    private static GameObject foxPrefab = Resources.Load("FoxPrefab") as GameObject;

    public static GameObject GetInstance()
    {
        return MonoBehaviour.Instantiate(foxPrefab);
    }
}
public class FoxPrefab
{
    GameObject gameObject;
    GameObject gameObjectHare;


    [SetUp]
    public void BeforeEveryTest()
    {
        gameObject = GetFoxPrefab.GetInstance();
        GameObject mainCamera = Resources.Load("MainCamera") as GameObject;
        MonoBehaviour.Instantiate(mainCamera);
    }

    [UnityTest]
    public IEnumerator FoxPrefabCurrentBarHealth()
    {
        
        gameObject.GetComponent<Fox>().setBar(ref gameObject.GetComponent<Animal>().currentHealth, 100, gameObject.GetComponent<Fox>().healthBar);
        yield return null;
        Assert.AreEqual(100,gameObject.GetComponent<Fox>().healthBar.slider.value);
    } 

    [UnityTest]
    public IEnumerator FoxPrefabCurrentBarHunger()
    {
        
        gameObject.GetComponent<Fox>().setBar(ref gameObject.GetComponent<Animal>().currentHunger, 100, gameObject.GetComponent<Fox>().hungerBar);
        yield return null;
        Assert.AreEqual(100,gameObject.GetComponent<Fox>().hungerBar.slider.value);
    } 
    
    [UnityTest]
    public IEnumerator FoxPrefabCurrentBarThirst()
    {
        
        gameObject.GetComponent<Fox>().setBar(ref gameObject.GetComponent<Animal>().currentThirst, 100, gameObject.GetComponent<Fox>().thirstBar);
        yield return null;
        Assert.AreEqual(100,gameObject.GetComponent<Fox>().thirstBar.slider.value);
    } 
    
    [UnityTest]
    public IEnumerator FoxPrefabCurrentBarHorny()
    {
        
        gameObject.GetComponent<Fox>().setBar(ref gameObject.GetComponent<Animal>().currentHorny, 0, gameObject.GetComponent<Fox>().hornyBar);
        yield return null;
        Assert.AreEqual(0,gameObject.GetComponent<Fox>().hornyBar.slider.value);
    } 

    [UnityTest]
    public IEnumerator FoxPrefabKillRange()
    {
        yield return null;
        Assert.AreEqual(4,gameObject.GetComponent<Fox>().killRange);
    } 
    [UnityTest]
    public IEnumerator FoxPrefabKillHareDefault()
    {
        yield return null;
        Assert.AreEqual(0,gameObject.GetComponent<Fox>().killedHares);
    } 

    [UnityTest]
    public IEnumerator changeHungerBarTest()
    {
        gameObject.GetComponent<Fox>().setBar(ref gameObject.GetComponent<Animal>().currentHunger, 100, gameObject.GetComponent<Fox>().hungerBar);
        gameObject.GetComponent<Fox>().changeBar(gameObject.GetComponent<Fox>().hungerBar,20, ref gameObject.GetComponent<Animal>().currentHunger,"minus");
        yield return null;
        Assert.AreEqual(80,gameObject.GetComponent<Fox>().hungerBar.slider.value);
    } 
    [UnityTest]
    public IEnumerator changeThirstBarTest()
    {
        gameObject.GetComponent<Fox>().setBar(ref gameObject.GetComponent<Animal>().currentThirst, 100, gameObject.GetComponent<Fox>().thirstBar);
        gameObject.GetComponent<Fox>().changeBar(gameObject.GetComponent<Fox>().thirstBar,20, ref gameObject.GetComponent<Animal>().currentThirst,"minus");
        yield return null;
        Assert.AreEqual(80,gameObject.GetComponent<Fox>().thirstBar.slider.value);
    } 
    [UnityTest]
    public IEnumerator changeHealthBarTest()
    {
        gameObject.GetComponent<Fox>().setBar(ref gameObject.GetComponent<Animal>().currentHealth, 100, gameObject.GetComponent<Fox>().healthBar);
        gameObject.GetComponent<Fox>().changeBar(gameObject.GetComponent<Fox>().healthBar,20, ref gameObject.GetComponent<Animal>().currentHealth,"minus");
        yield return null;
        Assert.AreEqual(80,gameObject.GetComponent<Fox>().healthBar.slider.value);
    }
    [UnityTest]
    public IEnumerator GetAnimalInfoTest()
    {

        yield return null;
        Assert.IsNotNull(gameObject.GetComponent<Fox>().getAnimalInfo());
    }

    [UnityTest]
    public IEnumerator GetDefaultAliveTest()
    {
        yield return null;
        Assert.IsTrue(gameObject.GetComponent<Fox>().isAlive);

    }
    [UnityTest]
    public IEnumerator FoxAninationIsMovingTest()
    {
        gameObject.GetComponent<Movement>().isWandering=false;
        //gameObject.GetComponent<FoxAnimation>().fox.isAlive=true;
        yield return null;
        Assert.IsFalse(gameObject.GetComponent<Movement>().isWandering);

    }


} 
