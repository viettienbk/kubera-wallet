using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class TransferHandle : MonoBehaviour {

    public InputField tfTo;
    public InputField tfValue;
    public InputField tfPrivatekey;
    public Text txtTransactionId;

    private string txId;
    
    // Use this for initialization
	void Start () {
        //wallet = new Wallet();
        //txId = "";
    }
	
	// Update is called once per frame
	void Update () {
		//if(txId != "")
  //      {
  //          txtTransactionId.text = txId;
  //          txId = "";
  //      }
	}

    public void transferToken()
    {
        //string to = tfTo.text;
        //string value = tfValue.text;
        //string privateKey = tfPrivatekey.text;

        //Debug.Log(to);
        //Debug.Log(privateKey);

        //wallet.importPrivateKey(privateKey);

        //uint amount = uint.Parse(value);
        //Debug.Log(amount);

        //Debug.Log(wallet.walletAddress);
        //txId = wallet.transferToken(to, uint.Parse(value)).Result;
    }

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }
}
