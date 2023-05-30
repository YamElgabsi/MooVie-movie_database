using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MovieReviews.Models;
using System.Net.NetworkInformation;
using System.Text;
using static System.Net.WebRequestMethods;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Net.Http.Json;
using Microsoft.Win32;
using System.Windows.Media;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using System.Windows.Input;
using System.Windows.Shapes;

namespace MovieReviews.Models
{
    public sealed class UserService
    {
        private static readonly UserService instance = new UserService();
        private static readonly HttpClient httpClient;
        private static readonly string baseUrl = "https://localhost:7206";

        public bool IsApiConnected { get; set; }

        private CancellationTokenSource cancellationTokenSource;  // Used to stop the background task

        static UserService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private UserService()
        {
            IsApiConnected = false;
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => CheckApiConnectionAsync(cancellationTokenSource.Token)); // Start a separate task to check API connection
        }

        public static UserService Instance
        {
            get
            {
                return instance;
            }
        }

        public void StopCheckingApiConnection()
        {
            cancellationTokenSource.Cancel();  // Cancel the background task
        }

        public async Task<User> GetUserAsync(string username)
        {
            User user = null;
            if (!IsApiConnected)
            {
                // Handle API connection error
                return user;
            }

            HttpResponseMessage response = await httpClient.GetAsync(baseUrl + "/api/users/" + username);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(content);
            }

