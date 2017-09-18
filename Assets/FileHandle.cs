using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FileHandle : MonoBehaviour {

    public InputField tfFileContent;
    public InputField tfPassword;
    public Text txtMsg;
    public Text txtAddress;
    public Text txtToken;
    public Text txtEth;

    private string address;
    private string token;
    private string eth;
    private KuberaWallet kuberaWallet;

	// Use this for initialization
	void Start () {
        kuberaWallet = new KuberaWallet();
    }
	
	// Update is called once per frame
	void Update () {
        if (kuberaWallet.isGetEthSuccess())
        {
            eth = kuberaWallet.getEthVal() + "";
            txtEth.text = eth;
        }

        if (kuberaWallet.isGetTokenSuccess())
        {
            token = kuberaWallet.getTokenVal() + "";
            Debug.Log(token);
            txtToken.text = token;
        }
    }

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void importWallet()
    {
        
        clear();
        string fileContent = tfFileContent.text;
        string password = tfPassword.text;
        bool isSuccess = kuberaWallet.recoverWallet(password, fileContent);
        
        if(isSuccess)
        {
            address = kuberaWallet.walletAddress;
            txtAddress.text = address;
            StartCoroutine(kuberaWallet.getEth(address));
            StartCoroutine(kuberaWallet.getToken(address));
        }
        else
        {
            txtMsg.text = "Error";
        }
        
    }

    private void clear()
    {
        address = "";
        token = "";
        eth = "";
        txtMsg.text = "";
        txtAddress.text = "";
        txtEth.text = "";
        txtToken.text = "";
    }
}
