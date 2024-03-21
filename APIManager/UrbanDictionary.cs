using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CSharpToolKit.APIManager
{
    /// <summary>
    /// Provides methods to interact with the Urban Dictionary API. Requiring a RapidAPI Key.
    /// </summary>
    public class UrbanDictionary : IDisposable
    {
        private string Key { get; set; }
        private string Param { get; set; }

        private readonly HttpClient Client;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrbanDictionary"/> class with the specified API key.
        /// </summary>
        /// <param name="APIkey">The RapidAPI key for accessing the Urban Dictionary API.</param>
        public UrbanDictionary(string APIkey)
        {
            Key = APIkey;
            Client = new HttpClient();
        }

        /// <summary>
        /// Represents the different types of entries to retrieve from Urban Dictionary.
        /// </summary>
        public readonly struct Id
        {
            /// <summary>
            /// Retrieves the definition of a word.
            /// </summary>
            public static readonly Id Definition = new Id("definition");

            /// <summary>
            /// Retrieves an example of usage for a word.
            /// </summary>
            public static readonly Id Example = new Id("example");

            /// <summary>
            /// Retrieves the word itself.
            /// </summary>
            public static readonly Id Word = new Id("word");

            /// <summary>
            /// Gets the value associated with the entry type.
            /// </summary>
            public string Value { get; }

            private Id(string value)
            {
                Value = value;
            }
        }

        /// <summary>
        /// Looks up the specified word in the Urban Dictionary API.
        /// </summary>
        /// <param name="word">The word to look up.</param>
        /// <param name="id">The type of entry to retrieve (default is definition).</param>
        /// <param name="method">The method to use for the lookup (0 for define, 1 for random, 2 for words of the day).</param>
        /// <returns>A list of entries matching the specified criteria.</returns>
        public async Task<IList<string>> LookUp(string word, Id? id = null, int method = 0)
        {
            IList<string> result;

            switch (method)
            {
                case 0:
                default:
                    Param = $"define?term={word}";
                    result = await Request(id ?? Id.Definition);
                    break;
                case 1:
                    Param = "random";
                    result = await Request(id ?? Id.Definition);
                    break;
                case 2:
                    Param = "words_of_the_day";
                    result = await Request(id ?? Id.Definition);
                    break;
            }

            return result;
        }

        private async Task<IList<string>> Request(Id id)
        {
            IList<string> args = new List<string>();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://urban-dictionary7.p.rapidapi.com/v0/{Param}"),
                Headers =
                {
                    { "X-RapidAPI-Key", Key },
                    { "X-RapidAPI-Host", "urban-dictionary7.p.rapidapi.com" },
                },
            };

            using (var response = await Client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(body);
                var keys = jsonObject.Properties().Select(p => p.Name);

                foreach (var key in keys)
                {
                    foreach (var value in jsonObject[key]?.ToArray())
                    {
                        string item = value[$"{id.Value}"].ToString();
                        args.Add(item);
                    }
                }
            }

            return args;
        }

        /// <summary>
        /// Disposes the resources used by the UrbanDictionary class.
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
