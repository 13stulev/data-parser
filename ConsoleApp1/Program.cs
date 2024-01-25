using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using Org;

class Program
{

    static async Task Main()
    {
        int n = 15;
        Task[] tasks = new Task[n];
        IWriteImplementation writeImplementation = new Org.TextWriter();
        for (int i = 1; i <= n; i++)
        {
            tasks[i] = GetRecords(i, n, writeImplementation);
        }
        
        await Task.WhenAll(tasks);
        
        writeImplementation.uniteFiles(n);
        
        //writer.Close();
        //StreamReader reader = new StreamReader("total.txt");
        //StreamWriter writer = new StreamWriter("newTotal.txt", false);
        //writer.WriteLine("id]org_fullname]org_shortName]ogrn]inn]kpp]grbs_code]chief]phone]url]region]okato]oktmo]ogrn_date]okveds]");
        //reader.ReadLine();
        //while (!reader.EndOfStream)
        //{
        //    string json = reader.ReadLine();
        //    string[] values = json.Split("]");
        //    values[values.Length - 2] = values[values.Length - 2].Substring(0, values[values.Length - 2].Length <= 4000 ? values[values.Length - 2].Length : 4000);
        //    json = "";
        //    foreach(string item in values)
        //    {
        //        json += $"{item}]";
        //    }
        //    writer.WriteLine(json);
        //}
        //reader.Close();
        //writer.Close();
        Console.WriteLine("Завершено");
    }
    static async Task GetRecords(int threadNum, int totalThreads, IWriteImplementation impl)
    {

        // определяем данные запроса
        var client = new HttpClient();
        Console.WriteLine($"Работа потока {threadNum} Началась");
        Organisation organisation = new Organisation();
        StreamWriter writer = new StreamWriter($"text{threadNum}.txt", false);
        int i = threadNum;

        try
        {
            do
            {
                string url = "https://bus.gov.ru/public-rest/api/7710568760-Generalinformation?size=400&page=" + i;
                Uri uri = new Uri(url);
                HttpRequestMessage request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.Headers.Add("Cookie", "5a492b5f63bbf580c9ad30fe5fe6a9e8=4da39b0e54884f7e0ba5c88ff8726902; session-cookie=17aa6e77f5e34b810b1d890a18991a249cf116a3600d8d097d50dafb416f296ba9679fbd245d72ec9913d2f00e34a6e8");
                request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.1.2222.33 Safari/537.36");
                request.RequestUri = uri;
                var response = await client.SendAsync(request);
                Console.WriteLine($"{threadNum} {response.StatusCode}");
                if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                {
                    int k = 400 * (i - 1) + 1;
                    Console.WriteLine($"Чтение страницы {i}");
                    string res = await response.Content.ReadAsStringAsync();
                    organisation = JsonSerializer.Deserialize<Organisation>(res);
                    impl.write(writer, organisation, k);
                    i += totalThreads;
                }
                else { continue; }
            } while (organisation.pagedContent.content.Count > 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            writer.Close();
            Console.WriteLine($"Работа потока {threadNum} завершена");
        }

    }
}