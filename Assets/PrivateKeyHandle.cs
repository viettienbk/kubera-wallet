using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Nethereum.Contracts;
using Nethereum.JsonRpc.UnityClient;
using System.Collections;
using Nethereum.RPC.Eth.DTOs;

public class PrivateKeyHandle : MonoBehaviour {

    public Text txtAddress;
    public Text txtBalance;
    public InputField tfPrivateKey;
    public Text txtETH;
    //private Wallet wallet;
    private string token;
    private string eth;
    string tokenAddress = "0xeeb01b6518461b597474db455f9aaa3aced9445b";
    string abi = @"[{""constant"":true,""inputs"":[],""name"":""name"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_spender"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""approve"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_from"",""type"":""address""},{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""decimals"",""outputs"":[{""name"":"""",""type"":""uint8""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[],""name"":""burn"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""standard"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""startTime"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""owner"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""symbol"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""game"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""},{""name"":"""",""type"":""address""}],""name"":""allowance"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""gameAddress"",""type"":""address""},{""name"":""value"",""type"":""uint256""}],""name"":""transferToGames"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""type"":""function""},{""inputs"":[],""payable"":false,""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""from"",""type"":""address""},{""indexed"":true,""name"":""to"",""type"":""address""},{""indexed"":false,""name"":""value"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""owner"",""type"":""address""},{""indexed"":true,""name"":""spender"",""type"":""address""},{""indexed"":false,""name"":""value"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""amount"",""type"":""uint256""}],""name"":""Burned"",""type"":""event""}]";

    private Function balanceOf;
    private EthCallUnityRequest tokenRequest;
    private Contract contract;
    private EthBlockNumberUnityRequest blockNumberRequest;
    private EthCallUnityRequest balanceRequest;

    // Use this for initialization
    void Start () {
        Debug.Log("Start");

        balanceRequest = new EthCallUnityRequest("http://118.69.187.7:8545");
        contract = new Contract(null, abi, tokenAddress);
        balanceOf = contract.GetFunction("balanceOf");

        StartCoroutine(abc());
    }

    private IEnumerator abc()
    {
        Debug.Log("start abc");
        yield return balanceRequest.SendRequest(balanceOf.CreateCallInput("0x0f816ba0d79ebb478fc86c1e0901460e5b43bfee"), BlockParameter.CreateLatest());
    }

    private IEnumerator transfer()
    {
        Debug.Log("start transfer");
        var transfer = getTransferToken();
        transfer.CreateTransactionInput("0x0f816ba0d79ebb478fc86c1e0901460e5b43bfee", 300000, 0, score, v, r, s);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("working");

        if (balanceRequest != null && balanceRequest.Result != null && balanceOf != null)
        {
            if (balanceRequest.Exception == null)
            {
                var token = balanceOf.DecodeSimpleTypeOutput<uint>(balanceRequest.Result);
                Debug.Log("Balance:" + token);
                txtBalance.text = token + "";
            }
        }
    }

    public void importPrivateKey()
    {
        string privateKey = tfPrivateKey.text;
    }

    public void backToHome()
    {
        SceneManager.LoadScene(0);
    }

    private Function getTransferToken()
    {
        return contract.GetFunction("transfer");
    }


    private void clear()
    {
        
    }
}
