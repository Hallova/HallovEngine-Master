using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallovEngine.Render
{
    public abstract class GraphicsEngine  
    {
        public abstract bool IsRunning { get; }
        public abstract void ChangeTitle(string ty);


        public abstract uint Init();
        public abstract uint Update();
        public abstract void PreRender();
        public abstract uint Render();
        public abstract void Destroy();
     
        
    }
}
