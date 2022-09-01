using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using LastPass.Data;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace LastPass
{
    public class GoogleDrive
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Drive API .NET Quickstart";
        public List<Model> NewModel = new List<Model>();
        public DriveService GetService()
        {
            UserCredential credential;
            using (var stream =
                   new FileStream("Credentials/TokenCmd.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }

        public string CreateFile(string Name)
        {
            var service = GetService();
            var fileMetadata = new Google.Apis.Drive.v3.Data.File();
            fileMetadata.Name = Name;
            fileMetadata.MimeType = "application/json";
            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            return file.Id;
        }
        public string UploadFileToDrive()
        {
            string contentType = "application / json";
            DriveService service = GetService();
            var updatedFileMetadata = new Google.Apis.Drive.v3.Data.File();
            updatedFileMetadata.Name = "googledrive";

            FilesResource.UpdateMediaUpload updateRequest;
            string fileId = "1OqjBbMfVGT2dZ-c3QXmAJnlcSY3-Zp0u";
            using (var stream = new FileStream(@"C:\Users\Elias-Pc\Desktop\TRABAJO\LastPass4 (1)\LastPass4 (1)\LastPass\LastPass\Credentials\googledrive.json", FileMode.OpenOrCreate))
            {
                updateRequest = service.Files.Update(updatedFileMetadata, fileId, stream, contentType);
                updateRequest.Upload();
                var file = updateRequest.ResponseBody;
                return file.Id;
            };
        }
        public string ReadFile()
        {
            try
            {
                string fileId = "1OqjBbMfVGT2dZ-c3QXmAJnlcSY3-Zp0u";
                var service = GetService();
                var request = service.Files.Get(fileId);
                var stream = new MemoryStream();
                request.Download(stream);
                MemoryStream memstream = stream;
                string decoded = Encoding.UTF8.GetString(stream.ToArray());
                return decoded;
            }
            catch (Exception e)
            {
                if (e is AggregateException)
                {
                    return "Credential Not found";
                }
                else
                {
                    throw;
                }
            }
        }
        public void Serializar(List<Model> NewModel )
        {
            var jsonPatientList = JsonConvert.SerializeObject(NewModel);
            System.IO.File.WriteAllText(@"C:\Users\Elias-Pc\Desktop\TRABAJO\LastPass4 (1)\LastPass4 (1)\LastPass\LastPass\Credentials\googledrive.json", jsonPatientList);

        }
        




    }
}
