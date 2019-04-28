using System;
using System.Threading.Tasks;
using Firebase.Storage;
namespace botonera.Repository.FileDownload
{
    public class FirebaseStorageHelper
    {
        FirebaseStorage firebaseStorage;

        public static FirebaseStorageHelper SharedInstance = new FirebaseStorageHelper();

        private FirebaseStorageHelper()
        {
            firebaseStorage = new FirebaseStorage("raspberry-instants.appspot.com");
        }

        public async Task<string> GetFile(string fileName)
        {
            return await firebaseStorage
                .Child("Sounds")
                .Child(fileName)
                .GetDownloadUrlAsync();
        }
    }
}
