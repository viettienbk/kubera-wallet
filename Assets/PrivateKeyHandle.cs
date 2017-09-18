using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrivateKeyHandle : MonoBehaviour {

    public Text txtAddress;
    public Text txtBalance;
    public InputField tfPrivateKey;
    public Text txtETH;
    private string token;
    private string eth;
    private KuberaWallet kuberaWallet;


    // Use this for initialization
    void Start () {
        kuberaWallet = new KuberaWallet();
    }
	
	// Update is called once per frame
	void Update () {
		if (kuberaWallet.isGetEthSuccess()) {
            eth = kuberaWallet.getEthVal() + "";
            txtETH.text = eth;
		}

		if (kuberaWallet.isGetTokenSuccess()) {
            token = kuberaWallet.getTokenVal() + "";
            Debug.Log(token);
            txtBalance.text = token;
        }
    }

    public void importPrivateKey()
    {
        clean();

        kuberaWallet.clearWallet();
        string privateKey = tfPrivateKey.text;

		if(privateKey.Trim() != "")
		{
			bool isSuceess = kuberaWallet.importPrivateKey(privateKey);
			var address = kuberaWallet.walletAddress;
			txtAddress.text = address;

			if (isSuceess) {
                StartCoroutine(kuberaWallet.getEth(address));
                StartCoroutine(kuberaWallet.getToken(address));
			} else 
			{
				txtAddress.text = "Invalid private key";
			}
		}
    }

    private void clean()
    {
        kuberaWallet.clearWallet();
        txtAddress.text = "";
        txtBalance.text = "";
        txtETH.text = "";
    }

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }
}
