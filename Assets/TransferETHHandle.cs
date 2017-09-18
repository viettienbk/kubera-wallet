using Nethereum.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransferETHHandle : MonoBehaviour {

    public Text txtMsg;
    public InputField tfTo;
    public InputField tfValue;
    public InputField tfPrivatekey;
    private string msg;
    private KuberaWallet kuberaWallet;
    private string txId;

	// Use this for initialization
	void Start ()
    {
        txId = "";
        kuberaWallet = new KuberaWallet();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (txId == "" && kuberaWallet.isTransferETHSuccess())
        {
            txId = kuberaWallet.getTransferEthHash();
            txtMsg.text = "TransactionId:" + txId;
        }
	}

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void transferETH()
    {
        Debug.Log("transfer");
        string to = tfTo.text;
        string value = tfValue.text;
        string privateKey = tfPrivatekey.text;

        kuberaWallet.importPrivateKey(privateKey);

        float amount = float.Parse(value);
        var ether = UnitConversion.Convert.ToWei(amount, UnitConversion.EthUnit.Ether);

        Debug.Log(ether);

        StartCoroutine(kuberaWallet.transferEth(to, ether));
    }
}
