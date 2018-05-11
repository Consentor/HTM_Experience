using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTM_1st_Experience
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    // Класс HTM региона
    public class HTM_Region
    {
        private const int region_column_count= 10;
        private const int column_neuron_count = 3;
        private const double synapse_activate_level = 0.8;

        // Класс колонки региона
        public class Column
        {
            public class Neuron
            {
                // Статусы: 0 - пассивен, 1 - предсказание, 2 - активен
                public int status;

                // Конструктор сразу делает новый нейрон пассивным
                public Neuron()
                {
                    status = 0;
                }
            }

            // Конструктор сразу заполняет колонку нейронами
            public Column()
            {
                Neuron[] neurons = new Neuron[column_neuron_count];
            }
        }

        void Init()
        {

        }
    }
}
