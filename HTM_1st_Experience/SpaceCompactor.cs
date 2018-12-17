using System;
using System.Linq;

namespace HTM_1st_Experience
{
    static partial class Program
    {
        // Пространственный группировщик
        static int[] Space_Compactor(int[] input_bits, Region region)
        {
            // Считаем минимальное значение перекрытия
            int minOverlap = input_bits_count / region_column_count;

            // Список номеров колонок, которые будут активированы входными данными
            int[] active_columns = new int[0];

            // ВНИМАНИЕ!!! Здесь выходит непонятно! По тексту нужно сначала умножать на boost, а потом уже сравнивать с minOverlap.
            // А по примеру кода все делается наоборот. Надо посмотреть, что лучше на практике.
            // !!!
            int j;
            for (j = 0; j < region_column_count; j++)
            {
                for (int i = 0; i < region.columns[j].synapses.Count(); i++)
                    // Если синапс подключен, то прибавляем значение его бита к значению перекрытия колонки
                    if (region.columns[j].synapses[i].permanence >= synapse_activate_level)
                        region.columns[j].overlap += input_bits[region.columns[j].synapses[i].bit_number];
                // Умножаем значение перекрытия на фактор агрессивности
                region.columns[j].overlap *= region.columns[j].boost;
                // Если перекрытие колонки меньше минимального, то сбрасываем в 0
                if (region.columns[j].overlap < minOverlap)
                    region.columns[j].overlap = 0;
            }

            // Сколько соседей по бокам от колонок будем учитывать при подавлении
            int neigbours_per_side = region_column_count / 10;
            // Сколько соседей могут быть активными рядом с текущей колонкой
            int active_neigbours_allowed = neigbours_per_side / 5;
            for (j = 0; j < region_column_count; j++)
            {
                // Считаем соседей сильнее текущей колонки
                int powerful_neigbours = 0;
                // Если перекрытие колонки больше 0 и активных соседей разрешено 0, значит текущая колонка остается активной
                if (region.columns[j].overlap > 0 && active_neigbours_allowed == 0)
                {
                    // Расширяем массив активных колонок и записываем номер текущей
                    Array.Resize(ref active_columns, active_columns.Count() + 1);
                    active_columns[active_columns.Count() - 1] = j;
                }
                // Если перекрытие колонки больше 0, но активные соседи разрешены
                else if (region.columns[j].overlap > 0)
                {
                    for (int i = 1; i <= neigbours_per_side; i++)
                    {
                        // Находим номер соседа справа
                        int target_neigbour = j + i;
                        // Если номер за пределами массива, корректируем
                        target_neigbour = get_overflow_number(target_neigbour, region_column_count);
                        // Если перекрытие соседа сильнее, учитываем его в переменной powerful_neigbours
                        if (region.columns[target_neigbour].overlap > region.columns[j].overlap)
                            powerful_neigbours++;

                        // Находим номер соседа справа
                        target_neigbour = j - i;
                        // Если номер за пределами массива, корректируем
                        target_neigbour = get_overflow_number(target_neigbour, region_column_count);
                        // Если перекрытие соседа сильнее, учитываем его в переменной powerful_neigbours
                        if (region.columns[target_neigbour].overlap > region.columns[j].overlap)
                            powerful_neigbours++;
                    }
                    // Если количество сильных соседей в пределах допустимого, то
                    if (powerful_neigbours <= active_neigbours_allowed)
                    {
                        // Расширяем массив активных колонок и записываем номер текущей
                        Array.Resize(ref active_columns, active_columns.Count() + 1);
                        active_columns[active_columns.Count() - 1] = j;
                    }
                }
            }

            return active_columns;
        }
    }
}
