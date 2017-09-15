using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using KuberaWalletLib;

public class TransferETHHandle : MonoBehaviour {

    public Text txtMsg;
    public InputField tfTo;
    public InputField tfValue;
    public InputField tfPrivatekey;
    private Wallet wallet;
    private string msg;

	// Use this for initialization
	void Start () {
        wallet = new Wallet();
        msg = "";
	}
	
	// Update is called once per frame
	void Update () {
		if(msg != "")
        {
            txtMsg.text = msg;
            Debug.Log(msg);
            msg = "";
        }
	}

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void transferETH()
    {
        wallet.clearWallet();
        string to = tfTo.text;
        float value = float.Parse(tfValue.text);
        string privateKey = tfPrivatekey.text;
        wallet.importPrivateKey(privateKey);
        if(wallet.walletAddress != "")
        {
            msg = wallet.transferETH(to, value).Result;
        }
    }
}
