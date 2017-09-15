using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using KuberaWalletLib;
using Nethereum.Util;
using Org.BouncyCastle.Math;

public class PrivateKeyHandle : MonoBehaviour {

    public Text txtAddress;
    public Text txtBalance;
    public InputField tfPrivateKey;
    public Text txtETH;
    private Wallet wallet;
    private string token;
    private string eth;
    
    // Use this for initialization
	void Start () {
        wallet = new Wallet();
        clear();
    }
	
	// Update is called once per frame
	void Update () {
		if(token != "")
        {
            txtBalance.text = token;
            token = "";
        }
        if(eth != "")
        {
            txtETH.text = eth;
            eth = "";
        }
	}

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void importPrivateKey()
    {
        clear();

        bool s = wallet.importPrivateKey(tfPrivateKey.text);

        if (s)
        {
            //  wallet address
            txtAddress.text = wallet.walletAddress;

            // token
            token = wallet.getTokenBalance(wallet.walletAddress).Result;

            // ETH
            eth = wallet.getBalance().Result;
        }
        else
        {
            txtAddress.text = "Invalid private key";
        }
    }

    private void clear()
    {
        token = "";
        eth = "";
        txtAddress.text = "";
        txtBalance.text = "";
        txtETH.text = "";
        if(wallet != null)
        {
            wallet.clearWallet();
        }
    }
}
