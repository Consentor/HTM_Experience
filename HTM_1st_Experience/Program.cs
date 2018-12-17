using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTM_1st_Experience
{
    static partial class Program
    {
        public const int region_column_count = 20;
        public const int column_neuron_count = 3;
        public const double synapse_activate_level = 0.8;
        public static int input_bits_count;
        public static int synapses_per_side;
        public static string text;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            HTM_Form form = new HTM_Form();
            Region region = new Region();

            // Вызов процедуры создания входных данных
            int[] input_bits = CreateRandomInput();

            // Инициализация региона
            Init(region);

            // Пространственный группировщик
            int[] active_columns = Space_Compactor(input_bits, region);

            // Вывод информации о регионе
            RegionOutput(region, active_columns);
            
            Application.Run(form);
            Application.Exit();
        }

        // Функция проверки выхода за пределы массива и выдачи номера в кольце
        static int get_overflow_number(int number, int count)
        {
            if (number < count && number >= 0)
                return number;
            else
            {
                if(number < 0) number += count;
                else number -= count;
            }
            return get_overflow_number(number, count);
        }
    }
}
