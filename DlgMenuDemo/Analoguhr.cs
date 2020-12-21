using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace DlgMenuDemo
{
    class Analoguhr
    {
        public Zeiger sek;
        public Zeiger min;
        public Zeiger h;

        public Analoguhr(Ellipse elli, Canvas cnvs)
        {
            sek = new Zeiger(elli, 5);
            min = new Zeiger(elli, 10);
            h = new Zeiger(elli, 15);

            sek.Draw(cnvs);
            min.Draw(cnvs);
            h.Draw(cnvs);
        }

        public void UpdateUhr(int T_sec)
        {
            sek.angle = Math.PI * T_sec / 30;
            min.angle = Math.PI * T_sec / 1800;
            h.angle = Math.PI * T_sec / 21600;

            sek.UpdateZeiger();
            min.UpdateZeiger();
            h.UpdateZeiger();
        }
        public void resize(double sx, double sy, Ellipse elli)
        {
            sek.Resize(sx, sy, elli);
            min.Resize(sx, sy, elli);
            h.Resize(sx, sy, elli);
        }
    }
}
