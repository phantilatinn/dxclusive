using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace dxclusive
{
    public class FirestoreService
    {
        private FirestoreDb db;

        public FirestoreService()
        {
            this.SetupFireStore();
        }

        private async Task SetupFireStore()
        {
            if (db == null)
            {
                var stream = await FileSystem.OpenAppPackageFileAsync("dxclusive-7459c-firebase-adminsdk-36sk6-0ad789642b.json");
                var reader = new StreamReader(stream);
                var contents = reader.ReadToEnd();
                db = new FirestoreDbBuilder
                {
                    ProjectId = "dxclusive-7459c",
                    JsonCredentials = contents
                }.Build();
            }
        }

        // Fetch a list of terms
        public async Task<List<string>> GetTermListAsync()
        {
            var termList = new List<string>();
            var snapshot = await db.Collection("terms").GetSnapshotAsync();

            foreach (var document in snapshot.Documents)
            {
                if (document.Exists && document.TryGetValue("name", out string termName))
                {
                    termList.Add(termName);
                }
            }

            return termList;
        }

        // Fetch class data based on a selected term
        public async Task<List<string>> GetClassDataAsync(string term)
        {
            var classList = new List<string>();

            try
            {
                // Reference Firestore collection for classes
                var snapshot = await db.Collection("classes")
                                       .WhereEqualTo("term", term)
                                       .GetSnapshotAsync();

                foreach (var document in snapshot.Documents)
                {
                    if (document.Exists && document.TryGetValue("name", out string className))
                    {
                        classList.Add(className);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching class data: {ex.Message}");
            }

            return classList;
        }
    }
}
