using System.Diagnostics;

namespace Concurrencia {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e) {
            pictureBox1.Visible = true;

            /* Durmiendo el proceso */
            //Thread.Sleep(3000); Sincrono

            var sw = new Stopwatch();
            sw.Start();

            var tareas = new List<Task>() {
                procesamientoLargoA(),
                procesamientoLargoB(),
                procesamientoLargoC()
            };

            await Task.WhenAll(tareas);

            sw.Stop();

            var duracion = $"El programa se ejecutó en { sw.ElapsedMilliseconds / 1000.0 } segundos.";
            Console.WriteLine($"{ duracion }");
            pictureBox1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private async Task procesamientoLargoA() {
            await Task.Delay(1000);
            Console.WriteLine($"Procesamiento de A.");
        }

        private async Task procesamientoLargoB() {
            await Task.Delay(1000);
            Console.WriteLine($"Procesamiento de B.");
        }

        private async Task procesamientoLargoC() {
            await Task.Delay(1000);
            Console.WriteLine($"Procesamiento de C.");
        }
    }
}