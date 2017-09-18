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
        if (txId == "" && kuberaWallet.isTransferTokenSuccess())
        {
            txId = kuberaWallet.getTransferTokenHash();
            txtTransactionId.text = txId;
        }
	}

    public void transferToken()
    {
        string to = tfTo.text;
        string value = tfValue.text;
        string privateKey = tfPrivatekey.text;

        kuberaWallet.importPrivateKey(privateKey);

        uint amount = uint.Parse(value);

        Debug.Log(amount);

        StartCoroutine(kuberaWallet.transferToken(to, amount));
    }

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }
}
