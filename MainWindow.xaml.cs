using RestSharp;
using System;
using System.Windows;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace GokOne
{
    public class utxoResult
    {
        public string txid { get; set; }
        public int vout { get; set; }
        public string address { get; set; }
        public string label { get; set; }
        public string scriptPubKey { get; set; }
        public double amount { get; set; }
        public int confirmations { get; set; }
        public int ancestorcount { get; set; }
        public int ancestorsize { get; set; }
        public int ancestorfees { get; set; }
        public bool spendable { get; set; }
        public bool solvable { get; set; }
        public string desc { get; set; }
        public bool safe { get; set; }
    }

    public class Params
    {
        public RestClient? client { get; set; }
        public RestRequest? request { get; set; }
    }

    public class Addresses
    {
        public string? address { get; set; }
    }

    public class Transaction
    {
        public string? hex { get; set; }
    }

    public class mainResponse
    {
        public string result { get; set; }
        public object error { get; set; }
        public string id { get; set; }
    }

    public class utxoResponse
    {
        public List<utxoResult> result { get; set; }
        public object error { get; set; }
        public string id { get; set; }
    }

    public class multiResponse
    {
        public string? address { get; set; }
        public string? redeemScript { get; set; }
        public string? descriptor { get; set; }
    }

    public class multiResult
    {
        public multiResponse? result { get; set; }
        public object? error { get; set; }
        public string? id { get; set; }
    }

    public partial class MainWindow : Window
    {
        private static Params restParams = new Params();
        private static Addresses btc_addresses = new Addresses();
        private static mainResponse mainResponse = new mainResponse();
        private static utxoResponse utxoResponse = new utxoResponse();
        private static Transaction Transaction = new Transaction();
        private static multiResponse multiResponse = new multiResponse();
        private static multiResult multiResult = new multiResult();

        public MainWindow()
        {
            InitializeComponent();
            offerlist.Document.Blocks.Clear();

            GetAddress();

            SaveAddress();

            ListAddresses();
        }

        public static Params ConnectNode()
        {
            var restParams = new Params();

            try
            {
                restParams.client = new RestClient("http://localhost:18332/wallet/");
                restParams.client.Timeout = -1;
                restParams.request = new RestRequest(Method.POST);
                restParams.request.AddHeader("Authorization", "Basic NTI3Mjc4NDdlYTA1ODJlMjc0ZTQ2ZjM2N2Y3NjI4NWQyYzg4ODIxNmIxOjYxYzIyZDg4YzA5ZGU4NGQwMzlmNDhhMzkzMTFhZWJkM2YwNGIzMTk0ZA==");
                restParams.request.AddHeader("Content-Type", "application/json");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return restParams;
        }

        public static string GetAddress()
        {
            var restParams = ConnectNode();

            const string? body = @"{""jsonrpc"": ""1.0"", ""id"": ""rpctest"", ""method"": ""getnewaddress"", ""params"": [""test"",""bech32""]}";
            restParams.request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = restParams.client.Execute(restParams.request);

            mainResponse newaddress = JsonSerializer.Deserialize<mainResponse>(response.Content);

            btc_addresses.address = newaddress.result;

            return btc_addresses.address;
        }

        public static async void SaveAddress()
        {
            string address = btc_addresses.address;

            string json_path = @"path\address_test.json";

            if (!File.Exists(json_path))
            {
                await using (StreamWriter writer = File.CreateText(json_path))
                {
                    writer.WriteLine(address);
                }
            }
            else
            {
                await using (StreamWriter writer = File.AppendText(json_path))
                {
                    writer.WriteLine(address);
                }
            }
        }

        public static List<utxoResult> GetOffers()
        {
            var restParams = ConnectNode();

            const string? body = @"{""jsonrpc"": ""1.0"", ""id"": ""curltest"", ""method"": ""listunspent"", ""params"": [0, 9999999, [""tb1qph0amwzslvzv3udk8x5ywc8x8ksvrzt6ug52u0""] , true]}";
            restParams.request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = restParams.client.Execute(restParams.request);

            utxoResponse offers = JsonSerializer.Deserialize<utxoResponse>(response.Content);

            utxoResponse.result = offers.result;

            return utxoResponse.result;
        }

        public static string CreateOffer()
        {
            var restParams = ConnectNode();

            const string? body = @"{""jsonrpc"": ""1.0"",""id"": ""reqId1"",""method"": ""createrawtransaction"",""params"": [[{""txid"": ""122831a5cc3d3875cadd89e2a2690c2e5bc9e703d177385e1a3318a44675b6d6"",""vout"":1}],{""mmZFY7b7NArkrnri43RwxL58m6TZUqty4t"": ""0.09999""},0]}";
            restParams.request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = restParams.client.Execute(restParams.request);

            mainResponse newoffer = JsonSerializer.Deserialize<mainResponse>(response.Content);

            Transaction.hex = newoffer.result;

            return Transaction.hex;
        }

        private void getoffers_button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder OfferString = new StringBuilder();
            List<utxoResult> offer_list = GetOffers();

            foreach (var item in offer_list)
            {
                OfferString.Append(item.txid);
                OfferString.AppendLine();
            }

            offerlist.AppendText(OfferString.ToString());
        }

        public static string AcceptOffer()
        {
            var restParams = ConnectNode();

            const string? body = @"{""jsonrpc"": ""1.0"", ""id"": ""curltest"", ""method"": ""createmultisig"", ""params"": [2, [""03217c1f9fe975bbc9f2fdfdd6e4247179f188dbb8d53861eef3782b1649cd4a39"",""021424787d68460022599d058866251192ea2abe7778781946687e6d0076511822""]]}";
            restParams.request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = restParams.client.Execute(restParams.request);

            multiResult multisig = JsonSerializer.Deserialize<multiResult>(response.Content);

            btc_addresses.address = multisig.result.address;

            return btc_addresses.address;
        }

        private void button_CreateOffer_Click(object sender, RoutedEventArgs e)
        {
            CreateOffer subWindow = new();
            subWindow.Show();
        }

        private void ListAddresses()
        {
            try
            {
                string json_path = @"path\address_test.json";
                string line = string.Empty;

                using (var sr = new StreamReader(json_path))
                {
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        address_list.Items.Add(line);
                        line = sr.ReadLine();
                        address_list.SelectedIndex = address_list.Items.Count - 1;
                        address_list.ScrollIntoView(address_list.SelectedItem);
                    }
                    sr.Close();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private void button_acceptoffer_Click(object sender, RoutedEventArgs e)
        {
            string address = AcceptOffer();

            MessageBox.Show(address);
        }
    }
}
