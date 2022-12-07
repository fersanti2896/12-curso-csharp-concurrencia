using Concurrencia.Models;
using Concurrencia.Utilidades;
using System.Diagnostics;

namespace Concurrencia {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        HttpClient httpClient = new HttpClient();

        private async void button1_Click(object sender, EventArgs e) {
            pictureBox1.Visible = true;

            /* Durmiendo el proceso */
            //Thread.Sleep(3000); Sincrono

            /* var sw = new Stopwatch();
            sw.Start();

            var tareas = new List<Task>() {
                procesamientoLargoA(),
                procesamientoLargoB(),
                procesamientoLargoC()
            };

            await Task.WhenAll(tareas);

            sw.Stop();

            var duracion = $"El programa se ejecutó en { sw.ElapsedMilliseconds / 1000.0 } segundos.";
            Console.WriteLine($"{ duracion }"); */

            /* var directorioactual = AppDomain.CurrentDomain.BaseDirectory;
            var destinoSecuencial = Path.Combine(directorioactual, @"Imagenes/secuencial");
            var destinoParalelo = Path.Combine(directorioactual, @"Imagenes/paralelo");

            prepararEjecucion(destinoParalelo, destinoSecuencial);

            Console.WriteLine("Inicio");
            var imgs = obtenerImagenes();

            // Parte Secuencial
            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (var img in imgs) {
                await procesarImagen(destinoSecuencial, img);
            }

            Console.WriteLine($"Duración Secuencial: { sw.ElapsedMilliseconds / 1000.0 } segundos");

            sw.Reset();

            // Parte Paralelo
            sw.Start();

            var tareasEnumerable = imgs.Select(async img => {
                await procesarImagen(destinoParalelo, img);
            });

            await Task.WhenAll(tareasEnumerable);

            sw.Stop();

            Console.WriteLine($"Duración Paralelo: { sw.ElapsedMilliseconds / 1000.0 } segundos"); */

            // Parallel.For
            int conteoColumnas = 1080;
            int conteoFilas = 1000;
            int conteoColumnas2 = 750;

            double[,] m1 = Matriz.InicializarMatriz(conteoFilas, conteoColumnas);
            double[,] m2 = Matriz.InicializarMatriz(conteoColumnas, conteoColumnas2);
            double[,] resultado = new double[conteoFilas, conteoColumnas2];

            Console.WriteLine("Ejecutando versión secuencial");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            await Task.Run(() => {
                Matriz.MultiplicarMatricesSecuencial(m1, m2, resultado);
            });

            sw.Stop();
            Console.WriteLine("Duración en segundos de la versión secuencial: {0}", sw.ElapsedMilliseconds / 1000.0);

            resultado = new double[conteoFilas, conteoColumnas2];

            Console.WriteLine("Ejecutando la versión en paralelo");
            sw.Reset();
            sw.Start();

            await Task.Run(() => {
                Matriz.MultiplicarMatricesParalelo(m1, m2, resultado);
            });

            sw.Stop();
            Console.WriteLine("Duración en segundos de la versión en paralelo: {0}", sw.ElapsedMilliseconds / 1000.0);

            pictureBox1.Visible = false;
        }

        private async Task procesarImagen(string directorio, Imagen img) {
            var resp = await httpClient.GetAsync(img.URL);
            var contenido = await resp.Content.ReadAsByteArrayAsync();

            Bitmap bitmap;

            using (var ms = new MemoryStream(contenido)) { 
                bitmap = new Bitmap(ms);
            }

            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            var destino = Path.Combine(directorio, img.Nombre);

            bitmap.Save(destino);
        }

        private void prepararEjecucion(string destinoParalelo, string destinoSecuencial) {
            if (!Directory.Exists(destinoParalelo)) {
                Directory.CreateDirectory(destinoParalelo);
            } 
            
            if (!Directory.Exists(destinoSecuencial)) {
                Directory.CreateDirectory(destinoSecuencial);
            }

            borrarArchivos(destinoParalelo);
            borrarArchivos(destinoSecuencial);
        }

        private void borrarArchivos(string directorio) { 
            var archivos = Directory.EnumerateFiles(directorio);

            foreach (var archivo in archivos) {
                File.Delete(archivo);
            }
        }

        private List<Imagen> obtenerImagenes() {
            var imgs = new List<Imagen>();

            for (int i = 0; i < 7; i++) {
                imgs.Add(new Imagen() {
                    Nombre = $"Mexico { i }.png",
                    URL = "https://upload.wikimedia.org/wikipedia/commons/1/17/Flag_of_Mexico.png"
                });

                imgs.Add(new Imagen() {
                    Nombre = $"Inglaterra { i }.png",
                    URL = "https://upload.wikimedia.org/wikipedia/commons/c/c2/Flag_of_England.PNG"
                });
            }

            return imgs;
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