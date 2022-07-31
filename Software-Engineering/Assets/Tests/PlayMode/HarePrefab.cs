using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

 public class GetHarePrefab
{
    private static GameObject harePrefab = Resources.Load("HarePrefab") as GameObject;

    public static GameObject GetInstance()
    {
        return MonoBehaviour.Instantiate(harePrefab);
    }
}
public class HarePrefab 
{
GameObject gameObject;


    [SetUp]
    public void BeforeEveryTest()
    {
        gameObject = GetHarePrefab.GetInstance();
        GameObject mainCamera = Resources.Load("MainCamera") as GameObject;
        MonoBehaviour.Instantiate(mainCamera);
/*         m_MainCamera.AddComponent("Camera");
        m_MainCamera.gameObject.tag="MainCamera"; */
    }

    [UnityTest]
    public IEnumerator HarePrefabCurrentBarHealth()
    {
        
        gameObject.GetComponent<Hare>().setBar(ref gameObject.GetComponent<Animal>().currentHealth, 100, gameObject.GetComponent<Hare>().healthBar);
        yield return null;
        Assert.AreEqual(100,gameObject.GetComponent<Hare>().healthBar.slider.value);
    } 

    [UnityTest]
    public IEnumerator HarePrefabCurrentBarHunger()
    {
        
        gameObject.GetComponent<Hare>().setBar(ref gameObject.GetComponent<Animal>().currentHunger, 100, gameObject.GetComponent<Hare>().hungerBar);
        yield return null;
        Assert.AreEqual(100,gameObject.GetComponent<Hare>().hungerBar.slider.value);
    } 
    
    [UnityTest]
    public IEnumerator HarePrefabCurrentBarThirst()
    {
        
        gameObject.GetComponent<Hare>().setBar(ref gameObject.GetComponent<Animal>().currentThirst, 100, gameObject.GetComponent<Hare>().thirstBar);
        yield return null;
        Assert.AreEqual(100,gameObject.GetComponent<Hare>().thirstBar.slider.value);
    } 
    
    [UnityTest]
    public IEnumerator HarePrefabCurrentBarHorny()
    {
        
        gameObject.GetComponent<Hare>().setBar(ref gameObject.GetComponent<Animal>().currentHorny, 0, gameObject.GetComponent<Hare>().hornyBar);
        yield return null;
        Assert.AreEqual(0,gameObject.GetComponent<Hare>().hornyBar.slider.value);
    } 
    [UnityTest]
    public IEnumerator ChangeHungerBarTest()
    {
        gameObject.GetComponent<Hare>().setBar(ref gameObject.GetComponent<Animal>().currentHunger, 100, gameObject.GetComponent<Hare>().hungerBar);
        gameObject.GetComponent<Hare>().changeBar(gameObject.GetComponent<Hare>().hungerBar,20, ref gameObject.GetComponent<Animal>().currentHunger,"minus");
        yield return null;
        Assert.AreEqual(80,gameObject.GetComponent<Hare>().hungerBar.slider.value);
    } 
    [UnityTest]
    public IEnumerator ChangeThirstBarTest()
    {
        gameObject.GetComponent<Hare>().setBar(ref gameObject.GetComponent<Animal>().currentThirst, 100, gameObject.GetComponent<Hare>().thirstBar);
        gameObject.GetComponent<Hare>().changeBar(gameObject.GetComponent<Hare>().thirstBar,20, ref gameObject.GetComponent<Animal>().currentThirst,"minus");
        yield return null;
        Assert.AreEqual(80,gameObject.GetComponent<Hare>().thirstBar.slider.value);
    } 
    [UnityTest]
    public IEnumerator ChangeHealthBarTest()
    {
        gameObject.GetComponent<Hare>().setBar(ref gameObject.GetComponent<Animal>().currentHealth, 100, gameObject.GetComponent<Hare>().healthBar);
        gameObject.GetComponent<Hare>().changeBar(gameObject.GetComponent<Hare>().healthBar,20, ref gameObject.GetComponent<Animal>().currentHealth,"minus");
        yield return null;
        Assert.AreEqual(80,gameObject.GetComponent<Hare>().healthBar.slider.value);
    }
    [UnityTest]
    public IEnumerator MoveToNearestGrassTest()
    {
        gameObject.GetComponent<Hare>().grassPositionList.Add(new Vector3(13f, 7f, 22f));

        yield return null;
        Assert.AreEqual(gameObject.GetComponent<Hare>().moveToNearestGrass(),new Vector3(13f, 7f, 22f));
    }
    [UnityTest]
    public IEnumerator HasFoundGrassTest()
    {
        gameObject.GetComponent<Hare>().grassPositionList.Add(new Vector3(13f, 7f, 22f));

        yield return null;
        Assert.IsTrue(gameObject.GetComponent<Hare>().hasFoundGrass());
    }
    [UnityTest]
    public IEnumerator EatGrassTests()
    {
       

        yield return null;
        Assert.IsTrue(gameObject.GetComponent<Hare>().eatGrass());
    }
    [UnityTest]
    public IEnumerator GetAnimalInfoTest()
    {

        yield return null;
        Assert.IsNotNull(gameObject.GetComponent<Hare>().getAnimalInfo());
    }
    [UnityTest]
    public IEnumerator SetRandomNameTest()
    {
        string[] namesMale = { "Bob", "Dave", "Ted", "Marvin", "Oscar", "Victor" };
        yield return null;
        Assert.AreEqual(namesMale,gameObject.GetComponent<Animal>().namesMale);
    }
    [UnityTest]
    public IEnumerator MutateTest()
    {
        yield return null;
        Assert.IsNotNull(gameObject.GetComponent<Animal>().mutate());
    }
    [UnityTest]
    public IEnumerator DrinkWater()
    {
        yield return null;
        Assert.IsTrue(gameObject.GetComponent<Animal>().drinkWater());
    }

}



    