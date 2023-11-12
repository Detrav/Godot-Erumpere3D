using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erumpere3D.Scripts
{
    public interface IPauseProcessor
    {
        void ShowPause();
        void HidePause();
        void QuitRequest();
    }
}
