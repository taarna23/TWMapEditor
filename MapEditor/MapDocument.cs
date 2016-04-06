using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapEditor
{
    public class MapParseException : Exception
    {
        public MapParseException(string message) : base(message)
        {
        }
    }

    public class MapDocument
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private D2MFile _d2m = new D2MFile();
        public MapEditor.D2MFile D2M
        {
            get { return _d2m; }
        }

        public bool Open(string fileLoc)
        {
            string filename = Path.GetFileNameWithoutExtension(fileLoc);
            string directory = Path.GetDirectoryName(fileLoc);
            if (D2M.ReadFromFile(directory + "\\" + filename + ".zip"))
            {
                return true;
            }
            throw (new MapParseException("Error while parsing d2m."));
        }

      
    }
}
