using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTM_1st_Experience
{
    // Класс - регион HTM
    public class Region
    {
        // У региона есть колонки - массив элементов класса Column
        public Column[] columns = new Column[Program.region_column_count];

        // Конструктор
        public Region()
        {
            // При создании региона, заполняем его колонками
            for (int j = 0; j < Program.region_column_count; j++)
            {
                // Каждая колонка - новый экземпляр класса Column
                columns[j] = new Column();
            }
        }
    }

    // Класс - колонка нейронов
    public class Column
    {
        // Свойства колонки:
        public int overlap;                  // значение "перекрытия" соседних колонок на основе текущего входа
        public int connected_synapses;       // подключенные синапсы
        public int boost;                    // фактор агрессивности (для усиления малоиспользуемых колонок)

        // У колонки есть определенное количество нейронов и неопределенное количество синапсов
        public Neuron[] neurons = new Neuron[Program.column_neuron_count];
        public Synapse[] synapses;

        // Конструктор колонки
        public Column()
        {
            overlap = 0;
            connected_synapses = 0;
            boost = 1;

            for (int i = 0; i < Program.column_neuron_count; i++)
            {
                neurons[i] = new Neuron();
            }
        }

        // Функция, возвращающая номер синапса с наибольшей перманентностью из тех, которые еще не связаны с входными битами
        public int get_best_synapse_number(int synapses_count)
        {
            // Устанавливаем номер синапса в -1 на случай, если все синапсы уже соединены (чтобы было что вернуть)
            int best_synapse_number = -1;
            for (int i = 0; i < synapses_count; i++)
            {
                // Если синапс существует и еще не закреплен за битом
                if (synapses[i] != null && synapses[i].bit_number == -1)
                    // И если лучший синапс еще на задан или перманентность претендента больше перманентности текущего лучшего синапса
                    if (best_synapse_number == -1 || synapses[best_synapse_number].permanence < synapses[i].permanence)
                        // То синапс-претендент становится лучшим
                        best_synapse_number = i;
            }
            return best_synapse_number;
        }
    }

    // Класс - нейрон
    public class Neuron
    {
        // Статусы: 0 - пассивен, 1 - предсказание, 2 - активен
        public int status;

        Random rand = new Random((int)(DateTime.Now.Ticks));

        // Конструктор сразу делает новый нейрон пассивным или активным
        public Neuron()
        {
            System.Threading.Thread.Sleep(1);
            status = rand.Next(0, 3);
        }
    }

    // Класс - синапс
    public class Synapse
    {
        // Перманентность синапса определяет, подключен он или нет (нужно не забыть про геометрический центр)
        public double permanence;
        // Номер бита в массиве входных данных, с которым связан синапс
        public int bit_number = -1;

        Random rand = new Random((int)(DateTime.Now.Ticks));

        // Конструктор сразу задает синапсу уровень перманентности в пределах 5% от synapse_activate_level
        public Synapse()
        {
            System.Threading.Thread.Sleep(1);
            permanence = Math.Round(Program.synapse_activate_level * 0.95 + rand.NextDouble() / 10, 2);
        }
    }
}
