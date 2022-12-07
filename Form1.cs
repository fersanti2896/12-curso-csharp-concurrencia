namespace Concurrencia {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e) {
            pictureBox1.Visible = true;
           
            /* Durmiendo el proceso */
            //Thread.Sleep(3000); Sincrono
            await Task.Delay(3000);
           
            MessageBox.Show("Has ejecutado la acción");
            pictureBox1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e) {

        }
    }
}