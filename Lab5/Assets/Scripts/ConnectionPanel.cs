using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


// This class is used to set up the connections between client and server.
// For the most part, it should be left as-is for the purposes of lab 4.
public class ConnectionPanel : MonoBehaviour
{

    //UI References
    public InputField ServerIPField;
    public InputField UsernameField;
    public Dropdown ServerIPDropdown;
    public Text PortWarning;
    
    // Convenience property for the NetworkManager Singleton.
    // This object uses the singleton pattern to make sure that only one exists in the scene.
    // It also makes itself available via a static reference.
    NetworkManager netMan { get { return NetworkManager.singleton; } } 

    void Start()
    {
        // This is how you get your local IPs (we use many in case that someone has multiple network cards)
        var host = Dns.GetHostEntry(Dns.GetHostName());
        var list = host.AddressList
            .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
            .Select(ip => new Dropdown.OptionData(ip.ToString()))
            .ToList();
        
        // add a header for the dropdown and send the list of local ips to the ui.
        list.Insert(0, new Dropdown.OptionData("Your Local IPs"));
        ServerIPDropdown.options = list;

        // We use the last info that the user entered for their convenience.
        ServerIPField.text = PlayerPrefs.GetString("LAST_IP_USED", "localhost");
        UsernameField.text = PlayerPrefs.GetString("PLAYER_USERNAME", GetRandomName());
        
        // Just update the warning for external IPs
        PortWarning.text = "Requires port forwarding ("+netMan.networkPort+")";
    }

    //When the user selects an option in the localIP dropdown (Other than the header), we populate the IP field.
    public void PopulateLocalIP(int option)
    {
        if (option == 0) return;
        ServerIPField.text = ServerIPDropdown.options[option].text;
    }


    //Populate the the IP field with an external Ip.
    public void PopulateExternalIP()
    {
        // This requires a web request, so we show a msg for responsiveness.
        ServerIPField.text = "Checking....";
        ServerIPDropdown.value = 0;
        StartCoroutine(CheckIP());
    }

    public void StartHost()
    {
        //Save settings for next time. We also save the username so that it can be used between scenes.
        PlayerPrefs.SetString("LAST_IP_USED", ServerIPField.text);
        PlayerPrefs.SetString("PLAYER_USERNAME", UsernameField.text);
        netMan.networkAddress = ServerIPField.text;
        netMan.StartHost();
    }

    public void StartClient()
    {
        PlayerPrefs.SetString("LAST_IP_USED", ServerIPField.text);
        PlayerPrefs.SetString("PLAYER_USERNAME", UsernameField.text);
        netMan.networkAddress = ServerIPField.text;
        netMan.StartClient();
    }
    
    // How to get an external IP:
    IEnumerator CheckIP()
    {
        // Other Services: 
        // http://icanhazip.com
        // http://bot.whatismyipaddress.com
        // http://ipinfo.io/ip
        // http://checkip.dyndns.org

        var extIPRequest = new WWW("http://icanhazip.com");
        if (extIPRequest == null)
        {
            ServerIPField.text = "Error";
            yield break;
        }
        yield return extIPRequest;
        if (extIPRequest.error != null) ServerIPField.text = extIPRequest.error;
        else ServerIPField.text = extIPRequest.text;
    }

    // Just some random names.
    public string GetRandomName()
    {
        var names = new[] { "Tim", "Kosta", "Eric", "Isaac", "Alex", "Dave", "Joshua", "Marcus", "Sophie", "Denise", "Imogen", "Zack", "Dante" };
        var r = UnityEngine.Random.Range(0, names.Length);
        return names[r];
    }
}
