using Google.Apis.Drive.v3.Data;
using LastPass.Data;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Intrinsics.X86;

namespace LastPass.Pages
{
    public partial class Page_Password
    {
        GoogleDrive googleDrive = new GoogleDrive();
        Deserialize d = new Deserialize();
        List<Model> ListModel = new List<Model>();
        public Page_Password()
        {
             this.ListModel = d.GetDeserializeJson(googleDrive.ReadFile());
         
        }
    }
}
