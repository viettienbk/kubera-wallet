using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeHandle : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveToPrivateKey()
    {
        SceneManager.LoadScene(2);
    }

    public void moveToFile()
    {
        SceneManager.LoadScene(1);
    }

    public void moveToTransferToken()
    {
        SceneManager.LoadScene(3);
    }

    public void moveToTransferETH()
    {
        SceneManager.LoadScene(4);
    }

}
