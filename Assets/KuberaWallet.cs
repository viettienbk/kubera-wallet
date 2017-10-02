using System.Collections;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.KeyStore;
using Nethereum.Hex.HexConvertors.Extensions;
using System;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using UnityEngine;
using Nethereum.Web3;
using System.Threading.Tasks;

public class KuberaWallet {

    private string tokenAddress;
    private string abi;
    private Function balanceOf;
    private EthCallUnityRequest tokenRequest;
    private Contract contract;
    private EthGetBalanceUnityRequest ethRequest;
    private string url = "http://118.69.187.7:8545";
    private string privateKey;
	public string walletAddress;
    public string serverAddress;

    public KuberaWallet()
    {
        privateKey = "";
        walletAddress = "";
        tokenAddress = "0xb80db791a23b114d93ef0c6d2c20214235da9cb0";
        serverAddress = "0xdb526bedb534cca762abf049d56c8a103d8dfa95";
        abi = @"[{""constant"":true,""inputs"":[],""name"":""name"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_spender"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""approve"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_from"",""type"":""address""},{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""decimals"",""outputs"":[{""name"":"""",""type"":""uint8""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[],""name"":""burn"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""standard"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""startTime"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""owner"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""symbol"",""outputs"":[{""name"":"""",""type"":""string""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_to"",""type"":""address""},{""name"":""_value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""name"":""success"",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[],""name"":""game"",""outputs"":[{""name"":"""",""type"":""address""}],""payable"":false,""type"":""function""},{""constant"":true,""inputs"":[{""name"":"""",""type"":""address""},{""name"":"""",""type"":""address""}],""name"":""allowance"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""gameAddress"",""type"":""address""},{""name"":""value"",""type"":""uint256""}],""name"":""transferToGames"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""type"":""function""},{""inputs"":[],""payable"":false,""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""from"",""type"":""address""},{""indexed"":true,""name"":""to"",""type"":""address""},{""indexed"":false,""name"":""value"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""owner"",""type"":""address""},{""indexed"":true,""name"":""spender"",""type"":""address""},{""indexed"":false,""name"":""value"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""amount"",""type"":""uint256""}],""name"":""Burned"",""type"":""event""}]";
        contract = new Contract(null, abi, tokenAddress);
    }

    public IEnumerator getToken(string _address)
    {
        tokenRequest = new EthCallUnityRequest(url);
        balanceOf = contract.GetFunction("balanceOf");
        yield return tokenRequest.SendRequest(balanceOf.CreateCallInput(_address), BlockParameter.CreateLatest());
    }

    public bool isGetTokenSuccess()
    {
        return tokenRequest != null && tokenRequest.Result != null && balanceOf != null;
    }

    public IEnumerator getEth(string _address)
    {
        ethRequest = new EthGetBalanceUnityRequest(url);
        yield return ethRequest.SendRequest(_address, BlockParameter.CreateLatest());
    }

    public bool isGetEthSuccess()
    {
        return ethRequest != null && ethRequest.Result != null;
    }

    public bool importPrivateKey(string key)
    {
        try
        {
            var ethKey = new EthECKey(key);
            privateKey = key;
            walletAddress = ethKey.GetPublicAddress();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool recoverWallet(string password, string json)
    {
        var service = new KeyStoreService();
        try
        {
            var keyObject = service.DecryptKeyStoreFromJson(password, json);
            walletAddress = "0x" + service.GetAddressFromKeyStore(json);
            privateKey = keyObject.ToHex();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public void clearWallet()
    {
        privateKey = "";
        walletAddress = "";
    }

	public uint getTokenVal()
    {
		return balanceOf.DecodeSimpleTypeOutput<uint>(tokenRequest.Result);
	}

    public BigInteger getEthVal()
    {
        return ethRequest.Result.Value;
    }

    private TransactionSignedUnityRequest transactionEth;
    private TransactionSignedUnityRequest transactionToken;

    public IEnumerator transferEth(string _to, BigInteger _value)
    {
        transactionEth = null;
        var transactionInput = new TransactionInput(){
            From = walletAddress,
            To = _to,
            Value = new HexBigInteger(_value)
        };
        
        transactionEth = new TransactionSignedUnityRequest(url, privateKey, walletAddress);
        yield return transactionEth.SignAndSendTransaction(transactionInput);
    }

    public IEnumerator transferToken(string _to, uint _value)
    {
        var transfer = contract.GetFunction("transfer");

        var gas = new HexBigInteger("0x493E0".HexToBigInteger(false));
        var value = new HexBigInteger("0x0".HexToBigInteger(false));

        var transactionInput = transfer.CreateTransactionInput(walletAddress, gas, value, new System.Object[] { _to, _value });

        transactionToken = new TransactionSignedUnityRequest(url, privateKey, walletAddress);

        Debug.Log(transactionToken);
        yield return transactionToken.SignAndSendTransaction(transactionInput);
    }

    public bool isTransferETHSuccess()
    {
        return transactionEth != null && transactionEth.Result != null;
    }

    public string getTransferEthHash()
    {
        return transactionEth.Result;
    }

    public bool isTransferTokenSuccess()
    {
        return transactionToken != null && transactionToken.Result != null;
    }

    public string getTransferTokenHash()
    {
        return transactionToken.Result;
    }

    public EthGetTransactionCountUnityRequest txCount;

    public IEnumerator getTransactionCount()
    {
        txCount = new EthGetTransactionCountUnityRequest(url);
        yield return txCount.SendRequest(walletAddress, BlockParameter.CreateLatest());
    }

    public string getRawData(uint amount, BigInteger txCount)
    {
        var transfer = contract.GetFunction("transfer");
        var data = transfer.GetData(serverAddress, amount);
        var gasPrice = BigInteger.Parse("60000000000");
        var gasLimit = BigInteger.Parse("200000");

        return Web3.OfflineTransactionSigner.SignTransaction(privateKey, tokenAddress, 0, txCount, gasPrice, gasLimit, data);
    }

    public bool isGetTxCountSuccess()
    {
        return txCount != null && txCount.Result != null;
    }
}