            return user;
        }

        public static bool IsValidEmail(string emailAddress)
        {
            // Regular expression pattern for email validation
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

            // Check if the email address matches the pattern
            return Regex.IsMatch(emailAddress, pattern);
        }
        public static string ImageToBase64(BitmapImage bitmapImage)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Create a BitmapEncoder and add the BitmapImage to it
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

                // Save the BitmapEncoder to the memory stream
                encoder.Save(memoryStream);

                // Convert the memory stream to a byte array
                byte[] imageBytes = memoryStream.ToArray();

                // Convert the byte array to a Base64-encoded string
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
        }

        public static BitmapImage Base64ToImage(string base64String)
        {
            // Convert the Base64-encoded string to a byte array
            byte[] imageBytes = Convert.FromBase64String(base64String);

            using (var memoryStream = new MemoryStream(imageBytes))
            {
                // Create a new BitmapImage and set its source to the memory stream
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public bool ShowErrorBox(string message, string title = "", string trueVal = "close", string falseVal = null)
        {
            ErrorDialogWindow errorWindow;
            // Create an instance of the custom dialog window
            if (falseVal == null)
            {
                errorWindow = new ErrorDialogWindow(message, title, trueVal);
            }
            else
            {
                errorWindow = new ErrorDialogWindow(message, title, trueVal, falseVal);
            }

            errorWindow.ShowDialog();

            // Get the user's input from the dialog window
            bool confirmChanges = errorWindow.ConfirmChanges;

            return confirmChanges;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            if (!IsApiConnected)
            {
                // Handle API connection error
                return false;
            }

            string jsonUser = JsonConvert.SerializeObject(user);
            HttpContent httpContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(baseUrl + "/api/users", httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(User user, string username)
        {
            if (!IsApiConnected)
            {
                // Handle API connection error
                return false;
            }

            string jsonUser = JsonConvert.SerializeObject(user);
            HttpContent httpContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(baseUrl + "/api/users/" + username, httpContent);

            return response.IsSuccessStatusCode;
        }



        private async Task CheckApiConnectionAsync(CancellationToken cancellationToken)
        {
            HttpClient httpClient = new HttpClient();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
                IsApiConnected = true;
                return;
            }
            catch (HttpRequestException)
            {
                // An exception occurred during the request, indicating the API is not available
                IsApiConnected = false;
                return;
            }
            finally
            {
                httpClient.Dispose(); // Dispose the HttpClient when you're done using it
            }


        }
        public async Task<List<Star>> GetStarsByNameStartingWithAsync(string str)
        {
            if (!string.IsNullOrEmpty(str)) str = $"/startswith/{str}";

            var response = await httpClient.GetAsync($"{baseUrl}/api/Stars{str}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var stars = JsonConvert.DeserializeObject<List<Star>>(content);

            return stars;
        }

        public async Task<List<User>> GetUsersByUsernameStartingWithAsync(string str)
        {
            
            if (!string.IsNullOrEmpty(str)) str = $"/startswith/{str}";

            var response = await httpClient.GetAsync($"{baseUrl}/api/Users{str}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(content);

            return users;
        }

        public async Task<List<Company>> GetCompaniesByNameStartingWithAsyn(string str)
        {

            if (!string.IsNullOrEmpty(str)) str = $"/startswith/{str}";

            var response = await httpClient.GetAsync($"{baseUrl}/api/Companies{str}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var companies = JsonConvert.DeserializeObject<List<Company>>(content);

            return companies;
        }

        public async Task<List<Movie>> GetMoviesByNameStartingWithAsyn(string str)
        {

            if (!string.IsNullOrEmpty(str)) str = $"/startswith/{str}";

            var response = await httpClient.GetAsync($"{baseUrl}/api/Movies{str}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<List<Movie>>(content);

            return movies;
        }

        public async Task<bool> DeleteUser(string username)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseUrl}/api/Users/{username}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> CreateStar(Star star)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/Stars", star);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> CreateActor(MovieStar movieStar)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/MovieStars", movieStar);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteStar(string star_id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseUrl}/api/Stars/{star_id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public static string GetImageFromUser() {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.EndInit();
                return ImageToBase64(bitmap);
            }
            return "";

        }

        public async Task<bool> DeleteCompany(string company_name)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseUrl}/api/Companies/{company_name}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }


        public async Task<bool> CreateCompany(Company company)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/Companies", company);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMovie(string movie_id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseUrl}/api/Movies/{movie_id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> CreateMovie(Movie movie)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/Movies", movie);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<Star> GetStarByIdAsync(string id)
        {
            var response = await httpClient.GetAsync($"{baseUrl}/api/stars/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var star = JsonConvert.DeserializeObject<Star>(content);

            return star;
        }

        public async Task<Company> GetCompanyAsync(string id)
        {
            var response = await httpClient.GetAsync($"{baseUrl}/api/Companies/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<Company>(content);

            return company;
        }

        public async Task<List<MovieStar>> GetMovieStarsByMovieId(string movieId)
        {
            List<MovieStar> stars = new List<MovieStar>();

            HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}/api/MovieStars/GetMovieStarsByMovieId/{movieId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                stars = JsonConvert.DeserializeObject<List<MovieStar>>(content);
            }
            else
            {
                Console.WriteLine($"Failed to retrieve stars. Status Code: {response.StatusCode}");
            }

            return stars;
        }

        public async Task<bool> DeleteMovieStarsByMovieID(string movie_id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseUrl}/api/MovieStars/DeleteByMovieId/{movie_id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMovieStarsByStarID(string star_id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"{baseUrl}/api/MovieStars/DeleteByStarId/{star_id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMovie(string movieId, Movie updatedMovie)
        {
            updatedMovie.Id = movieId;
            try
            {
                var json = JsonConvert.SerializeObject(updatedMovie);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{baseUrl}/api/Movies/{movieId}", content);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
        public async Task<bool> UpdateMoviesByDeletedDirector(string directorId)
        {
            try
            {
                var response = await httpClient.PutAsync($"{baseUrl}/api/Movies/UpdateMoviesByDeletedDirector/{directorId}", null);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateMoviesByDeletedCompany(string company)
        {
            try
            {
                var response = await httpClient.PutAsync($"{baseUrl}/api/Movies/UpdateMoviesByDeletedCompany/{company}", null);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateMoviesByCompany(string oldCompanyName, string newCompanyName)
        {
            try
            {
                var updateRequest = new CompanyUpdateRequest
                {
                    OldCompanyName = oldCompanyName,
                    NewCompanyName = newCompanyName
                };

                var content = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(updateRequest),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await httpClient.PutAsync($"{baseUrl}/api/Movies/UpdateMoviesByCompany", content);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

    
    }



}