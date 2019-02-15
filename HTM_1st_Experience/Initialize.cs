using System;
using System.Linq;

namespace HTM_1st_Experience
{
    static partial class Program
    {
        // Создание входных данных
        static int[] CreateRandomInput()
        {
            // Количество битов во входном массиве
            int n = 30;
            // Количество единиц во входном массиве
            int ones_count = 0;
            // Текст для вывода на форму
            text = "";
            // Временно создаем рандомный массив из n битов
            text += "Входной массив битов:" + Environment.NewLine;
            int[] input_bits = new int[n];
            Random rand = new Random((int)(DateTime.Now.Ticks));
            for (int i = 0; i < n; i++)
            {
                System.Threading.Thread.Sleep(2);
                input_bits[i] = rand.Next(0, 2);
                text += input_bits[i];
                if (i < n-1 && (i + 1) % 10 != 0) text += "   ";
                else text += Environment.NewLine;
                if (input_bits[i] == 1) ones_count++;
            }
            text += "Всего единиц в массиве: " + ones_count + Environment.NewLine + Environment.NewLine;

            // На случай, если входные биты будут приходить потоком, определяем их количество
            input_bits_count = input_bits.Count();

            return input_bits;
        }

        // Инициализация региона
        static void Init(Region region)
        {
            // Создание потенциальных синапсов для колонок
            // Внимание!!! Число synapses_per_side - это количество подключаемых синапсами битов с каждой стороны от direct_bit
            // таким образом, всего потенциальных синапсов у колонки (synapses_per_side * 2 + 1)
            // Спорно! Попробуем округлять synapses_per_side после деления вверх до ближайшего целого
            synapses_per_side = (int)Math.Ceiling((double)input_bits_count / region_column_count);

            for (int j = 0; j < region_column_count; j++)
            {
                // Создаем синапсы в количестве (synapses_per_side * 2 + 1) для каждой колонки (перманентность определяется рандомно)
                region.columns[j].synapses = new Synapse[synapses_per_side * 2 + 1];
                for (int i = 0; i < (synapses_per_side * 2 + 1); i++)
                    region.columns[j].synapses[i] = new Synapse();
                // Находим прямой бит входных данных для колонки (для соединения с синапсом с наибольшей перманентностью)
                // Здесь я делю текущий номер колонки на количество колонок и умножаю на количество входных битов
                // по аналогии с определением процентов (количество битов здесь для нас заменяет 100%)
                // Преобразование в double нужно для возможности дробного деления. Результат конвертируем обратно в int
                int direct_bit = (int)((double)j / region_column_count * input_bits_count);
                // Сразу устанавливаем сильнейший синапс для прямого бита
                region.columns[j].synapses[region.columns[j].get_best_synapse_number(synapses_per_side * 2 + 1)].bit_number = direct_bit;
                // Теперь закрепляем сильнейшие синапсы слева и справа от direct_bit
                for (int i = 1; i <= synapses_per_side; i++)
                {
                    // Закрепляем синапс справа от direct_bit
                    int target_bit = direct_bit + i;
                    if (target_bit >= input_bits_count) target_bit -= input_bits_count;
                    // На вход get_best_synapse приходится передвать общее число синапсов (в будущем хорошо бы все это в БД хранить,
                    // было бы проще получать лучший синапс простым селектом)
                    region.columns[j].synapses[region.columns[j].get_best_synapse_number(synapses_per_side * 2 + 1)].bit_number = target_bit;

                    // Закрепляем синапс слева от direct_bit
                    target_bit = direct_bit - i;
                    if (target_bit < 0) target_bit += input_bits_count;
                    region.columns[j].synapses[region.columns[j].get_best_synapse_number(synapses_per_side * 2 + 1)].bit_number = target_bit;
                }
            }
        }
    }
}