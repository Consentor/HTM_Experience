using System;
using System.Linq;

namespace HTM_1st_Experience
{
    static partial class Program
    {
        // Вывод информации о регионе
        static void RegionOutput(Region region, int[] active_columns)
        {
            /*
            // Вывод списка синапсов
            text += "Вывод перманентности синапсов по колонкам:" + Environment.NewLine;
            for (int j = 0; j < region_column_count; j++)
            {
                text += j + " колонка" + ":" + Environment.NewLine;
                for (int i = 0; i < synapses_per_side * 2 + 1; i++)
                {
                    text += "   " + i + " синапс соединен с битом " + region.columns[j].synapses[i].bit_number +
                            " с перманентностью " + region.columns[j].synapses[i].permanence + Environment.NewLine;
                }
            }
            text += Environment.NewLine;*/

            // Вывод соединений, превысивших synapse_activate_level
            text += "Вывод активных соединений по битам:" + Environment.NewLine;
            for (int i = 0; i < input_bits_count; i++)
            {
                text += i + " бит соединен с синапсами: ";
                for (int j = 0; j < region_column_count; j++)
                    for (int k = 0; k < synapses_per_side * 2 + 1; k++)
                        if (region.columns[j].synapses[k].bit_number == i && region.columns[j].synapses[k].permanence >= synapse_activate_level)
                            text += j + "." + k + "; ";
                text += Environment.NewLine;
            }
            text += Environment.NewLine;

            // Вывод региона на форму
            text = text + region_column_count + " колонок по " + column_neuron_count + " нейрона:" + Environment.NewLine;
            for (int i = 0; i < column_neuron_count; i++)
            {
                for (int j = 0; j < region_column_count; j++)
                {
                    text += region.columns[j].neurons[i].status.ToString();
                    // Если символ не последний, добавляем пробел
                    if (j < region_column_count - 1)
                        text += "   ";
                }
                // Если строка не последняя, делаем перенос
                if (i < column_neuron_count - 1)
                    text += Environment.NewLine;
            }
            text += Environment.NewLine + Environment.NewLine;

            // Вывод перекрытия колонок
            text = text + "Значения перекрытия колонок:" + Environment.NewLine;
            for (int j = 0; j < region_column_count; j++)
                text += region.columns[j].overlap + " ";
            text += Environment.NewLine + Environment.NewLine;

            // Вывод количества и списка активных колонок
            text = text + active_columns.Count() + " активных колонок:" + Environment.NewLine;
            for (int j = 0; j < active_columns.Count(); j++)
                text += active_columns[j] + " ";
            text += Environment.NewLine;

            // Выводим строку с результатами в текстбокс
            HTM_Form.output.Text = text;
        }
    }
}