using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestChat
{
    public partial class chatbox : Component
    {
        public chatbox()
        {
            InitializeComponent();
        }

        public chatbox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
