using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestao.Services
{
    public class DataService<T> where T : class
    {
        private string _filePath;

        public DataService(string fileName)
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", fileName); ;
        }
        public List<T> LoadData()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }

        public void SaveData(List<T> data)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
