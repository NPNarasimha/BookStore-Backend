using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoyLayer.Interfaces
{
    public interface IBooksRepo
    {
        public void UploadBooksFromCSV(string path);
    }
}
