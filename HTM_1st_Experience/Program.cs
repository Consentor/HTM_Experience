using System.Windows.Forms;
using System;

namespace HTM_1st_Experience
{
    static partial class Program
    {
        public const int region_column_count = 60;
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

        // Функция определения номера элемента в массиве (если вышли за пределы,
        // то номер вычисляем как абсолютный остаток от деления номера на размер массива,
        // т. к. регион HTM у нас не отрезок, а кольцо)
        // На входе номер элемента и длина массива
        static int get_overflow_number(int number, int count)
        {
            return (Math.Abs(number%count));
        }
    }
}
