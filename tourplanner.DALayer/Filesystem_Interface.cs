using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace tourplanner.DALayer
{
    public interface Filesystem_Interface
    {
        string OpenFileDialog(string defaultPath);
    }
}
