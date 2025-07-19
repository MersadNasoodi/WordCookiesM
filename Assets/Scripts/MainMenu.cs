using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StratLevel1()
    {
        SceneManager.LoadScene(1);
    }
    
    
    public void StratLevel2()
    {
        SceneManager.LoadScene(2);
    }
}
