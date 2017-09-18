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
    // private Wallet wallet;
    private string address;
    private string token;
    private string eth;
    private bool err;

	// Use this for initialization
	void Start () {
       //  wallet = new Wallet();
        clear();
    }
	
	// Update is called once per frame
	void Update () {
        /*
		if(token != "")
        {
            txtToken.text = token;
            token = "";
        }

        if (eth != "")
        {
            txtEth.text = eth;
            eth = "";
        }

        if (err)
        {
            txtMsg.text = "Invalide wallet";
        }
        */
	}

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void importWallet()
    {
        /*
        clear();
        string fileContent = tfFileContent.text;
        string password = tfPassword.text;
        wallet.recoverWallet(password, fileContent);
        address = wallet.walletAddress;
        txtAddress.text = address;

        if(address != "")
        {
            token = wallet.getTokenBalance(address).Result;
            eth = wallet.getBalance().Result;
        }
        else
        {
            err = true;
        }
        */
    }

    private void clear()
    {
        //address = "";
        //token = "";
        //eth = "";
        //err = false;
        //txtMsg.text = "";
        //txtAddress.text = "";
        //txtEth.text = "";
        //txtToken.text = "";
        //if(wallet != null)
        //{
        //    wallet.clearWallet();
        //}
    }
}
