using System.Drawing;
using System.Windows.Forms;

namespace Rowatch2.Embeding
{
    public class SkillTimer
    {
        public string Name { get; set; }
        public int Intervall { get; set; }
        public Keys Key { get; set; }
        public Color Color { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}