using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface IBooksManager
    {
        public void UploadBooksFromCSV(string path);
    }
}
