using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackToMap : MonoBehaviour
{
    public string SceneName;
    public void Scene()
    {
        SceneManager.LoadScene(SceneName);
    }
}