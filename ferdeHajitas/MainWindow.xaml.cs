using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ferdeHajitas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static double m=-1, v0=-1, alfa=1, cd=-1, g = 0;
        Canvas canvas;
        bool loaded = false;

        public MainWindow()
        {
            InitializeComponent();
            playButton.IsEnabled = false;
        }

        private void velocityTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            double temp;
            if (velocityTB.Text != String.Empty && double.TryParse(velocityTB.Text, out temp) && temp > 0)
            {
                v0 = double.Parse(velocityTB.Text);
            }
            else if (velocityTB.Text != String.Empty)
            {
                MessageBox.Show("Hibás adatbevitel!");
            }
            checkIfNoEmpty();
        }

        private void bTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            double temp;
            if (bTB.Text != String.Empty && double.TryParse(bTB.Text, out temp) && temp >= 0)
            {
                cd = double.Parse(bTB.Text);
            }
            else if (bTB.Text != String.Empty)
            {
                MessageBox.Show("Hibás adatbevitel!");
            }
            checkIfNoEmpty();
        }

        private void defaultValue_Click(object sender, RoutedEventArgs e)
        {
            m = -1;
            v0 = -1;
            cd = -1;
            bTB.Text = String.Empty;
            massTB.Text = String.Empty;
            velocityTB.Text = String.Empty;
            checkIfNoEmpty();
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if(v0 < 2.5)
            {
                canvas.Unit =150;
            }
            else if(v0<10)
            {
                canvas.Unit = 80;
            }
            else if(v0<40)
            {
                canvas.Unit = 5;
            }
            else
            {
                canvas.Unit = 1;
            }
            
            canvas.Clear(true, false);
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            x.Add(0);
            y.Add(0);
            double vx = v0 * Math.Cos(alfa / 180 * Math.PI);
            double vy = v0 * Math.Sin(alfa / 180 * Math.PI);
            double t = 0;
            double drag = cd * v0 * v0;
            double ax = -(drag * Math.Cos(alfa / 180 * Math.PI)) / m;
            double ay = -g-(drag * Math.Sin(alfa / 180 * Math.PI) / m);
            double step = .05; // Ez a pontosság és ez van legjobban hatással a teljesítményre. Minél kisebb annál pontosabb.
            int counter = 0;
            while(y[counter]>=0)
            {
                t += step;

                vx += step * ax;
                vy += step * ay;

                x.Add(x[counter]+step * vx);
                y.Add(y[counter]+step * vy);

                canvas.DrawLine(x[counter], y[counter], x[counter+1], y[counter+1]);

                double velocity = Math.Sqrt(vx * vx + vy * vy);
                drag = cd * velocity * velocity;
                ax = -(drag * Math.Cos(alfa / 180 * Math.PI)) / m;
                ay = -g - (drag * Math.Sin(alfa / 180 * Math.PI) / m);

                counter++;
            }
            tav.Content = x[counter];
            hajT.Content = t;
            hajMax.Content = y.Max(x => x);
        }

        private void gTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            double temp;
            if (gTB.Text != String.Empty && double.TryParse(gTB.Text, out temp) && temp >= 0)
            {
                g = double.Parse(gTB.Text);
            }
            else if (gTB.Text != String.Empty)
            {
                MessageBox.Show("Hibás adatbevitel!");
            }
            checkIfNoEmpty();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            canvas = new Canvas(canvasControl);
            loaded = true;
        }

        private void SliderGetValue(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(loaded)
            {
                alfa = alfaSlider.Value;
                fok.Content = alfa.ToString();
            }
        }

        private void checkIfNoEmpty()
        {
            if (m > 0 && v0 > 0 && cd >= 0 && g > 0)
            {
                playButton.IsEnabled = true;
            }
            else
            {
                playButton.IsEnabled = false;
            }
        }

        private void massTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            double temp;
            if (massTB.Text != String.Empty && double.TryParse(massTB.Text, out temp) && temp > 0)
            {
                m = double.Parse(massTB.Text);
            }
            else if (massTB.Text != String.Empty)
            {
                MessageBox.Show("Hibás adatbevitel!");
            }
            checkIfNoEmpty();
        }
    }
}
