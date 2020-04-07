using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public enum Scene
    {
        TestLevel,
        MainMenu,
        Level_1,
        Level_2
    };

    public void SwitchPanel(GameObject to)
    {
        to.SetActive(true);
        gameObject.SetActive(false);
    }

    public void LoadScene(string id)
    {
        SceneManager.LoadScene(id);
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void ExitGame()
    {
        Debug.Break();
    }

}
