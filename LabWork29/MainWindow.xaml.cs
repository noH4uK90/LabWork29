using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using LabWork28.Models;

namespace LabWork29
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:5279/api/")
        };
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var newProduct = new Product
            {
                Name = "iPhone 14 Pro Max",
                Price = 100000
            };

            var changedProduct = new Product
            {
                Id = 1,
                Name = "Macbook Pro 14 M1 Pro",
                Price = 100000
            };

            var product = await GetProductAsync(2);
            var message = await DeleteProductAsync(4);
            var location = await AddProductAsync(newProduct);
            var putMessage = await PutProductAsync(changedProduct);
            MessageBox.Show(putMessage);
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"Product/{id}");
        }

        public async Task<string> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Product/{id}");
            return response.StatusCode == HttpStatusCode.OK ? $"Продукт с Id {id} был удален" : "При удалении произошла ошибка";
        }

        public async Task<string> AddProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("Product", product);
            return "Продукт был успешно добавлен";
        }

        public async Task<string> PutProductAsync(Product product)
        {
            await _httpClient.PutAsJsonAsync("Product", product);
            return "Продукт был успешно изменен";
        }
    }
}