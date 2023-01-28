namespace Lumia.Helpers
{
    public static class FileManager
    {
        public static string SaveImage(string root, string folder, IFormFile imageFile)
        {
            string fileName = imageFile.FileName;

            fileName = (fileName.Length > 64 ? fileName.Substring(fileName.Length - 64, 64) : fileName);

            fileName = Guid.NewGuid().ToString() + fileName;

            string savingRoot = Path.Combine(root, folder, fileName);

            using(FileStream fileStream = new FileStream(savingRoot, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            return fileName;
        }
    }
}
