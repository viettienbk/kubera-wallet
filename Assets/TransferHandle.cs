using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Nethereum.Util;

public class TransferHandle : MonoBehaviour {

    public InputField tfTo;
    public InputField tfValue;
    public InputField tfPrivatekey;
    public Text txtTransactionId;

    private string txId;
    private KuberaWallet kuberaWallet;
    
    // Use this for initialization
	void Start ()
    {
        txId = "";
        kuberaWallet = new KuberaWallet();
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*
        if (txId == "" && kuberaWallet.isTransferTokenSuccess())
        {
            txId = kuberaWallet.getTransferTokenHash();
            txtTransactionId.text = txId;
        }
        */

        if (kuberaWallet.isGetTxCountSuccess())
        {
            Debug.Log(kuberaWallet.txCount.Result.Value);
            var encoded = kuberaWallet.getRawData(uint.Parse(tfValue.text), kuberaWallet.txCount.Result.Value);
            Debug.Log(encoded);
            txtTransactionId.text = encoded;
            kuberaWallet.txCount = null;
        }
	}

    public void transferToken()
    {
        string to = tfTo.text;
        string value = tfValue.text;
        string privateKey = tfPrivatekey.text;

        // kuberaWallet.importPrivateKey(privateKey);
        kuberaWallet.importPrivateKey("0d8777e4047bafd97be74425c6c73e5842d85c484bf91390cd72c3f7097cf0fa");

        uint amount = uint.Parse(value);

        Debug.Log(amount);

        // StartCoroutine(kuberaWallet.transferToken(to, amount));
        StartCoroutine(kuberaWallet.getTransactionCount());
    }

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }
}
