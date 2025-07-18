using Role_Based_Auth_App.Models;
using System.Text.Json;

namespace Role_Based_Auth_App.Helper
{
    public static class RequestStore
    {
        private static string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RequestData", "requests.json");

        public static List<RequestModel> GetRequests()
        {
            if (!File.Exists(_filePath)) return new List<RequestModel>();
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<RequestModel>>(json) ?? new List<RequestModel>();
        }



        public static List<RequestModel> GetRequestsByUser(string userName)
        {
            var allRequests = GetRequests();
            return allRequests.Where(r => r.EmployeeName == userName).ToList();
        }

        public static void SaveRequests(List<RequestModel> requests)
        {
            var json = JsonSerializer.Serialize(requests, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public static void AddRequest(RequestModel request)
        {
            var list = GetRequests();
            request.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
            list.Add(request);
            SaveRequests(list);
        }

        public static void UpdateStatus(int id, string newStatus)
        {
            var list = GetRequests();
            var req = list.FirstOrDefault(x => x.Id == id);
            if (req != null)
            {
                req.Status = newStatus;

                if (newStatus == "Approved")
                {
                    req.IsApproved = true;
                }
                else
                {
                    req.IsApproved = false;

                }
                SaveRequests(list);
            }
        }
    }

}
